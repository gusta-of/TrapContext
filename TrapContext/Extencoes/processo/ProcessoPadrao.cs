using TrapContext.Trap;

namespace Extencoes.processo
{
    //[ProcessoExtendido(10)]
    public class ProcessoPadrao : TrapContext.Processo.Negocio.ProcessoPadrao
    {

        public override byte[] Executa()
        {
            return new byte[] { 0, 1, 1 };
        }
    }
}
