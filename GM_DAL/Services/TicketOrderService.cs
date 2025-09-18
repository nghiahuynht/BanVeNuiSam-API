using Dapper;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.Ticket;
using GM_DAL.Models.TicketOrder;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Services
{
    public class TicketOrderService : BaseService, ITicketOrderService
    {
    
        private SQLAdoContext adoContext;
        public TicketOrderService(SQLAdoContext adoContext)
        {
            this.adoContext = adoContext;
        }

        public async Task<APIResultObject<ResCommon>> SaveOrderToData(PostOrderSaveModel model, string userName, string gateName)
        {
            var res = new APIResultObject<ResCommon>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", 0);
                parameters.Add("@CustomerCode", model.CustomerCode);
                parameters.Add("@CustomerName", string.Empty);
                parameters.Add("@CustomerType", model.CustomerType);
                parameters.Add("@TicketCode", model.TicketCode);
                parameters.Add("@Quanti", model.Quanti);
                parameters.Add("@Price", model.Price);
                parameters.Add("@UserName", userName);
                parameters.Add("@BienSoXe", model.BienSoXe);
                parameters.Add("@IsCopy", false);
                parameters.Add("@GateName", gateName);
                parameters.Add("@Objtype", model.ObjType);
                parameters.Add("@IsFree", model.IsFree);
                parameters.Add("@PrintType", model.PrintType);
                parameters.Add("@DiscountPercent", model.DiscountPercent);
                parameters.Add("@DiscountValue", Convert.ToDecimal(model.DiscountValue));
                parameters.Add("@TienKhachDua", model.TienKhachDua);
                parameters.Add("@PaymentType", model.PaymentType);

                // Output parameter
                parameters.Add("@OrderId", dbType: DbType.Int64, direction: ParameterDirection.Output);

                using (var connection = adoContext.CreateConnection())
                {
                    await connection.ExecuteAsync(
                        "sp_SaveOrderTicket",
                        parameters,
                        commandType: CommandType.StoredProcedure
                    );

                    // Lấy giá trị output parameter
                    res.data = new ResCommon(); // khởi tạo object
                    res.data.value = parameters.Get<long>("@OrderId");
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
