using GM_DAL.APIModels;
using GM_DAL.Models;
using GM_DAL.Models.HomPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.IServices
{
    public interface IBankService
    {
        Task<APIResultObject<List<LinhVucItemModel>>> GetAllListVucHomePage();
        Task<APIResultObject<List<CardInfoModel>>> GetCardByLinhVucMobile(int idlinhVuc);
        Task<APIResultObject<List<CardInfoModel>>> GetCardByBankCode(string bankCode);
        Task<APIResultObject<CardInfoModel>> GetCardInfoDetail(long cardId);
        Task<APIResultObject<List<CardMCCModel>>> GetMCCByCard(string? cardCode);
        Task<APIResultObject<List<ComboboxModel>>> GetAllBank();
    }
}
