// 4. HAFTA

//ÖDEVLER (4. Hafta)

/* // 1. Soru

//Kullanıcıdan bölümümüzde okuyan her öğrencinin sınıf (1-4 arasında tamsayı) ve genel not ortalamasi 
//(0-100 arasında tamsayı) ile mezun olduğu lise adı verilerini alan, her öğrenciden sonra başka öğrenci olup olmadığıni
//(e/E/h/H karakterleri) soran; tüm veri girişleri bittikten sonra ise her liseden mezun olan bölümümüz öğrenci sayılarini
//ve bu öğrencilerin not ortalamalarını, ayrica bölümümüzdeki her sınıftaki öğrencilerin sayılarinı ve 10 puanlik genel not ortalaması 
//araliklarına göre dağılımlarinı (yüzdelerini) bulan ve aşağıdaki gibi ekrana yazdıran bir program yazınız.
*/


using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("*********************************** 1. Soru ****************************************\n\n");
        Dictionary<string, List<int>> liseBilgileri = new Dictionary<string, List<int>>();
        Dictionary<int, int> sinifSayilari = new Dictionary<int, int>();

        while (true)
        {
            Console.WriteLine("Öğrencinin bilgilerini giriniz...");
            Console.Write("Öğrencinin Sınıfını Giriniz (1-4 arasında tamsayı): ");
            int sinif = Convert.ToInt32(Console.ReadLine());

            Console.Write("Öğrencinin Genel Not Ortalamasını Giriniz (0-100 arasında tamsayı): ");
            int notOrtalamasi = Convert.ToInt32(Console.ReadLine());

            Console.Write("Öğrencinin Mezun Olduğu Lise Adını Giriniz: ");
            string liseAdi = Console.ReadLine();

            // Lise bilgilerini güncelle
            if (liseBilgileri.ContainsKey(liseAdi))
            {
                liseBilgileri[liseAdi].Add(notOrtalamasi);
            }
            else
            {
                liseBilgileri[liseAdi] = new List<int> { notOrtalamasi };
            }

            // Sınıf sayılarını güncelle
            if (sinifSayilari.ContainsKey(sinif))
            {
                sinifSayilari[sinif]++;
            }
            else
            {
                sinifSayilari[sinif] = 1;
            }

            Console.Write("Başka bir öğrenci eklemek istiyor musunuz? (e/E/h/H): ");
            char devam = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (devam != 'e' && devam != 'E')
            {
                break;
            }
        }
        Console.ReadLine();

        // Lise öğrenci sayılarını ve not ortalamalarını yazdır
        Console.WriteLine("\nLiselerdeki Mezun Öğrenci Bilgileri:");
        Console.WriteLine($"Lise Adı                             Öğrenci Sayısı                      Ortalama Not");
        foreach (var lise in liseBilgileri)
        {
            string liseAdi = lise.Key;
            List<int> notlar = lise.Value;
            int ogrenciSayisi = notlar.Count;
            double notOrtalamasi = OrtalamaHesapla(notlar);

            Console.WriteLine($"{liseAdi}                                         {ogrenciSayisi}                                 {notOrtalamasi:F2}");
        }


        // Not Ortalaması Dağılımını yazdır
        Console.WriteLine("\nNot Ortalaması Dağılımı:");
        foreach (var sinif in sinifSayilari.OrderBy(x => x.Key))
        {
            int sinifNo = sinif.Key;
            int ogrenciSayisiSinif = sinif.Value;

            Console.Write($"Sınıf {sinifNo} Not Dağılımı:");

            var notOrtalamalari = liseBilgileri.Values.SelectMany(notlar => notlar).Where(not => sinifBelirle(not) == sinifNo);
            var notOrtalamalariGruplu = notOrtalamalari.GroupBy(not => not / 10 * 10).OrderBy(grup => grup.Key);

            foreach (var grup in notOrtalamalariGruplu)
            {
                var aralik = grup.Key;
                var araliktakiNotlar = grup.ToList();
                var yuzde = (double)araliktakiNotlar.Count / notOrtalamalari.Count() * 100;

                Console.Write($" Not Aralığı {aralik}-{aralik + 9}: %{yuzde:F2}");
            }

            Console.WriteLine(); // Sınıfın sonunda bir satır atla
        }

        Console.ReadLine();
    }

    static double OrtalamaHesapla(List<int> notlar)
    {
        double toplam = 0;
        foreach (var not in notlar)
        {
            toplam += not;
        }
        return toplam / notlar.Count;
    }

    static int sinifBelirle(int not)
    {
        if (not >= 0 && not <= 25) return 1;
        if (not >= 26 && not <= 50) return 2;
        if (not >= 51 && not <= 75) return 3;
        if (not >= 76 && not <= 100) return 4;
        return -1; // Geçersiz not durumu
    }
}