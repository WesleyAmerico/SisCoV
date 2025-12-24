using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SisCoV.Models;
using SistAL.Models.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            {
                return RedirectToAction("Login");
            }

            Item item = new Item(1);
            var retorno = item.BuscarItens(1);

            ConverterObjectToJson converterObjtectToJson = new ConverterObjectToJson();
            var json = converterObjtectToJson.ConverteObjectParaJSon(retorno);
           

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == "admin" && password == "admin")
            {
                HttpContext.Session.SetString("Username", username);
                return RedirectToAction("Index");
            }
            ViewData["Error"] = "Invalid username or password.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
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
