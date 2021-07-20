using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProjetoAspNetMVC03.Data.Entities;
using ProjetoAspNetMVC03.Data.Interfaces;
using ProjetoAspNetMVC03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Controllers
{
    public class AccountController : Controller
    {
        //atributo para acessar os métodos do repositorio (IUsuarioRepository)
        private readonly IUsuarioRepository _usuarioRepository;

        //método construtor atraves do qual do AspNet MVC irá inicializar
        //a interface IUsuarioRepository (injeção de dependencia)
        public AccountController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        //método utilizado para abrir a página de Login
        public IActionResult Login()
        {
            return View();
        }

        //método executado pelo formulário da página
        //chamado pelo SUBMIT (HTTP POST)
        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {
            //verificar se todos os campos passaram
            //nas regras de validação com sucesso!
            if (ModelState.IsValid)
            {
                try
                {
                    //consultar o usuario no banco de dados atraves do email e senha
                    var usuario = _usuarioRepository.Obter(model.Email, model.Senha);

                    //verificar se o usuario foi encontrado
                    if (usuario != null)
                    {
                        //criando a autenticação do usuario
                        var autenticacao = new ClaimsIdentity(
                            new[] { new Claim(ClaimTypes.Name, usuario.Email) },
                            CookieAuthenticationDefaults.AuthenticationScheme
                            );

                        //gravar em cookie a autenticação
                        var claim = new ClaimsPrincipal(autenticacao);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claim);

                        //redirecionar para a página /Home/Index
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        TempData["Mensagem"] = "Acesso negado.";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = "Erro: " + e.Message;
                }
            }

            return View();
        }

        //método para abrir a página /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost] //receber os campos enviados pelo SUBMIT do formulário
        public IActionResult Register(AccountRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = new Usuario();

                    usuario.Nome = model.Nome;
                    usuario.Email = model.Email;
                    usuario.Senha = model.Senha;

                    //verificar se o email informado já existe no banco de dados
                    if (_usuarioRepository.Obter(usuario.Email) != null)
                    {
                        TempData["Mensagem"] = "O email informado já encontra-se cadastrado.";
                    }
                    else
                    {
                        _usuarioRepository.Inserir(usuario);

                        TempData["Mensagem"] = $"Usuário {usuario.Nome}, cadastrado com sucesso!";
                        ModelState.Clear(); //limpar os campos do formulário
                    }
                }
                catch (Exception e)
                {
                    //gerar uma mensagem de erro..
                    TempData["Mensagem"] = "Erro: " + e.Message;
                }
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
