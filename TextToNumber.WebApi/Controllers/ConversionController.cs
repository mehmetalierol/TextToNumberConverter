using System.Web.Http;
using TextToNumber.Business;
using TextToNumber.WebApi.Models;

namespace TextToNumber.WebApi.Controllers
{
    public class ConversionController : ApiController
    {
        /// <summary>
        /// Örnek Request : {"UserText": "yüz bin lira kredi kullanmak istiyorum"}
        /// </summary>
        /// <param name="content">Çevrilmesi istenen metinsel ifadeler</param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel Post([FromBody]RequestModel content)
        {
            if (content == null)
            {
                return new ResponseModel { Output = "Geçerli bir metin gönderilmedi!" };
            }
            else
            {
                try
                {
                    var Converter = new TextConverter();
                    return new ResponseModel { Output = Converter.DoConvertion(content.UserText).Replace("  ", "") };
                }
                catch
                {
                    return new ResponseModel { Output = "Çevirme işlemi sırasında hata oluştu! Lütfen metninizi kontrol edin, ard ard aralarında başka karakter olmayan sayılar var ise aralarına , ekleyin. (Örn: on yirmi beş yüz gibi sayıları on, yirmi, şeklinde yazın. Rakam olarak girilen değerlerin ise metinsel ifadelerden birer boş bırakılarak yazılması gerekir. (Örn: 5milyon yerine 5 milyon yazılmalı)" };
                }
            }
        }
    }
}