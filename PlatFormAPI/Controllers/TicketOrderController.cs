using GM_DAL.IServices;
using GM_DAL.Models.User;
using GM_DAL.Models;
using GM_DAL.Services;
using Microsoft.AspNetCore.Mvc;
using GM_DAL.Models.TicketOrder;

namespace PlatFormAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class TicketOrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private ITicketOrderService _ticketOrder;
        public TicketOrderController(IConfiguration configuration, ITicketOrderService ticketOrder)
        {
            _configuration = configuration;
            _ticketOrder = ticketOrder;
        }

        [HttpPost]
        [ProducesResponseType(typeof(APIResultObject<ResCommon>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<ResCommon>> SaleOrder([FromBody] PostOrderSaveModel model)
        {
            // string passEncr = EncrypMD5(pass);
            var res = await _ticketOrder.SaveOrderToData(model,model.UserName, model.GateName);
            
            if (res.data.value == 0)
            {
                res.code = GM_DAL.Enum.ResultCode.ErrorInputInvalid;
                res.message.message = res.message.message;
            }
            return res;
        }
    }
}
