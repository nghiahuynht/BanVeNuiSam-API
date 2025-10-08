using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.TicketOrder
{
    public class SearchFilterModel
    {
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public string userName { get; set; }
        public string keyword { get; set; }
    }
}
