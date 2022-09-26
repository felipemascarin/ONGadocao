using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONGadocao
{
    internal class ContatoAdotante
    {
        public string Numero { get; set; }

        public string Tipo { get; set; }


        public ContatoAdotante(string numero, string tipo)
        {
            Numero = numero;
            Tipo = tipo;
        }
    }
}
