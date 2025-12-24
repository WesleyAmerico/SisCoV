using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SisCoV.Models;
using SistAL.Models.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SisCoV.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public PedidoController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CategoriaPedidos()
        {
            return View();
        }

        public IActionResult ExibirLanchesPedidos() 
        {
            return View();
        }
        public IActionResult ExibirPorcoesPedidos()
        {
            return View();
        }
        public IActionResult ExibirBebidasPedidos()
        {
            return View();
        }
        public IActionResult LanchesPedidos()
        {
            try
            {
                ExibirPedidos exibirPedidos = new ExibirPedidos();
                var _listaLanchesPedidos = exibirPedidos.ExibirFilaLanchesPedidos();


                ConverterObjectToJson converterObjtectToJson = new ConverterObjectToJson();
                var _jsonListaLanchesPedidos = converterObjtectToJson.ConverteObjectParaJSon(_listaLanchesPedidos);

                return Ok(_jsonListaLanchesPedidos);


            }
            catch (Exception)
            {

                throw;
            }
            
        }
       
        public IActionResult CriarPedido(int idMesa, string NomeCliente)
        {
            try
            {
                string Resposta = string.Empty;
                Mesa mesa = new Mesa();
                var _statusMesa = mesa.VerificarStatusMesa(idMesa);

                ConverterObjectToJson converterObjtectToJson = new ConverterObjectToJson();
                if (_statusMesa) 
                {
                    Resposta = "Não é possivel criar pedido em mesa ocupada, escolha outra mesa, por favor!";
                    var _jsonStatusMesa = converterObjtectToJson.ConverteObjectParaJSon(_statusMesa);
                    return Ok(_jsonStatusMesa);
                }
                else
                {
                    Pedido pedido = new Pedido(0, idMesa, 1, 0);
                    pedido.CadastrarPedido(pedido);
                    return Redirect("Pedido/CategoriaPedidos");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IActionResult FecharPedido(long IdPedido, int IdMesa, int TipoPagamento)
        {
            try
            {
                Pedido pedido = new Pedido(IdPedido, IdMesa, 1,TipoPagamento);
                pedido.FecharPedido(pedido);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok();
        }

        public IActionResult VerificarStatusMesa(int Id)
        {
            try
            {
                Mesa mesa = new Mesa();
                var _statusMesa = mesa.VerificarStatusMesa(Id);

                ConverterObjectToJson converterObjtectToJson = new ConverterObjectToJson();
                var _jsonStatusMesa = converterObjtectToJson.ConverteObjectParaJSon(_statusMesa);

                return Ok(_jsonStatusMesa);
            }
            catch (Exception)
            {

                throw;
            }
            

        }

        public IActionResult ItemPedido()
        {
            try
            {
                ItemPedido itemPedido = new ItemPedido();
                var _listItensPedido = itemPedido.BuscarItemPedido(1);

                ConverterObjectToJson converterObjtectToJson = new ConverterObjectToJson();
                var _jsonListItensPedido = converterObjtectToJson.ConverteObjectParaJSon(_listItensPedido);

                return Ok(_jsonListItensPedido);
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        public IActionResult GetTipoPagamento()
        {
            try
            {
                TipoPagamento tipoPagamento = new TipoPagamento();
                var _listTipoPagamento = tipoPagamento.BuscarTipoPagamento();

                ConverterObjectToJson converterObjtectToJson = new ConverterObjectToJson();
                var _jsonListTipoPagamento = converterObjtectToJson.ConverteObjectParaJSon(_listTipoPagamento);

                return Ok(_jsonListTipoPagamento);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IActionResult GetIdPedido(int idMesa)
        {
            try
            {
                Pedido _pedido = new Pedido();
                var _idPedido = _pedido.RetornaIdPedido(idMesa);

                ConverterObjectToJson converterObjtectToJson = new ConverterObjectToJson();
                var _jsonListTipoPagamento = converterObjtectToJson.ConverteObjectParaJSon(_idPedido);

                return Ok(_jsonListTipoPagamento);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
