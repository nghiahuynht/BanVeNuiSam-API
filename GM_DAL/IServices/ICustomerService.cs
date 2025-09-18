using GM_DAL.Models;
using GM_DAL.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.IServices
{
    public interface ICustomerService
    {
        Task<APIResultObject<List<CustomerModel>>> GetAllCustomer();
    }
}
