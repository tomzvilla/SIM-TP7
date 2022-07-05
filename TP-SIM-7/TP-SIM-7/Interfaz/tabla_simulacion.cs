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
using TP_SIM_7.Runge_Kutta;

namespace TP_SIM_7.Interfaz
{
    public partial class tabla_simulacion : Form
    {
        // Parámetros
        public double media_exp_llegada;
        public double atencion_a;
        public double atencion_b;
        public double rep_a; 
        public double rep_b; 
        public int num_iteraciones; 
        public int cant_zapatos_inicial; 
        public double hora_inicio;
        public double filas_a_mostrar;
        public double fila_desde;

        // Algunos datos para la tabla

        public int contador_id_zap = 1;
        public int contador_id_cli = 1;
        public int columas_a_agregar_AC = 0;
        public int columas_a_agregar_ZP = 0;
        public List<int> lista_ids_clientes = new List<int>();
        public List<int> lista_ids_zapatos = new List<int>();

        public bool calcularSecado = true;

        // Para RK 
        public string pathFile;
        public bool bandera = true;
        public double secado = 0;
        public tabla_simulacion(double _media_exp_llegada, double _atencion_a, double _atencion_b, double _rep_a, double _rep_b, int _num_iteraciones, int _cant_zapatos_inicial, double _hora_inicio, double _filas_a_mostrar, double _fila_desde)
        {
            InitializeComponent();
            media_exp_llegada = _media_exp_llegada;
            atencion_a = _atencion_a;
            atencion_b = _atencion_b;
            rep_a = _rep_a;
            rep_b = _rep_b;
            num_iteraciones = _num_iteraciones;
            cant_zapatos_inicial = _cant_zapatos_inicial;
            hora_inicio = _hora_inicio * 60;
            filas_a_mostrar = _filas_a_mostrar;
            fila_desde = _fila_desde;
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
            simular();
        }

