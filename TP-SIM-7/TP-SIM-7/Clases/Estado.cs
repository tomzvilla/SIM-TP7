using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_SIM_7.Clases
{
    public enum Estado
    {
        atendiendo_cliente = 0,
        reparando_zapato = 1,
        libre = 2,
        siendo_atendido = 3,
        esperando_atencion = 4,
        siendo_reparado = 5,
        esperando_reparacion = 6,
        esperando_retiro = 7,
        reparacion_suspendida = 8,
        destruido = 9
        

    }
}
