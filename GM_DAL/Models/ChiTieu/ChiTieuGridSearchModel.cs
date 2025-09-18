using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.ChiTieu
{
    public class ChiTieuGridSearchModel
    {
        public long id { get; set; }
        public string category { get; set; }// lĩnh vực
        public decimal amount { get; set; }
        public string paymentMethod { get; set; } // Cash: tienmat, CreditCard, CK
        public decimal cashbackAmount { get; set; }
        public string note { get; set; }
        public int? cardId { get; set; }
        public string? cardName { get; set; }
        public string? createdBy { get; set; }
        public DateTime? createdDate { get; set; }
        public string tranType { get; set; }
    }
}
