using GM_DAL.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GM_DAL.Models;
using GM_DAL.Models.User;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data;
using GM_DAL.APIModels;

namespace GM_DAL.Services
{
    public class UserInfoService: BaseService, IUserInfoService
    {
        private SQLAdoContext adoContext;
        public UserInfoService(SQLAdoContext adoContext)
        {
            this.adoContext = adoContext;
        }


        public async Task<APIResultObject<AuthenSuccessModel>> Login(string userName,string pass)
        {
            var res = new APIResultObject<AuthenSuccessModel>();
            try
            {
                

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", CommonHelper.CheckStringNull(userName));
                parameters.Add("@Pass", CommonHelper.CheckStringNull(pass));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<AuthenSuccessModel>("sp_Login", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.FirstOrDefault();
                }



            }
            catch(Exception ex)
            {
                res.message.exMessage = ex.Message;
            }

            return res;
        }
        public async Task<APIResultObject<ResCommon>> ChangePass(ChangePassModel model)
        {
            var res = new APIResultObject<ResCommon>();
            try
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", CommonHelper.CheckStringNull(model.userName));
                parameters.Add("@CurentPass", CommonHelper.CheckStringNull(model.currentPass));
                parameters.Add("@NewPass", CommonHelper.CheckStringNull(model.newPass));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<ResCommon>("sp_ChangePass", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.FirstOrDefault();
                }



            }
            catch (Exception ex)
            {
                res.code = Enum.ResultCode.ErrorException;
                res.message.exMessage = ex.Message;
            }

            return res;
        }


        public async Task<APIResultObject<List<MenuModel>>> GetMenuByRole(string role)
        {
            var res = new APIResultObject<List<MenuModel>>();
            try
            {
               

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RoleCode", CommonHelper.CheckStringNull(role));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<MenuModel>("sp_GetMenuByRole", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }


            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }

            return res;
        }



        public async Task<APIResultObject<ResCommon>> SaveUserInfo(UserInfoModel model,string userName)
        {
            var res = new APIResultObject<ResCommon>();
            try
            {
                

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FullName", CommonHelper.CheckStringNull(model.fullName));
                parameters.Add("@Phone", CommonHelper.CheckStringNull(model.phone));
                parameters.Add("@Email", CommonHelper.CheckStringNull(model.email));
                parameters.Add("@Pass", CommonHelper.CheckStringNull(model.password));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<ResCommon>("sp_UserRegisterAccount", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }


            return res;
        }



        public async Task<APIResultObject<UserInfoModel>> GetUserById(int id)
        {
            var res = new APIResultObject<UserInfoModel>();
            try
            {
                

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserId", CommonHelper.CheckIntNull(id));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<UserInfoModel>("sp_GetUserInfoById", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.FirstOrDefault();
                }



            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }


            return res;
        }

        public async Task<DataTableResultModel<UserInfoModel>> SearchUserAccount(SearchUserFilterModel filter)
        {
            var res = new DataTableResultModel<UserInfoModel>();
            try
            {
               


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@RoleCode", CommonHelper.CheckStringNull(filter.roleCode));
                parameters.Add("@Status", CommonHelper.CheckStringNull(filter.status));
                parameters.Add("@Keyword", CommonHelper.CheckStringNull(filter.keyword));
                parameters.Add("@Start", CommonHelper.CheckStringNull(filter.start));
                parameters.Add("@Length", CommonHelper.CheckStringNull(filter.length));
                parameters.Add(name: "@TotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

                using (var connection = adoContext.CreateConnection())
                {


                    var resultExcute = await connection.QueryAsync<UserInfoModel>("sp_SearchUserInfo", parameters, commandType: CommandType.StoredProcedure);
                    res.recordsTotal = parameters.Get<long>("TotalRow");
                    res.data = resultExcute.ToList();

                }

            }
            catch (Exception ex)
            {
                res.data = new List<UserInfoModel>();
            }


            return res;
        }


        public async Task<APIResultObject<ResCommon>> AddMyCard(AddMyCardModel model)
        {
            var res = new APIResultObject<ResCommon>();
            try
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", CommonHelper.CheckStringNull(model.userName));
                parameters.Add("@CardId", CommonHelper.CheckLongNull(model.cardId));

                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<ResCommon>("sp_AddMyCard", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }


            return res;
        }

        public async Task<APIResultObject<ResCommon>> DeleteMyCard(AddMyCardModel model)
        {
            var res = new APIResultObject<ResCommon>();
            try
            {


                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserName", CommonHelper.CheckStringNull(model.userName));
                parameters.Add("@CardId", CommonHelper.CheckLongNull(model.cardId));

                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<ResCommon>("sp_DeleteMyCard", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                res.message.exMessage = ex.Message;
            }


            return res;
        }

        public async Task<APIResultObject<List<CardInfoModel>>> GetListMyCard(string userName)
        {
            var res = new APIResultObject<List<CardInfoModel>>();
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@UserLogin", CommonHelper.CheckStringNull(userName));
                using (var connection = adoContext.CreateConnection())
                {
                    var resultExcute = await connection.QueryAsync<CardInfoModel>("sp_GetListMyCard", parameters, commandType: CommandType.StoredProcedure);
                    res.data = resultExcute.ToList();
                }
            }
            catch (Exception ex)
            {
                res.code = Enum.ResultCode.ErrorException;
                res.message.message = "error";
                res.message.exMessage = ex.Message;
                res.data = new List<CardInfoModel>();
            }

            return res;
        }



    }
}
