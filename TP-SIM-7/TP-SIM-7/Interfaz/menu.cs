using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TP_SIM_7.Interfaz;

namespace TP_SIM_7
{
    public partial class menu : Form
    {
        private bool flag = false;
        public menu()
        {
            InitializeComponent();
        }

        private void btn_cambiar_p_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                this.pn_prob.Visible = false;
                flag = false;
            }
            else
            {
                this.pn_prob.Visible = true;
                flag = true;
            }
        }

        private void btn_iniciar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                this.pn_main.Visible = false;
                iniciar();
            }
        }

        private void iniciar()
        {
            this.pn_simular.Visible = true;
        }

        private bool validar()
        {
            if(this.num_cli_a.Value > this.num_cli_b.Value || this.num_rep_a.Value > this.num_rep_b.Value)
            {
                MessageBox.Show("El valor de A no puede ser mayor que el de B", "Error", MessageBoxButtons.OK);
            }
            return true;
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.pn_simular.Visible = false;
            this.pn_main.Visible = true;
        }

        private void btn_simular_Click(object sender, EventArgs e)
        {
            if (fila_desde.Value + filas_a_mostrar.Value - 1 <= num_iteraciones.Value)
            {
                var tp7 = new tabla_simulacion((double)this.num_exp_neg.Value, (double)this.num_cli_a.Value, (double)this.num_cli_b.Value, (double)this.num_rep_a.Value, (double)this.num_rep_a.Value, (int)this.num_iteraciones.Value, (int)this.num_cant_zapatos.Value, (double)this.num_hora_inicio.Value); ;
                tp7.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("El numero de filas a mostrar tiene que ser menor a la cantidad de iteraciones", "Alerta", MessageBoxButtons.OK);
                
            }
        }
    }
}
