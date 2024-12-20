using MauiAppDoviz.Model;

using System.Xml;
using System.Xml.Serialization;

namespace MauiAppDoviz;

public partial class TCMBPage : ContentPage
{
	public TCMBPage()
	{
		InitializeComponent();
	}

    private async void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {
		DateTime date = (sender as DatePicker).Date;
		var xmlData = await Services.loadXmlTCMBKurData(date);


        // XML belgesini yükleme
        if (string.IsNullOrEmpty(xmlData))
            return;

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xmlData);

        // Ana düðümü alma
        XmlNode root = doc.DocumentElement;

        // Tarih bilgisi
        string tarih = root.Attributes["Tarih"]?.InnerText;
        Console.WriteLine($"Tarih: {tarih}");

        // Döviz bilgilerini alma
        XmlNodeList currencyNodes = root.SelectNodes("Currency");

        List<TCMBAnlikKurBilgileri> list = new List<TCMBAnlikKurBilgileri>();
        foreach (XmlNode currency in currencyNodes)
        {
            list.Add(new TCMBAnlikKurBilgileri
            {
                Isim = currency["Isim"]?.InnerText,
                ForexBuying = currency["ForexBuying"]?.InnerText,
                ForexSelling = currency["ForexSelling"]?.InnerText
            });
        }

        listKurlar.ItemsSource = list;
    }
}