using System;
using System.Data.SqlClient;

namespace ONGadocao
{
    internal class Program
    {
        static void Pausa()
        {
            Console.WriteLine("\nAperte 'ENTER' para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        static bool PausaMensagem()
        {
            bool repetirdo;
            do
            {
                Console.WriteLine("\nPressione S para informar novamente ou C para cancelar:");
                ConsoleKeyInfo op = Console.ReadKey(true);
                if (op.Key == ConsoleKey.S)
                {
                    Console.Clear();
                    return false;
                }
                else
                {
                    if (op.Key == ConsoleKey.C)
                    {
                        Console.Clear();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Escolha uma opção válida!");
                        repetirdo = true;
                    }
                }
            } while (repetirdo == true);
            return true;
        }

        static void CadastrarAdotante()
        {
            bool voltar;
            do
            {
                try
                {
                    Console.WriteLine("\nCadastro de adotante\n");
                    Console.Write("Nome: ");
                    string nomeadotante = Console.ReadLine();
                    Console.Write("CPF (Apenas 11 números): ");
                    string cpf = Console.ReadLine();
                    Console.Write("Data de Nascimento (Digite as barras dd/MM/yyyy): ");
                    DateTime datanascimento = DateTime.Parse(Console.ReadLine());
                    Console.Write("Sexo (M / F): ");
                    char sexo = char.Parse(Console.ReadLine().ToUpper());

                    Console.Write("Numero de Contato: ");
                    string numero = Console.ReadLine();
                    Console.Write("Tipo de Contato (Celular, Telefone fixo): ");
                    string tipo = Console.ReadLine();


                    Console.WriteLine("\nEndereço\n");
                    Console.Write("CEP:");
                    string cep = Console.ReadLine();
                    Console.Write("Tipo Logradouro (Rua, Avenida...): ");
                    string tipologradouro = Console.ReadLine();
                    Console.Write("Nome do logradouro: ");
                    string nomelogradouro = Console.ReadLine();
                    Console.Write("Bairro:");
                    string bairro = Console.ReadLine();
                    Console.Write("Cidade:");
                    string cidade = Console.ReadLine();
                    Console.Write("Estado:");
                    string estado = Console.ReadLine();

                    ConexaoBanco conn = new ConexaoBanco();
                    SqlConnection connection = conn.AbrirConexao();

                    String comando1 = "insert into Adotante Values('" + cpf + "', '" + nomeadotante + "', '" + datanascimento.ToString("yyyy-MM-dd") + "', '" + sexo + "');";
                    String comando2 = "insert into EnderecoAdotante Values('" + cpf + "', '" + cep + "', '" + bairro + "', '" + cidade + "', '" + estado + "', '" + tipologradouro + "', '" + nomelogradouro + "');";
                    String comando3 = "insert into ContatoAdotante Values('" + cpf + "', '" + numero + "', '" + tipo + "');";

                    SqlCommand sql_cmnd = new SqlCommand(comando1, connection);

                    sql_cmnd.Connection.Open();

                    sql_cmnd.CommandText = comando1;
                    sql_cmnd.ExecuteNonQuery();

                    sql_cmnd = new SqlCommand(comando2, connection);
                    sql_cmnd.CommandText = comando2;
                    sql_cmnd.ExecuteNonQuery();

                    sql_cmnd = new SqlCommand(comando3, connection);
                    sql_cmnd.CommandText = comando3;
                    sql_cmnd.ExecuteNonQuery();

                    sql_cmnd.Connection.Close();
                    voltar = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Erro de exceção: Não foi possível cadastrar, Verifique a conexão e Insira os valores corretamente!");
                    Console.WriteLine("Verifique também se esse CPF já Possui Cadastro no menu principal.");
                    voltar = PausaMensagem();
                }
                Console.Clear();
            } while (voltar == false);
        }

        static void CadastrarAnimal()
        {
            bool voltar;
            do
            {
                try
                {
                    voltar = false;
                    Console.WriteLine("\nCadastro de Animal\n");
                    Console.WriteLine("Número do chip (4 numeros obrigatoriamente): ");
                    int chip = int.Parse(Console.ReadLine());
                    Console.WriteLine("Nome do animal: ");
                    string nomeanimal = Console.ReadLine();
                    Console.WriteLine("Familia pertencente (Gato, Cachorro...): ");
                    string familia = Console.ReadLine();
                    Console.WriteLine("Sexo (M / F): ");
                    char sexo = char.Parse(Console.ReadLine().ToUpper());
                    Console.WriteLine("Raça do animal: ");
                    string raca = Console.ReadLine();

                    ConexaoBanco conn = new ConexaoBanco();
                    SqlConnection connection = conn.AbrirConexao();

                    String comando1 = "insert into Animal Values('" + chip.ToString() + "', '" + nomeanimal + "', '" + sexo + "');";
                    String comando2 = "insert into TipoAnimal Values('" + chip.ToString() + "', '" + familia + "', '" + raca + "');";

                    SqlCommand sql_cmnd = new SqlCommand(comando1, connection);

                    sql_cmnd.Connection.Open();

                    sql_cmnd.CommandText = comando1;
                    sql_cmnd.ExecuteNonQuery();

                    sql_cmnd = new SqlCommand(comando2, connection);

                    sql_cmnd.CommandText = comando2;
                    sql_cmnd.ExecuteNonQuery();

                    sql_cmnd.Connection.Close();
                    voltar = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Erro de exceção: Não foi possível cadastrar, Verifique a conexão e Insira os valores corretamente!");
                    Console.WriteLine("Verifique também se esse CHIP já Possui Cadastro no menu principal.");
                    voltar = PausaMensagem();
                }
                Console.Clear();
            } while (voltar == false);
        }

        static void CadastrarAdocao()
        {
            string cpf, chip;
            bool voltar;
            do
            {
                try
                {
                    SqlDataReader reader;
                    voltar = false;

                    Console.WriteLine("Digite o CPF do adotante: ");
                    cpf = Console.ReadLine();

                    //Resgatar informações do banco de dados:

                    ConexaoBanco conn = new ConexaoBanco();
                    SqlConnection connection = conn.AbrirConexao();

                    String comando1 = "select cpf from Adotante where cpf = '" + cpf + "';";

                    SqlCommand sql_cmnd = new SqlCommand(comando1, connection);

                    sql_cmnd.CommandText = comando1;

                    sql_cmnd.Connection.Open();

                    reader = sql_cmnd.ExecuteReader();

                    if (reader.HasRows == false)
                    {
                        Console.WriteLine("Adotante não encontrado, insira um CPF cadastrado");
                        voltar = PausaMensagem();
                        sql_cmnd.Connection.Close();
                    }
                    else
                    {
                        sql_cmnd.Connection.Close();

                        Console.WriteLine("Digite o número do CHIP do animal a ser adotado: ");
                        chip = Console.ReadLine();

                        String comando2 = "select chip from Animal where chip = '" + chip + "';";

                        sql_cmnd = new SqlCommand(comando2, connection);

                        sql_cmnd.CommandText = comando2;

                        sql_cmnd.Connection.Open();

                        reader = sql_cmnd.ExecuteReader();

                        if (reader.HasRows == false)
                        {
                            Console.WriteLine("Animal não encontrado, insira um número de CHIP cadastrado");
                            voltar = PausaMensagem();
                            sql_cmnd.Connection.Close();
                        }
                        else
                        {
                            sql_cmnd.Connection.Close();

                            String comando3 = "select chip from Adocao where chip = '" + chip + "';";

                            sql_cmnd = new SqlCommand(comando3, connection);

                            sql_cmnd.CommandText = comando3;

                            sql_cmnd.Connection.Open();

                            reader = sql_cmnd.ExecuteReader();

                            if (reader.HasRows == false)
                            {
                                sql_cmnd.Connection.Close();

                                DateTime data = System.DateTime.Now;

                                String comando4 = "insert into Adocao Values('" + chip + "', '" + cpf + "', '" + data.ToString("yyyy-MM-dd") + "');";

                                sql_cmnd = new SqlCommand(comando4, connection);

                                sql_cmnd.Connection.Open();

                                sql_cmnd.CommandText = comando4;
                                sql_cmnd.ExecuteNonQuery();

                                sql_cmnd.Connection.Close();
                                Console.WriteLine("Adoção Realizada com sucesso!");
                                Pausa();
                                voltar = true;
                            }
                            else
                            {
                                Console.WriteLine("Esse animal já foi adotado!");
                                voltar = PausaMensagem();
                                sql_cmnd.Connection.Close();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Erro de exceção: Não foi possível cadastrar, Verifique a conexão e Insira os valores corretamente!");
                    voltar = PausaMensagem();
                }
                Console.Clear();
            } while (voltar == false);
        }

        static void EditarDadosAdotante(string cpf)
        {
            int op;
            bool voltar;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\nEditar dados Adotante\n");
                    Console.WriteLine(" 1  - Alterar o nome");
                    Console.WriteLine(" 2  - Alterar a Data de Nascimento");
                    Console.WriteLine(" 3  - Alterar o sexo");
                    Console.WriteLine(" 4  - Alterar o Número de Contato");
                    Console.WriteLine(" 5  - Alterar o Tipo do Número de Contato");
                    Console.WriteLine(" 6  - Alterar CEP");
                    Console.WriteLine(" 7  - Alterar Tipo do Logradouro");
                    Console.WriteLine(" 8  - Alterar Nome do Logradouro");
                    Console.WriteLine(" 9  - Alterar Bairro");
                    Console.WriteLine(" 10 - Alterar Cidade");
                    Console.WriteLine(" 11 - Alterar Estado");

                    Console.WriteLine("\n 0 - Voltar");
                    Console.WriteLine("\n Digite a Opção: ");

                    op = int.Parse(Console.ReadLine());
                    Console.Clear();

                    switch (op)
                    {
                        case 0:

                            return;

                        case 1:

                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Novo nome: ");
                                    string novonome = Console.ReadLine();


                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update Adotante set NomeAdotante = '" + novonome + "' where cpf = '" + cpf + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;

                        case 2:

                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Alterar Data de Nascimento para (escreva as barras dd/MM/yyy): ");
                                    DateTime datanascimento = DateTime.Parse(Console.ReadLine());


                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update Adotante set DataNascimento = '" + datanascimento.ToString("dd-MM-yyyy") + "' where cpf = '" + cpf + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;

                        case 3:

                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Alterar informação do Sexo (M / F): ");
                                    char sexo = char.Parse(Console.ReadLine().ToUpper());


                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update Adotante set Sexo = '" + sexo + "' where cpf = '" + cpf + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);



                            break;

                        case 4:
                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Novo Número de contato: ");
                                    int numero = int.Parse(Console.ReadLine());

                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update ContatoAdotante set Numero = '" + numero.ToString() + "' where cpf = '" + cpf + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);


                            break;

                        case 5:
                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Alterar Tipo do contato para: ");
                                    string tipo = Console.ReadLine();

                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update ContatoAdotante set Tipo = '" + tipo + "' where cpf = '" + cpf + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;

                        case 6:
                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Alterar CEP: ");
                                    string cep = Console.ReadLine();


                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update EnderecoAdotante set CEP = '" + cep + "' where cpf = '" + cpf + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;

                        case 7:
                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Alterar Tipo do Logradouro (Rua, Avenida...): ");
                                    string tipologradouro = Console.ReadLine();


                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update EnderecoAdotante set TipoLogradouro = '" + tipologradouro + "' where cpf = '" + cpf + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;

                        case 8:

                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Alterar Nome do Logradouro (Nome da rua, nome da avenida...): ");
                                    string nomelogradouro = Console.ReadLine();


                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update EnderecoAdotante set NomeLogradouro = '" + nomelogradouro + "' where cpf = '" + cpf + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;

                        case 9:

                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Alterar Bairro: ");
                                    string bairro = Console.ReadLine();


                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update EnderecoAdotante set Bairro = '" + bairro + "' where cpf = '" + cpf + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;

                        case 10:

                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Alterar Cidade: ");
                                    string cidade = Console.ReadLine();


                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update EnderecoAdotante set Cidade = '" + cidade + "' where cpf = '" + cpf + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;

                        case 11:

                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Alterar Estado: ");
                                    string estado = Console.ReadLine();


                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update EnderecoAdotante set Estado = '" + estado + "' where cpf = '" + cpf + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;

                    }
                    Console.Clear();
                }
                catch (Exception)
                {
                    Console.WriteLine("Erro de exceção: Escolha uma opção válida!");
                    Pausa();
                }
            } while (true);
        }

        static void EditarDadosAnimal(string chip)
        {
            int op;
            bool voltar;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\nEditar dados Animal\n");
                    Console.WriteLine(" 1  - Alterar o Nome do Animal");
                    Console.WriteLine(" 2  - Alterar o Sexo do Animal");

                    Console.WriteLine("\n 0 - Voltar");
                    Console.WriteLine("\n Digite a Opção: ");

                    op = int.Parse(Console.ReadLine());
                    Console.Clear();

                    switch (op)
                    {
                        case 0:

                            return;

                        case 1:

                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Novo nome para o animal: ");
                                    string novonome = Console.ReadLine();


                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update Animal set NomeAnimal = '" + novonome + "' where Chip = '" + chip + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;

                        case 2:

                            do
                            {
                                voltar = false;
                                try
                                {
                                    Console.WriteLine("Novo sexo para o animal (M / F): ");
                                    char sexo = char.Parse(Console.ReadLine());


                                    ConexaoBanco conn = new ConexaoBanco();
                                    SqlConnection connection = conn.AbrirConexao();

                                    String comando = "Update Animal set Sexo = '" + sexo + "' where Chip = '" + chip + "';";

                                    SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.Connection.Open();

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.ExecuteNonQuery();

                                    sql_cmnd.Connection.Close();

                                    Console.WriteLine("Alteração salva com sucesso!");
                                    Pausa();
                                    voltar = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;

                    }
                    Console.Clear();
                }
                catch (Exception)
                {
                    Console.WriteLine("Erro de exceção: Escolha uma opção válida!");
                    Pausa();
                }
            } while (true);

        }

        static void Main(string[] args)
        {

            int op;
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\nSistema de adoção de animais (ONG Adoção)\n");
                    Console.WriteLine(" 1 - Cadastrar Animal");
                    Console.WriteLine(" 2 - Cadastrar Adotante");
                    Console.WriteLine(" 3 - Cadastrar Adoção");
                    Console.WriteLine(" 4 - Adoções realizadas");
                    Console.WriteLine(" 5 - Ver/Editar dados de Adotante");
                    Console.WriteLine(" 6 - Ver/Editar dados de Animal");
                    Console.WriteLine("\n 0 - Sair");
                    Console.WriteLine("\n Digite a Opção: ");

                    op = int.Parse(Console.ReadLine());
                    Console.Clear();

                    switch (op)
                    {
                        case 1:

                            CadastrarAnimal();

                            break;


                        case 2:

                            CadastrarAdotante();

                            break;


                        case 3:

                            CadastrarAdocao();

                            break;

                        case 4:

                            ConexaoBanco conn = new ConexaoBanco();

                            SqlConnection connection = conn.AbrirConexao();

                            String comando = "select Adotante.NomeAdotante as 'Nome Adotante', Adocao.CPF, Animal.NomeAnimal " +
                                "as 'Nome do Animal', TipoAnimal.Familia as 'Família', TipoAnimal.Raca as 'Raça', Adocao.Chip, Convert(varchar, Adocao.DataAdocao, 101)" +
                                "as 'Data da adoção' from Adocao join Animal on Adocao.Chip = Animal.Chip join TipoAnimal on Animal.Chip = TipoAnimal.Chip " +
                                "join Adotante on Adocao.CPF = Adotante.CPF; ";

                            SqlCommand sql_cmnd = new SqlCommand(comando, connection);

                            sql_cmnd.CommandText = comando;

                            sql_cmnd.Connection.Open();

                            SqlDataReader reader = sql_cmnd.ExecuteReader();

                            Console.WriteLine("DADOS DE TODAS ADOÇÕES\n");

                            while (reader.Read())
                            {
                                Console.WriteLine("\nNome Adotante: {0}", reader.GetString(0));
                                Console.WriteLine("CPF: {0}", reader.GetString(1));
                                Console.WriteLine("Nome do animal adotado: {0}", reader.GetString(2));
                                Console.WriteLine("Família de animal: {0}", reader.GetString(3));
                                Console.WriteLine("Raça: {0}", reader.GetString(4));
                                Console.WriteLine("Número do Chip: {0}", reader.GetString(5));
                                Console.WriteLine("Data da adoção: {0}\n", reader.GetString(6));
                            }

                            sql_cmnd.Connection.Close();

                            Pausa();

                            break;


                        case 5:
                            string cpf;
                            bool voltar;
                            do
                            {
                                try
                                {
                                    voltar = false;
                                    Console.WriteLine("Digite o CPF do adotante: ");
                                    cpf = Console.ReadLine();

                                    conn = new ConexaoBanco();

                                    connection = conn.AbrirConexao();

                                    comando = "select cpf from Adotante where cpf = '" + cpf + "';";

                                    sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.Connection.Open();

                                    reader = sql_cmnd.ExecuteReader();

                                    if (reader.HasRows == false)
                                    {
                                        Console.WriteLine("Adotante não encontrado, insira um CPF cadastrado");
                                        voltar = PausaMensagem();
                                        sql_cmnd.Connection.Close();
                                    }
                                    else
                                    {
                                        sql_cmnd.Connection.Close();

                                        comando = "select a.CPF, a.NomeAdotante as 'Nome do Adotante', Convert(varchar,a.DataNascimento,101) " +
                                            "as 'Data de Nascimento', a.Sexo, ca.Numero, ca.Tipo, ea.Cidade, ea.Estado, ea.TipoLogradouro, ea.NomeLogradouro, ea.Bairro, ea.CEP from Adotante " +
                                            "as a, EnderecoAdotante as ea, ContatoAdotante as ca Where a.CPF = '" + cpf + "' and ea.CPF = a.CPF and ca.CPF = a.CPF;";

                                        sql_cmnd = new SqlCommand(comando, connection);

                                        sql_cmnd.CommandText = comando;

                                        sql_cmnd.Connection.Open();

                                        reader = sql_cmnd.ExecuteReader();

                                        reader.Read();

                                        Console.Clear();
                                        Console.WriteLine("CPF: {0}.", reader.GetSqlString(0));
                                        Console.WriteLine("Nome do Adotante: {0}", reader.GetSqlString(1));
                                        Console.WriteLine("Data de Nascimento: {0}", reader.GetSqlString(2));
                                        Console.WriteLine("Sexo: {0}", reader.GetSqlString(3));
                                        Console.WriteLine("Número: {0}", reader.GetSqlString(4));
                                        Console.WriteLine("Tipo: {0}", reader.GetSqlString(5));
                                        Console.WriteLine("Cidade: {0}", reader.GetSqlString(6));
                                        Console.WriteLine("Estado: {0}", reader.GetSqlString(7));
                                        Console.WriteLine("Tipo Logradouro: {0}", reader.GetSqlString(8));
                                        Console.WriteLine("Nome Logradouro: {0}", reader.GetSqlString(9));
                                        Console.WriteLine("Bairro: {0}", reader.GetSqlString(10));
                                        Console.WriteLine("CEP: {0}", reader.GetSqlString(11));
                                        sql_cmnd.Connection.Close();
                                        Pausa();
                                        EditarDadosAdotante(cpf);
                                        voltar = true;
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;




                        case 6:

                            string chip;
                            do
                            {
                                try
                                {
                                    voltar = false;
                                    Console.WriteLine("Digite o CHIP do Animal: ");
                                    chip = Console.ReadLine();

                                    conn = new ConexaoBanco();

                                    connection = conn.AbrirConexao();

                                    comando = "select Chip from Animal where Chip = '" + chip + "';";

                                    sql_cmnd = new SqlCommand(comando, connection);

                                    sql_cmnd.CommandText = comando;

                                    sql_cmnd.Connection.Open();

                                    reader = sql_cmnd.ExecuteReader();

                                    if (reader.HasRows == false)
                                    {
                                        Console.WriteLine("Animal não encontrado, insira um CHIP cadastrado");
                                        voltar = PausaMensagem();
                                        sql_cmnd.Connection.Close();
                                    }
                                    else
                                    {
                                        sql_cmnd.Connection.Close();

                                        comando = "select a.chip, a.NomeAnimal as 'Nome do Animal', a.Sexo, t.Familia as 'Família', t.Raca " +
                                            "as 'Raça' from Animal as a, TipoAnimal as t where a.Chip = '" + chip + "' and t.Chip = a.Chip;";

                                        sql_cmnd = new SqlCommand(comando, connection);

                                        sql_cmnd.CommandText = comando;

                                        sql_cmnd.Connection.Open();

                                        reader = sql_cmnd.ExecuteReader();

                                        reader.Read();

                                        Console.Clear();
                                        Console.WriteLine("CHIP: {0}", reader.GetSqlString(0));
                                        Console.WriteLine("Nome do Animal: {0}", reader.GetSqlString(1));
                                        Console.WriteLine("Sexo: {0}", reader.GetSqlString(2));
                                        Console.WriteLine("Família: {0}", reader.GetSqlString(3));
                                        Console.WriteLine("Raça: {0}", reader.GetSqlString(4));
                                        sql_cmnd.Connection.Close();
                                        Pausa();
                                        EditarDadosAnimal(chip);
                                        voltar = true;
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Erro de exceção: Verifique a conexão e Insira os valores corretamente!");
                                    voltar = PausaMensagem();
                                }
                            } while (voltar == false);

                            break;
                    }
                }
                catch (Exception)
                {
                    op = 20;
                }
            } while (op != 0);
        }
    }
}
