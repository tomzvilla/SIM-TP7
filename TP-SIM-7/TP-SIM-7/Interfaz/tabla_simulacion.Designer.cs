
namespace TP_SIM_7.Interfaz
{
    partial class tabla_simulacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv_simulacion = new System.Windows.Forms.DataGridView();
            this.reloj = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.evento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rnd1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempo_entre_llegadas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prox_llegada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rnd2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.motivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rnd3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempo_atencion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempo_f_atencion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rnd4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempo_rep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempo_secado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempo_f_reparacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tiempo_restante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cola_clientes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cola_zapatos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estado_z1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cola_retirar_zap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porc_clientes_no_atendidos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.t_prom_rep = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.porc_t_reparando = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.t_prom_cola = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_clientes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_simulacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_simulacion
            // 
            this.dgv_simulacion.AllowUserToAddRows = false;
            this.dgv_simulacion.AllowUserToDeleteRows = false;
            this.dgv_simulacion.AllowUserToResizeRows = false;
            this.dgv_simulacion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgv_simulacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_simulacion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.reloj,
            this.evento,
            this.rnd1,
            this.tiempo_entre_llegadas,
            this.prox_llegada,
            this.rnd2,
            this.motivo,
            this.rnd3,
            this.tiempo_atencion,
            this.tiempo_f_atencion,
            this.rnd4,
            this.tiempo_rep,
            this.tiempo_secado,
            this.tiempo_f_reparacion,
            this.tiempo_restante,
            this.cola_clientes,
            this.cola_zapatos,
            this.estado_z1,
            this.cola_retirar_zap,
            this.porc_clientes_no_atendidos,
            this.t_prom_rep,
            this.porc_t_reparando,
            this.t_prom_cola});
            this.dgv_simulacion.Location = new System.Drawing.Point(12, 12);
            this.dgv_simulacion.Name = "dgv_simulacion";
            this.dgv_simulacion.Size = new System.Drawing.Size(1137, 637);
            this.dgv_simulacion.TabIndex = 0;
            // 
            // reloj
            // 
            this.reloj.HeaderText = "Reloj (min)";
            this.reloj.Name = "reloj";
            this.reloj.Width = 75;
            // 
            // evento
            // 
            this.evento.HeaderText = "Evento";
            this.evento.Name = "evento";
            this.evento.Width = 66;
            // 
            // rnd1
            // 
            this.rnd1.HeaderText = "RND";
            this.rnd1.Name = "rnd1";
            this.rnd1.Width = 56;
            // 
            // tiempo_entre_llegadas
            // 
            this.tiempo_entre_llegadas.HeaderText = "Tiempo Entre Llegadas";
            this.tiempo_entre_llegadas.Name = "tiempo_entre_llegadas";
            this.tiempo_entre_llegadas.Width = 129;
            // 
            // prox_llegada
            // 
            this.prox_llegada.HeaderText = "Proxima Llegada";
            this.prox_llegada.Name = "prox_llegada";
            this.prox_llegada.Width = 101;
            // 
            // rnd2
            // 
            this.rnd2.HeaderText = "RND";
            this.rnd2.Name = "rnd2";
            this.rnd2.Width = 56;
            // 
            // motivo
            // 
            this.motivo.HeaderText = "Motivo Cliente";
            this.motivo.Name = "motivo";
            this.motivo.Width = 91;
            // 
            // rnd3
            // 
            this.rnd3.HeaderText = "RND";
            this.rnd3.Name = "rnd3";
            this.rnd3.Width = 56;
            // 
            // tiempo_atencion
            // 
            this.tiempo_atencion.HeaderText = "Tiempo Atencion";
            this.tiempo_atencion.Name = "tiempo_atencion";
            this.tiempo_atencion.Width = 103;
            // 
            // tiempo_f_atencion
            // 
            this.tiempo_f_atencion.HeaderText = "T Fin Atencion";
            this.tiempo_f_atencion.Name = "tiempo_f_atencion";
            this.tiempo_f_atencion.Width = 93;
            // 
            // rnd4
            // 
            this.rnd4.HeaderText = "RND";
            this.rnd4.Name = "rnd4";
            this.rnd4.Width = 56;
            // 
            // tiempo_rep
            // 
            this.tiempo_rep.HeaderText = "T Reparacion";
            this.tiempo_rep.Name = "tiempo_rep";
            this.tiempo_rep.Width = 89;
            // 
            // tiempo_secado
            // 
            this.tiempo_secado.HeaderText = "T Secado";
            this.tiempo_secado.Name = "tiempo_secado";
            this.tiempo_secado.Width = 73;
            // 
            // tiempo_f_reparacion
            // 
            this.tiempo_f_reparacion.HeaderText = "T Fin Reparacion";
            this.tiempo_f_reparacion.Name = "tiempo_f_reparacion";
            this.tiempo_f_reparacion.Width = 105;
            // 
            // tiempo_restante
            // 
            this.tiempo_restante.HeaderText = "T Restante";
            this.tiempo_restante.Name = "tiempo_restante";
            this.tiempo_restante.Width = 78;
            // 
            // cola_clientes
            // 
            this.cola_clientes.HeaderText = "Cola Clientes";
            this.cola_clientes.Name = "cola_clientes";
            this.cola_clientes.Width = 86;
            // 
            // cola_zapatos
            // 
            this.cola_zapatos.HeaderText = "Cola Zapatos";
            this.cola_zapatos.Name = "cola_zapatos";
            this.cola_zapatos.Width = 87;
            // 
            // estado_z1
            // 
            this.estado_z1.HeaderText = "Estado Z1";
            this.estado_z1.Name = "estado_z1";
            this.estado_z1.Width = 75;
            // 
            // cola_retirar_zap
            // 
            this.cola_retirar_zap.HeaderText = "Zapatos a Retirar";
            this.cola_retirar_zap.Name = "cola_retirar_zap";
            this.cola_retirar_zap.Width = 77;
            // 
            // porc_clientes_no_atendidos
            // 
            this.porc_clientes_no_atendidos.HeaderText = "% Clientes No Atendidos";
            this.porc_clientes_no_atendidos.Name = "porc_clientes_no_atendidos";
            this.porc_clientes_no_atendidos.Width = 134;
            // 
            // t_prom_rep
            // 
            this.t_prom_rep.HeaderText = "T Prom Reparacion";
            this.t_prom_rep.Name = "t_prom_rep";
            this.t_prom_rep.Width = 114;
            // 
            // porc_t_reparando
            // 
            this.porc_t_reparando.HeaderText = "% T Reparando";
            this.porc_t_reparando.Name = "porc_t_reparando";
            this.porc_t_reparando.Width = 97;
            // 
            // t_prom_cola
            // 
            this.t_prom_cola.HeaderText = "T Prom Zapatos en Cola";
            this.t_prom_cola.Name = "t_prom_cola";
            this.t_prom_cola.Width = 115;
            // 
            // dgv_clientes
            // 
            this.dgv_clientes.AllowUserToAddRows = false;
            this.dgv_clientes.AllowUserToDeleteRows = false;
            this.dgv_clientes.AllowUserToResizeRows = false;
            this.dgv_clientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clientes.Location = new System.Drawing.Point(1168, 50);
            this.dgv_clientes.Name = "dgv_clientes";
            this.dgv_clientes.Size = new System.Drawing.Size(604, 599);
            this.dgv_clientes.TabIndex = 5;
            // 
            // tabla_simulacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1784, 661);
            this.Controls.Add(this.dgv_clientes);
            this.Controls.Add(this.dgv_simulacion);
            this.Name = "tabla_simulacion";
            this.Text = "tabla_simulacion";
            this.Load += new System.EventHandler(this.tabla_simulacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_simulacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clientes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_simulacion;
        private System.Windows.Forms.DataGridView dgv_clientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn reloj;
        private System.Windows.Forms.DataGridViewTextBoxColumn evento;
        private System.Windows.Forms.DataGridViewTextBoxColumn rnd1;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempo_entre_llegadas;
        private System.Windows.Forms.DataGridViewTextBoxColumn prox_llegada;
        private System.Windows.Forms.DataGridViewTextBoxColumn rnd2;
        private System.Windows.Forms.DataGridViewTextBoxColumn motivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn rnd3;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempo_atencion;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempo_f_atencion;
        private System.Windows.Forms.DataGridViewTextBoxColumn rnd4;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempo_rep;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempo_secado;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempo_f_reparacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn tiempo_restante;
        private System.Windows.Forms.DataGridViewTextBoxColumn cola_clientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn cola_zapatos;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado_z1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cola_retirar_zap;
        private System.Windows.Forms.DataGridViewTextBoxColumn porc_clientes_no_atendidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn t_prom_rep;
        private System.Windows.Forms.DataGridViewTextBoxColumn porc_t_reparando;
        private System.Windows.Forms.DataGridViewTextBoxColumn t_prom_cola;
    }
}