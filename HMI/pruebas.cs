using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMI
{
    public class pruebas
    {
        DataClasses1DataContext db = new DataClasses1DataContext();

        public void registrar(sensores values)
        {
            db.sensores.InsertOnSubmit(values);
        }
    }
}
