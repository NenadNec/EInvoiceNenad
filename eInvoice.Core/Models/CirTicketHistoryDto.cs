using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eInvoice.Core.Models
{
    public class CirTicketHistoryDto
    {
        public int cirTicketId { get; set; }
        public List<cthd_CirTicketChanx> cirTicketChanges { get; set; }
    }
    public class cthd_User
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class cthd_CirTicketChanx
    {
        public int id { get; set; }
        public string propertyName { get; set; }
        public string oldValue { get; set; }
        public string newValue { get; set; }
        public DateTime dateChanged { get; set; }
        public cthd_User user { get; set; }
        public int version { get; set; }
        public bool serviceDesk { get; set; }
    }
}
