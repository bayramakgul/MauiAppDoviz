using MauiAppDoviz.Model;

using System.Text.Json;

namespace MauiAppDoviz
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }


        private async void LoadDataClicked(object sender, EventArgs e)
        {
            string url = "https://hasanadiguzel.com.tr/api/kurgetir";
            string jsondata = await Services.LoadJsonKurData(url);


            var root = JsonSerializer.Deserialize<Root>(jsondata);

            listKurlar.ItemsSource = root.TCMB_AnlikKurBilgileri;

        }
    }

}
