using Dapper;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.Customer;
using GM_DAL.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private SQLAdoContext adoContext;
        public CustomerService(SQLAdoContext adoContext)
        {
            this.adoContext = adoContext;
        }
        public async Task<APIResultObject<List<CustomerModel>>> GetAllCustomer()
        {
            var res = new APIResultObject<List<CustomerModel>>();
            try
            {

                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<CustomerModel>("sp_GetAllCustomer",  commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
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
