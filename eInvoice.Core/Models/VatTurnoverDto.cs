using System;
using System.Collections.Generic;
using eInvoice.Core.Models.ERP;

namespace eInvoice.Core.Models
{
    public class VatTurnoverDto
    {
        public int vatTurnoverId {get; set;}
        public double taxableAmount20 {get; set;}
        public double taxAmount20 {get; set;}
        public double totalAmount20 {get; set;}
        public double taxableAmount10 {get; set;}
        public double taxAmount10 {get; set;}
        public double totalAmount10 {get; set;}
    }
}