using System;
using System.Collections.Generic;
using eInvoice.Core.Models.ERP;

namespace eInvoice.Core.Models
{
    public class GroupVatDto
    {
        public int groupVatId {get; set;}
        public int companyId {get; set;}
        public int year {get; set;}
        public VatPeriod vatPeriod {get; set;}
        public VatRecordingStatus vatRecordingStatus {get; set;}
        public VatTurnoverDto TurnoverWithFee {get; set;}
        public VatTurnoverDto TurnoverWithoutFee {get; set;}
        public double  vatReductionFromPreviosPeriodAmount {get; set;}
        public double vatIncreaseFromPreviousPeriodAmount {get; set;}
        public string sendDate{get; set;}

        
    }
}