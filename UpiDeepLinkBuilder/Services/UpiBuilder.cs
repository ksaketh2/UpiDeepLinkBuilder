using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTonix.UpiDeepLinkBuilder.Models;

namespace TechTonix.UpiDeepLinkBuilder.Services
{
    public sealed class UpiBuilder
    {
        private UpiTransaction _transactionObj = new UpiTransaction();

        public UpiBuilder() { }

        public UpiBuilder(string upiDeepLinkUrl)
        {
            UpiParser parser = new UpiParser();
            Dictionary<string, string> parsedData = parser.ParseUpiDeeplink(upiDeepLinkUrl);

            this._transactionObj = new UpiTransaction
            {
                PayeeAddress = parsedData.TryGetValue("pa", out string? pa) ? pa : string.Empty,
                PayeeName = parsedData.TryGetValue("pn", out string? pn) ? pn : string.Empty,
                TransactionNote = parsedData.TryGetValue("tn", out string? tn) ? tn : string.Empty,
                Amount = parsedData.TryGetValue("am", out string? am) ? Convert.ToDecimal(am) : 0,
                Currency = parsedData.TryGetValue("cu", out string? cu) ? cu : string.Empty,
                TransactionRefId = parsedData.TryGetValue("tr", out string? tr) ? tr : string.Empty,
                MerchantCode = parsedData.TryGetValue("mc", out string? mc) ? mc : string.Empty,
                UpiTransactionId = parsedData.TryGetValue("tid", out string? tid) ? tid : string.Empty
            };
        }

        public UpiBuilder SetPayeeAddress(string payeeAddress)
        {
            _ = string.IsNullOrWhiteSpace(payeeAddress) ? throw new ArgumentException("Payee address cannot be null or empty.", nameof(payeeAddress)) : _transactionObj.PayeeAddress = payeeAddress;
            return this;
        }

        public UpiBuilder SetPayeeName(string payeeName)
        {
            _ = string.IsNullOrWhiteSpace(payeeName) ? throw new ArgumentException("Payee name cannot be null or empty.", nameof(payeeName)) : _transactionObj.PayeeName = payeeName;
            return this;
        }

        public UpiBuilder SetAmount(decimal amount)
        {
            _ = amount < 0 ? throw new ArgumentException("Amount must be greater than zero.", nameof(amount)) : _transactionObj.Amount = amount;
            return this;
        }

        public UpiBuilder SetCurrency(string currency)
        {
            _ = string.IsNullOrWhiteSpace(currency) ? throw new ArgumentException("Currency cannot be null or empty.", nameof(currency)) : _transactionObj.Currency = currency;
            return this;
        }

        public UpiBuilder SetTransactionNote(string note)
        {
            _transactionObj.TransactionNote = note ?? string.Empty;
            return this;
        }

        public UpiBuilder SetTransactionRefId(string transactionRefId)
        {
            _transactionObj.TransactionRefId = transactionRefId ?? string.Empty;
            return this;
        }

        public UpiBuilder SetMerchantCode(string merchantCode)
        {
            _transactionObj.MerchantCode = merchantCode ?? string.Empty;
            return this;
        }

        public UpiBuilder SetUpiTransactionId(string upiTransactionId)
        {
            _transactionObj.UpiTransactionId = upiTransactionId ?? string.Empty;
            return this;
        }

        public string Build()
        {
            if (string.IsNullOrWhiteSpace(_transactionObj.PayeeAddress))
            {
                throw new InvalidOperationException("Payee address is required.");
            }

            if (_transactionObj.Amount < 0)
            {
                throw new InvalidOperationException("Amount must be greater than or equal to zero.");
            }

            return this._transactionObj.ToString();
        }
    }
}
