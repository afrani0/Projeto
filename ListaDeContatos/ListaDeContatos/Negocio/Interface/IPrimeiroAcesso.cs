using ListaDeContatos.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Negocio.Interface
{
    public interface IPrimeiroAcessoNegocio
    {

        Task<RespostaNegocio> ConcluirPrimeiroAcesso(string id, string senhaAtual, string novaSenha, string confirmarNovaSenha);

    }
}
