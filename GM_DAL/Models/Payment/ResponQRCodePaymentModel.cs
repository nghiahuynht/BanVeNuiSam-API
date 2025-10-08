using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.Payment
{
    public class ResponQRCodePaymentModel
    {
        public string code { get; set; }
        public string desc { get; set; }
        public QRCodeDetailModel data { get; set; }
    }
}
