using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models
{
    public class DataTableResultModel<T>
    {
        public DataTableResultModel()
        {
            this.data = new List<T>();
            this.totalValue = 0;
        }
        public long recordsTotal { get; set; }
        public long recordsFiltered { get; set; }
        public decimal totalValue { get; set; }
        public List<T> data { get; set; }
    }
}
