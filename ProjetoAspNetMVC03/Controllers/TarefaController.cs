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
                    tarefa.IdUsuario = usuario.IdUsuario; 

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
            try
            {
                var email = User.Identity.Name;
                var usuario = _usuarioRepository.Obter(email);

                var tarefas = _tarefaRepository.ConsultarPorUsuario(usuario.IdUsuario);

                return View(tarefas);
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View();
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                var tarefa = _tarefaRepository.ObterPorId(id);
                _tarefaRepository.Excluir(tarefa);
                TempData["Mensagem"] = $"Tarefa {tarefa.Nome}, excluida com sucesso.";
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(Guid id)
        {
            try
            {
                var tarefa = _tarefaRepository.ObterPorId(id);

                var model = new TarefaEdicaoModel();

                model.IdTarefa = tarefa.IdTarefa;
                model.Nome = tarefa.Nome;
                model.Data = tarefa.Data.ToString("yyyy-MM-dd");
                model.Hora = tarefa.Hora.ToString(@"hh\:mm");
                model.Descricao = tarefa.Descricao;
                model.Prioridade = (PrioridadeTarefa)Enum.Parse(typeof(PrioridadeTarefa), tarefa.Prioridade);

                return View(model); 
            }
            catch (Exception e)
            {
                TempData["Mensagem"] = e.Message;
            }

            return View();
        }

        public IActionResult Relatorio()
        {
            return View();
        }
    }
}


