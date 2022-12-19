using AcessoBancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisCoV.Models
{
    public class ItemPedido
    {
        public long Id { get; set; }
        public int ItenId { get; set; }
        public long PedidoId { get; set; }
        public string Nome { get; set; }
        public string Cliente { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public int Quantidade { get; set; }
        public int StatusPedidoId { get; set; }
        public DateTime DataPedido { get; set; }
        public int TipoProdutoId { get; set; }

        public ItemPedido() { }
        public ItemPedido(int ItenId, long PedidoId,string Nome, string Cliente, decimal ValorUnitario, int Quantidade, int StatusPedidoId, int TipoProdutoID)
        {
            this.Id = 0;
            this.ItenId = ItenId;
            this.PedidoId = PedidoId;
            this.Nome = Nome;
            this.Cliente = Cliente;
            this.ValorUnitario = ValorUnitario;
            this.ValorTotal = ValorUnitario * Quantidade;
            this.Quantidade = Quantidade;
            this.StatusPedidoId = StatusPedidoId;
            this.DataPedido = DateTime.Now;
            this.TipoProdutoId = TipoProdutoId;
        }

        public string CadastrarItemPedido(ItemPedido itemPedido)
        {
            try
            {
                string _query = "INSERT INTO tipoproduto (Id, Item_Id, Pedido_Id, Cliente," +
                    "ValorUnitario, ValorTotal, DataPedido, TipoProduto_Id )" +
                    "VALUES (@Id,@Item_Id,@Pedido_Id,@Cliente,@ValorUnitario, @ValorTotal, @Quantidade, " +
                    "@DataPedido, @ItemPedido, TipoProduto_Id)";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("Id", itemPedido.Id);
                conn.InserirParametro("Item_Id", itemPedido.ItenId);
                conn.InserirParametro("Pedido_Id", itemPedido.PedidoId);
                conn.InserirParametro("Cliente", itemPedido.Cliente);
                conn.InserirParametro("ValorUnitario", itemPedido.ValorUnitario);
                conn.InserirParametro("ValorTotal", itemPedido.ValorTotal);
                conn.InserirParametro("Quantidade", itemPedido.Quantidade);
                conn.InserirParametro("DataPedido", itemPedido.DataPedido);
                conn.InserirParametro("TipoProduto_Id", itemPedido.TipoProdutoId);


                int _resultado = Convert.ToInt32(conn.ExecutarManipulacao(CommandType.Text, _query));
                if (_resultado.Equals(0))
                    return "Nao foi possivel cadastrar StatusPedido";
                else
                    return "StatusPedido cadastrado com sucesso";
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ItemPedido> BuscarItemPedido(long pedido)
        {
            pedido = 16;
            try
            {
                List<ItemPedido> itemPedido = new List<ItemPedido>();
                string _query = $"SELECT pedidos.id AS Pedido, " +
                                $"itens_pedidos.Id AS Id_Itens, " +
                                $"itens.Nome, " +
                                $"itens_pedidos.Cliente, " +
                                $"itens_pedidos.Quantidade, " +
                                $"itens_pedidos.ValorTotalItens AS Total_Item, " +
                                $"pedidos.Valor_Total AS Total_Pedido " +
                                $"FROM pedidos " +
                                $"INNER JOIN itens_pedidos ON itens_pedidos.Pedido_id = pedidos.id " +
                                $"INNER JOIN itens ON itens.Id = itens_pedidos.Item_id " +
                                $"WHERE pedidos.Id = @Id ";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("@Id", pedido);
                var dt = conn.RetornaDadosTabela(CommandType.Text, _query);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var _idPedido = Convert.ToInt64(dt.Rows[i]["Pedido"]);
                    var _idItens = Convert.ToInt32(dt.Rows[i]["Id_Itens"]);
                    var _nome = dt.Rows[i]["Nome"].ToString();
                    var _cliente = dt.Rows[i]["Cliente"].ToString();
                    var _quantidade = Convert.ToInt32(dt.Rows[i]["Quantidade"]);
                    var _totalItem = Convert.ToInt64(dt.Rows[i]["Total_Item"]);
                    var _totalPedido = Convert.ToInt64(dt.Rows[i]["Total_Pedido"]);

                    ItemPedido item = new ItemPedido(_idItens, _idPedido, _nome, _cliente, 0, _quantidade, 0, 0);
                    itemPedido.Add(item);
                }
                return itemPedido;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool ExcluirItemPedido(long Id)
        {
            try
            {
                string _query = $"DELETE itenspedidos WHERE Id = @Id";
                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("Id", Id);
                conn.ExecutarManipulacao(CommandType.Text, _query);

                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
