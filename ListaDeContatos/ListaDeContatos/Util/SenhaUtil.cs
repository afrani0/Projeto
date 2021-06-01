using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaDeContatos.Util
{
    public class SenhaUtil
    {
        // Gera a senha conforme as definições
        public virtual string GeraSenhaAleatoria()
        {
            // Gera uma senha com 6 caracteres entre numeros e letras
            string letraMinuscula = "abcdefghjkmnpqrstuvwxyz";
            string letraMaiuscula = "abcdefghjkmnpqrstuvwxyz".ToUpper();
            string numero = "0123456789";
            string caracterEspecial = @"~!@#$%^&*():;[]{}<>,.?/\|";



            int tamanhoSenha = 6;

            string novaSenha = "";

            Random random = new Random();
            for (tamanhoSenha = 0; tamanhoSenha < 6; tamanhoSenha++)
            {
                if (tamanhoSenha == 0)
                    novaSenha = novaSenha + letraMinuscula.Substring(random.Next(0, letraMinuscula.Length - 1), 1);
                else if (tamanhoSenha == 1)
                    novaSenha = novaSenha + letraMaiuscula.Substring(random.Next(0, letraMaiuscula.Length - 1), 1);
                else if (tamanhoSenha == 2)
                    novaSenha = novaSenha + numero.Substring(random.Next(0, numero.Length - 1), 1);
                else if (tamanhoSenha == 3)
                    novaSenha = novaSenha + caracterEspecial.Substring(random.Next(0, caracterEspecial.Length - 1), 1);
                else
                    novaSenha = novaSenha + letraMinuscula.Substring(random.Next(0, letraMinuscula.Length - 1), 1);
            }
            return novaSenha;
        }
    }
}
