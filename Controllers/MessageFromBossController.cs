using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectTracker_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageFromBossController : ControllerBase
    {
        [HttpGet]
        public  ActionResult Get()
        {
            var motivationList = new List<string> { 
                { "Senin almaya cesaret edemediğin riskleri alanlar, senin yaşamak istediğin hayatı yaşarlar." }, 
                { "Yüzüstü yere serilseniz bile, hala ileriye doğru hareket ediyorsunuzdur."},
                {"Bırakma. Şimdi acı çek ve hayatının geri kalanını bir şampiyon olarak yaşa."},
                {"Hazırlamayı başaramazsanız, başarısızlığa hazırlanıyorsunuz demektir!"},
                {"Eğer kendi yaşam planını tasarlamazsan, başkalarının planında kendine yer bulursun. Peki onların senin için ne planladığından haberin var mı?"},
                {"Yüksek bir hedefe giderken, size karşı ola insanların üstesinden gelmeniz gerekir!"},
                {"Başka bir hedef belirlemek ve yeni rüyalarını gerçekleştirmek için asla çok geç değil!"},
                {"Dünyada herkesin başarılı olabileceği bir iş bulunur, önemli olan insanda bulunan yeteneklerin ortaya çıkarılmasıdır. Her insan ayrı bir dünyadır, her dünya ise yeni bir yaşamdır."},
                {"Ne istediğime karar verdim ve başarana kadar asla pes etmeyeceğim."},
                {"Zaferin coşkusunu hissedebilmeniz için zorlukları kabul edin."},
                {"Pes etmeyen kişiyi asla yenemezsin."},
                {"Asla geri çekilme ve açıklama yapma. Bitir ve arkalarına bakmadan gitmelerini sağla!"},
                {"Ya büyük oyna ya da hiç oynama. Doğru olan şu ki kaybedecek hiçbir şeyin yok!"},
                {"Başarı yolunda karşına birçok zorluk çıkabilir. Yenilsen bile pes etmemeyi öğrendiğin zaman kazandın demektir."}
            };
            Random rnd = new Random();

            return Ok(motivationList[rnd.Next(motivationList.Count-1)]);
        }
    }
}
