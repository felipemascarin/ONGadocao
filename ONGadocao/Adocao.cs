using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONGadocao
{
    internal class Adocao
    {
        public string Chip { get; set; }
        public string CPF { get; set; }

        public DateTime DataAdocao { get; set; }


        public Adocao(string chip, string cpf, DateTime dataAdocao)
        {
            Chip = chip;
            CPF = cpf;
            DataAdocao = dataAdocao;
        }

    }
}