        private void simular()
        {
            // algunos contadores
            int contadoresZapatos = 0;
            int contadoresClientes = 0;

            // Para las métricas

            int cant_clientes_atendidos = 0;
            int cant_clientes_no_atendidos = 0;

            double tiempo_reparacion = 0;
            int cant_zapatos_reparados = 0;

            double tiempo_zapatero_reparacion = 0;

            double tiempo_acum_espera = 0;
            int cant_zapatos_cola = 0;

            // Se crean los generadores

            var gen_semillas = new Random();
            var gen_llegada = new Random(gen_semillas.Next());
            var gen_motivo = new Random(gen_semillas.Next());
            var gen_atencion= new Random(gen_semillas.Next());
            var gen_reparacion= new Random(gen_semillas.Next());

            // Atributos de soporte

            var reparacion_suspendida = false;

            // se crea la fila inicial
            var fila_anterior = new VectorEstado();

            fila_anterior.reloj = this.hora_inicio;
            fila_anterior.evento = Eventos.inicializacion;
            var resultadosInicial = generarRNDExponencial(gen_llegada, this.media_exp_llegada);
            fila_anterior.rnd1 = resultadosInicial[0];
            fila_anterior.t_llegada = resultadosInicial[1];
            fila_anterior.t_prox_llegada = fila_anterior.reloj + fila_anterior.t_llegada;
            fila_anterior.rnd2 = 0;
            fila_anterior.rnd3 = 0;
            fila_anterior.t_atencion = 0;
            fila_anterior.t_fin_atencion = 0;
            fila_anterior.rnd4 = 0;
            fila_anterior.t_reparacion = 0;
            fila_anterior.t_secado = 0;
            fila_anterior.t_fin_reparacion = 0;
            fila_anterior.t_restante_reparacion = 0;
            fila_anterior.cola_clientes = new List<Cliente>();
            fila_anterior.cola_zapatos_reparacion = new List<Zapatos>();
            fila_anterior.cola_retirar_zap = new List<Zapatos>();
            fila_anterior.zapatero = new Servidor()
            {
                id = 1,
                estado = Estado.libre,
            };
            fila_anterior.zapatos = new List<Zapatos>();
            fila_anterior.clientes = new List<Cliente>();
            fila_anterior.porc_clientes_no_atendidos = 0;
            fila_anterior.t_prom_fin_reparacion = 0;
            fila_anterior.t_prom_zap_cola = 0;

            for (int i = 0; i < this.cant_zapatos_inicial; i++)
            {
                var obj = new Zapatos();
                obj.id = contador_id_zap;
                obj.estado = Estado.esperando_retiro;
                fila_anterior.zapatos.Add(obj);
                fila_anterior.cola_retirar_zap.Add(obj);
                this.contador_id_zap++;


            };
            contadoresZapatos = imprimirFila(fila_anterior, contadoresZapatos);
            // Se crea la siguiente fila 
            var fila_actual = new VectorEstado();

            // Comienza a simular

            for (int i = 0; i < this.num_iteraciones; i++)
            {
                var prox_reloj = calcularProximoReloj(fila_anterior.t_prox_llegada, fila_anterior.t_fin_atencion, fila_anterior.t_fin_reparacion);
                fila_actual.reloj = prox_reloj[0];
                fila_actual.evento = (Eventos)prox_reloj[1];

                // Se arrastran los valores para modificarlos
                fila_actual.t_prox_llegada = fila_anterior.t_prox_llegada;
                fila_actual.t_fin_atencion = fila_anterior.t_fin_atencion;
                fila_actual.t_fin_reparacion = fila_anterior.t_fin_reparacion;
                fila_actual.t_restante_reparacion = fila_anterior.t_restante_reparacion;
                fila_actual.cola_clientes = fila_anterior.cola_clientes;
                fila_actual.cola_retirar_zap = fila_anterior.cola_retirar_zap;
                fila_actual.cola_zapatos_reparacion = fila_anterior.cola_zapatos_reparacion;
                fila_actual.zapatero = fila_anterior.zapatero;
                fila_actual.zapatos = fila_anterior.zapatos;
                fila_actual.clientes = fila_anterior.clientes;


                if (fila_actual.evento == Eventos.llegada_cliente)
                {
                    fila_actual.motivo_cliente = Motivo.vacio;
                    fila_actual.rnd2 = 0;
                    // Se crea el cliente que llega
                    var cliente = new Cliente
                    {
                        id = contador_id_cli,
                        servidor_atencion = null,
                    };
                    contador_id_cli++;
                    cant_clientes_atendidos++;

                    // Se calcula la proxima llegada, solo si la zapateria esta abierta

                    //var valor = Convert.ToString((double)fila_anterior.reloj / 1440).Split('.');
                    var valor = fila_anterior.reloj/1440 - Math.Truncate(fila_anterior.reloj / 1440);
                    var horaDia = Math.Round(valor * 24,2) ;
                    if (horaDia >= 8 && horaDia <= 16)
                    {
                        var prox_llegada = generarRNDExponencial(gen_llegada, this.media_exp_llegada);
                        fila_actual.rnd1 = prox_llegada[0];
                        fila_actual.t_llegada = prox_llegada[1];
                        fila_actual.t_prox_llegada = fila_actual.reloj + fila_actual.t_llegada;

                        fila_actual.clientes.Add(cliente);
                        // El cliente chequea si hay servidores libres
                        bool servidorEstaLibre = fila_anterior.zapatero.estado == Estado.libre ? true : false;
                        if (servidorEstaLibre || fila_anterior.zapatero.estado == Estado.reparando_zapato)
                        {

                            // Si el zapatero estaba reparando pasa esto

                            if (fila_anterior.zapatero.estado == Estado.reparando_zapato)
                            {
                                reparacion_suspendida = true;
                                var zapato_reparandose = obtenerZapato(fila_actual.zapatos);
                                zapato_reparandose.estado = Estado.reparacion_suspendida;
                                fila_actual.t_restante_reparacion = fila_anterior.t_fin_reparacion - fila_actual.reloj;
                                fila_actual.t_fin_reparacion = 0;
                                tiempo_zapatero_reparacion += fila_actual.reloj - fila_actual.zapatero.tiempo_inicio_reparacion;
                                fila_actual.zapatero.tiempo_inicio_reparacion = 0;
                            }

                            // Se cambia el estado del cliente
                            cliente.estado = Estado.siendo_atendido;
                            cliente.servidor_atencion = fila_actual.zapatero;

                            // Se cambia el estado del servidor

                            fila_actual.zapatero.estado = Estado.atendiendo_cliente;

                            // Se calcula el tiempo de atencion
                            var resultados = generarRNDUniforme(gen_atencion, atencion_a, atencion_b);
                            fila_actual.rnd3 = resultados[0];
                            fila_actual.t_atencion = resultados[1];
                            fila_actual.t_fin_atencion = fila_actual.reloj + fila_actual.t_atencion;
                        }
                        else
                        {
                            fila_actual.cola_clientes.Add(cliente);
                            cliente.estado = Estado.esperando_atencion;
                        }
                    }
                    else 
                    {
                        // El cliente llego fuera de hora, no puede atenderse
                        cliente.estado = Estado.destruido;
                        fila_actual.clientes.Add(cliente);

                        cant_clientes_no_atendidos++;
                        // Se calcula la proxima llegada del cliente al dia siguiente

                        var prox_llegada = generarRNDExponencial(gen_llegada, this.media_exp_llegada);
                        fila_actual.rnd1 = prox_llegada[0];
                        fila_actual.t_llegada = prox_llegada[1] + 960;
                        fila_actual.t_prox_llegada = fila_actual.reloj + fila_actual.t_llegada;



                    };
                    
                    
                    
  
                } 
                else if (fila_actual.evento == Eventos.fin_atencion_cliente)
                {
                    fila_actual.t_atencion = 0;
                    fila_actual.t_fin_atencion = 0;
                    fila_actual.rnd3 = 0;
                    var zapato_entregar = new Zapatos();
                    // Se calcula el motivo del cliente

                    fila_actual.rnd2 = gen_motivo.NextDouble();
                    fila_actual.motivo_cliente = fila_actual.rnd2 < 0.5 ? Motivo.entregar_pedido : Motivo.retirar_pedido;

                    // Si el motivo es entregar se crea el zapato, sino, se retira el zapato

                    if(fila_actual.motivo_cliente == Motivo.entregar_pedido)
                    {
                        zapato_entregar.id = contador_id_zap;
                        contador_id_zap++;
                        zapato_entregar.t_inicio_reparacion = fila_actual.reloj;
                        fila_actual.zapatos.Add(zapato_entregar);
                    }
                    else
                    {
                        if(fila_actual.cola_retirar_zap.Count != 0)
                        {
                            var zapato_retirar = fila_actual.cola_retirar_zap[0];
                            zapato_retirar.estado = Estado.destruido;
                            fila_actual.cola_retirar_zap.RemoveAt(0);
                        } 
                        
                    }

                    // Se destruye el cliente una vez atendido
                    var cliente_atendido = obtenerClienteAtendido(fila_actual.clientes);
                    cliente_atendido.servidor_atencion = null;
                    cliente_atendido.estado = Estado.destruido;

                    // Chequea si ya es hora de cierre

                    var valor = Convert.ToString((double)fila_anterior.reloj / 1440).Split('.')[0];
                    var horaDia = Math.Round(Convert.ToDouble(valor) * 24, 2);
                    if (horaDia < 8 || horaDia > 16)
                    {
                        // Se deben eliminar los clientes en cola
                        foreach(Cliente cliente in fila_actual.cola_clientes)
                        {
                            cliente.estado = Estado.destruido;
                            cliente.servidor_atencion = null;
                            cant_clientes_no_atendidos++;
                        }
                        fila_actual.cola_clientes = new List<Cliente>();
                        fila_actual.t_fin_atencion = 0;

                    }

                    // Chequea la cola

                    if (fila_actual.cola_clientes.Count != 0)
                    {
                        // Calcula el nuevo fin de atencion
                        var resultados = generarRNDUniforme(gen_atencion, atencion_a, atencion_b);
                        fila_actual.rnd3 = resultados[0];
                        fila_actual.t_atencion = resultados[1];
                        fila_actual.t_fin_atencion = fila_actual.reloj + fila_actual.t_atencion;

                        // Se cambia el estado del cliente a siendo atendido

                        var nuevo_cliente = fila_actual.cola_clientes[0];
                        nuevo_cliente.estado = Estado.siendo_atendido;
                        nuevo_cliente.servidor_atencion = fila_actual.zapatero;
                        fila_actual.cola_clientes.RemoveAt(0);
                        if (fila_actual.motivo_cliente == Motivo.entregar_pedido)
                        {
                            // Pone al nuevo zapato en la cola
                            zapato_entregar.estado = Estado.esperando_reparacion;
                            zapato_entregar.servidor_atencion = null;
                            zapato_entregar.t_inicio_reparacion = fila_actual.reloj;
                            zapato_entregar.t_inico_espera = fila_actual.reloj;
                            cant_zapatos_cola++;
                            fila_actual.cola_zapatos_reparacion.Add(zapato_entregar);
                            
                        }

                    }
                    else
                    {
                        // Si ya no quedan clientes por atender, sigue reparando zapatos
                        // Primero chequea si suspendio alguna reparacion
                        if (reparacion_suspendida)
                        {
                            var zapato_suspendido = obtenerZapatoSuspendido(fila_actual.zapatos);
                            zapato_suspendido.estado = Estado.siendo_reparado;
                            zapato_suspendido.servidor_atencion = fila_actual.zapatero;
                            fila_actual.t_fin_reparacion = fila_actual.reloj + fila_actual.t_restante_reparacion;
                            fila_actual.zapatero.estado = Estado.reparando_zapato;
                            fila_actual.zapatero.tiempo_inicio_reparacion = fila_actual.reloj;
                            if (fila_actual.motivo_cliente == Motivo.entregar_pedido)
                            {
                                // Pone al nuevo zapato en la cola
                                zapato_entregar.estado = Estado.esperando_reparacion;
                                zapato_entregar.servidor_atencion = null;
                                zapato_entregar.t_inicio_reparacion = fila_actual.reloj;
                                zapato_entregar.t_inico_espera = fila_actual.reloj;
                                cant_zapatos_cola++;
                                fila_actual.cola_zapatos_reparacion.Add(zapato_entregar);
                            }
                            fila_actual.t_restante_reparacion = 0;
                            reparacion_suspendida = false;
                        }
                        else
                        {
                            // Si no suspendio reparacion, chequea la cola de zapatos por reparar
                            if(fila_actual.cola_zapatos_reparacion.Count != 0)
                            {
                                // Se pone en la cola el zapato dejado por el cliente
                                if (fila_actual.motivo_cliente == Motivo.entregar_pedido)
                                {
                                    // Pone al nuevo zapato en la cola
                                    zapato_entregar.estado = Estado.esperando_reparacion;
                                    zapato_entregar.servidor_atencion = null;
                                    zapato_entregar.t_inicio_reparacion = fila_actual.reloj;
                                    zapato_entregar.t_inico_espera = fila_actual.reloj;
                                    cant_zapatos_cola++;
                                    fila_actual.cola_zapatos_reparacion.Add(zapato_entregar);
                                }

                                // Se toma el primer zapato de la cola y se lo repara

                                var zapato_a_reparar = fila_actual.cola_zapatos_reparacion[0];
                                zapato_a_reparar.estado = Estado.siendo_reparado;
                                zapato_a_reparar.servidor_atencion = fila_actual.zapatero;
                                if(zapato_a_reparar.t_inico_espera != 0)
                                    tiempo_acum_espera += fila_actual.reloj - zapato_a_reparar.t_inico_espera;
                                fila_actual.cola_zapatos_reparacion.RemoveAt(0);

                                // Cambia el estado del zapatero

                                fila_actual.zapatero.estado = Estado.reparando_zapato;
                                fila_actual.zapatero.tiempo_inicio_reparacion = fila_actual.reloj;

                                // Calcula el nuevo fin de reparacion

                                var resultadosRep = generarRNDUniforme(gen_reparacion, rep_a, rep_b);
                                fila_actual.rnd4 = resultadosRep[0];
                                fila_actual.t_reparacion = resultadosRep[1];
                                if (calcularSecado)
                                {
                                    calcularTiempoSecado(fila_actual.reloj, this.pathFile);
                                    calcularSecado = false;
                                }
                                fila_actual.t_secado = this.secado;
                                fila_actual.t_fin_reparacion = fila_actual.reloj + fila_actual.t_reparacion + secado;
                            }
                            // Si la cola esta vacia repara el zapato que recibio
                            else
                            {
                                if (fila_actual.motivo_cliente == Motivo.entregar_pedido)
                                {
                                    cant_zapatos_cola++;
                                    zapato_entregar.estado = Estado.siendo_reparado;
                                    zapato_entregar.servidor_atencion = fila_actual.zapatero;
                                    if(zapato_entregar.t_inico_espera !=0)
                                        tiempo_acum_espera += fila_actual.reloj - zapato_entregar.t_inico_espera;
                                    zapato_entregar.t_inicio_reparacion = fila_actual.reloj;
                                    // Cambia el estado del zapatero

                                    fila_actual.zapatero.estado = Estado.reparando_zapato;
                                    fila_actual.zapatero.tiempo_inicio_reparacion = fila_actual.reloj;

                                    // Calcula el nuevo fin de reparacion
                                    if (calcularSecado)
                                    {
                                        calcularTiempoSecado(fila_actual.reloj, this.pathFile);
                                        calcularSecado = false;
                                    }
                                    fila_actual.t_secado = this.secado;

                                    var resultadosRep = generarRNDUniforme(gen_reparacion, rep_a, rep_b);
                                    fila_actual.rnd4 = resultadosRep[0];
                                    fila_actual.t_reparacion = resultadosRep[1];
                                    fila_actual.t_fin_reparacion = fila_actual.reloj + fila_actual.t_reparacion + secado;
                                }
                                else
                                {
                                    fila_actual.zapatero.estado = Estado.libre;
                                }
                                
                            }
                        }
                    }
                }
                else if(fila_actual.evento == Eventos.fin_reparacion)
                {
                    fila_actual.motivo_cliente = Motivo.vacio;
                    fila_actual.rnd2 = 0;
                    fila_actual.t_reparacion = 0;
                    fila_actual.t_fin_reparacion = 0;
                    fila_actual.rnd4 = 0;
                    var zapato_reparado = obtenerZapato(fila_actual.zapatos);
                    zapato_reparado.estado = Estado.esperando_retiro;
                    zapato_reparado.servidor_atencion = null;

                    // Se calculan metricas

                    tiempo_reparacion += fila_actual.reloj - zapato_reparado.t_inicio_reparacion;
                    cant_zapatos_reparados++;

                    tiempo_zapatero_reparacion += fila_actual.reloj - fila_actual.zapatero.tiempo_inicio_reparacion;


                    fila_actual.cola_retirar_zap.Add(zapato_reparado);

                    if(fila_actual.cola_zapatos_reparacion.Count != 0)
                    {
                        var zapato_a_reparar = fila_actual.cola_zapatos_reparacion[0];
                        zapato_a_reparar.estado = Estado.siendo_reparado;
                        zapato_a_reparar.servidor_atencion = fila_actual.zapatero;
                        if (zapato_a_reparar.t_inico_espera != 0)
                            tiempo_acum_espera += fila_actual.reloj - zapato_a_reparar.t_inico_espera;
                        fila_actual.zapatero.tiempo_inicio_reparacion = fila_actual.reloj;
                        fila_actual.cola_zapatos_reparacion.RemoveAt(0);

                        // Calcula el nuevo fin de reparacion

                        if (calcularSecado)
                        {
                            calcularTiempoSecado(fila_actual.reloj, this.pathFile);
                            calcularSecado = false;
                        }
                        fila_actual.t_secado = this.secado;

                        var resultadosRep = generarRNDUniforme(gen_reparacion, rep_a, rep_b);
                        fila_actual.rnd4 = resultadosRep[0];
                        fila_actual.t_reparacion = resultadosRep[1];
                        fila_actual.t_fin_reparacion = fila_actual.reloj + fila_actual.t_reparacion + secado;

                    }
                    else
                    {
                        fila_actual.zapatero.estado = Estado.libre;
                    }
                }

                // Se calculan metricas

                // % de clientes no atendidos
                if(cant_clientes_atendidos != 0)
                    fila_actual.porc_clientes_no_atendidos = (double)cant_clientes_no_atendidos / cant_clientes_atendidos;

                var cant_dias = Math.Truncate(fila_actual.reloj / 1440);
                fila_actual.porc_tiempo_reparacion = ((double)tiempo_zapatero_reparacion / (fila_actual.reloj- hora_inicio - (960*cant_dias))) *100;
                // Tiempo promedio de reparación

                if (cant_zapatos_reparados != 0)
                    fila_actual.t_prom_fin_reparacion = tiempo_reparacion / cant_zapatos_reparados;

                // Tiempo promedio en cola de los zapatos
                if(cant_zapatos_cola != 0)
                    fila_actual.t_prom_zap_cola = tiempo_acum_espera / cant_zapatos_cola;

                //Se cambia el orden de las filas
                if ((i >= this.fila_desde - 1 && i < this.fila_desde + this.filas_a_mostrar - 1) || (i == this.num_iteraciones - 1 && this.num_iteraciones != this.fila_desde + this.filas_a_mostrar - 1))
                {
                    contadoresZapatos = imprimirFila(fila_actual, contadoresZapatos);
                    contadoresClientes = imprimirClientes(fila_actual, contadoresClientes);
                };
                fila_anterior = fila_actual;


            }




        }

