using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_SIM_7.Clases
{
    public class VectorEstado
    {
        public double reloj { get; set; }
        public Eventos evento { get; set; }
        public double rnd1 { get; set; }
        public double t_llegada { get; set; }
        public double t_prox_llegada { get; set; }
        public double rnd2 { get; set; }
        public Motivo motivo_cliente { get; set; }
        public double rnd3 { get; set; }
        public double t_atencion { get; set; }
        public double t_fin_atencion { get; set; }
        public double rnd4 { get; set; }
        public double t_reparacion { get; set; }
        public double t_secado { get; set; }
        public double t_fin_reparacion { get; set; }
        public double t_restante_reparacion { get; set; }
        public List<Cliente> cola_clientes { get; set; }
        public List<Zapatos> cola_zapatos_reparacion { get; set; }
        public List<Zapatos> cola_retirar_zap { get; set; }
        public Servidor zapatero { get; set; }
        public List<Zapatos> zapatos { get; set; }
        public List<Cliente> clientes { get; set; }
        public double porc_clientes_no_atendidos { get; set; }
        public double t_prom_fin_reparacion { get; set; }
        public double porc_tiempo_reparacion { get; set; }
        public double t_prom_zap_cola { get; set; }

    }
}
