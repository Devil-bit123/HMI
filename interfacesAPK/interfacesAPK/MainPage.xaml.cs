using interfacesAPK.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace interfacesAPK
{
    public partial class MainPage : ContentPage
    {
        private string URL = "https://interfaceselec.azurewebsites.net/api/sensors";

        HttpClient cliente = new HttpClient();
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            string contenido= await cliente.GetStringAsync(URL);
            IEnumerable<userModel> lista  = JsonConvert.DeserializeObject<IEnumerable<userModel>>(contenido);
            collection.ItemsSource= new ObservableCollection<userModel>(lista);
            base.OnAppearing();
        }

        private async void RefreshView_Refreshing(object sender, EventArgs e)
        {
            string contenido = await cliente.GetStringAsync(URL);
            IEnumerable<userModel> lista = JsonConvert.DeserializeObject<IEnumerable<userModel>>(contenido);
            collection.ItemsSource = new ObservableCollection<userModel>(lista);
            refreshView.IsRefreshing = false;

        }
    }
}
