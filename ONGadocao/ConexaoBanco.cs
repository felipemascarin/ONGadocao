﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONGadocao
{
    internal class ConexaoBanco
    {
        string Conexao = "Data Source=localhost; Initial Catalog=OngAdocao; User id=sa; Password=159357;";

        SqlConnection conn;

        public ConexaoBanco()
        {
            conn = new SqlConnection(Conexao);
        }

        public SqlConnection AbrirConexao()
        {
            return conn;
        }


        public void FecharConexao()
        {
            conn.Close();
        }



    }
}
