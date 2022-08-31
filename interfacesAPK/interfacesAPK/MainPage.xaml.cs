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
using Plugin.LocalNotification;

namespace interfacesAPK
{
    public partial class MainPage : ContentPage
    {
        private string URL = "https://interfaceselec.azurewebsites.net/api/sensors";
        int temp;
        HttpClient cliente = new HttpClient();
        public MainPage()
        {
            InitializeComponent();
            notificacion();
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
                
                foreach (var l in lista)
                {
                    vlT.Text = l.ValorTemperatura.ToString();
                   
                    try { 
                    
                    temp  = Convert.ToInt32(vlT.Text);
                    }
                    catch
                    {

                    }
                   
                }

                if (temp > 25)
                {
                    notificacion();
                }
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
              
                foreach (var l in lista)
                {
                   vlT.Text = l.ValorTemperatura.ToString();
                    
                    try
                    {

                        temp = Convert.ToInt32(vlT.Text);
                    }
                    catch
                    {

                    }
                }
                refreshView.IsRefreshing = false;
                
                if (temp > 25)
                {
                    notificacion();
                }

            }
        }
            
       private void notificacion()
        {
            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = "La temperatura a exedido el limite permitido! " + vlT.Text + "grados",
                Title = "ALERTA!",
                ReturningData = "La temperatura a exedido el limite permitido! " + vlT.Text + "grados",
                NotificationId = 1337,
                NotifyTime = DateTime.Now,

            };
            NotificationCenter.Current.Show(notification);
        }
    }
}

