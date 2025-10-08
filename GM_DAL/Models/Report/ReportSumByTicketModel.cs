using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.Report
{
    public class ReportSumByTicketModel
    {
        public string ticketCode { get; set; }
        public int quanti { get; set; }
        public decimal total { get; set; }
    }
}
