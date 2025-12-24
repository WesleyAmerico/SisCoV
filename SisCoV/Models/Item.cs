using AcessoBancoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SisCoV.Models
{
    public class Item
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public int Tipo { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }

        public Item(int id, string Nome, int Tipo, decimal Preco, int Quantidade)
        {
            if (id.Equals(null) || id.Equals(0))
                this.Id = 0;
            else
             this.Id = id;
            this.Nome = Nome;
            this.Tipo = Tipo;
            this.Preco = Preco;
            this.Quantidade = Quantidade;
        }
        public Item(int Tipo)
        {
            this.Tipo = Tipo;
        }

        private Item() { }

        public int CadastrarItem(Item item)
        {
            try
            {
                string _query = "INSERT INTO itens (Id, Nome, Tipo, Preco, Quantidade)" +
                    " VALUES (@Id, @Nome, @Tipo, @Preco, @Quantidade)";

                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                conn.InserirParametro("Id", item.Id);
                conn.InserirParametro("Nome", item.Nome);
                conn.InserirParametro("Tipo", item.Tipo);
                conn.InserirParametro("Preco", item.Preco);
                conn.InserirParametro("Quantidade", item.Quantidade);
                return Convert.ToInt32(conn.ExecutarManipulacao(CommandType.Text,_query));
            }
            catch (Exception ex)
            {
                return -1;
                throw;
            }
        }

        public List<Item> BuscarItens(int tipoProduto) 
        {
            try
            {
                List<Item> itens = new List<Item>();

                string _query = $"SELECT * FROM itens WHERE Tipo = {tipoProduto}";
                ConexaoMySQL conn = new ConexaoMySQL();
                conn.LimparParamentros();
                var _produtos = conn.RetornaDadosTabela(CommandType.Text, _query);

                for (int i = 0; i < _produtos.Rows.Count; i++)
                {
                    var Id = Convert.ToInt32(_produtos.Rows[i]["Id"]);
                    var Nome = _produtos.Rows[i]["Nome"].ToString();
                    var Preco = Convert.ToDecimal(_produtos.Rows[i]["Preco"]);
                    var Tipo = Convert.ToInt32(_produtos.Rows[i]["Tipo"]);
                    var Quantidade = Convert.ToInt32(_produtos.Rows[i]["Quantidade"]);

                    Item item = new Item(Id, Nome, Tipo, Preco, Quantidade);

                    /*item.Id = Convert.ToInt32(_produtos.Rows[i]["Id"]);
                    item.Nome = _produtos.Rows[i]["Nome"].ToString();
                    item.Preco = Convert.ToDecimal(_produtos.Rows[i]["Preco"]);
                    item.Tipo = Convert.ToInt32(_produtos.Rows[i]["Tipo"]);
                    item.Quantidade = Convert.ToInt32(_produtos.Rows[i]["Quantidade"]);*/

                    itens.Add(item);
                }
                return itens;
            }
            catch (Exception ex)
            {

                //throw ex;
                return null;
            }
        }
    }
}
