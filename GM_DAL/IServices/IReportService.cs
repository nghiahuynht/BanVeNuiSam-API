using GM_DAL.Models.Report;
using GM_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.IServices
{
    public interface IReportService
    {
        Task<APIResultObject<List<TotalByCardModel>>> ReportFinanByCard(ReportFilterModel filter);
    }
}
