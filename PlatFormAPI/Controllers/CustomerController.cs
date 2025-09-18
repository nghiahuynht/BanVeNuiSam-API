using GM_DAL.IServices;
using GM_DAL.Models.Ticket;
using GM_DAL.Models;
using GM_DAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GM_DAL.Models.Customer;

namespace PlatFormAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CustomerController :  ControllerBase
    {
        private readonly IConfiguration _configuration;
        private ICustomerService _customerService;
        public CustomerController(IConfiguration configuration, ICustomerService customerService)
        {
            _configuration = configuration;
            _customerService = customerService;
        }
        #region Customer
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(APIResultObject<List<CustomerModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<CustomerModel>>> GetAllCustomer()
        {
            var res = await _customerService.GetAllCustomer();
            return res;
        }
        #endregion
    }
}
