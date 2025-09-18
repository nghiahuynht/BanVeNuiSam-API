using Dapper;
using GM_DAL.IServices;
using GM_DAL.Models.ChiTieu;
using GM_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Services
{
    public class ChiTieuService:IChiTieuService
    {
        private SQLAdoContext adoContext;
        public ChiTieuService(SQLAdoContext adoContext)
        {
            this.adoContext = adoContext;
        }

        public async Task<APIResultObject<ResCommon>> SaveChiTieu(ChiTieuModel model, string userName)
        {
            var res = new APIResultObject<ResCommon>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", model.id);
                parameters.Add("@Category", CommonHelper.CheckStringNull(model.category));
                parameters.Add("@Amount", model.amount);
                parameters.Add("@PaymentMethod", CommonHelper.CheckStringNull(model.paymentMethod));
                parameters.Add("@CashbackAmount", model.cashbackAmount);
                parameters.Add("@Note", CommonHelper.CheckStringNull(model.note));
                parameters.Add("@CardId", model.cardId);
                parameters.Add("@TranType", model.tranType);
                parameters.Add("@UserName", CommonHelper.CheckStringNull(userName));

                using (var connection = adoContext.CreateConnection())
                {
                    var resultExecute = await connection.QueryAsync<ResCommon>("sp_SaveChiTieu", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExecute.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
                // Consider logging the exception for debugging
            }

            return res;
        }



        public async Task<APIResultObject<ChiTieuModel>> GetById(long id)
        {
            var res = new APIResultObject<ChiTieuModel>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                using (var connection = adoContext.CreateConnection())
                {
                    var resultExecute = await connection.QueryAsync<ChiTieuModel>("sp_GetChiTieuById", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExecute.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }

            return res;
        }


        public async Task<DataTableResultModel<ChiTieuGridSearchModel>> SearchChiTieu(ChiTieuFilterModel filter)
        {
            var res = new DataTableResultModel<ChiTieuGridSearchModel>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@PaymentMethod", CommonHelper.CheckStringNull(filter.paymentMethod));
                parameters.Add("@Category", CommonHelper.CheckStringNull(filter.category));
                parameters.Add("@FromDate", filter.fromDate);
                parameters.Add("@ToDate", filter.toDate);
                parameters.Add("@Keyword", CommonHelper.CheckStringNull(filter.keyword));
                parameters.Add("@TranType", CommonHelper.CheckStringNull(filter.tranType));
                parameters.Add("@Start", CommonHelper.CheckStringNull(filter.start));
                parameters.Add("@Length", CommonHelper.CheckStringNull(filter.length));
                parameters.Add(name: "@TotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);
                parameters.Add(name: "@TotalChiTieu", dbType: DbType.Decimal, direction: ParameterDirection.Output);
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExecute = await connection.QueryAsync<ChiTieuGridSearchModel>("sp_SearchChiTieu", parameters, commandType: CommandType.StoredProcedure);
                    res.recordsTotal = parameters.Get<long>("@TotalRow");
                    res.totalValue = parameters.Get<decimal>("@TotalChiTieu");
                    res.data = resultExecute.ToList();
                }
            }
            catch (Exception ex)
            {
                res.data = new List<ChiTieuGridSearchModel>();
                res.recordsTotal = 0;
            }

            return res;
        }



        public async Task<APIResultObject<ResCommon>> DeleteChiTieu(long id)
        {
            var res = new APIResultObject<ResCommon>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);

                using (var connection = adoContext.CreateConnection())
                {
                    var resultExecute = await connection.QueryAsync<ResCommon>("sp_DeleteChiTieuById", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExecute.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }

            return res;
        }






    }
}
