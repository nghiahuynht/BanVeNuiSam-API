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

namespace GM_DAL.Services
{
    public class ReportService:BaseService, IReportService
    {
        private SQLAdoContext adoContext;
        public ReportService(SQLAdoContext adoContext)
        {
            this.adoContext = adoContext;
        }

        public async Task<APIResultObject<List<TotalByCardModel>>> ReportFinanByCard(ReportFilterModel filter)
        {
            var res = new APIResultObject<List<TotalByCardModel>>();
            try
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CardId", CommonHelper.CheckLongNull(filter.carId));
                parameters.Add("@YearView", CommonHelper.CheckIntNull(filter.yearView));
                parameters.Add("@FromMonth", CommonHelper.CheckIntNull(filter.fromMonth));
                parameters.Add("@ToMonth", CommonHelper.CheckIntNull(filter.toMonth));
                parameters.Add("@UserName", CommonHelper.CheckStringNull(filter.userName));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<TotalByCardModel>("sp_ReportFinanByCard", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }


            }
            catch (Exception ex)
            {
                res.code = Enum.ResultCode.ErrorException;
                res.message.message = "error";
                res.message.exMessage = ex.Message;
            }

            return res;
        }



    }
}
