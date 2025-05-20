using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebManagementSystem.Controllers
{
    [Authorize(Roles = "GestorAdmin, Gestor, Subordinado")]
    public class HomeController : Controller
    {       
        [HttpGet]
        public ActionResult Index()
        {
            
            return RedirectToAction("ConsultarTarefas", "Tarefa");  
        }
    }
}
