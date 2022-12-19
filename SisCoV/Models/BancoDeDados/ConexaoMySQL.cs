using MySql.Data.MySqlClient;
using System;
using System.Data;



namespace AcessoBancoDados
{
     public class ConexaoMySQL
    {
        //variavel para adcionar parametros que serao enviados para banco de dados  via comandoSQL
        private MySqlParameterCollection mySqlParameterCollection = new MySqlCommand().Parameters;

        //Variavel que vai passar os parametros para conectar com o banco
        private string _stringConexao;

        //PRECISAMOS USAR UM OBJETO PARA A CONEXÃO COM O BANCO O 
        //OBJETO MySqlConnection TRATA A CONEXÃO COM O MySQL ou MariaDB
        private MySqlConnection _objetoConexao;
        private string _erros;
        private bool retorno;

        public bool _retorno
        {
            get { return this.retorno; }
            set { this.retorno = value; }
        }
        public string comandoerros
        {
            get { return this._erros; }
        }
        private MySqlDataAdapter mDAdap;

        //Metodo construtor da classe
        public ConexaoMySQL(String dadosConexao)
        {

            //instanciando um novo objeto de conexao 
            this._objetoConexao = new MySqlConnection();
            //recebendo os dados do usuario e armazenando na variavel que vai passar os parametros para o objeto
            this._stringConexao = dadosConexao;
            //passando os parametros da variavel para o objeto
            this._objetoConexao.ConnectionString = dadosConexao;

        }
        public ConexaoMySQL()
        {

            //instanciando um novo objeto de conexao 
            this._objetoConexao = new MySqlConnection();
            //recebendo os dados do usuario e armazenando na variavel que vai passar os parametros para o objeto
            this._stringConexao = @"Persist Security Info=False; Server = localhost; Database = siscov; uid=root; pwd= '123'; convert zero datetime=True";
            //passando os parametros da variavel para o objeto
            this._objetoConexao.ConnectionString = this._stringConexao;

        }


        public string stringConexao
        {
            get { return this._stringConexao; }
            set { this._stringConexao = value; }
        }

        public MySqlConnection objetoConexao
        {
            get { return this._objetoConexao; }
            set { this._objetoConexao = value; }
        }

        //Teste Método com Retorno Para Conexao
        

        public bool Conectar()
        {
            try
            {
                //objeto conexao com os parametros adicionados, vai enviar dados para o banco e abrir a conexao
                this._objetoConexao.Open();
                return true;
            }
            catch (Exception ex)
            {


                //TratamentoLog.GravarLog("Não foi possivel Conectar com Banco" + ex, TratamentoLog.NivelLog.Erro);
                return false;
                throw;
            }
           
        }

        public void Desconectar()
        {
            if (_objetoConexao.State == ConnectionState.Open)
            {
                //objeto conexao com os parametros adicionados, vai enviar dados para o banco e fechar a conexao
                this._objetoConexao.Dispose();
                this._objetoConexao.Close();
            }

        }



       

       

        //Metodo para Deixar a coleçoa de parametros limpos para quando for adicionados pelo comando
        public void LimparParamentros()
        {
            mySqlParameterCollection.Clear();
        }

        //Metodo que vai inserir o parametro pegando o nome e o valor do parametro : exemplo(@nome. classe.nome)
        public void InserirParametro(string nomeparametro, object valorparametro)
        {
            mySqlParameterCollection.Add(new MySqlParameter(nomeparametro, valorparametro));
        }

        //Executa as manipulacao dos dados, conecta com o banco 
        public object ExecutarManipulacao(CommandType tipocomando, string query)
        {
            try
            {
                //String de Conexao com banco
                MySqlConnection conexaoMySQL = this._objetoConexao;
                Conectar();
                //Criar comando que vai conectar com o banco
                MySqlCommand cmd = conexaoMySQL.CreateCommand();
                //colocar os dados que vao trafegar dentro do comando
                cmd.CommandType = tipocomando;
                cmd.CommandText = query;

                //Adiciona os parametros no comando
                foreach (MySqlParameter parametros in mySqlParameterCollection)
                {
                    cmd.Parameters.Add(new MySqlParameter(parametros.ParameterName, parametros.Value));
                }
                //Executa o comando, manda o comando ir ate o banco de dados
                // return cmd.ExecuteScalar();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception sqlex)
            {
                _erros = sqlex.Message;
                Desconectar();
                return 0;
                throw;
            }


        }

        public DataTable RetornaDataTable(string query)
        {
            try
            {
                DataTable dt = new DataTable();

                MySqlCommand cmd = new MySqlCommand(query, this._objetoConexao);
                cmd.CommandType = CommandType.Text;
                MySqlDataAdapter mDAdap = new MySqlDataAdapter(cmd);

                Conectar();

                mDAdap.Fill(dt);
                mDAdap.Dispose();


                Desconectar();
                return dt;
            }
            catch (MySqlException sqlex)
            {

                Desconectar();
                return null;
            }
        }

        public DataTable RetornaDadosTabela(CommandType comando, string queryouStoredProcedure)
        {
            try
            {
                MySqlConnection conexaoMySQL = this._objetoConexao;
                Conectar();

                MySqlCommand cmd = conexaoMySQL.CreateCommand();
                cmd.CommandType = comando;
                cmd.CommandText = queryouStoredProcedure;

                foreach (MySqlParameter parametros in mySqlParameterCollection)
                {
                    cmd.Parameters.Add(new MySqlParameter(parametros.ParameterName, parametros.Value));
                }

                MySqlDataAdapter myAdapt = new MySqlDataAdapter(cmd);
                DataTable dadosTabela = new DataTable();
                myAdapt.Fill(dadosTabela);

                return dadosTabela;
            }
            catch (Exception sqlex)
            {

                //TratamentoLog.GravarLog("Não foi possivel executar RetornaDadosTabela ", TratamentoLog.NivelLog.Erro);
                throw;
            }

        }
    }
}