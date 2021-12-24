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
                { "Senin almaya cesaret edemedi�in riskleri alanlar, senin ya�amak istedi�in hayat� ya�arlar." },
                { "Y�z�st� yere serilseniz bile, hala ileriye do�ru hareket ediyorsunuzdur."},
                {"B�rakma. �imdi ac� �ek ve hayat�n�n geri kalan�n� bir �ampiyon olarak ya�a."},
                {"Haz�rlamay� ba�aramazsan�z, ba�ar�s�zl��a haz�rlan�yorsunuz demektir!"},
                {"E�er kendi ya�am plan�n� tasarlamazsan, ba�kalar�n�n plan�nda kendine yer bulursun. Peki onlar�n senin i�in ne planlad���ndan haberin var m�?"},
                {"Y�ksek bir hedefe giderken, size kar�� ola insanlar�n �stesinden gelmeniz gerekir!"},
                {"Ba�ka bir hedef belirlemek ve yeni r�yalar�n� ger�ekle�tirmek i�in asla �ok ge� de�il!"},
                {"D�nyada herkesin ba�ar�l� olabilece�i bir i� bulunur, �nemli olan insanda bulunan yeteneklerin ortaya ��kar�lmas�d�r. Her insan ayr� bir d�nyad�r, her d�nya ise yeni bir ya�amd�r."},
                {"Ne istedi�ime karar verdim ve ba�arana kadar asla pes etmeyece�im."},
                {"Zaferin co�kusunu hissedebilmeniz i�in zorluklar� kabul edin."},
                {"Pes etmeyen ki�iyi asla yenemezsin."},
                {"Asla geri �ekilme ve a��klama yapma. Bitir ve arkalar�na bakmadan gitmelerini sa�la!"},
                {"Ya b�y�k oyna ya da hi� oynama. Do�ru olan �u ki kaybedecek hi�bir �eyin yok!"},
                {"Ba�ar� yolunda kar��na bir�ok zorluk ��kabilir. Yenilsen bile pes etmemeyi ��rendi�in zaman kazand�n demektir."}
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