        private int imprimirClientes(VectorEstado fila_actual, int contadoresClientes)
        {
            var cant_columnas = fila_actual.clientes.Count;
            var fila = new string[cant_columnas];
            int clientes_a_agregar = fila_actual.clientes.Count - contadoresClientes;

            for (int i = clientes_a_agregar; i > 0; i--)
            {
                if (fila_actual.clientes[fila_actual.clientes.Count - i].estado != Estado.destruido)
                {
                    dgv_clientes.Columns.Add($"Cliente{fila_actual.clientes[fila_actual.clientes.Count - i].id}", $"Cliente {fila_actual.clientes[fila_actual.clientes.Count - i].id}");
                    columas_a_agregar_AC++;
                    lista_ids_clientes.Add(fila_actual.clientes[fila_actual.clientes.Count - i].id);
                }
            }
            var contador = 0;
            for (int i = 0; i < columas_a_agregar_AC; i++)
            {
                fila[i] = fila_actual.clientes[lista_ids_clientes[contador] - 1].estado.ToString();
                contador++;

            }
            if(columas_a_agregar_AC >0)
                dgv_clientes.Rows.Add(fila);
            return fila_actual.clientes.Count;
        }

        private int imprimirFila(VectorEstado fila_actual, int contadoresZapatos)
        {
            var cant_columnas = 23 + fila_actual.zapatos.Count;
            var fila = new string[cant_columnas];
            fila[0] = fila_actual.reloj.ToString("0.00");
            fila[1] = fila_actual.evento.ToString();
            fila[2] = fila_actual.rnd1.ToString("0.00");
            fila[3] = fila_actual.t_llegada.ToString("0.00");
            fila[4] = fila_actual.t_prox_llegada.ToString("0.00");
            fila[5] = fila_actual.rnd2.ToString("0.00");
            fila[6] = fila_actual.motivo_cliente.ToString();
            fila[7] = fila_actual.rnd3.ToString("0.00");
            fila[8] = fila_actual.t_atencion.ToString("0.00");
            fila[9] = fila_actual.t_fin_atencion.ToString("0.00");
            fila[10] = fila_actual.rnd4.ToString("0.00");
            fila[11] = fila_actual.t_reparacion.ToString("0.00");
            fila[12] = fila_actual.t_secado.ToString("0.00");
            fila[13] = fila_actual.t_fin_reparacion.ToString("0.00");
            fila[14] = fila_actual.t_restante_reparacion.ToString("0.00");
            fila[15] = fila_actual.cola_clientes.Count.ToString();
            fila[16] = fila_actual.cola_zapatos_reparacion.Count.ToString();
            fila[17] = fila_actual.zapatero.estado.ToString();
            fila[18] = fila_actual.cola_retirar_zap.Count.ToString();
            fila[19] = "%" + fila_actual.porc_clientes_no_atendidos.ToString("0.00");
            fila[20] = fila_actual.t_prom_fin_reparacion.ToString("0.00");
            fila[21] = "%" + fila_actual.porc_tiempo_reparacion.ToString("0.00");
            fila[22] = fila_actual.t_prom_zap_cola.ToString("0.00");
            int zapatos_a_agregar = fila_actual.zapatos.Count - contadoresZapatos;

            int puntero = 23;
            var contador = 0;

            // Agregar zapatos

            for (int i = zapatos_a_agregar; i > 0; i--)
            {
                if (fila_actual.zapatos[fila_actual.zapatos.Count - i].estado != Estado.destruido)
                {
                    dgv_simulacion.Columns.Add($"Zapato{fila_actual.zapatos[fila_actual.zapatos.Count - i].id}", $"Zapato {fila_actual.zapatos[fila_actual.zapatos.Count - i].id}");
                    columas_a_agregar_ZP++;
                    lista_ids_zapatos.Add(fila_actual.zapatos[fila_actual.zapatos.Count - i].id);
                }
            }
            for (int i = puntero; i <= columas_a_agregar_ZP + puntero - 1; i++)
            {
                fila[i] = fila_actual.zapatos[lista_ids_zapatos[contador] - 1].estado.ToString();
                contador++;
            }

            dgv_simulacion.Rows.Add(fila);

            return fila_actual.zapatos.Count;
        }

