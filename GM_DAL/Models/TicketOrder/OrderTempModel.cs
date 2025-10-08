using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.TicketOrder
{
    public class OrderTempModel
    {
        public string customerCode { get; set; }
        public string customerName { get; set; }
        public string customerType { get; set; }
        public string userName { get; set; }
        public string location { get; set; }
        public string paymentType { get; set; }
        public List<TicketOrderItemModel> items { get; set; }
        
    }
}
