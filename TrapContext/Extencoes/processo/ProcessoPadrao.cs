﻿using TrapContext.Trap;

namespace Extencoes.processo
{
    [ProcessoExtentdido]
    public class ProcessoPadrao : TrapContext.Processo.Negocio.ProcessoPadrao
    {

        public override byte[] Executa()
        {
            return new byte[] { 0, 1, 1 };
        }
    }
}
