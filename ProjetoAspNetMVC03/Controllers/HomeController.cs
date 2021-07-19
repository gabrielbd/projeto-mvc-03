using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Controllers
{
    [Authorize] //Permitir somente usuarios autenticados!
    public class HomeController : Controller
    {
        //método para abrir a página /Home/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}
