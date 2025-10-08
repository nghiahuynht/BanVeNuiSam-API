using GM_DAL.APIModels;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.Ticket;
using GM_DAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PlatFormAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TicketController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private ITicketService _ticketService;
        public TicketController (IConfiguration configuration,ITicketService ticketService)
        {
            _configuration = configuration;
            _ticketService = ticketService;
        }

        #region Ticket User
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(APIResultObject<List<TicketModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<TicketModel>>> GetTicketByUser(int userId)
        {
            // string passEncr = EncrypMD5(pass);
            var res = await _ticketService.GetTicketByUser(userId);
            return res;
        }
        #endregion
    }
}
