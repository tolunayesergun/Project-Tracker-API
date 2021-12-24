using Microsoft.AspNetCore.Mvc;
using ProjectTracker_API.Controllers;
using System;
using System.Collections.Generic;
using Xunit;

namespace ProjectTracker.Test
{
    public class MessageFromBossTest
    {
        [Fact]
        public void Get_Message_Is_Valid()
        {
            var motivationList = new List<string> {
                { "Senin almaya cesaret edemediðin riskleri alanlar, senin yaþamak istediðin hayatý yaþarlar." },
                { "Yüzüstü yere serilseniz bile, hala ileriye doðru hareket ediyorsunuzdur."},
                {"Býrakma. Þimdi acý çek ve hayatýnýn geri kalanýný bir þampiyon olarak yaþa."},
                {"Hazýrlamayý baþaramazsanýz, baþarýsýzlýða hazýrlanýyorsunuz demektir!"},
                {"Eðer kendi yaþam planýný tasarlamazsan, baþkalarýnýn planýnda kendine yer bulursun. Peki onlarýn senin için ne planladýðýndan haberin var mý?"},
                {"Yüksek bir hedefe giderken, size karþý ola insanlarýn üstesinden gelmeniz gerekir!"},
                {"Baþka bir hedef belirlemek ve yeni rüyalarýný gerçekleþtirmek için asla çok geç deðil!"},
                {"Dünyada herkesin baþarýlý olabileceði bir iþ bulunur, önemli olan insanda bulunan yeteneklerin ortaya çýkarýlmasýdýr. Her insan ayrý bir dünyadýr, her dünya ise yeni bir yaþamdýr."},
                {"Ne istediðime karar verdim ve baþarana kadar asla pes etmeyeceðim."},
                {"Zaferin coþkusunu hissedebilmeniz için zorluklarý kabul edin."},
                {"Pes etmeyen kiþiyi asla yenemezsin."},
                {"Asla geri çekilme ve açýklama yapma. Bitir ve arkalarýna bakmadan gitmelerini saðla!"},
                {"Ya büyük oyna ya da hiç oynama. Doðru olan þu ki kaybedecek hiçbir þeyin yok!"},
                {"Baþarý yolunda karþýna birçok zorluk çýkabilir. Yenilsen bile pes etmemeyi öðrendiðin zaman kazandýn demektir."}
            };

            var request = new MessageFromBossController().Get();
            
            var okObjectResult = request as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var model = okObjectResult.Value as string;
            Assert.NotNull(model);

            bool isContain = false;
            if (motivationList.Contains(model)) isContain = true;

            Assert.True(isContain);
        }
    }
}
