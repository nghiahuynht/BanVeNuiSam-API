using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Models.Report
{
    public class ReportFilterModel
    {
        public long carId { get; set; }
        public int yearView { get; set; }    
        public int fromMonth { get; set; }
        public int toMonth { get; set; }
        public string userName { get; set; }
    }
}
