using Dapper;
using GM_DAL.APIModels;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.HomPage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_DAL.Services
{
    public class BankService:BaseService, IBankService
    {

        private SQLAdoContext adoContext;
        public BankService(SQLAdoContext adoContext) 
        {
            this.adoContext = adoContext;
        }

        public async Task<APIResultObject<List<LinhVucItemModel>>> GetAllListVucHomePage()
        {
            var res = new APIResultObject<List<LinhVucItemModel>>();
            try
            {


                DynamicParameters parameters = new DynamicParameters();
                //parameters.Add("@RoleCode", CommonHelper.CheckStringNull(role));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<LinhVucItemModel>("sp_GetAllLinhVucMobileHomePage", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }


            }
            catch (Exception ex)
            {
                res.code = Enum.ResultCode.ErrorException;
                res.message.message = "error";
                res.message.exMessage = ex.Message;
            }

            return res;
        }


        public async Task<APIResultObject<List<CardInfoModel>>> GetCardByLinhVucMobile(int idlinhVuc)
        {
            var res = new APIResultObject<List<CardInfoModel>>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@LinhVuc", CommonHelper.CheckIntNull(idlinhVuc));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<CardInfoModel>("sp_GetCardByLinhVucMobile", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }
            }
            catch (Exception ex)
            {
                res.code = Enum.ResultCode.ErrorException;
                res.message.message = "error";
                res.message.exMessage = ex.Message;
            }

            return res;
        }


        public async Task<APIResultObject<List<CardInfoModel>>> GetCardByBankCode(string bankCode)
        {
            var res = new APIResultObject<List<CardInfoModel>>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BankCode", CommonHelper.CheckStringNull(bankCode));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<CardInfoModel>("sp_GetCardByBankCode", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }
            }
            catch (Exception ex)
            {
                res.code = Enum.ResultCode.ErrorException;
                res.message.message = "error";
                res.message.exMessage = ex.Message;
            }

            return res;
        }




        public async Task<APIResultObject<CardInfoModel>> GetCardInfoDetail(long cardId)
        {
            var res = new APIResultObject<CardInfoModel>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CardId", CommonHelper.CheckIntNull(cardId));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<CardInfoModel>("sp_GetCardById", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                res.code = Enum.ResultCode.ErrorException;
                res.message.message = "error";
                res.message.exMessage = ex.Message;
            }

            return res;
        }


        public async Task<APIResultObject<List<CardMCCModel>>> GetMCCByCard(string? cardCode)
        {
            var res = new APIResultObject<List<CardMCCModel>>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CardCode", CommonHelper.CheckStringNull(cardCode));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<CardMCCModel>("sp_GetMCCByCardCode", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }
            }
            catch (Exception ex)
            {
                res.code = Enum.ResultCode.ErrorException;
                res.message.message = "error";
                res.message.exMessage = ex.Message;
                res.data = new List<CardMCCModel>();
            }

            return res;
        }


        public async Task<APIResultObject<List<ComboboxModel>>> GetAllBank()
        {
            var res = new APIResultObject<List<ComboboxModel>>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<ComboboxModel>("sp_GetAllBank", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }
            }
            catch (Exception ex)
            {
                res.code = Enum.ResultCode.ErrorException;
                res.message.message = "error";
                res.message.exMessage = ex.Message;
                res.data = new List<ComboboxModel>();
            }

            return res;
        }







    }
}
