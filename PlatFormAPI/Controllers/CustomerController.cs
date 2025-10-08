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


        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(APIResultObject<List<ComboboxModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<ComboboxModel>>> GetListCustomerByType(string customerType)
        {
            var res = await _customerService.GetListCustomerByType(customerType);
            return res;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(APIResultObject<List<ComboboxModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<ComboboxModel>>> GetAllCustType()
        {
            var res = new APIResultObject<List<ComboboxModel>>();
            res.data = new List<ComboboxModel>
            {
                new ComboboxModel {value="CaNhan",text="Cá nhân"},
                new ComboboxModel {value="CongTy",text="Công ty/tổ chức"},
                new ComboboxModel {value="Khac",text="Khác"}
            };
            return res;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(APIResultObject<List<ComboboxModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<ComboboxModel>>> GetAllArea()
        {
            var res = new APIResultObject<List<ComboboxModel>>();
            res.data = new List<ComboboxModel>
            {
                new ComboboxModel {value="QuocGia-NuiSam",text="KDL Quốc gia Núi Sam"},
            };
            return res;
        }
        #endregion
    }
}
