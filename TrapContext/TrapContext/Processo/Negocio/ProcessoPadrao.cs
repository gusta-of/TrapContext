using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrapContext.Processo.Negocio
{
    public class ProcessoPadrao : ProcessoDeContextoDeInterceptacao
    {
        public virtual byte[] Executa()
        {
            MessageBox.Show("Padrão");

            return Executa("Padrão");
        }

        private byte[] Executa(string teste)
        {
            return new byte[] { 0, 1, 1 };
        }

        public virtual bool PodeUtilizarFormatoEspecifico()
        {
            return false;
        }

    }
}
