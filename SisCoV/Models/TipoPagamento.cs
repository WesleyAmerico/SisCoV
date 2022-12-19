using AcessoBancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisCoV.Models
{
    public class TipoPagamento
    {
        public int Id { get; set; }
        public string Tipo { get; set; }

        public TipoPagamento(string Tipo)
        {
            this.Id = 0;
            this.Tipo = Tipo;
        }
        public TipoPagamento()
        {}
        public TipoPagamento(int Id, string Tipo)
        {
            this.Id = Id;
            this.Tipo = Tipo;
        }

        public string CadastrarTipoPagamento(TipoPagamento tipoPagamento)
        {
            try
            {
                string _query = "INSERT INTO tipopagamento (Id, Tipo )" +
                  " VALUES (@Id,@Tipo)";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("Id", tipoPagamento.Id);
                conn.InserirParametro("Tipo", tipoPagamento.Tipo);

                int _resultado = Convert.ToInt32(conn.ExecutarManipulacao(CommandType.Text, _query));
                if (_resultado.Equals(0))
                    return "Nao foi possivel cadastrar TipoPagamento";
                else
                    return "TipoPagamento cadastrado com sucesso";
            }
            catch (Exception ex)
            {
                return $"Erro {ex.Message}";
                throw;
            }
        }

        public List<TipoPagamento> BuscarTipoPagamento() 
        {
            try
            {
                TipoPagamento _tipoPagamento = null;
                List<TipoPagamento> _listTipoPagamentos = new List<TipoPagamento>();

                string _sql = "SELECT * FROM Tipo_Pagamentos ORDER BY id";
                ConexaoMySQL conn = new ConexaoMySQL();

                var _dtTipoPagamento = conn.RetornaDataTable(_sql);

                for (int i = 0; i < _dtTipoPagamento.Rows.Count; i++)
                {
                    var _id = Convert.ToInt32(_dtTipoPagamento.Rows[i]["Id"]);
                    var _tipo = _dtTipoPagamento.Rows[i]["Tipo"].ToString();

                    _tipoPagamento = new TipoPagamento(_id, _tipo);
                    _listTipoPagamentos.Add(_tipoPagamento);
                }
                return _listTipoPagamentos;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
