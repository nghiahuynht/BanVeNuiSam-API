using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.TicketOrder
{
    public class TicketOrderSearchGridModel
    {
        public long id { get; set; }
        public string ticketCode { get; set; }
        public int quanti { get; set; }
        public decimal price { get; set; }
        public decimal total { get; set; }
        public string createdBy { get; set; }
        public DateTime? createdDate { get; set; }
    }
}
