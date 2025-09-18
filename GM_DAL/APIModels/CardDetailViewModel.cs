using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.APIModels
{
    public class CardDetailViewModel
    {
        public CardDetailViewModel()
        {
            card = new CardInfoModel();
            mccList = new List<CardMCCModel>();
        }
        public CardInfoModel card { get; set; }
        public List<CardMCCModel> mccList { get; set; } 
    }
}
