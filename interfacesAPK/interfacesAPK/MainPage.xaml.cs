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
using Microcharts;
using SkiaSharp;
using System.Data;

namespace interfacesAPK
{
    public partial class MainPage : ContentPage
    {
        private string URL = "https://interfaceselec.azurewebsites.net/api/sensors";
        private float volt = 0;
        private float temp = 0;
        private float dist = 0;

        
        
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
                    if (l.ValorVoltaje == "" || l.ValorTemperatura == "" || l.ValorDistancia == "")
                    {
                        vlV.Text = "Esperando valores...";
                        vlT.Text = "Esperando valores...";
                        vlD.Text = "Esperando valores...";
                        vlF.Text = "Esperando valores...";
                    }
                    else
                    {
                        vlV.Text = l.ValorVoltaje.ToString();
                        vlT.Text = l.ValorTemperatura.ToString();
                        vlD.Text = l.ValorDistancia.ToString();
                        vlF.Text = l.Fecha.ToString();
                        volt = float.Parse(l.ValorVoltaje.ToString());
                        temp=  float.Parse(l.ValorTemperatura.ToString());
                        dist=  float.Parse(l.ValorDistancia.ToString());
                        double aux = Convert.ToDouble(vlT.Text);
                        if (aux == 20 || aux <= 30)
                        {
                            CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 20 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                        }
                        else if (aux == 40 || aux <= 50)
                        {
                            CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 40 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                        }
                        else if (aux == 60 || aux <= 70)
                        {
                            CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 60 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                        }
                        else if (aux == 80 || aux <= 90)
                        {
                            CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 80 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                        }

                        

                        ChartEntry[] entries = new ChartEntry[] {
                        
                            new ChartEntry(0)
                        {
                            Label="0%",
                            ValueLabel="0",
                            Color=SKColor.Parse("#FF1943"),
                        },

                                new ChartEntry(204)
                        {
                            Label="20%",
                            ValueLabel="204,8",
                            Color=SKColor.Parse("#FF1943"),
                        },

                            new ChartEntry(409)
                        {
                            Label="40%",
                            ValueLabel="409,6",
                            Color=SKColor.Parse("#FF1943"),
                        },

                                new ChartEntry(614)
                        {
                            Label="60%",
                            ValueLabel="614,4",
                            Color=SKColor.Parse("#FF1943"),
                        },

                        new ChartEntry(819)
                        {
                            Label="80%",
                            ValueLabel="819,2",
                            Color=SKColor.Parse("#FF1943"),
                        },
                          new ChartEntry(1024)
                        {
                            Label="100%",
                            ValueLabel="1024",
                            Color=SKColor.Parse("#FF1943"),
                        },
                        new ChartEntry(volt)
                        {
                            Label="Voltaje Analogo",
                            ValueLabel=l.ValorVoltaje.ToString(),
                            Color=SKColor.Parse("#FF1943"),
                        },
                        };

                        ChartEntry[] entries1 = new ChartEntry[] {
                              new ChartEntry(0)
                        {
                            Label="0%",
                            ValueLabel="0",
                            Color = SKColor.Parse("00BFFF"),
                        },
                                new ChartEntry(20)
                        {
                            Label="20%",
                            ValueLabel="20°",
                            Color = SKColor.Parse("00BFFF"),
                        },
                                  new ChartEntry(40)
                        {
                            Label="40%",
                            ValueLabel="40°",
                            Color = SKColor.Parse("00BFFF"),
                        },
                                    new ChartEntry(80)
                        {
                            Label="80%",
                            ValueLabel="80°",
                            Color = SKColor.Parse("00BFFF"),
                        },
                        new ChartEntry(100)
                        {
                            Label="100°",
                            ValueLabel="100°",
                            Color = SKColor.Parse("00BFFF"),
                        },
                          new ChartEntry(temp)
                        {
                            Label="Temperatura",
                            ValueLabel=l.ValorTemperatura.ToString(),
                            Color = SKColor.Parse("00BFFF"),
                        },
                        };
                                             
                        ChartEntry[] entries2 = new ChartEntry[] {

                           new ChartEntry(0)
                        {
                            Label="0cmm",
                            ValueLabel="0cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                             new ChartEntry(70)
                        {
                            Label="20%",
                            ValueLabel="70cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                               new ChartEntry(140)
                        {
                            Label="40%",
                            ValueLabel="70cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                                 new ChartEntry(210)
                        {
                            Label="60%",
                            ValueLabel="210cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                                   new ChartEntry(280)
                        {
                            Label="80%",
                            ValueLabel="280cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                                     new ChartEntry(100)
                        {
                            Label="100%",
                            ValueLabel="350cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                        new ChartEntry(dist)
                        {
                            Label="Distancia",
                            ValueLabel=l.ValorDistancia.ToString(),   
                            Color =  SKColor.Parse("#00CED1"),                              
                        },

                        };


                        chartViewBar1.Chart = new LineChart { Entries = entries, MaxValue=1024, LabelTextSize=26, PointSize=10, LineSize = 3,LabelOrientation=Orientation.Horizontal};                          
                        chartViewBar2.Chart = new LineChart { Entries = entries1, MaxValue =100, LabelTextSize = 26, PointSize = 10, LineSize = 3, LabelOrientation = Orientation.Horizontal };
                        chartViewBar3.Chart = new LineChart { Entries = entries2, MaxValue = 350, LabelTextSize = 26, PointSize = 10, LineSize=3, LabelOrientation = Orientation.Horizontal };
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
                    volt = float.Parse(l.ValorVoltaje.ToString());
                    temp = float.Parse(l.ValorTemperatura.ToString());
                    dist = float.Parse(l.ValorDistancia.ToString());
                    double aux = Convert.ToDouble(vlT.Text);
                    if (aux==20 || aux<=30)
                    {
                        CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 20 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                    }else if (aux == 40 || aux <= 50)
                    {
                        CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 40 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                    }else if(aux == 60 || aux <= 70)
                    {
                        CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 60 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                    }else if(aux == 80 || aux <= 90)
                    {
                        CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 80 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                    }

                    ChartEntry[] entries = new ChartEntry[] {

                            new ChartEntry(0)
                        {
                            Label="0%",
                            ValueLabel="0",
                            Color=SKColor.Parse("#FF1943"),
                        },

                                new ChartEntry(204)
                        {
                            Label="20%",
                            ValueLabel="204,8",
                            Color=SKColor.Parse("#FF1943"),
                        },

                            new ChartEntry(409)
                        {
                            Label="40%",
                            ValueLabel="409,6",
                            Color=SKColor.Parse("#FF1943"),
                        },

                                new ChartEntry(614)
                        {
                            Label="60%",
                            ValueLabel="614,4",
                            Color=SKColor.Parse("#FF1943"),
                        },

                        new ChartEntry(819)
                        {
                            Label="80%",
                            ValueLabel="819,2",
                            Color=SKColor.Parse("#FF1943"),
                        },
                          new ChartEntry(1024)
                        {
                            Label="100%",
                            ValueLabel="1024",
                            Color=SKColor.Parse("#FF1943"),
                        },
                        new ChartEntry(volt)
                        {
                            Label="Voltaje Analogo",
                            ValueLabel=l.ValorVoltaje.ToString(),
                            Color=SKColor.Parse("#FF1943"),
                        },
                        };

                    ChartEntry[] entries1 = new ChartEntry[] {
                              new ChartEntry(0)
                        {
                            Label="0%",
                            ValueLabel="0",
                            Color = SKColor.Parse("00BFFF"),
                        },
                                new ChartEntry(20)
                        {
                            Label="20%",
                            ValueLabel="20°",
                            Color = SKColor.Parse("00BFFF"),
                        },
                                  new ChartEntry(40)
                        {
                            Label="40%",
                            ValueLabel="40°",
                            Color = SKColor.Parse("00BFFF"),
                        },
                                    new ChartEntry(80)
                        {
                            Label="80%",
                            ValueLabel="80°",
                            Color = SKColor.Parse("00BFFF"),
                        },
                        new ChartEntry(100)
                        {
                            Label="100°",
                            ValueLabel="100°",
                            Color = SKColor.Parse("00BFFF"),
                        },
                          new ChartEntry(temp)
                        {
                            Label="Temperatura",
                            ValueLabel=l.ValorTemperatura.ToString(),
                            Color = SKColor.Parse("00BFFF"),
                        },
                        };

                    ChartEntry[] entries2 = new ChartEntry[] {

                           new ChartEntry(0)
                        {
                            Label="0cmm",
                            ValueLabel="0cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                             new ChartEntry(70)
                        {
                            Label="20%",
                            ValueLabel="70cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                               new ChartEntry(140)
                        {
                            Label="40%",
                            ValueLabel="70cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                                 new ChartEntry(210)
                        {
                            Label="60%",
                            ValueLabel="210cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                                   new ChartEntry(280)
                        {
                            Label="80%",
                            ValueLabel="280cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                                     new ChartEntry(100)
                        {
                            Label="100%",
                            ValueLabel="350cm",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                        new ChartEntry(dist)
                        {
                            Label="Distancia",
                            ValueLabel=l.ValorDistancia.ToString(),
                            Color =  SKColor.Parse("#00CED1"),
                        },

                        };


                    chartViewBar1.Chart = new LineChart { Entries = entries, MaxValue = 1024, LabelTextSize = 26, PointSize = 10, LineSize = 3, LabelOrientation = Orientation.Horizontal };
                    chartViewBar2.Chart = new LineChart { Entries = entries1, MaxValue = 100, LabelTextSize = 26, PointSize = 10, LineSize = 3, LabelOrientation = Orientation.Horizontal };
                    chartViewBar3.Chart = new LineChart { Entries = entries2, MaxValue = 350, LabelTextSize = 26, PointSize = 10, LineSize = 3, LabelOrientation = Orientation.Horizontal };
                }
                refreshView.IsRefreshing = false; 
                

            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await DisplayAlert("ERROR!", "No tiene acceso a  internet porfavor acceda con WIFI o datos moviles", "OK");
                System.Environment.Exit(0);
            }
            else
            {
                string contenido = await cliente.GetStringAsync(URL);
                IEnumerable<userModel> lista = JsonConvert.DeserializeObject<IEnumerable<userModel>>(contenido);
                collection.ItemsSource = new ObservableCollection<userModel>(lista);

                foreach (var l in lista)
                {
                    if (l.ValorVoltaje == "" || l.ValorTemperatura == "" || l.ValorDistancia == "")
                    {
                        vlV.Text = "Esperando valores...";
                        vlT.Text = "Esperando valores...";
                        vlD.Text = "Esperando valores...";
                        vlF.Text = "Esperando valores...";
                    }
                    else
                    {
                        vlV.Text = l.ValorVoltaje.ToString();
                        vlT.Text = l.ValorTemperatura.ToString();
                        vlD.Text = l.ValorDistancia.ToString();
                        vlF.Text = l.Fecha.ToString();
                        volt = float.Parse(l.ValorVoltaje.ToString());
                        temp = float.Parse(l.ValorTemperatura.ToString());
                        dist = float.Parse(l.ValorDistancia.ToString());
                        double aux = Convert.ToDouble(vlT.Text);
                        if (aux == 20 || aux <= 30)
                        {
                            CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 20 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                        }
                        else if (aux == 40 || aux <= 50)
                        {
                            CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 40 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                        }
                        else if (aux == 60 || aux <= 70)
                        {
                            CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 60 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                        }
                        else if (aux == 80 || aux <= 90)
                        {
                            CrossLocalNotifications.Current.Show("Alerta!", "La tempertatura se a excedido los 80 Grados, encendiendo refirgeracion.", 0, DateTime.Now);
                        }
                        ChartEntry[] entries = new ChartEntry[] {

                            new ChartEntry(0)
                        {
                            Label="0%",
                            ValueLabel="0",
                            Color=SKColor.Parse("#FF1943"),
                        },

                                new ChartEntry(204)
                        {
                            Label="20%",
                            ValueLabel="204,8",
                            Color=SKColor.Parse("#FF1943"),
                        },

                            new ChartEntry(409)
                        {
                            Label="40%",
                            ValueLabel="409,6",
                            Color=SKColor.Parse("#FF1943"),
                        },

                                new ChartEntry(614)
                        {
                            Label="60%",
                            ValueLabel="614,4",
                            Color=SKColor.Parse("#FF1943"),
                        },

                        new ChartEntry(819)
                        {
                            Label="80%",
                            ValueLabel="819,2",
                            Color=SKColor.Parse("#FF1943"),
                        },
                          new ChartEntry(1024)
                        {
                            Label="100%",
                            ValueLabel="1024",
                            Color=SKColor.Parse("#FF1943"),
                        },
                        new ChartEntry(volt)
                        {
                            Label="Voltaje Analogo",
                            ValueLabel=l.ValorVoltaje.ToString(),
                            Color=SKColor.Parse("#FF1943"),
                        },
                        };

                        ChartEntry[] entries1 = new ChartEntry[] {
                              new ChartEntry(0)
                        {
                            Label="0%",
                            ValueLabel="0",
                            Color = SKColor.Parse("00BFFF"),
                        },
                                new ChartEntry(20)
                        {
                            Label="20%",
                            ValueLabel="20",
                            Color = SKColor.Parse("00BFFF"),
                        },
                                  new ChartEntry(40)
                        {
                            Label="40%",
                            ValueLabel="40",
                            Color = SKColor.Parse("00BFFF"),
                        },
                                    new ChartEntry(80)
                        {
                            Label="80%",
                            ValueLabel="80",
                            Color = SKColor.Parse("00BFFF"),
                        },
                        new ChartEntry(100)
                        {
                            Label="100°",
                            ValueLabel="100",
                            Color = SKColor.Parse("00BFFF"),
                        },
                          new ChartEntry(temp)
                        {
                            Label="Temperatura",
                            ValueLabel=l.ValorTemperatura.ToString(),
                            Color = SKColor.Parse("00BFFF"),
                        },
                        };

                        ChartEntry[] entries2 = new ChartEntry[] {

                           new ChartEntry(0)
                        {
                            Label="0cmm",
                            ValueLabel="0",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                             new ChartEntry(70)
                        {
                            Label="20%",
                            ValueLabel="70",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                               new ChartEntry(140)
                        {
                            Label="40%",
                            ValueLabel="70",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                                 new ChartEntry(210)
                        {
                            Label="60%",
                            ValueLabel="210",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                                   new ChartEntry(280)
                        {
                            Label="80%",
                            ValueLabel="280",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                                     new ChartEntry(100)
                        {
                            Label="100%",
                            ValueLabel="350",
                            Color =  SKColor.Parse("#00CED1"),
                        },
                        new ChartEntry(dist)
                        {
                            Label="Distancia",
                            ValueLabel=l.ValorDistancia.ToString(),
                            Color =  SKColor.Parse("#00CED1"),
                        },

                        };

                        chartViewBar1.Chart = new LineChart { Entries = entries, MaxValue = 1024, LabelTextSize = 26, PointSize = 10, LineSize = 3, LabelOrientation = Orientation.Horizontal };                       
                        chartViewBar2.Chart = new LineChart { Entries = entries1, MaxValue = 100, LabelTextSize = 26, PointSize = 10, LineSize = 3, LabelOrientation = Orientation.Horizontal };
                        chartViewBar3.Chart = new LineChart { Entries = entries2, MaxValue = 350, LabelTextSize = 26, PointSize = 10, LineSize = 3, LabelOrientation = Orientation.Horizontal };
                    }

                }
                base.OnAppearing();
            }

        }

       
    }
}

