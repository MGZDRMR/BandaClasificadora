using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightProject
{
    public class sensors
    {
        public double contador { get; set; }
        public double distancia { get; set; }
        public double peque { get; set; }
        public double medi { get; set; }
        public double gde { get; set; }

        public string str_contador { get; set; }
        public string str_distancia { get; set; }
        public string str_peque { get; set; }
        public string str_medi { get; set; }
        public string str_gde { get; set; }

        public void obtiene()
        {
            try
            {
                this.contador = Convert.ToDouble(str_contador);
                this.distancia = Convert.ToDouble(str_distancia);
                this.peque = Convert.ToDouble(str_peque);
                this.medi = Convert.ToDouble(str_medi);
                this.medi = Convert.ToDouble(str_gde);
                //this.tama = Convert.ToChar(str_tama);
            }
            catch {
                System.Windows.Forms.MessageBox.Show("No se pudo convertir un string");
            
            }

        }
    }
}
