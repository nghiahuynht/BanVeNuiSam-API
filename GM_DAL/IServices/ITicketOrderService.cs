using GM_DAL.Enum;
using GM_DAL.Models;
using GM_DAL.Models.TicketOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.IServices
{
    public interface ITicketOrderService
    {
        Task<APIResultObject<ResCommon>> SaveOrderToData(PostOrderSaveModel model, string userName, string gateName);
    }
}
