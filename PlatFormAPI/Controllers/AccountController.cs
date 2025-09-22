using Microsoft.AspNetCore.Mvc;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.User;
using System.Text;
using System.Security.Cryptography;
using GM_DAL.APIModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace PlatFormAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private IUserInfoService _userInfoService;
        private readonly IConfiguration _configuration;
        public AccountController(IUserInfoService userInfoService, IConfiguration configuration)
        {
            _userInfoService = userInfoService;
            _configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(typeof(APIResultObject<AuthenSuccessModel>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<AuthenSuccessModel>> Login(LoginModel model)
        {
           // string passEncr = EncrypMD5(pass);
            var res =await _userInfoService.Login(model.userName, model.password);
            if (res.data != null)
            {
                res.data.token= GenerateJwtToken(res.data.userName);
                res.data.expires = DateTime.Now.AddDays(360);
            }
            return res;
        }


        [HttpPost]
        [ProducesResponseType(typeof(APIResultObject<ResCommon>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<ResCommon>> ChangePss(ChangePassModel model)
        {
            // string passEncr = EncrypMD5(pass);
            var res = await _userInfoService.ChangePass(model);
            if (res.data.value == 0)
            {
                res.code = GM_DAL.Enum.ResultCode.ErrorInputInvalid;
                res.message.message = res.data.message;
            }
            return res;
        }


        [HttpPost]
        [ProducesResponseType(typeof(APIResultObject<ResCommon>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<ResCommon>> Register(UserInfoModel model)
        {
            // string passEncr = EncrypMD5(pass);
            var res = await _userInfoService.SaveUserInfo(model,"register");
            
            return res;
        }

        #region ============================== My card=========================


        [HttpPost]
        [ProducesResponseType(typeof(APIResultObject<ResCommon>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<ResCommon>> AddMyCard(AddMyCardModel model)
        {
            // string passEncr = EncrypMD5(pass);
            var res = await _userInfoService.AddMyCard(model);
            return res;
        }


        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(APIResultObject<ResCommon>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<ResCommon>> DeleteMyCard(AddMyCardModel model)
        {
            // string passEncr = EncrypMD5(pass);
            var res = await _userInfoService.DeleteMyCard(model);
            return res;
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(APIResultObject<List<CardInfoModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<CardInfoModel>>> GetListMyCard(string userName)
        {
            // string passEncr = EncrypMD5(pass);
            var res = await _userInfoService.GetListMyCard(userName);
            return res;
        }

        #endregion







        //private string EncrypMD5(string pass)
        //{
        //    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        //    md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(pass));
        //    byte[] result = md5.Hash;
        //    StringBuilder str = new StringBuilder();
        //    for (int i = 0; i < result.Length; i++)
        //    {
        //        str.Append(result[i].ToString("x2"));
        //    }
        //    return str.ToString();
        //}


        private string GenerateJwtToken(string username)
        {
            // Lấy khóa bí mật
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tạo claims
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username)
            };

            // Tạo token (không cần Issuer và Audience)
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(360),
                signingCredentials: creds
            );

            // Trả về token dưới dạng chuỗi
            return new JwtSecurityTokenHandler().WriteToken(token);
        }




    }
}

