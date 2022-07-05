using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_SIM_7.Clases
{
    public class Zapatos
    {
        public int id { get; set; }
        public Estado estado { get; set; }
        public Servidor servidor_atencion { get; set; }
        public double t_inicio_reparacion { get; set; }
        public double t_inico_espera { get; set; }
    }
}
