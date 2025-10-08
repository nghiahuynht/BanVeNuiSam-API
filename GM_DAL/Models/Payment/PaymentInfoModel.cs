using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.Payment
{
    public class PaymentInfoModel
    {
        public Int64 orderId { get; set; }
        public decimal total { get; set; }
    }
}
