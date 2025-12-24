using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SisCoV.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SisCoV.Controllers
{
    public class CadastroController : Controller
    {
        private readonly ILogger<CadastroController> _logger;

        public CadastroController(ILogger<CadastroController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ItemPedido itemPedido = new ItemPedido();
            itemPedido.BuscarItemPedido(1);
            return View();
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
