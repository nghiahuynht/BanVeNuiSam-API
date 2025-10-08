using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.TicketOrder
{
    public class TicketOrderItemModel
    {
        public string ticketCode { get; set; }
        public int quanti { get; set; }
        public decimal price { get; set; }
        public decimal total { get; set; }
    }
}
