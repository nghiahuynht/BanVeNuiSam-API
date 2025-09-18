using Dapper;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.Ticket;
using GM_DAL.Models.User;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Services
{

    public class TicketService : BaseService, ITicketService
    {

        private SQLAdoContext adoContext;
        public TicketService(SQLAdoContext adoContext)
        {
            this.adoContext = adoContext;
        }

        public async Task<APIResultObject<List<TicketUserModel>>> GetTicketByUser(int UserId)
        {
            var res = new APIResultObject<List<TicketUserModel>>();
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", UserId);

           
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<TicketUserModel>("sp_GetTicketByUserId", parameters, commandType: CommandType.StoredProcedure);
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