        private void calcularTiempoSecado(double reloj, string pathFile)
        {
            if (this.bandera)
            {
                var rk = new tabla_rk(reloj, pathFile);
                this.secado = rk.valorBuscado;
                bandera = false;
            }
        }

        private Cliente obtenerClienteAtendido(List<Cliente> clientes)
        {
            Cliente cliente = new Cliente();
            for (int i = 0; i < clientes.Count; i++)
            {
                if (clientes[i].servidor_atencion != null && clientes[i].servidor_atencion.id == 1)
                {
                    cliente = clientes[i];
                }
            }
            return cliente;
        }
        

        private Zapatos obtenerZapato(List<Zapatos> zapatos)
        {
            Zapatos zapato = new Zapatos();
            for (int i = 0; i < zapatos.Count; i++)
            {
                if (zapatos[i].servidor_atencion != null && zapatos[i].servidor_atencion.id == 1)
                {
                    zapato = zapatos[i];
                }
            }
            return zapato;
        }

        private Zapatos obtenerZapatoSuspendido(List<Zapatos> zapatos)
        {
            Zapatos zapato = new Zapatos();
            for (int i = 0; i < zapatos.Count; i++)
            {
                if (zapatos[i].servidor_atencion != null && zapatos[i].estado == Estado.reparacion_suspendida)
                {
                    zapato = zapatos[i];
                }
            }
            return zapato;
        }

        private List<double> calcularProximoReloj(double t_prox_llegada, double t_fin_atencion, double t_fin_reparacion)
        {
            var lista = new List<double>();
            double minimo = 1000000000000000000;
            int evento = 0;
            if (t_prox_llegada < minimo && t_prox_llegada != 0)
            {
                minimo = t_prox_llegada;
                evento = (int)Eventos.llegada_cliente;
            }
            if (t_fin_atencion < minimo && t_fin_atencion != 0)
            {
                minimo = t_fin_atencion;
                evento = (int)Eventos.fin_atencion_cliente;
            }
            if (t_fin_reparacion < minimo && t_fin_reparacion != 0)
            {
                minimo = t_fin_reparacion;
                evento = (int)Eventos.fin_reparacion;
            }
               

            lista.Add(minimo);
            lista.Add(evento);
            return lista;
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

        public List<double> generarRNDUniforme(Random generador, double a, double b)
        {
            var lista_resultado = new List<double>();
            var rnd = generador.NextDouble();
            lista_resultado.Add(rnd);
            double valor = a + rnd * (b - a);
            lista_resultado.Add(valor);
            return lista_resultado;
        }
    }
}
