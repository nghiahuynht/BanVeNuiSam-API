using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.Print
{
    public class PrintInfoModel
    {
        public long orderId { get; set; }
        public long subId { get; set; }
        public string ticketCode { get; set; }
        public decimal? price { get; set; }
        public int quanti { get; set; }
        public decimal total { get; set; }
        public DateTime? createdDate { get; set; }
        public string createdBy { get; set; }
        public string locationName { get; set; }
        public string subCode { get; set; }
        public string imgQr { get; set; }
    }
}
