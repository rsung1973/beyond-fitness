using System;
using System.IO;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Net;
using System.Xml;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Pkcs;
using System.Collections.Generic;

using Utility;
using CommonLib.Properties;


namespace CommonLib.Security.UseCrypto
{
	/// <summary>
	/// PKCS7 ���K�n�y�z�C
	/// </summary>
	public class CryptoBase
	{

        internal static uint MY_ENCODING_TYPE = Win32.Win32.PKCS_7_ASN_ENCODING | Win32.Win32.X509_ASN_ENCODING;
        internal static X509Certificate IssuerCert;

		protected X509Certificate2 _cert ;
        protected dsPKCS7 _ds;
        protected dsPKCS7.pkcs7EnvelopRow _log;

        public Func<X509Certificate2, bool> CheckCertificateTrusted
        {
            get;
            set;
        }

        protected CryptoBase()
		{
			//
			// TODO: �b���[�J�غc�禡���{���X
			//
            try
            {
                if (!String.IsNullOrEmpty(Settings.Default.PKCS7LogType))
                {
                    Type type = Type.GetType(Settings.Default.PKCS7LogType);
                    if (type.IsSubclassOf(typeof(dsPKCS7)))
                    {
                        _ds = (dsPKCS7)type.Assembly.CreateInstance(type.FullName);
                    }
                }
            }
            finally
            {
                if (_ds == null)
                {
                    _ds = new dsPKCS7();
                }
                _log = _ds.pkcs7Envelop.Newpkcs7EnvelopRow();
                _ds.pkcs7Envelop.Addpkcs7EnvelopRow(_log);
            }

		}

        public DateTime FileTimeToDateTime(Win32.FILETIME ft)
		{
			return DateTime.FromFileTime(((long)ft.dwHighDateTime<<32) + (long)ft.dwLowDateTime);
		}

        protected bool isCertTrusted(X509Certificate2 certificate)
        {
            if (CheckCertificateTrusted != null)
            {
                return CheckCertificateTrusted(certificate);
            }

            bool bResult = false;

            if (null != IssuerCert)
            {
                bResult = IssuerCert.Subject.Equals(certificate.Issuer);
            }

            if (bResult)
                return true;

            #region �qCertificate Store���o�H��������

            X509Store store = new X509Store(StoreName.CertificateAuthority, StoreLocation.LocalMachine);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadOnly);

            foreach(X509Certificate2 cert in  store.Certificates)
            {
                if (cert.Subject.Equals(certificate.Issuer))
                {
                    bResult = true;
                    IssuerCert = cert;
                    break;
                }
            }

            store.Close();

            #endregion

            return bResult;
        }




		protected bool isCertTrusted(IntPtr pCert)
		{
			bool bResult = false;

			if(null!=IssuerCert)
			{
                bResult = IssuerCert.Subject.Equals(_cert.Issuer);
			}

			if(bResult)
				return true;

			#region �qCertificate Store���o�H��������

            IntPtr hCertStore = Win32.Win32.CertOpenSystemStore(IntPtr.Zero, "CA");
			if(IntPtr.Zero == hCertStore)
			{
                _log.Message = Win32.Win32.ShowWin32Error(Marshal.GetLastWin32Error());
				return false;
			}

			IntPtr issuer = IntPtr.Zero;
			uint dwCheck;

            while ((issuer = Win32.Win32.CertEnumCertificatesInStore(hCertStore, issuer)) != IntPtr.Zero)
			{
                dwCheck = Win32.Win32.CERT_STORE_SIGNATURE_FLAG;
                bResult = Win32.Win32.CertVerifySubjectCertificateContext(pCert, issuer, ref dwCheck) && (dwCheck == 0);
				if(bResult)
				{
                    Win32.Win32.CERT_CONTEXT certCtx = (Win32.Win32.CERT_CONTEXT)Marshal.PtrToStructure(issuer, typeof(Win32.Win32.CERT_CONTEXT));
					byte[] certBytes = new byte[certCtx.cbCertEncoded];
					Marshal.Copy(certCtx.pbCertEncoded,certBytes,0,certBytes.Length);

					IssuerCert = new X509Certificate(certBytes);
					//���U�o��y�k���T,���O���B����o�˥�,�ݭncert context�ƻs�X��
//					IssuerCert = new X509Certificate(issuer);

                    Win32.Win32.CertFreeCertificateContext(issuer);
					break;
				}
			}

            Win32.Win32.CertCloseStore(hCertStore, Win32.Win32.CERT_CLOSE_STORE_FORCE_FLAG);

			#endregion

			return bResult;
		}

		public dsPKCS7.pkcs7EnvelopRow CA_Log
		{
			get
			{
				return _log;
			}
		}

        protected void beforeVerify(string dataToSign, string dataSignature, X509Certificate2 cert2)
        {
            _log.DataToSign = dataToSign;
            _log.DataSignature = dataSignature;
            _log.ActionTime = DateTime.Now;
            _log.Issuer = cert2.Issuer;
            _log.NotAfter = cert2.NotAfter.ToString();
            _log.NotBefore = cert2.NotBefore.ToString();
            _log.Subject = cert2.Subject;
            _log.UniqueID = cert2.SerialNumber;
        }

        protected void beforeVerify(XmlDocument xmlSignature, X509Certificate2 cert2)
        {
            _ds.XmlSignature = xmlSignature;
            _log.ActionTime = DateTime.Now;
            _log.Issuer = cert2.Issuer;
            _log.NotAfter = cert2.NotAfter.ToString();
            _log.NotBefore = cert2.NotBefore.ToString();
            _log.Subject = cert2.Subject;
            _log.UniqueID = cert2.SerialNumber;
        }


        protected virtual bool verifySignerCertificate(X509Certificate2 cert2)
        {
            bool result = false;

            #region �ˬd���ҬO�_�Q�H��

            if (isCertTrusted(cert2))
            {
                #region �ˬd���ҬO�_�w�M�P
                X509CrlUtility crlUtil = new X509CrlUtility(cert2);

                if (crlUtil.IsNotRevoked())
                {
                    #region �ˬd���ҬO�_�L��
                    if (cert2.NotAfter >= DateTime.Now)
                    {
                        result = true;
                    }
                    else
                    {
                        _log.Message = "���Ҥw�L��!";
                    }

                    #endregion

                }
                else
                {
                    _log.Message = crlUtil.ErrorMessage;
                }

                #endregion

            }
            else
            {
                _log.Message = "���ҬO�ѥ��Q�H�����o�ҳ��ҵo�X!";
            }


            #endregion
            return result;
        }


        public X509Certificate2 SignerCertificate
        {
            get
            {
                return _cert;
            }
        }
	}
}
