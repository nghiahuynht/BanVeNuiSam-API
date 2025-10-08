using GM_DAL.IServices;
using GM_DAL.Models.User;
using GM_DAL.Models;
using GM_DAL.Services;
using Microsoft.AspNetCore.Mvc;
using GM_DAL.Models.TicketOrder;
using System.Drawing;
using Newtonsoft.Json;
using QRCoder;
using System.Drawing.Imaging;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace PlatFormAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class TicketOrderController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private ITicketOrderService _ticketOrder;
        public TicketOrderController(IConfiguration configuration, ITicketOrderService ticketOrder)
        {
            _configuration = configuration;
            _ticketOrder = ticketOrder;
        }



        [HttpGet]
        [ProducesResponseType(typeof(APIResultObject<List<TicketOrderSearchGridModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<TicketOrderSearchGridModel>>> SearchOrder(SearchFilterModel filter)
        {
            // string passEncr = EncrypMD5(pass);
            var res = await _ticketOrder.SearchOrder(filter);
            return res;
        }




        [HttpPost]
        [ProducesResponseType(typeof(APIResultObject<ResCommon>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<ResCommon>> SaleOrder([FromBody] PostOrderSaveModel model)
        {
            // string passEncr = EncrypMD5(pass);
            var res = await _ticketOrder.SaveOrderToData(model,model.UserName, model.GateName);
            
            if (res.data.value == 0)
            {
                res.code = GM_DAL.Enum.ResultCode.ErrorInputInvalid;
                res.message.message = res.message.message;
            }
            return res;
        }

        [HttpPost]
        [ProducesResponseType(typeof(APIResultObject<List<OrderSucessResponModel>>), StatusCodes.Status200OK)]
        public async Task<APIResultObject<List<OrderSucessResponModel>>> CreateOrder([FromBody] OrderTempModel model)
        {
            var res = new APIResultObject<List<OrderSucessResponModel>>();
            
            var lstStringOrderSuccess = new List<Int64>();
            if (model != null && model.items.Any())
            {
                foreach (var item in model.items)
                {
                    var order = new PostOrderSaveModel
                    {
                        TicketCode=item.ticketCode,
                        CustomerCode=model.customerCode,
                        CustomerName=model.customerName,
                        CustomerType=model.customerType,
                        ObjType="MacDinh",
                        Quanti=item.quanti,
                        Price=item.price,
                        GateName=model.location,
                        IsFree=false,
                        PrintType="InGop",
                        DiscountPercent=0,
                        DiscountValue=0,
                        PaymentType= model.paymentType,
                        CartLineId=0,
                        UserName=model.userName
                    };
                    var resCretea = await _ticketOrder.SaveOrderToData(order, model.userName, model.location);
                    if (resCretea.data.value > 0)
                    {
                        lstStringOrderSuccess.Add(Convert.ToInt64(resCretea.data.value));
                    }


                }

                if (lstStringOrderSuccess.Any())
                {
                    string orderIds = string.Join(",", lstStringOrderSuccess);
                    res = await _ticketOrder.GetOrderWaitingScreenMobile(orderIds);
                }



            }

            if (!res.data.Any())
            {
                res.data = new List<OrderSucessResponModel>();
                res.code = GM_DAL.Enum.ResultCode.ErrorInputInvalid;
                res.message.message = res.message.message;
            }
            return res;
        }


      


        private void CreateQRCode(long orderId)
        {

            string fileFullPath = "";
            var log = new StringBuilder();
            try
            {

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string qrImagesDirectory = Path.Combine(baseDirectory, "QRImages");
                QRCodeGenerator QrGenerator = new QRCodeGenerator();
                QRCodeData QrCodedata = QrGenerator.CreateQrCode(orderId.ToString(), QRCodeGenerator.ECCLevel.Q);
                BitmapByteQRCode qrCode = new BitmapByteQRCode(QrCodedata);
                var imageBytes = qrCode.GetGraphic(50);

                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    using (Bitmap qrBitmap = new Bitmap(ms))
                    {
                        // 4. Lưu Bitmap dưới định dạng JPEG (kiểm soát chất lượng)
                        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);

                        // Đặt chất lượng nén JPG (90L là chất lượng cao)
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 90L);
                        myEncoderParameters.Param[0] = myEncoderParameter;

                        fileFullPath = string.Format(qrImagesDirectory, orderId.ToString());
                        qrBitmap.Save(fileFullPath, jpgEncoder, myEncoderParameters);
                    }
                }

            }
            catch (Exception ex)
            {
                log.AppendLine($"[Exception] create qrcode: {ex}");
            }


        }



        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            // Lấy danh sách tất cả các bộ mã hóa (codecs) đang có sẵn
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            // Tìm kiếm bộ mã hóa có GUID khớp với định dạng yêu cầu (ví dụ: JPG)
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null; // Trả về null nếu không tìm thấy bộ mã hóa
        }


      

    }
}
