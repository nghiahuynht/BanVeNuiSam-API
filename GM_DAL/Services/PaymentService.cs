using Dapper;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.Payment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Services
{
    public class PaymentService:BaseService, IPaymentService
    {
        private SQLAdoContext adoContext;
        public PaymentService(SQLAdoContext adoContext)
        {
            this.adoContext = adoContext;
        }


        public async Task<APIResultObject<PaymentInfoModel>> GetPaymentInfoByOrderId(long orderId)
        {

            var res = new APIResultObject<PaymentInfoModel>();
            try
            {

                using (var connection = adoContext.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@OrderId", CommonHelper.CheckLongNull(orderId));
                    var resultExcute = await connection.QueryAsync<PaymentInfoModel>("sp_GetPaymentInfoByOrderId", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.FirstOrDefault();
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
