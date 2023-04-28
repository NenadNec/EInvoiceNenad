using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eInvoice.Core.Models.Enums;

namespace eInvoice.Core.Models
{

    public class CirTicketListResponse
    {
        public int total { get; set; }
        public List<ctlr_Ticket> tickets { get; set; }
    }
    public class ctlr_OperatorComment
    {
    }

    public class ctlr_User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class ctlr_CirTicketChanx
    {
        public int id { get; set; }
        public string propertyName { get; set; }
        public string oldValue { get; set; }
        public string newValue { get; set; }
        public DateTime dateChanged { get; set; }
        public ctlr_User user { get; set; }
        public int version { get; set; }
        public bool serviceDesk { get; set; }
    }

    public class ctlr_CirTicketHistory
    {
        public int cirTicketId { get; set; }
        public List<ctlr_CirTicketChanx> cirTicketChanges { get; set; }
    }

    public class ctlr_Ticket
    {
        public int id { get; set; }
        public string cirId { get; set; }
        public string category { get; set; }
        public string data { get; set; }
        public int organizationId { get; set; }
        public string userComment { get; set; }
        public ctlr_OperatorComment operatorComment { get; set; }
        public CirTicketStatus status { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime closingDate { get; set; }
        public string categoryCyr { get; set; }
        public string companyNumber { get; set; }
        public string organizationName { get; set; }
        public string resourceId { get; set; }
        public ctlr_CirTicketHistory cirTicketHistory { get; set; }
    }
}

