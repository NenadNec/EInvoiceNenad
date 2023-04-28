using System;
using System.Collections.Generic;
using System.Text;

namespace eInvoice.Core.Models
{
    public class CirHistoryDto
    {
        public string comment { get; set; }
        public chd_Assignment assignment { get; set; }
        public List<chd_AmountChanx> amountChanges { get; set; }
        public chd_Cancellation cancellation { get; set; }
        public List<chd_Settlement> settlements { get; set; }
    }

    public class chd_Assignment
    {
        public string assignmentContractNr { get; set; }
        public string assignmentDebtorName { get; set; }
        public string assignmentDebtorCompanyNr { get; set; }
        public string assignmentIdfNr { get; set; }
        public string originalIdfNr { get; set; }
    }

    public class chd_AmountChanx
    {
        public string comments { get; set; }
        public int amount { get; set; }
        public DateTime cancelDate { get; set; }
        public DateTime creationDate { get; set; }
        public string cancelComments { get; set; }
        public int changedId { get; set; }
        public int id { get; set; }
    }

    public class chd_Cancellation
    {
        public DateTime cancelDate { get; set; }
        public int cancelledBy { get; set; }
        public string reason { get; set; }
    }

    public class chd_Settlement
    {
        public DateTime settlementDate { get; set; }
        public int amount { get; set; }
        public string comment { get; set; }
    }
}
