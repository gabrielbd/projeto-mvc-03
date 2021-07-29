using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoAspNetMVC03.Data.Interfaces;
using ProjetoAspNetMVC03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Controllers
{
    [Authorize] 
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IActionResult MeusDados()
        {
            try
            {
                var email = User.Identity.Name;
                var usuario = _usuarioRepository.Obter(email);

                TempData["IdUsuario"] = usuario.IdUsuario;
                TempData["Nome"] = usuario.Nome;
                TempData["Email"] = usuario.Email;
                TempData["DataCadastro"] = usuario.DataCadastro.ToString("dd/MM/yyyy");
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View();
        }

        public IActionResult EditarSenha()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EditarSenha(UsuarioEditarSenhaModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var email = User.Identity.Name;
                    var usuario = _usuarioRepository.Obter(email);

                    if (_usuarioRepository.Obter(usuario.Email, model.SenhaAtual) != null)
                    {
                        _usuarioRepository.Alterar(usuario.IdUsuario, model.NovaSenha);
                        TempData["Mensagem"] = "Nova senha atualizada com sucesso. Saia e entre novamente no sistema para atualizar.";
                    }
                    else
                    {
                        TempData["Mensagem"] = "Sua senha atual está incorreta, por favor tente novamente.";
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


