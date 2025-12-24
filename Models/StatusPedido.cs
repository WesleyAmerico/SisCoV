using AcessoBancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisCoV.Models
{
    public class StatusPedido
    {
        public int Id { get; set; }
        public string Status { get; set; }

        public StatusPedido(string Status)
        {
            this.Id = 0;
            this.Status = Status;
        }

        public string CadastrarStatusPedido(StatusPedido statusPedido)
        {
            try
            {
                string _query = "INSERT INTO tipoproduto (Id, Status )" +
                   " VALUES (@Id,@Status)";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("Id", statusPedido.Id);
                conn.InserirParametro("Status", statusPedido.Status);

                int _resultado = Convert.ToInt32(conn.ExecutarManipulacao(CommandType.Text, _query));
                if (_resultado.Equals(0))
                    return "Nao foi possivel cadastrar StatusPedido";
                else
                    return "StatusPedido cadastrado com sucesso";
            }
            catch (Exception ex)
            {
                return $"Erro {ex.Message}";
                throw;
            }
        }
    }
}
