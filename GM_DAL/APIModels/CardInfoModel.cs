using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.APIModels
{
    public class CardInfoModel
    {
        public long id { get; set; }
        public string? cardCode { get; set; }
        public string? cardName { get; set; }
        public string? conditionReturn { get; set; }
        public string? note { get; set; }
        public string? bankCode { get; set; }
        public decimal? yearlyFee { get; set; }
        public string? imgPath { get; set; }
        public int? periodPayment { get; set; }
        public decimal? totalAmount { get; set; } // tổng chi tiêu
    }
}
