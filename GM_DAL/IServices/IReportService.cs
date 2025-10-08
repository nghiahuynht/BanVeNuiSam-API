using GM_DAL.Models.Report;
using GM_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GM_DAL.Models.Print;

namespace GM_DAL.IServices
{
    public interface IReportService
    {
        Task<APIResultObject<List<ReportSumByTicketModel>>> GetReportSumByTicket(ReportFilterModel filter);
        Task<APIResultObject<List<PrintInfoModel>>> GetInfoPrint(long orderId);
    }
}
