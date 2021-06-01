using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React_Arquivos_Backend.ExceptionCustom
{
    public class ExceptionBusiness : Exception
    {
        public ExceptionBusiness(string message) : base(message)
        {

        }
    }
}
