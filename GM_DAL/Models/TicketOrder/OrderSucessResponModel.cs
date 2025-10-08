using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.TicketOrder
{
    public class OrderSucessResponModel
    {
        public long orderId { get; set; }
        public string ticketCode { get; set; }
        public string customerName { get; set; }
        public decimal price { get; set; }
        public decimal quanti { get; set; }
        public decimal total { get; set; }
        public int paymentStatus { get; set; }

    }
}
