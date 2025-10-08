using Dapper;
using GM_DAL.IServices;
using GM_DAL.Models.HomPage;
using GM_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GM_DAL.Models.Report;
using GM_DAL.Models.Print;

namespace GM_DAL.Services
{
    public class ReportService:BaseService, IReportService
    {
        private SQLAdoContext adoContext;
        public ReportService(SQLAdoContext adoContext)
        {
            this.adoContext = adoContext;
        }


        public async Task<APIResultObject<List<ReportSumByTicketModel>>> GetReportSumByTicket(ReportFilterModel filter)
        {
            var res = new APIResultObject<List<ReportSumByTicketModel>>();
            try
            {

                using (var connection = adoContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@FromDate", CommonHelper.CheckDateNull(filter.fromDate));
                    parameters.Add("@ToDate", CommonHelper.CheckDateNull(filter.toDate));
                    parameters.Add("@UserName", CommonHelper.CheckStringNull(filter.userName));
                    var resultExcute = await connection.QueryAsync<ReportSumByTicketModel>("sp_GetReportSumByTicket", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }
            }
            catch (Exception ex)
            {
                res.data = new List<ReportSumByTicketModel>();
                res.message.exMessage = ex.Message;
            }
            return res;
        }


        public async Task<APIResultObject<List<PrintInfoModel>>> GetInfoPrint(long orderId)
        {
            var res = new APIResultObject<List<PrintInfoModel>>();
            try
            {

                using (var connection = adoContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@OrderId", CommonHelper.CheckLongNull(orderId));
                    var resultExcute = await connection.QueryAsync<PrintInfoModel>("sp_GetPrintForMobile", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }
            }
            catch (Exception ex)
            {
                res.data = new List<PrintInfoModel>();
                res.message.exMessage = ex.Message;
            }
            return res;
        }



    }
}
