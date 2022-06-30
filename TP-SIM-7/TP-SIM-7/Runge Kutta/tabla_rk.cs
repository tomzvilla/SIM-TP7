using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TP_SIM_7.Runge_Kutta
{
    public partial class tabla_rk : Form
    {
        SLDocument oSLDocument = new SLDocument();
        DataTable dt = new DataTable();
        public string pathFile;
        public string archivo;
        public double reloj;
        public double h;
        public double valorBuscado;
        public tabla_rk(double _reloj, string _pathFile)
        {
            InitializeComponent();
            añadirColumnas();
            pathFile = _pathFile;
            this.reloj = _reloj;
            archivo = pathFile + $"/excelSecadoCemento-" + reloj.ToString("0.00") + ".xlsx";
            this.h = 0.01;
            calcularRK();
        }

        private void calcularRK()
        {
            txt_iteracion.Text = this.reloj.ToString("0.00");

            var fila_anterior = new fila_rk();
            fila_anterior.xi1 = 0;
            fila_anterior.yi1 = 0;


            dt.Rows.Add(fila_anterior.x, fila_anterior.y, fila_anterior.dy_dx, fila_anterior.K2, fila_anterior.K3, fila_anterior.K4, fila_anterior.xi1, fila_anterior.yi1);


            var fila_actual = new fila_rk();
            while (fila_actual.y < 60)
            {
                fila_actual.x = fila_anterior.xi1;
                fila_actual.y = fila_anterior.yi1;
                fila_actual.dy_dx = (31 * fila_actual.y) + 5;

                fila_actual.a = fila_actual.x * (double)(this.h / 2);
                fila_actual.b = fila_actual.y + ((double)(this.h / 2) * fila_actual.dy_dx);
                fila_actual.K2 = (31 * fila_actual.b) + 5;

                fila_actual.c = fila_actual.x * (double)(this.h / 2);
                fila_actual.d = fila_actual.y + ((double)(this.h / 2) * fila_actual.K2);
                fila_actual.K3 = (31 * fila_actual.d) + 5;

                fila_actual.e = fila_actual.x + this.h;
                fila_actual.f = fila_actual.y + (this.h * fila_actual.K3);
                fila_actual.K4 = (31 * fila_actual.f) + 5;

                fila_actual.xi1 = fila_actual.x + this.h;
                fila_actual.yi1 = fila_actual.y + ((double)(this.h / 6) * (fila_actual.dy_dx + 2 * fila_actual.K2 + 2 * fila_actual.K3 + fila_actual.K4));


                dt.Rows.Add(fila_actual.x, fila_actual.y, fila_actual.dy_dx, fila_actual.K2, fila_actual.K3, fila_actual.K4, fila_actual.xi1, fila_actual.yi1);


                fila_anterior = fila_actual;
            }
            var t = fila_anterior.x;
            this.valorBuscado = t * 1;
            dt.Rows.Add();
            dt.Rows.Add();
            dt.Rows.Add(this.valorBuscado);
            oSLDocument.ImportDataTable(1, 1, dt, true);
            oSLDocument.SaveAs(archivo);
        }

        private void añadirColumnas()
        {
            dt.Columns.Add("Valor X", typeof(decimal));
            dt.Columns.Add("Valor Y", typeof(decimal));
            dt.Columns.Add("dY/dX (K1)", typeof(decimal));
            dt.Columns.Add("K2", typeof(decimal));
            dt.Columns.Add("K3", typeof(decimal));
            dt.Columns.Add("K4", typeof(decimal));
            dt.Columns.Add("Xi+1", typeof(decimal));
            dt.Columns.Add("Yi+1", typeof(decimal));
        }

        private void tabla_rk_Load(object sender, EventArgs e)
        {

        }
    }
}
