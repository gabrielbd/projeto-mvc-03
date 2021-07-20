using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Controllers
{
    [Authorize]
    public class TarefaController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }
        public IActionResult Consulta()
        {
            return View();
        }
        public IActionResult Relatorio()
        {
            return View();
        }
    }
}
