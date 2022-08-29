using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace interfacesAPK.Models
{
    public class userModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange([CallerMemberName] string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }


        private int id;

		public int Id
		{
			get { return id; }
			set { id = value; OnPropertyChange(); }
		}

		private string valorVoltaje;

		public string ValorVoltaje
		{
			get { return valorVoltaje; }
			set { valorVoltaje = value; OnPropertyChange(); }
		}

		private string valorTemperatura;

		public string ValorTemperatura
		{
			get { return valorTemperatura; }
			set { valorTemperatura = value; OnPropertyChange(); }
		}

		private string valorDistancia;

		public string ValorDistancia
		{
			get { return valorDistancia; }
			set { valorDistancia = value; OnPropertyChange(); }
		}

		private string fecha;	


        public string Fecha
		{
			get { return fecha; }
			set { fecha = value; OnPropertyChange(); }
		}


	}
}
