using Microsoft.AspNetCore.Mvc;
using BibliotecaBusiness.Models;
using BibliotecaBusiness.Services;
using WebManagementSystem.ViewModels;
using BibliotecaBusiness.Mappers;

namespace WebManagementSystem.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            this.usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
              return View();
        }

        [HttpPost]
        public ActionResult<UsuarioViewModel> CadastrarUsuario(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid) 
            { 
                var erros = ModelState.Values.SelectMany(u => u.Errors).Select(erros => erros.ErrorMessage);  
                return BadRequest(erros);
            }

            Usuario usuario = UsuarioMapper.Map(usuarioViewModel);
            
            ServiceResult serviceResult = usuarioService.CadastrarUsuario(usuario);

            if (!serviceResult.Success)
            {
                return BadRequest(serviceResult.Erros);
            }

            UsuarioViewModel viewModel = UsuarioMapper.Map(usuario);

            return viewModel;
        }


        [HttpGet]
        public ActionResult<Usuario> ConsultarUsuario(Guid id) 
        {

            ServiceResult<Usuario> serviceResult = usuarioService.ConsultarUsuario(id);

            if (serviceResult.Success)
            {
                if(serviceResult.Value == null)
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