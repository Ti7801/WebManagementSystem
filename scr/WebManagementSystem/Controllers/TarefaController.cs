using BibliotecaBusiness.Models;
using BibliotecaBusiness.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WebManagementSystem.Mappers;
using WebManagementSystem.ViewModels;

namespace WebManagementSystem.Controllers
{
    public class TarefaController : Controller
    {
        private readonly TarefaService tarefaService;
        private readonly UsuarioService usuarioService;
        private readonly UserManager<IdentityUser<Guid>> userManager;

        public TarefaController(TarefaService tarefaService, UsuarioService usuarioService,
                                UserManager<IdentityUser<Guid>> userManager)
        {
            this.tarefaService = tarefaService;         
            this.usuarioService = usuarioService;
            this.userManager = userManager;
        }

        [Authorize(Roles = "GestorAdmin, Gestor")]
        [HttpGet]
        public async Task<ActionResult> CadastrarTarefa()
        {
            ServiceResult<List<Usuario>?> usuarios = usuarioService.ObterUsuarios();

            if (!usuarios.HasValue())
            {
                ModelState.AddModelError(string.Empty, "Não foi possível carregar os usuários.");
                return View();
            }

            var subordinados = new List<SelectListItem>();

            foreach (Usuario usuario in usuarios.Value)
            {
                var identityUser = await userManager.FindByIdAsync(usuario.Id.ToString());

                if (identityUser != null)
                {
                    bool subordinado = await userManager.IsInRoleAsync(identityUser, "Subordinado");

                    if (subordinado)
                    {
                        subordinados.Add(new SelectListItem
                        {
                            Value = usuario.Id.ToString(),
                            Text = usuario.Nome
                        });
                    }
                }
            }

            // Passando a lista de subordinados para a View
            ViewBag.UsuariosSubordinados = subordinados;

            return View();
        }

        [Authorize(Roles = "GestorAdmin, Gestor")]
        [HttpPost] 
        public ActionResult<CadastrarTarefaViewModel> CadastrarTarefa(CadastrarTarefaViewModel tarefaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(tarefaViewModel);
            }

            string? userId = (User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Usuário não atenticado");
            }


            Tarefa tarefa = TarefaMapper.Map(tarefaViewModel);
            tarefa.GestorId = Guid.Parse(userId);

            ServiceResult serviceResult = tarefaService.CadastrarTarefa(tarefa); 

            if (!serviceResult.Success)
            {
                ModelState.AddModelError(string.Empty, string.Join(",", serviceResult.Erros));
                return View(tarefaViewModel);
            }

            CadastrarTarefaViewModel viewModel = TarefaMapper.Map(tarefa);

            return RedirectToAction("ConsultarTarefas", "Tarefa");
        }


        [Authorize(Roles = "GestorAdmin, Gestor, Subordinado")]
        [HttpGet]
        public ActionResult<List<CadastrarTarefaViewModel>> ConsultarTarefas()
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
                    List<CadastrarTarefaViewModel> viewModel = TarefaMapper.Map(serviceResult.Value);
                    return View(viewModel);
                }                  
            }
            else
            {
                return View("Error");            
            }
        }

        [Authorize(Roles = "GestorAdmin, Gestor, Subordinado")]
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
