

using System.Windows.Forms;
using TrapContext.Trap;

namespace ExtentionManager.processo
{
    //[ProcessoExtendido(10)]
    public class ProcessoPadrao : TrapContext.Processo.Negocio.ProcessoPadrao
    {
        public override byte[] Executa()
        {
            MessageBox.Show("Metodo de Extenção!");
            return new byte[] { 0, 1, 1 };
        }
    }
}
