using GM_DAL.Models.Ticket;
using GM_DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GM_DAL.IServices;
using GM_DAL.Models.Report;
using GM_DAL.Models.Print;
using GM_DAL.Models.TicketOrder;

namespace PlatFormAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReportController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private IReportService _reportService;
        public ReportController(IConfiguration configuration, IReportService reportService)
        {
            _configuration = configuration;
            _reportService = reportService;
        }



        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(APIResultObject<List<ReportSumByTicketModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<ReportSumByTicketModel>>> GetReportSumByTicket(ReportFilterModel filter)
        {
            // string passEncr = EncrypMD5(pass);
            var res = await _reportService.GetReportSumByTicket(filter);
            return res;
        }


        [HttpGet]
        [ProducesResponseType(typeof(APIResultObject<List<PrintInfoModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<PrintInfoModel>>> GetPrintInfo(long orderId)
        {
            // string passEncr = EncrypMD5(pass);
            var res = await _reportService.GetInfoPrint(orderId);


            string fileFullPath = "https://androidride.com/wp-content/uploads/2021/06/qr-code-generator-flutter.jpeg";
            foreach (var item in res.data)
            {
                item.imgQr = fileFullPath;
            }
            return res;
        }



       





    }
}
