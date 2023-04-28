using System;
using System.Collections.Generic;
using eInvoice.Core.Models.ERP;

namespace eInvoice.Core.Models
{
    public class GroupVatListDto 
    {
        public int groupVatId {get; set;}
        public int companyId {get; set;}
        public int year {get; set;}
        public VatPeriod vatPeriod {get; set;}
        public VatRecordingStatus vatRecordingStatus {get;set;}
        public string sendDate {get; set;}
    }
}