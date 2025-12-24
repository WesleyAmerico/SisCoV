using AcessoBancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisCoV.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public int Numero { get; set; }

        public bool Status { get; set; }

        public Mesa(int Id, int Numero, bool Status)
        {
            this.Id = Id;
            this.Numero = Numero;
            this.Status = Status;
        }
        public Mesa() { }

        public string CadastrarMesa(Mesa mesa)
        {
            try
            {
                string _query = "INSERT INTO mesa (Id, Tipo, Status )" +
                   " VALUES (@Id,@Tipo, @Status)";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("Id", mesa.Id);
                conn.InserirParametro("Numero", mesa.Numero);
                conn.InserirParametro("Status", mesa.Status);


                int _resultado = Convert.ToInt32(conn.ExecutarManipulacao(CommandType.Text, _query));
                if (_resultado.Equals(0))
                    return "Nao foi possivel cadastrar TipoProduto";
                else
                    return "Produto cadastrado com sucesso";
            }
            catch (Exception ex)
            {
                return $"Erro {ex.Message}";
                throw;
            }
        }

        public void OcuparMesa(int IdMesa)
        {
            try
            {
                string _query = "UPDATE mesas set Status = @Status WHERE Id = @Id";
                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("Id", IdMesa);
                conn.InserirParametro("Status", true);


                int _resultado = Convert.ToInt32(conn.ExecutarManipulacao(CommandType.Text, _query));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void LiberarMesa()
        {
            try
            {
                string _query = $"UPDATE mesas set statusmesa = @statusmesa WHERE mesa = {Numero}";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("@statusmesa", false);
                conn.ExecutarManipulacao(CommandType.Text, _query);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Mesa> VerificarStatusMesa()
        {
            try
            {
                List<Mesa> mesas = new List<Mesa>();

                string _query = $"SELECT * FROM mesas WHERE Id > 0";

                ConexaoMySQL conn = new ConexaoMySQL();
                var retorno = conn.RetornaDataTable(_query);
                if(retorno != null) 
                { 
                    for (int i = 0; i < retorno.Rows.Count; i++)
                    {
                        var Id = Convert.ToInt32(retorno.Rows[i]["Id"]);
                        var Numero = Convert.ToInt32(retorno.Rows[i]["Numero"]);
                        var Status = Convert.ToBoolean(retorno.Rows[i]["Status"]);

                        Mesa mesa = new Mesa(Id, Numero, Status);

                        mesas.Add(mesa);
                    }
                }
                return mesas;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public bool VerificarStatusMesa(int Id)
        {
            try
            {
                bool _status = false;
                string _query = "SELECT Status FROM mesas WHERE Id = @Id";
                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("Id", Id);
                var dt = conn.RetornaDadosTabela(CommandType.Text,_query);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    _status = Convert.ToBoolean(dt.Rows[i]["Status"]);
                }
                   
                
                return _status;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
