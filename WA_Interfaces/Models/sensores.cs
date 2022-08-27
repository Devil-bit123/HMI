using System.ComponentModel.DataAnnotations;

namespace WA_Interfaces.Models
{
    public class sensores
    {
        [Key]
        public int id { get; set; }
        public string valorVoltaje { get; set; }
        public string valorTemperatura { get; set; }
        public string valorDistancia { get; set; }
        public string Fecha { get; set; }

    }
}
