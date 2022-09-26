using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONGadocao
{
    internal class Animal
    {
        public string Chip { get; set; }

        public string NomeAnimal { get; set; }

        public string Familia { get; set; }

        public char Sexo { get; set; }

        public string Raca { get; set; }


        public Animal(string chip, string nomeanimal, string familia, char sexo, string raca)
        {
            Chip = chip;
            NomeAnimal = nomeanimal;
            Familia = familia;
            Sexo = sexo;
            Raca = raca;
        }
    }
}
