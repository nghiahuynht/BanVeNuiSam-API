using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.ChiTieu
{
    public class ChiTieuFilterModel: DataTableDefaultParamModel
    {
        public string paymentMethod {  get; set; }
        public string category { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get;set; }
        public string? keyword { get; set; }
        public string tranType { get; set; }
    }
}
