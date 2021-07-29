using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProjetoAspNetMVC03.Data.Entities;
using ProjetoAspNetMVC03.Data.Interfaces;
using ProjetoAspNetMVC03.Messages;
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
        private readonly IUsuarioRepository _usuarioRepository;

        public AccountController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(AccountLoginModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = _usuarioRepository.Obter(model.Email, model.Senha);

                    if (usuario != null)
                    {
                        var autenticacao = new ClaimsIdentity(
                            new[] { new Claim(ClaimTypes.Name, usuario.Email) },
                            CookieAuthenticationDefaults.AuthenticationScheme
                            );

                        var claim = new ClaimsPrincipal(autenticacao);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claim);

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
        public IActionResult PasswordRecovery()
        {
            return View();
        }
        [HttpPost] //receber o SUBMIT POST do formulário
        public IActionResult PasswordRecovery(AccountPasswordRecoveryModel model)
        {
            //verificar se não há erros de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //buscar o usuario no banco de dados atraves do email informado..
                    var usuario = _usuarioRepository.Obter(model.Email);

                    //verificar se o usuario foi encontrado
                    if (usuario != null)
                    {
                        //gerar uma nova senha composta apenas de numeros aleatorios..
                        var novaSenha = new Random().Next(99999999, 999999999).ToString();
                        //atualizar a senha do usuario no banco de dados
                        _usuarioRepository.Alterar(usuario.IdUsuario, novaSenha);

                        //enviando a nova senha por email para o usuario
                        var to = usuario.Email;
                        var subject = "Nova senha gerada com sucesso - Sistema de controle de tarefas";
                        var body = $@"
                            <div style='text-align: center; margin: 40px; padding: 60px; border: 2px solid #ccc; font-size: 16pt;'>
                            <img src='https://plancredi.com.br//simulation/rotina/imagens/form_logo.png' />
                            <br/><br/>
                            Olá <strong>{usuario.Nome}</strong>,
                            <br/><br/>    
                            O sistema gerou uma nova senha para que você possa acessar sua conta.<br/>
                            Por favor utilize a senha: <strong>{novaSenha}</strong>
                            <br/><br/>
                            Não esqueça de, ao acessar o sistema, atualizar esta senha para outra
                            de sua preferência.
                            <br/><br/>              
                            Att<br/>   
                            Equipe PLANCREDI
                            </div>
                        ";

                        var message = new EmailServiceMessage();
                        message.EnviarMensagem(to, subject, body);

                        TempData["Mensagem"] = $"Uma nova senha foi gerada com sucesso e enviada para o email {usuario.Email}.";
                        ModelState.Clear(); //limpar o formulário
                    }
                    else
                    {
                        TempData["Mensagem"] = "O email informado não está cadastrado no sistema.";
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

            return View();
        }
    }
}
