using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace GAB
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public async void Handle_Clicked(object sender, EventArgs e)
        {
            try
            {
                loading.IsVisible = true;
                loading.IsRunning = true;

                var httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri("http://globalazurebootcamp19funcs.azurewebsites.net/");

                var response = await httpClient.GetAsync("api/GetMonkeys?filter=monkey&code=n2k6TqhA9H/jXKk4ndhnbad25Rrct/Zu5vak8kWT3lslfVS9UHTJ5A==");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var monkeys = JsonConvert.DeserializeObject<List<Monkey>>(data);

                    if (monkeys != null)
                    {
                        this.listView.ItemsSource = monkeys;
                    }
                    loading.IsVisible = false;
                    loading.IsRunning = false;
                }
                else
                {
                    loading.IsVisible = false;
                    loading.IsRunning = false;

                    await DisplayAlert(string.Empty, "Erro ao executar chamada", "OK");
                }
            }
            catch (Exception ex)
            {
                loading.IsVisible = false;
                loading.IsRunning = false;

                await DisplayAlert(string.Empty, $"Erro: {ex.Message}", "OK");
            }
            finally
            {
                loading.IsVisible = false;
                loading.IsRunning = false;
            }
        }

        public MainPage()
        {
            InitializeComponent();
        }
    }

    public class Monkey
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}
