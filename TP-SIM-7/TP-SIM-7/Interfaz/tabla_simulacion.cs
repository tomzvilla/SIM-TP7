using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP_SIM_7.Clases;

namespace TP_SIM_7.Interfaz
{
    public partial class tabla_simulacion : Form
    {
        // Parámetros
        public double media_exp_llegada;
        public double llegada_a;
        public double llegada_b;
        public double rep_a; 
        public double rep_b; 
        public int num_iteraciones; 
        public int cant_zapatos_inicial; 
        public double hora_inicio;

        // Algunos datos para la tabla

        public List<Zapatos> lista_zapatos = new List<Zapatos>();

        // Para RK 
        public string pathFile;
        public tabla_simulacion(double _media_exp_llegada, double _llegada_a, double _llegada_b, double _rep_a, double _rep_b, int _num_iteraciones, int _cant_zapatos_inicial, double _hora_inicio)
        {
            InitializeComponent();
            media_exp_llegada = _media_exp_llegada;
            llegada_a = _llegada_a;
            llegada_b = _llegada_b;
            rep_a = _rep_a;
            rep_b = _rep_b;
            num_iteraciones = _num_iteraciones;
            cant_zapatos_inicial = _cant_zapatos_inicial;
            hora_inicio = _hora_inicio * 60;
        }
        private void borrarArchivos()
        {
            this.pathFile = AppDomain.CurrentDomain.BaseDirectory + "excel";
            Directory.CreateDirectory(this.pathFile);
            DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "excel");
            FileInfo[] files = di.GetFiles();
            if (files.Length != 0)
            {
                foreach (FileInfo file in files)
                {
                    file.Delete();
                }
            }
        }

        private void tabla_simulacion_Load(object sender, EventArgs e)
        {
            borrarArchivos();
            cargarZapatos();
            simular();
        }

        private void simular()
        {
            // Se crean los generadores

            var gen_llegada = new Random();

            // se crea la fila inicial
            VectorEstado fila_anterior = calcularFilaInicial(gen_llegada);
            
            // Se crea la siguiente fila 
            var fila_actual = new VectorEstado();

            // Comienza a simular

            for (int i = 0; i < this.num_iteraciones; i++)
            {

            }

        }

        private VectorEstado calcularFilaInicial(Random gen_llegada)
        {
            var fila = new VectorEstado();

            fila.reloj = this.hora_inicio;
            fila.evento = Eventos.inicializacion;
            var resultados = generarRNDExponencial(gen_llegada, this.media_exp_llegada);
            fila.rnd1 = resultados[0];
            fila.t_llegada = resultados[1];
            fila.t_prox_llegada = fila.reloj + fila.t_llegada;
            fila.rnd2 = 0;
            fila.motivo_cliente = "";
            fila.rnd3 = 0;
            fila.t_atencion = 0;
            fila.t_fin_atencion = 0;
            fila.rnd4 = 0;
            fila.t_reparacion = 0;
            fila.t_secado = 0;
            fila.t_fin_reparacion = 0;
            fila.t_restante_reparacion = 0;
            fila.cola_clientes = new List<Cliente>();
            fila.cola_zapatos = new List<Zapatos>();
            fila.zapatero = new Servidor() 
            {
                id = 1,
                estado = Estado.libre,
            };
            fila.zapatos = new List<Zapatos>();
            fila.clientes = new List<Cliente>();

            return fila;
        }

        private void cargarZapatos()
        {
            for(int i = 0; i < this.cant_zapatos_inicial;i++)
            {
                var obj = new Zapatos();
                obj.id = i;
                obj.estado = Estado.esperando_retiro;
                lista_zapatos.Add(obj);

            }
        }

        public List<double> generarRNDExponencial(Random generador, double m)
        {
            var lista_resultado = new List<double>();
            var rnd = generador.NextDouble();
            lista_resultado.Add(rnd);
            double valor = -(m) * Math.Log(1 - rnd);
            lista_resultado.Add(valor);
            return lista_resultado;

        }

        public double generarRNDUniforme(Random generador)
        {
            var rnd = generador.NextDouble();
            double valor = 0 + rnd * (1);
            return valor;
        }
    }
}
