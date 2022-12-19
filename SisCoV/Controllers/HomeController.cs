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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            int a = 5;
            int b = 4;
            int c = 9;
            string resultadoVer;

            if (a > b && a > c)
                resultadoVer = "A MAior";
            else if (b > a && b > c)
                resultadoVer = "B MAior";
            else if(c > a && c > b)
                resultadoVer = "C MAior";

            Item item = new Item(1);
            var retorno = item.BuscarItens(1);

            ConverterObjectToJson converterObjtectToJson = new ConverterObjectToJson();
            var json = converterObjtectToJson.ConverteObjectParaJSon(retorno);
           

            return View();
        }

        public IActionResult ExibirInformacoesNaTela()
        {
           
            Mesa mesa = new Mesa();
            var status = mesa.VerificarStatusMesa();

            ConverterObjectToJson c = new ConverterObjectToJson();
            var retorno = c.ConverteObjectParaJSon(status);

            return Ok(retorno);
        }
        public IActionResult Teste(int idMesa)
        {

            Pedido pedido = new Pedido(0, idMesa, 1, 0);
            pedido.CadastrarPedido(pedido);

            return Ok();
        }

        public IActionResult Privacy(long IdPedido)
        {
            ItemPedido itemPedido = new ItemPedido();
            var _listItensPedido = itemPedido.BuscarItemPedido(IdPedido);

            ConverterObjectToJson converterObjtectToJson = new ConverterObjectToJson();
            var _jsonListItensPedido = converterObjtectToJson.ConverteObjectParaJSon(_listItensPedido);

            return Ok(_jsonListItensPedido);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
