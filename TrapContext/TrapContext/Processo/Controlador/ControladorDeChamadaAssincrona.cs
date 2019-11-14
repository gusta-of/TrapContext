using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrapContext.Processo.Controlador
{
    public class ControladorDeChamadaAssincrona
    {
        public ControladorDeChamadaAssincrona()
        {
        }

        public void Execute(Type typeOf)
        {
            Execute(typeOf, "Executa", null);
        }

        private void Execute(Type processador, string nomeDoMetodo, params object[] parametros)
        {
            Execute(() =>
            {
                var classe = Activator.CreateInstance(processador);
                return processador.InvokeMember(nomeDoMetodo, BindingFlags.InvokeMethod, null, classe, parametros) as byte[];
            });
        }

        private void Execute(Func<byte[]> acaoExecucao)
        {
            acaoExecucao.Invoke();
        }
    }
}
