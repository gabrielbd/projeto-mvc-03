using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoAspNetMVC03.Data.Entities;
using ProjetoAspNetMVC03.Data.Interfaces;
using ProjetoAspNetMVC03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAspNetMVC03.Controllers
{
    [Authorize]
    public class TarefaController : Controller
    {
        //atributos
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public TarefaController(ITarefaRepository tarefaRepository, IUsuarioRepository usuarioRepository)
        {
            _tarefaRepository = tarefaRepository;
            _usuarioRepository = usuarioRepository;
        }


        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost] 
        public IActionResult Cadastro(TarefaCadastroModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var email = User.Identity.Name;
                    var usuario = _usuarioRepository.Obter(email);

                    var tarefa = new Tarefa();

                    tarefa.Nome = model.Nome;
                    tarefa.Data = DateTime.Parse(model.Data);
                    tarefa.Hora = TimeSpan.Parse(model.Hora);
                    tarefa.Descricao = model.Descricao;
                    tarefa.Prioridade = model.Prioridade.ToString();
                    tarefa.IdUsuario = usuario.IdUsuario; //chave estrangeira

                    //cadastrando a tarefa
                    _tarefaRepository.Inserir(tarefa);

                    TempData["Mensagem"] = $"Tarefa {tarefa.Nome}, cadastrada com sucesso.";
                    ModelState.Clear();
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }
            }

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
