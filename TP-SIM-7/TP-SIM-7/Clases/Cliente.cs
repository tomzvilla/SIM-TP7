using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_SIM_7.Clases
{
    public class Cliente
    {
        public int id { get; set; }
        public Estado estado { get; set; }
        public Servidor servidor_atencion { get; set; }
    }
}
