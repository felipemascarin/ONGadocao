using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONGadocao
{
    internal class EnderecoAdotante
    {
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string TipoLogradouro { get; set; }
        public string NomeLogradouro { get; set; }

        public EnderecoAdotante(string cep, string bairro, string cidade, string estado, string tipologradouro, string nomelogradouro)
        {
            CEP = cep;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            TipoLogradouro = tipologradouro;
            NomeLogradouro = nomelogradouro;
        }
    }
}
