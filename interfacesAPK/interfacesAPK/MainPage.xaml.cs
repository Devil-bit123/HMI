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
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration;
using Newtonsoft.Json.Linq;

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
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
               await DisplayAlert("ERROR!","No tiene acceso a  internet porfavor acceda con WIFI o datos moviles","OK");
                System.Environment.Exit(0);
            }
            else { 
            string contenido= await cliente.GetStringAsync(URL);
            IEnumerable<userModel> lista  = JsonConvert.DeserializeObject<IEnumerable<userModel>>(contenido);
                collection.ItemsSource= new ObservableCollection<userModel>(lista);
                base.OnAppearing();
            }
        }

        private async void RefreshView_Refreshing(object sender, EventArgs e)
        {

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("ERROR!", "No tiene acceso a  internet porfavor acceda con WIFI o datos moviles", "OK");
               

            }
            else {
                string contenido = await cliente.GetStringAsync(URL);
                IEnumerable<userModel> lista = JsonConvert.DeserializeObject<IEnumerable<userModel>>(contenido);
                collection.ItemsSource = new ObservableCollection<userModel>(lista);
                foreach(var l in lista)
                {
                   vlT.Text = l.ValorTemperatura.ToString();
                }
                refreshView.IsRefreshing = false;

            }
        }
            
        
    }
}

