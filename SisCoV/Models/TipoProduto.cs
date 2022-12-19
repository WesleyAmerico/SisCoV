using AcessoBancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisCoV.Models
{
    public class TipoProduto
    {
        public int Id { get; private set; }
        public string Tipo { get; private set; }

        public TipoProduto(string Tipo)
        {
            this.Id = 0;
            this.Tipo = Tipo;
        }

        public string CadastrarTipoProduto(TipoProduto tipoProduto)
        {
            try
            {
                string _query = "INSERT INTO tipoproduto (Id, Tipo )" +
                   " VALUES (@Id,@Tipo)";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("Id", tipoProduto.Id);
                conn.InserirParametro("Tipo", tipoProduto.Tipo);
                
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
    }
}
