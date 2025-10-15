using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTonix.UpiDeepLinkBuilder.Models
{
    public sealed class UpiTransaction
    {
        public string PayeeAddress { get; set; } = string.Empty; // pa

        public string PayeeName { get; set; } = string.Empty;    // pn

        public string TransactionNote { get; set; } = string.Empty; // tn

        public decimal Amount { get; set; } = 0.0M;              // am

        public string Currency { get; set; } = "INR";            // cu

        public string MerchantCode { get; set; } = string.Empty; // mc

        public string TransactionRefId { get; set; } = string.Empty; // tr

        public string UpiTransactionId { get; set; } = string.Empty; // tid

        public string UpiApp { get; set; } = string.Empty;       // url scheme for specific UPI app (optional)

        public string AdditionalParams { get; set; } = string.Empty; // any additional params (optional)

        public UpiTransaction() { }

        public UpiTransaction(string payeeAddress, string payeeName, decimal amount, string currency = "INR", string note = "")
        {
            this.PayeeAddress = payeeAddress;
            this.PayeeName = payeeName;
            this.Amount = amount;
            this.Currency = currency;
            this.TransactionNote = note;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("upi://pay?");

            if (!string.IsNullOrWhiteSpace(PayeeAddress))
                sb.Append($"pa={Uri.EscapeDataString(PayeeAddress)}&");

            if (!string.IsNullOrWhiteSpace(PayeeName))
                sb.Append($"pn={Uri.EscapeDataString(PayeeName)}&");

            if (Amount > 0)
                sb.Append($"am={Amount:F2}&");

            if (!string.IsNullOrWhiteSpace(Currency))
                sb.Append($"cu={Uri.EscapeDataString(Currency)}&");

            if (!string.IsNullOrWhiteSpace(TransactionNote))
                sb.Append($"tn={Uri.EscapeDataString(TransactionNote)}&");

            if (!string.IsNullOrWhiteSpace(MerchantCode))
                sb.Append($"mc={Uri.EscapeDataString(MerchantCode)}&");

            if (!string.IsNullOrWhiteSpace(TransactionRefId))
                sb.Append($"tr={Uri.EscapeDataString(TransactionRefId)}&");

            if (!string.IsNullOrWhiteSpace(UpiTransactionId))
                sb.Append($"tid={Uri.EscapeDataString(UpiTransactionId)}&");

            if (!string.IsNullOrWhiteSpace(AdditionalParams))
                sb.Append($"{AdditionalParams}&");

            // Remove the trailing '&' if present
            if (sb[sb.Length - 1] == '&')
                sb.Length--;

            return sb.ToString();
        }
    }
}
