using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net;
using System.Threading;


namespace Utility
{
    /// <summary>
    /// Summary description for ValueValidity.
    /// </summary>
    public static class ValueValidity
    {

        private static string[] strArrayNum = { "�s", "��", "�L", "��", "�v", "��", "��", "�m", "��", "�h" };
        private static string[] strArrayUnit1 = { "", "�B", "��", "�a" };
        private static string[] strArrayUnit2 = { "����", "�U", "��", "��" };
        public static char[] __CHINESE_NUM_CHAR = { '�s', '��', '�L', '��', '�v', '��', '��', '�m', '��', '�h' };


        public static char[] HELFWIDTH_CODE = {
												  '1','2','3','4','5','6','7','8','9','0','-','=','q','w','e','r','t','y','u','i','o','p','[',']',
												  '\\','a','s','d','f','g','h','j','k','l',';','\'','z','x','c','v','b','n','m',',','.','/','!','@',
												  '#','$','%','^','&','*','(',')','_','+','Q','W','E','R','T','Y','U','I','O','P','{','}','|','A',
												  'S','D','F','G','H','J','K','L',':','"','Z','X','C','V','B','N','M','<','>','?','~',' '
											  };
        public static char[] FULLWIDTH_CODE = {
												  '��','��','��','��','��','��','��','��','��','��','��','��','��','�@','��','��','��','�B','��','��',
												  '��','��','�e','�f','�@','��','��','��','��','��','��','��','��','��','�F','��','�C','�A','��','��',
												  '��','��','��','�A','�D','��','�I','�I','��','�C','�H','�s','��','��','�]','�^','��','��','��','��',
												  '��','��','��','��','��','��','��','��','�a','�b','�U','��','��','��','��','��','��','��','��','��',
												  '�G','��','��','��','��','��','��','��','��','��','��','�H','��','�@'
											  };
        public static char[] DISCERNIBLE_CODE = {'2', '3', '4', '5', '6', '8', 
                                    '9', 'A', 'B', 'C', 'D', 'E', 
                                    'F', 'G', 'H', 'J', 'K', 'L', 
                                    'M', 'N', 'P', 'R', 'S', 'T', 
                                    'W', 'X', 'Y'};
        public static char[] NUMERIC_STRING = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };


        public static bool IsSignificantString(object obj)
        {
            if (obj != null && (obj is string))
            {
                return ((string)obj).Length > 0;
            }
            return false;
        }

        public static void CheckAndCreatePath(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }


        public static void AppendEMailAddr(IList list, object emailAddr)
        {
            if (IsSignificantString(emailAddr))
            {
                string[] email = emailAddr.ToString().Split(',', ';', ' ', '\r', '\n');
                foreach (string s in email)
                {
                    list.Add(s);
                }
            }
        }

        public static string SaveUploadFile(System.Web.HttpPostedFile file, string storePath, string ext)
        {
            if (file != null)
            {
                string fileName = System.Guid.NewGuid().ToString() + ext;
                file.SaveAs(storePath + "\\" + fileName);
                return fileName;
            }
            return null;
        }

