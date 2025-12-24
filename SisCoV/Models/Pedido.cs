using AcessoBancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisCoV.Models
{
    public class Pedido
    {
        public long Id { get; set; }
        public int MesaId { get; set; }
        public DateTime HorarioAberto { get; set; }
        public DateTime HorarioFechado { get; set; }
        public int StatusPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public int TipoPagamento { get; set; }

        public Pedido() { }
        public Pedido(long id, int Mesa, int statusPedido, int tipoPagamento)
        {
            this.Id = id;
            this.MesaId = Mesa;
            this.HorarioAberto = DateTime.Now;
            this.HorarioFechado = DateTime.Now.AddHours(-1);
            this.StatusPedido = statusPedido;
            this.TipoPagamento = tipoPagamento;
        } 

        public bool CadastrarPedido(Pedido pedido) 
        {
            try
            {
                string _query = "INSERT INTO pedidos (Mesa_id, Horario_Aberto, Horario_Fechamento, Status_Pedido, Tipo_Pagamento, Valor_Total)" +
                    $"Values ( @MesaId, @HorarioAberto, @HorarioFechamento, @StatusPedido, @TipoPagamento, @ValorTotal)";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("@MesaId", pedido.MesaId);
                conn.InserirParametro("@HorarioAberto", pedido.HorarioAberto);
                conn.InserirParametro("@HorarioFechamento", pedido.HorarioFechado);
                conn.InserirParametro("@StatusPedido", pedido.StatusPedido);
                conn.InserirParametro("@TipoPagamento", pedido.TipoPagamento);
                conn.InserirParametro("@ValorTotal", 0.0d);
                var statusRetornoBanco = Convert.ToInt32(conn.ExecutarManipulacao(CommandType.Text, _query));

                if (pedido.MesaId != 0 && statusRetornoBanco.Equals(1)) 
                {
                    Mesa mesa = new Mesa(pedido.MesaId, pedido.MesaId, true);
                    mesa.OcuparMesa(pedido.MesaId);
                    return true;
                }
                else
                return false;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public bool FecharPedido(Pedido pedido)
        {
            try
            {
                string _query = $"UPDATE pedidos SET HorarioFechado = @HorarioFechado, " +
                    $"Statuspedido = @StatusPedido, TipoPagamento = @TipoPagamento WHERE pedido = {pedido.Id}";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("@HorarioFechado",DateTime.Now);
                conn.InserirParametro("@StatusPedido",pedido.StatusPedido);
                conn.InserirParametro("@TipoPagamento",pedido.TipoPagamento);
                conn.ExecutarManipulacao(CommandType.Text, _query);

                Mesa _mesa = new Mesa(pedido.MesaId, pedido.MesaId, true);
                _mesa.LiberarMesa();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public long RetornaIdPedido(int idMesa)
        {
            try
            {
                long _pedido = 0;
                string _sql = "SELECT id FROM Pedidos WHERE Mesa_id = @Mesa_id AND Status_pedido = 1";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("@Mesa_id", idMesa);
                var _dtPedido = conn.RetornaDadosTabela(CommandType.Text, _sql);

                for (int i = 0; i < _dtPedido.Rows.Count; i++)
                {
                    _pedido = Convert.ToInt64(_dtPedido.Rows[i]["Id"]);
                }
                return _pedido;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
