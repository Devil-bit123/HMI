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
using Plugin.LocalNotifications;

namespace interfacesAPK
{
    public partial class MainPage : ContentPage
    {
        private string URL = "https://interfaceselec.azurewebsites.net/api/sensors";
        string temp;
        int tempe;
        private static readonly int NOTIFICATION_ID = 1000;
        private static readonly string CHANNEL_ID="location_notification";
        
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
                
                foreach (var l in lista)
                {
                    vlV.Text = l.ValorVoltaje.ToString();
                    vlT.Text = l.ValorTemperatura.ToString();
                    vlD.Text = l.ValorDistancia.ToString();
                    vlF.Text = l.Fecha.ToString();
                    double aux = Convert.ToDouble(vlT.Text);
                    if (aux > 25)
                    {
                        CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido "+aux+ " Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                    }


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
                    vlV.Text = l.ValorVoltaje.ToString();
                   vlT.Text = l.ValorTemperatura.ToString();
                    vlD.Text=l.ValorDistancia.ToString();
                    vlF.Text = l.Fecha.ToString();
                    double aux = Convert.ToDouble(vlT.Text);
                    if (aux > 25)
                    {
                        CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido "+aux+" Grados, encendiendo refirgeracion.", 0, DateTime.Now.AddSeconds(1));
                    }
                }
                refreshView.IsRefreshing = false;
                
                

            }
        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            

        }
    }
}