        public static void ResetHtmlInputText(ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is HtmlInputText)
                {
                    ((HtmlInputText)control).Value = "";
                }
            }
        }

        public static string GetDateStylePath(this string prefix)
        {
            return GetDateStylePath(prefix, DateTime.Now);
        }

        public static string GetDateStylePath(this string prefix, DateTime date)
        {
            string path = Path.Combine(prefix,String.Format("{0:yyyy}\\{0:MM}\\{0:dd}", date));

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }


        #region ConvertChineseDate

        /// <summary>
        /// �N����ন�������
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        /// 
        public static string ConvertChineseDate(object dateTime)
        {
            if (dateTime != null && dateTime is DateTime)
            {
                DateTime dt = (DateTime)dateTime;
                return String.Format("{0}.{1:00}.{2:00}", dt.Year - 1911, dt.Month, dt.Day);
            }

            return null;

        }


        public static string ConvertChineseDate(object dateTime, string strAsNull)
        {
            if (dateTime != null && dateTime is DateTime)
            {
                DateTime dt = (DateTime)dateTime;
                return String.Format("{0}.{1:00}.{2:00}", dt.Year - 1911, dt.Month, dt.Day);
            }

            return strAsNull;

        }


        #endregion

        #region ConvertChineseDateString
        public static string ConvertChineseDateString(object dateTime)
        {
            if (dateTime != null && dateTime is DateTime)
            {

                DateTime dt = (DateTime)dateTime;
                return String.Format("{0}�~{1}��{2}��", dt.Year - 1911, dt.Month, dt.Day);
            }
            return null;
        }

        #endregion


        public static string ConvertChineseDateTimeString(object dateTime)
        {
            if (dateTime != null && dateTime is DateTime)
            {

                DateTime dt = (DateTime)dateTime;
                return String.Format("{0}�~{1}��{2}��{3}��{4}��{5}��",
                    dt.Year - 1911, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            }
            return null;
        }

        #region MoneyShow

        public static string MoneyShow(object numValue)
        {
            string strMoney = Convert.ToInt64(numValue).ToString();
            if (strMoney.Length > 15) return strMoney; //�ʥ��H�W���B�z

            #region �ܼƫŧi
            int intUnit2;
            int intUnit1;

            string[] strAnser = new string[4];
            int intLength = strMoney.Length - 1;
            string strTemp = "";
            int intTemp;
            string strResult = "";
            #endregion

            try
            {
                #region �Ʀr�ഫ
                for (int i = 0; i <= intLength; i++)
                {
                    intUnit1 = i % 4;
                    intUnit2 = i / 4;
                    strTemp = strMoney.Substring(intLength - i, 1);
                    intTemp = int.Parse(strTemp);
                    if (intTemp == 0)
                        strAnser[intUnit2] = strArrayNum[intTemp] + strAnser[intUnit2];
                    else
                        strAnser[intUnit2] = strArrayNum[intTemp] + strArrayUnit1[intUnit1] + strAnser[intUnit2];
                }
                #endregion


                #region �g�J���P�s�����
                for (int i = 0; i <= (intLength / 4); i++)
                {
                    if ((i > 0) && strAnser[i].Length > 3)
                    {
                        if ((strAnser[i].Substring((strAnser[i].Length - 3), 3) == "�s�s�s"
                            && ((strAnser[i].Substring(0, 1) != "�s"))))
                        {
                            strAnser[i] = strAnser[i].Replace("�s�s�s", "");
                            strAnser[i] = strAnser[i] + strArrayUnit2[i] + "�s";
                        }
                        else
                        {
                            strAnser[i] = strAnser[i].Replace("�s�s�s�s", "�s");
                            strAnser[i] = strAnser[i].Replace("�s�s�s", "�s");
                            strAnser[i] = strAnser[i].Replace("�s�s", "�s");
                            if (strAnser[i].Substring((strAnser[i].Length - 1), 1) == "�s")
                                strAnser[i] = strAnser[i].Substring(0, (strAnser[i].Length - 1)) + strArrayUnit2[i] + "�s";
                            else
                                strAnser[i] = strAnser[i] + strArrayUnit2[i];
                        }
                    }
                    else
                    {
                        strAnser[i] = strAnser[i].Replace("�s�s�s�s", "�s");
                        strAnser[i] = strAnser[i].Replace("�s�s�s", "�s");
                        strAnser[i] = strAnser[i].Replace("�s�s", "�s");
                        if (strAnser[i].Substring((strAnser[i].Length - 1), 1) == "�s")
                            strAnser[i] = strAnser[i].Substring(0, (strAnser[i].Length - 1)) + strArrayUnit2[i] + "�s";
                        else
                            strAnser[i] = strAnser[i] + strArrayUnit2[i];
                    }
                }
                #endregion

                strResult = strAnser[3] + strAnser[2] + strAnser[1] + strAnser[0];

                #region �䥦�B�z
                strResult = strResult.Replace("�����U", "��");
                strResult = strResult.Replace("����", "��");
                strResult = strResult.Replace("���U", "���s");
                strResult = strResult.Replace("�s��", "�s");
                strResult = strResult.Replace("�s��", "�s");
                strResult = strResult.Replace("�s�U", "�s");
                strResult = strResult.Replace("�s�s�s", "�s");
                strResult = strResult.Replace("�s�s", "�s");
                strResult = strResult.Replace("�s����", "����");
                if (strResult.Substring(0, 1) == "�s")
                    strResult = strResult.Substring(1, (strResult.Length - 1));
                if (strResult.Substring(strResult.Length - 1, 1) == "�s")
                    strResult = strResult.Substring(0, (strResult.Length - 1));
                #endregion

                return strResult;
            }
            catch
            {
                return strMoney;
            }
        }

        #endregion

        #region ConvertToFullWidthString �ন���Φr��
        public static char ConverttoFullWidthChar(this char ch)
        {
            for (int i = 0; i < HELFWIDTH_CODE.Length; i++)
            {
                if (HELFWIDTH_CODE[i] == ch)
                    return FULLWIDTH_CODE[i];
            }
            return ch;
        }

        public static string ConverttoFullWidthString(this string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                return new String(str.Where(c => c != '\r' && c != '\n' && c != '\t')
                    .Select(c => c.ConverttoFullWidthChar()).ToArray());
            }

            return str;
        }
        #endregion

        #region ConvertMoneyToCNShow

        public static string ConvertMoneyToCNShow(object numValue)
        {

            string strMoney = numValue.ToString();
            int i;
            int intZeroCount;
            int intLength;
            bool boolStart = false;
            string[] strArrayNum = new string[10] { "�s", "��", "�L", "��", "�v", "��", "��", "�m", "��", "�h" };
            string[] strArrayUnit = new string[13] { "", "�B", "��", "�a", "�U", "�B", "��", "�a", "��", "�B", "��", "�a", "��" };
            string strAnswer = "";
            string strAnswreTemp = "";
            string strTemp = "";
            string strZero = "";
            intZeroCount = 0;
            int intTemp = 0;
            int intDecIndex;
            string strMoneyInt;
            string strMoneyDec;

            intDecIndex = strMoney.IndexOf(".");
            strMoneyInt = strMoney.Substring(0, intDecIndex);
            strMoneyDec = strMoney.Substring((intDecIndex + 1), (strMoney.Length - intDecIndex - 1));
            intLength = strMoneyInt.Length - 1;

            for (i = 0; i <= intLength; i++)
            {
                strTemp = strMoneyInt.Substring(intLength - i, 1);
                intTemp = int.Parse(strTemp);
                //boolStart True:�ɹs False:���ɹs

                if (intTemp > 0)
                {
                    if ((i == 5) || (i == 9))
                        if (intZeroCount >= 1)
                        {
                            if (boolStart == true)
                                strAnswer = strArrayNum[intTemp] + strArrayUnit[i] + strArrayUnit[i - 1] + strArrayNum[0] + strAnswer;
                            else
                                strAnswer = strArrayNum[intTemp] + strArrayUnit[i] + strArrayUnit[i - 1] + strAnswer;
                            intZeroCount = 0;
                        }
                        else
                            //							if (i >5 )
                            strAnswer = strArrayNum[intTemp] + strArrayUnit[i] + strAnswer;
                    //							else 
                    //								strAnswer = strArrayNum[intTemp] + strArrayUnit[i] + strArrayUnit[i-1] + strAnswer;
                    else
                    {
                        if (intZeroCount >= 1)
                        {
                            if (boolStart == true)
                                strAnswer = strArrayNum[intTemp] + strArrayUnit[i] + strArrayNum[0] + strAnswreTemp + strAnswer;
                            else
                                if (i >= 3)
                                    strAnswer = strArrayNum[intTemp] + strArrayUnit[i] + strAnswreTemp + strArrayNum[0] + strAnswer;
                                else
                                    strAnswer = strArrayNum[intTemp] + strArrayUnit[i] + strAnswreTemp + strAnswer;
                            intZeroCount = 0;
                            strAnswreTemp = "";

                        }
                        else
                            strAnswer = strArrayNum[intTemp] + strArrayUnit[i] + strAnswer;
                    }
                    strZero = "";
                    boolStart = true;
                }
                else
                {
                    if ((i == 5) || (i == 9))
                    {
                        strAnswreTemp = strArrayUnit[i - 1];         //�U�P���O���,�J�ϬO�s�]�n�g�J
                        boolStart = false;                         //����ɹs
                    }
                    intZeroCount = intZeroCount + 1;
                    strZero = strArrayNum[0];
                }
            }
            //�ҥ~�B�z
            if (strAnswer.Substring(strAnswer.Length - 1, 1) == "�s")     //���Ƭ��s�ɧR���F
                strAnswer = strAnswer.Substring(0, strAnswer.Length - 1);
            if (strAnswer.Length > 2)
                if (strAnswer.Substring(strAnswer.Length - 2, 2) == "���U")   //���Ƭ����U�ɧR���U
                    strAnswer = strAnswer.Substring(0, strAnswer.Length - 1);
            //���ƥ["����"
            strAnswer = strAnswer + "����";
            return strAnswer;

        }

        #endregion

        #region "�˴��K��Τ@�s��"
        public static bool CheckRegno(this string strNo)
        {
            if (strNo == null)
                return false;
            String receiptNo = strNo.Trim();
            if (receiptNo.Length != 8)
                return false;

            int code;
            if(!int.TryParse(receiptNo,out code))
            {
                return false;
            }

            Func<int, int> digitSum = d =>
            {
                return (d / 10) + (d % 10);
            };
            //1,2,1,2,1,2,4,1
            int checkSum = (code / 10000000) +
                digitSum(((code % 10000000) / 1000000) * 2) +
                ((code % 1000000) / 100000) +
                digitSum(((code % 100000) / 10000) * 2) +
                ((code % 10000) / 1000) +
                digitSum(((code % 1000) / 100) * 2) +
                digitSum(((code % 100) / 10) * 4) +
                (code % 10);

            if (checkSum % 10 == 0)
                return true;

            if ((code % 100) / 10 == 7 && (checkSum + 1) % 10 == 0)
                return true;

            return false;

        }
        #endregion

        #region HasAtLeastOneRow
        public static bool HasAtLeastOneRow(DataSet ds)
        {
            return (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0);
        }

        #endregion


        public static void AssignRowValue(DataRow srcRow, DataRow targetRow)
        {
            DataColumnCollection srcCols = srcRow.Table.Columns;
            DataColumnCollection targetCols = targetRow.Table.Columns;
            foreach (DataColumn targetCol in targetCols)
            {
                DataColumn srcCol = srcCols[targetCol.ColumnName];
                if ((null != srcCol) && !DBNull.Value.Equals(srcRow[srcCol]))
                {
                    if (srcCol.DataType.Equals(typeof(String)))
                    {
                        targetRow[targetCol] = Convert.ChangeType(((string)srcRow[srcCol]).Trim(), targetCol.DataType);
                    }
                    else
                    {
                        targetRow[targetCol] = Convert.ChangeType(srcRow[srcCol], targetCol.DataType);
                    }
                }
            }
        }

        public static byte[] GetFileBytes(String filename)
        {
            if (!File.Exists(filename))
                return null;
            Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read);
            int datalen = (int)stream.Length;
            byte[] filebytes = new byte[datalen];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(filebytes, 0, datalen);
            stream.Close();
            return filebytes;
        }

        public static string MakePassword(string password)
        {
            MD5 md5 = MD5.Create();
            return String.Join("", md5.ComputeHash(Encoding.Default.GetBytes(password)).Select(i => String.Format("{0:X02}", i)));
        }

        public static Boolean ChkID(string id)
        {


            //string LegalID = "0123456789";
            //string LegalUserName = "0123456789abcdefghijklmnopqrstuvwxyz_";
            int value = 0;
            string sId;
            if (String.IsNullOrEmpty(id) || id.Length != 10)
            {
                return false;
            }
            else
            {
                sId = id.ToUpper();
                value = sId[0] - 'A' + 10;

                if (value < 10 || value > 35)
                    return false;

            }

            long suffix;
            if (!long.TryParse(sId.Substring(1), out suffix))
                return false;

            value = value/10 + (value % 10) * 9 +
            sId[1] * 8 +
            sId[2] * 7 +
            sId[3] * 6 +
            sId[4] * 5 +
            sId[5] * 4 +
            sId[6] * 3 +
            sId[7] * 2 +
            sId[8] +
            sId[9];
            value = value % 10;
            if (value != 0)
            {
                return false;
            }

            return true;


        }

        public static string CreateRandomStringCode(this int codeLength)
        {
            //���ҽX���r�����A�h���F�@�Ǯe���V�c���r��
            Thread.Sleep(1);
            Random oRnd = new Random();
            char[] sCode = new char[codeLength];

            //�ͦ����ҽX�r��
            for (int n = 0; n < codeLength; n++)
            {
                sCode[n] = DISCERNIBLE_CODE[oRnd.Next(DISCERNIBLE_CODE.Length)];
            }
            return new String(sCode);
        }

        public static string GenerateRandomCode(this int codeLength)
        {
            //���ҽX���r�����A�h���F�@�Ǯe���V�c���r��
            Thread.Sleep(1);
            Random oRnd = new Random();
            char[] sCode = new char[codeLength];

            //�ͦ����ҽX�r��
            for (int n = 0; n < codeLength; n++)
            {
                sCode[n] = NUMERIC_STRING[oRnd.Next(NUMERIC_STRING.Length)];
            }
            return new String(sCode);
        }


        public static char[] GetChineseNumberSeries(this int number,int length)
        {
            char[] result = new char[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = __CHINESE_NUM_CHAR[number % 10];
                number /= 10;
            }
            return result;
        }

        public static byte[] UploadData(this XmlNode data, String url)
        {
            using (WebClient client = new WebClient())
            {
                Encoding utf8 = new UTF8Encoding();
                client.Encoding = utf8;
                return client.UploadData(url, utf8.GetBytes(data.OuterXml));
            }
        }

        public static XmlDocument UploadDocument(this XmlNode data, String url)
        {
            byte[] result = data.UploadData(url);
            if (result != null)
            {
                using (MemoryStream ms = new MemoryStream(result))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(ms);
                    return doc;
                }
            }
            return null;
        }

        public static void SaveAs(this byte[] buf, String fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buf, 0, buf.Length);
                fs.Flush();
                fs.Close();
            }
        }

        public static T NullDefault<T>(this Nullable<T> val)
            where T : struct
        {
            return val.HasValue ? val.Value : default(T);
        }


        #region "�����J���r��"
        /// 
        /// �P�_��J���r�������C�@
        /// 
        /// ��J���r��C(string) 
        /// �n����������A�i��ܤ������p�U�C��C(int)         
        /// ����q�L�h�Ǧ^ True�A�Ϥ��h�� False�C
        public static bool ValidateString(String _value, int _kind)
        {
            string RegularExpressions = null;
            CompareValidator cv = new CompareValidator();
            switch (_kind)
            {
                case 1:
                    //��26�ӭ^��r���զ����r��
                    RegularExpressions = "^[A-Za-z]+$";
                    break;
                case 2:
                    //����� 
                    RegularExpressions = "^[0-9]*[1-9][0-9]*$";
                    break;
                case 3:
                    //�D�t��ơ]����� + 0)
                    RegularExpressions = "^\\d+$";
                    break;
                case 4:
                    //�D����ơ]�t��� + 0�^
                    RegularExpressions = @"^((-\\d+)|(0+))$";
                    break;
                case 5:
                    //�t��� 
                    RegularExpressions = @"^-[0-9]*[1-9][0-9]*$";
                    break;
                case 6:
                    //���
                    RegularExpressions = @"^-?\\d+$";
                    break;
                case 7:
                    //�D�t�B�I�ơ]���B�I�� + 0�^
                    RegularExpressions = @"^\\d+(\\.\\d+)?$";
                    break;
                case 8:
                    //���B�I��
                    RegularExpressions = @"^(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
                    break;
                case 9:
                    //�D���B�I�ơ]�t�B�I�� + 0�^
                    RegularExpressions = @"^((-\\d+(\\.\\d+)?)|(0+(\\.0+)?))$";
                    break;
                case 10:
                    //�t�B�I��
                    RegularExpressions = @"^(-(([0-9]+\\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\\.[0-9]+)|([0-9]*[1-9][0-9]*)))$";
                    break;
                case 11:
                    //�B�I��
                    RegularExpressions = @"^(-?\\d+)(\\.\\d+)?$";
                    break;
                case 12:
                    //��26�ӭ^��r�����j�g�զ����r��
                    RegularExpressions = "^[A-Z]+$";
                    break;
                case 13:
                    //��26�ӭ^��r�����p�g�զ����r��
                    RegularExpressions = "^[a-z]+$";
                    break;
                case 14:
                    //�ѼƦ�M26�ӭ^��r���զ����r��
                    RegularExpressions = "^[A-Za-z0-9]+$";
                    break;
                case 15:
                    //�ѼƦ�B26�ӭ^��r���Ϊ̤U���u�զ����r�� 
                    RegularExpressions = "^[0-9a-zA-Z_]+$";
                    break;
                case 16:
                    //email�a�}
                    RegularExpressions = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
                    break;
                case 17:
                    //url
                    RegularExpressions = "^[a-zA-z]+://(\\w+(-\\w+)*)(\\.(\\w+(-\\w+)*))*(\\?\\S*)?$";
                    break;
                case 18:
                    //�u���J����
                    RegularExpressions = "^[^\u4E00-\u9FA5]";
                    break;
                case 19:
                    //�u���J0�M�D0���Y���Ʀr
                    RegularExpressions = "^(0|[1-9][0-9]*)$";
                    break;
                case 20:
                    //�u���J�Ʀr
                    RegularExpressions = "^[0-9]*$";
                    break;
                case 21:
                    //�u���J�Ʀr�[2��p��
                    RegularExpressions = "^[0-9]+(.[0-9]{1,2})?$";
                    break;
                case 22:
                    //�u���J0�M�D0���Y���Ʀr�[2��p��
                    RegularExpressions = "^(0|[1-9]+)(.[0-9]{1,2})?$";
                    break;
                case 23:
                    //�u���J0�M�D0���Y���Ʀr�[2��p�ơA�����ǰt0.00
                    RegularExpressions = "^(0(.(0[1-9]|[1-9][0-9]))?|[1-9]+(.[0-9]{1,2})?)$";
                    break;
                //case 24:
                //    //���Ҥ���榡 YYYYMMDD, �d��19000101~20991231
                //    RegularExpressions = "(19|20)\\d\\d+(0[1-9]|1[012])+(0[1-9]|[12][0-9]|3[01])$";
                //    break;
                //case 25:
                //    //���Ҥ���榡 MMDDYYYY
                //    RegularExpressions = "(0[1-9]|1[012])+(0[1-9]|[12][0-9]|3[01])+(19|20)\\d\\d$";
                //    break;
                //case 26:
                //    //���Ҥ���榡 YYYYMM
                //    RegularExpressions = "(19|20)\\d\\d+(0[1-9]|1[012])$";
                //    break;
                //case 27:
                //    //���Ҥ���榡 YYYYMMDD, �d��00010101~99991231
                //    RegularExpressions = "(^0000|0001|9999|[0-9]{4})+(0[1-9]|1[0-2])+(0[1-9]|[12][0-9]|3[01])$";
                //    break; 

                case 28:  //���Үɶ��榡HH/MM/SS
                    RegularExpressions = @"([0-1][0-9]|2[0-3])\:[0-5][0-9]\:[0-5][0-9]";
                    break;

                case 29:  //���үS��r��
                    RegularExpressions = "(?=.*[@#$%^&+=])";
                    break;
                case 30: //���Ҥ���榡YYYY/MM/DD

                    cv.ControlToValidate = _value;
                    cv.Operator = ValidationCompareOperator.DataTypeCheck;
                    cv.Type = ValidationDataType.Date;
                    // RegularExpressions = "^((19|20)[0-9]{2}[- /.](0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01]))$";
                    break;
                default:
                    break;
            }
            if (_kind < 30)
            {
                Match m = Regex.Match(_value, RegularExpressions);
                if (m.Success)
                    return true;
                else
                    return false;
            }
            else
            {

                return cv.IsValid;
            }
        }
        #endregion


    }
}
