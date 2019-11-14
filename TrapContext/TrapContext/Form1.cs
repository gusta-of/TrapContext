using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrapContext.Processo.Controlador;
using TrapContext.Processo.Negocio;
using TrapContext.Trap;

namespace TrapContext
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            var processo = new ProcessoPadrao();
            var boleano = processo.PodeUtilizarFormatoEspecifico();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            
            var controlador = new ControladorDeChamadaAssincrona();
            controlador.Execute(typeof(ProcessoPadrao));
        }
    }
}
