using GM_DAL.Models;
using GM_DAL.Models.ChiTieu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.IServices
{
    public interface IChiTieuService
    {
        Task<APIResultObject<ResCommon>> DeleteChiTieu(long id);
        Task<APIResultObject<ResCommon>> SaveChiTieu(ChiTieuModel model, string userName);
        Task<APIResultObject<ChiTieuModel>> GetById(long id);
        Task<DataTableResultModel<ChiTieuGridSearchModel>> SearchChiTieu(ChiTieuFilterModel filter);
    }
}
