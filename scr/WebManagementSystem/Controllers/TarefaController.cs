using BibliotecaBusiness.Models;
using BibliotecaBusiness.Services;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult CadastrarTarefa()
        {
            return View();
        }

        [HttpPost] 
        public ActionResult<TarefaViewModel> CadastrarTarefa(TarefaViewModel tarefaViewModel)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values.SelectMany(u => u.Errors).Select(erros => erros.ErrorMessage);
                return BadRequest(erros);
            }

            Tarefa tarefa = TarefaMapper.Map(tarefaViewModel);   

            ServiceResult serviceResult = tarefaService.CadastrarTarefa(tarefa); 

            if (!serviceResult.Success)
            {
                return BadRequest(serviceResult.Erros);
            }

            TarefaViewModel viewModel = TarefaMapper.Map(tarefa);

            return viewModel;
        }

        [HttpGet]
        public ActionResult ConsultarTarefa(Guid id)
        {
            ServiceResult<Tarefa> serviceResult = tarefaService.ConsultarTarefa(id);

            if (serviceResult.Success)
            {
                if (serviceResult.Value == null)
                    return NotFound();
                else
                    return Ok(serviceResult.Value);
            }
            else
            {
                return Problem();
            }
        }
    }
}
