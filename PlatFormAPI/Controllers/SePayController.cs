using GM_DAL;
using GM_DAL.IServices;
using GM_DAL.Models;
using GM_DAL.Models.Payment;
using GM_DAL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace PlatFormAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SePayController : ControllerBase
    {
        private IConfiguration _configuration;
        private IPaymentService _paymentService;
        private readonly IHttpClientService _httpClient;
        public SePayController(IPaymentService paymentService, IConfiguration configuration
            , IHttpClientService httpClient
            )
        {
            _paymentService = paymentService;
            _configuration = configuration;
            _httpClient = httpClient;
        }
        [HttpGet]
       // [Authorize]
        [ProducesResponseType(typeof(APIResultObject<List<ComboboxModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<string>> GetImageQrPayment(long orderId)
        {
            var res = new APIResultObject<string>();
            var paymentInfo = await _paymentService.GetPaymentInfoByOrderId(orderId);
            if (paymentInfo != null)
            {
                var apiKey = _configuration["SePayQRCodeSettings:ApiKey"];
                var clientId = _configuration["SePayQRCodeSettings:ClientId"];
                var sourceURL = _configuration["SePayQRCodeSettings:GenerateQRCodeSourceURL"];
                string madon = CommonHelper.GenMaWith000(orderId);

                string accBankNo = _configuration["BankAccountOwner:AccountNo"];
                string accBankName = _configuration["BankAccountOwner:AccountName"];
                string bankCode = _configuration["BankAccountOwner:AcqId"];
                string orderRefixQR = _configuration["BaseSettings:OrderRefixQR"];

                var qrModel = new SePayQRPaymentModel
                {
                    accountNo = accBankNo,
                    accountName = accBankName,
                    acqId = bankCode,
                    addInfo = string.Format("{0} DH{1}", orderRefixQR, madon),
                    amount = paymentInfo.data.total.ToString(),
                    template = "compact2",
                };

                string imageBase64 = GenerateQRCodePaymentSepay(qrModel, apiKey, clientId, sourceURL);
                res.data = imageBase64;

            }
            else
            {
                res.data = string.Empty;
                res.message.message = "generate QRcode failed";
            }

            return res;
        }



        private string GenerateQRCodePaymentSepay(SePayQRPaymentModel jsbody, string apiKey, string clientId, string urlSource)
        {


            var result = "NOQR";
            try
            {
                var headerAuthen = new Dictionary<string, string>();
                headerAuthen.Add("x-client-id", clientId);
                headerAuthen.Add("x-api-key", apiKey);
                var json = JsonConvert.SerializeObject(jsbody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var resultCallAPI = _httpClient.PostAsync<ResponQRCodePaymentModel>(urlSource, string.Empty, headerAuthen, jsbody).GetAwaiter().GetResult();
                if (resultCallAPI != null && resultCallAPI.code == "00")
                {
                    return resultCallAPI.data.qrDataURL;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            //return result;
        }






    }
}
