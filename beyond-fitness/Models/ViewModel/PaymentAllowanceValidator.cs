﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Utility;
using WebHome.Helper;
using WebHome.Models.DataEntity;
using WebHome.Models.Locale;

namespace WebHome.Models.ViewModel
{
    public partial class PaymentAllowanceValidator<TEntity>
        where TEntity : class, new()
    {
        protected ModelSource<TEntity> _mgr;

        protected Payment _payment;

        protected InvoiceAllowance _newItem;
        protected Organization _seller;
        protected List<InvoiceAllowanceItem> _productItems;
        protected DateTime _allowanceDate;


        public PaymentAllowanceValidator(ModelSource<TEntity> mgr)
        {
            _mgr = mgr;
        }

        public InvoiceAllowance Allowance
        {
            get 
            {
                return _newItem;
            }
        }

        public virtual Exception Validate(Payment dataItem)
        {
            _payment = dataItem;

            Exception ex;

            _seller = null;
            _newItem = null;
            
            if((ex = CheckBusiness()) != null)
            {
                return ex;
            }

            if((ex = CheckMandatoryFields()) != null)
            {
                return ex;
            }

            if((ex = CheckAllowanceItem()) != null)
            {
                return ex;
            }

            return null;
        }

        protected virtual Exception CheckBusiness()
        {
            _seller = _payment.PaymentTransaction.BranchStore.Organization;

            return null;
        }

        protected virtual Exception CheckMandatoryFields()
        {
            _allowanceDate = DateTime.Now;

            return null;
        }

        protected virtual Exception CheckAllowanceItem()
        {
            _productItems = new List<InvoiceAllowanceItem>();
            var invTable = _mgr.GetTable<InvoiceItem>();

                InvoiceItem originalInvoice = _payment.InvoiceItem;

                if (originalInvoice == null)
                {
                    return new Exception("發票資料不存在!!");
                }

                if (originalInvoice.InvoiceCancellation != null)
                {
                    return new Exception("該發票已作廢，不可折讓。");
                }

                var allowanceItem = new InvoiceAllowanceItem
                {
                    Amount = Math.Round((decimal)_payment.PayoffAmount / 1.05m),
                    InvoiceNo = originalInvoice.TrackCode + originalInvoice.No,
                    InvoiceDate = originalInvoice.InvoiceDate,
                    ItemNo = originalInvoice.InvoiceDetails[0].InvoiceProduct.InvoiceProductItem[0].ItemNo,
                    OriginalSequenceNo = originalInvoice.InvoiceDetails[0].InvoiceProduct.InvoiceProductItem[0].No,
                    Piece = originalInvoice.InvoiceDetails[0].InvoiceProduct.InvoiceProductItem[0].Piece,
                    PieceUnit = originalInvoice.InvoiceDetails[0].InvoiceProduct.InvoiceProductItem[0].PieceUnit,
                    OriginalDescription = originalInvoice.InvoiceDetails[0].InvoiceProduct.Brief,
                    TaxType = originalInvoice.InvoiceDetails[0].InvoiceProduct.InvoiceProductItem[0].TaxType,
                    No = originalInvoice.InvoiceDetails[0].InvoiceProduct.InvoiceProductItem[0].No,
                    UnitCost = originalInvoice.InvoiceDetails[0].InvoiceProduct.InvoiceProductItem[0].UnitCost,
                };

            allowanceItem.Tax = _payment.PayoffAmount - allowanceItem.Amount;

                _productItems.Add(allowanceItem);
            

            _newItem = new InvoiceAllowance() 
            {
                Document = new Document
                {
                    DocDate = DateTime.Now,
                    DocType = (int)Naming.DocumentTypeDefinition.E_Allowance
                },
                AllowanceDate = _allowanceDate,
                AllowanceNumber = originalInvoice.TrackCode+originalInvoice.No,
                AllowanceType = 1,
                InvoiceID = originalInvoice.InvoiceID,
                BuyerId = originalInvoice.InvoiceBuyer.ReceiptNo,
                SellerId = _seller.ReceiptNo,
                TaxAmount = allowanceItem.Tax,
                TotalAmount = allowanceItem.Amount,
                //InvoiceID =  invTable.Where(i=>i.TrackCode + i.No == item.AllowanceItem.Select(a=>a.OriginalInvoiceNumber).FirstOrDefault()).Select(i=>i.InvoiceID).FirstOrDefault(),
                InvoiceAllowanceBuyer = new InvoiceAllowanceBuyer
                {
                    Name = originalInvoice.InvoiceBuyer.Name,
                    ReceiptNo = originalInvoice.InvoiceBuyer.ReceiptNo,
                    CustomerID = originalInvoice.InvoiceBuyer.CustomerID,
                    ContactName = originalInvoice.InvoiceBuyer.ContactName,
                    CustomerName = originalInvoice.InvoiceBuyer.CustomerName
                },
                InvoiceAllowanceSeller = new InvoiceAllowanceSeller
                {
                    Name = _seller.CompanyName,
                    ReceiptNo = _seller.ReceiptNo,
                    Address = _seller.Addr,
                    ContactName = _seller.ContactName,
                    CustomerName = _seller.CompanyName,
                    EMail = _seller.ContactEmail,
                    Fax = _seller.Fax,
                    Phone = _seller.Phone,
                    PersonInCharge = _seller.UndertakerName,
                    SellerID = _seller.CompanyID,
                }
            };

            _newItem.InvoiceAllowanceDetails.AddRange(_productItems.Select(p => new InvoiceAllowanceDetails 
            {
                InvoiceAllowanceItem = p,
            }));

            return null;
        }
    }
}
