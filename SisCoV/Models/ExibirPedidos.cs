using AcessoBancoDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SisCoV.Models
{
    public class ExibirPedidos
    {
        public long IdPedido { get; set; }
        public long IdItemPedido { get; set; }
        public string Cliente { get; set; }
        public string Nome { get;  set; }
        public int Quantidade { get;  set; }

        public ExibirPedidos(){}
        public ExibirPedidos(long IdPedido, long IdItemPedido, string Cliente, string Nome, int Quantidade) 
        {
            this.IdPedido = IdPedido;
            this.IdItemPedido = IdItemPedido;
            this.Cliente = Cliente;
            this.Nome = Nome;
            this.Quantidade = Quantidade;
        }
        public List<ExibirPedidos> ExibirFilaPorcoesPedidos()
        {
            try
            {
                List<ExibirPedidos> _listaPorcoesPedidos = new List<ExibirPedidos>();
                ExibirPedidos _exibirPedidos = null;

                string _sql = "SELECT itens_pedidos.Quantidade, " +
                             "pedidos.Id AS IdPedido," +
                             "itens_pedidos.Id AS idItemPedido," +
                             "itens.Nome," +
                             "itens_pedidos.Cliente " +
                             "FROM itens_pedidos " +
                             "INNER JOIN pedidos ON pedidos.Id = itens_pedidos.Pedido_Id " +
                             "INNER JOIN itens ON itens.Id = itens_pedidos.Item_id " +
                             "WHERE pedidos.Status_Pedido = 1 " +
                             "AND itens_pedidos.Status_Preparacao = 0 " +
                             "AND itens_pedidos.TipoProduto_id = 3 " +
                             "OR itens_pedidos.TipoProduto_id = 4 " +
                             "OR itens_pedidos.TipoProduto_id = 5 " +
                             "ORDER BY  itens_pedidos.DataPedido ";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                var dt = conn.RetornaDadosTabela(System.Data.CommandType.Text, _sql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var _idPedido = Convert.ToInt64(dt.Rows[i]["IdPedido"]);
                    var _idItemPedido = Convert.ToInt64(dt.Rows[i]["IdItemPedido"]);
                    var _cliente = dt.Rows[i]["Cliente"].ToString();
                    var _nome = dt.Rows[i]["Nome"].ToString();
                    var _quantidade = Convert.ToInt32(dt.Rows[i]["Quantidade"]);

                    _exibirPedidos = new ExibirPedidos(_idPedido, _idItemPedido, _cliente, _nome, _quantidade);
                    _listaPorcoesPedidos.Add(_exibirPedidos);
                }

                return _listaPorcoesPedidos;
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public List<ExibirPedidos> ExibirFilaLanchesPedidos()
        {
            try
            {
                List<ExibirPedidos> _listaLanchesPedidos = new List<ExibirPedidos>();
                ExibirPedidos _exibirPedidos = null;

                string _sql = "SELECT itens_pedidos.Quantidade, " +
                             "pedidos.Id AS IdPedido," +
                             "itens_pedidos.Id AS idItemPedido," +
                             "itens.Nome," +
                             "itens_pedidos.Cliente " +
                             "FROM itens_pedidos " +
                             "INNER JOIN pedidos ON pedidos.Id = itens_pedidos.Pedido_Id " +
                             "INNER JOIN itens ON itens.Id = itens_pedidos.Item_id " +
                             "WHERE pedidos.Status_Pedido = 1 " +
                             "AND itens_pedidos.TipoProduto_id = 1 " +
                             "AND itens_pedidos.Status_Preparacao = 0 " +
                             "ORDER BY  itens_pedidos.DataPedido ";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                var dt = conn.RetornaDadosTabela(System.Data.CommandType.Text, _sql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var _idPedido = Convert.ToInt64(dt.Rows[i]["IdPedido"]);
                    var _idItemPedido = Convert.ToInt64(dt.Rows[i]["IdItemPedido"]);
                    var _cliente = dt.Rows[i]["Cliente"].ToString();
                    var _nome = dt.Rows[i]["Nome"].ToString();
                    var _quantidade = Convert.ToInt32(dt.Rows[i]["Quantidade"]);

                    _exibirPedidos = new ExibirPedidos(_idPedido, _idItemPedido, _cliente, _nome, _quantidade);
                    _listaLanchesPedidos.Add(_exibirPedidos);
                }

                return _listaLanchesPedidos;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ExibirPedidos> ExibirFilaBebidasPedidos()
        {
            try
            {
                List<ExibirPedidos> _listaLanchesPedidos = new List<ExibirPedidos>();
                ExibirPedidos _exibirPedidos = null;

                string _sql = "SELECT itens_pedidos.Quantidade, " +
                             "pedidos.Id AS IdPedido," +
                             "itens_pedidos.Id AS idItemPedido," +
                             "itens.Nome," +
                             "itens_pedidos.Cliente " +
                             "FROM itens_pedidos " +
                             "INNER JOIN pedidos ON pedidos.Id = itens_pedidos.Pedido_Id " +
                             "INNER JOIN itens ON itens.Id = itens_pedidos.Item_id " +
                             "WHERE pedidos.Status_Pedido = 1 " +
                             "AND itens_pedidos.TipoProduto_id = 2 " +
                             "AND itens_pedidos.Status_Preparacao = 0 " +
                             "ORDER BY  itens_pedidos.DataPedido ";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                var dt = conn.RetornaDadosTabela(System.Data.CommandType.Text, _sql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var _idPedido = Convert.ToInt64(dt.Rows[i]["IdPedido"]);
                    var _idItemPedido = Convert.ToInt64(dt.Rows[i]["IdItemPedido"]);
                    var _cliente = dt.Rows[i]["Cliente"].ToString();
                    var _nome = dt.Rows[i]["Nome"].ToString();
                    var _quantidade = Convert.ToInt32(dt.Rows[i]["Quantidade"]);

                    _exibirPedidos = new ExibirPedidos(_idPedido, _idItemPedido, _cliente, _nome, _quantidade);
                    _listaLanchesPedidos.Add(_exibirPedidos);
                }

                return _listaLanchesPedidos;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
