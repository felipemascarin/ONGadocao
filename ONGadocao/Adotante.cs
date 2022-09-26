using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONGadocao
{
    internal class Adotante
    {
        public string CPF { get; set; }
        public string NomeAdotante { get; set; }
        public DateTime DataNascimento { get; set; }
        public char Sexo { get; set; }
        public EnderecoAdotante Endereco { get; set; }
        public ContatoAdotante ContatoAdotante { get; set; }

        public Adotante(string cpf, string nomeadotante, DateTime datanascimento, char sexo, EnderecoAdotante endereco, ContatoAdotante contatoadotante)
        {
            CPF = cpf;
            NomeAdotante = nomeadotante;
            DataNascimento = datanascimento;
            Sexo = sexo;
            Endereco = endereco;
            ContatoAdotante = contatoadotante;
        }
    }
}
