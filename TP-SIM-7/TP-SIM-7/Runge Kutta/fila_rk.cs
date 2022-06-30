using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_SIM_7.Runge_Kutta
{
    public class fila_rk
    {
        public double x { get; set; }
        public double y { get; set; }
        public double dy_dx { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public double K2 { get; set; }
        public double c { get; set; }
        public double d { get; set; }
        public double K3 { get; set; }
        public double e { get; set; }
        public double f { get; set; }
        public double K4 { get; set; }
        public double xi1 { get; set; }
        public double yi1 { get; set; }
    }
}
