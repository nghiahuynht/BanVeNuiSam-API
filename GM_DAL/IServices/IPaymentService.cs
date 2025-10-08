using GM_DAL.Models.Payment;
using GM_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.IServices
{
    public interface IPaymentService
    {
        Task<APIResultObject<PaymentInfoModel>> GetPaymentInfoByOrderId(long orderId);
    }
}
