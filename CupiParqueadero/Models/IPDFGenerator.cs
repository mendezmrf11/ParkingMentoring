using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFormativo.Modelos
{
    public interface IPDFGenerator
    {
        byte[] GeneratePDF();
    }
}
