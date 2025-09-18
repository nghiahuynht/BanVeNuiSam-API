using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.Report
{
    public class TotalByCardModel
    {
        public long cardId { get; set; }
        public string cardName { get; set; }
        public decimal totalSpend { get; set; }
        public decimal totalCashback { get; set; }
    }
}
