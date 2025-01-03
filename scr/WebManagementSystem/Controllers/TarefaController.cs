using BibliotecaBusiness.Models;
using BibliotecaBusiness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebManagementSystem.Mappers;
using WebManagementSystem.ViewModels;

namespace WebManagementSystem.Controllers
{
    public class TarefaController : Controller
    {
        private readonly TarefaService tarefaService;
        
        public TarefaController(TarefaService tarefaService)
        {
            this.tarefaService = tarefaService;              
        }

        [Authorize(Roles = "GestorAdmin, Gestor")]
        [HttpGet]
        public ActionResult CadastrarTarefa()
        {
            return View();
        }

        [Authorize(Roles = "GestorAdmin, Gestor")]
        [HttpPost] 
        public ActionResult<TarefaViewModel> CadastrarTarefa(TarefaViewModel tarefaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(tarefaViewModel);
            }

            Tarefa tarefa = TarefaMapper.Map(tarefaViewModel);   

            ServiceResult serviceResult = tarefaService.CadastrarTarefa(tarefa); 

            if (!serviceResult.Success)
            {
                ModelState.AddModelError(string.Empty, string.Join(",", serviceResult.Erros));
                return View(tarefaViewModel);
            }

            TarefaViewModel viewModel = TarefaMapper.Map(tarefa);

            return RedirectToAction("CadastrarTarefa", "Tarefa");
        }


        [Authorize(Roles = "GestorAdmin, Gestor, Subordinado")]
        [HttpGet]
        public ActionResult<List<TarefaViewModel>> ConsultarTarefas()
        {
            string? userId = (User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Usuário não atenticado");  
            }

            Guid gestorId;
            Guid usuarioId;

            if (!Guid.TryParse(userId, out gestorId))
            {
                return BadRequest("O ID do usuário inválido.");
            }

            if (!Guid.TryParse(userId, out usuarioId))
            {
                return BadRequest("O ID do usuário inválido.");
            }

            ServiceResult<List<Tarefa>?> serviceResult;

            if (User.IsInRole("Subordinado"))
            {
                serviceResult = tarefaService.ConsultarTarefaPorUsuario(usuarioId);
            }
            else
            {
                serviceResult = tarefaService.ConsultarTarefasPorGestor(gestorId);
            }

            if (serviceResult.Success)
            {
                if (serviceResult.Value == null)
                    return View(null);
                else
                {
                    List<TarefaViewModel> viewModel = TarefaMapper.Map(serviceResult.Value);
                    return View(viewModel);
                }                  
            }
            else
            {
                return View("Error");            
            }
        }

        [Authorize(Roles = "GestorAdmin, Gestor")]
        [HttpPost]
        public ActionResult AlterarStatusDaTarefa(Guid id, StatusTarefa statusTarefa)
        {

            ServiceResult serviceResult = tarefaService.MudarStatusDaTarefa(id, statusTarefa);

            if (serviceResult.Success)
            {
                return RedirectToAction("ConsultarTarefas", "Tarefa");
            }
            else
            {
                return View("Error");
            }
        }
    }
}
