using Microsoft.AspNetCore.Mvc;
using BibliotecaBusiness.Models;
using BibliotecaBusiness.Services;
using WebManagementSystem.ViewModels;
using BibliotecaBusiness.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace WebManagementSystem.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService usuarioService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager; 
        private readonly ILogger<UsuarioController> logger;

        public UsuarioController(UsuarioService usuarioService,
                                 UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signInManager, 
                                 ILogger<UsuarioController> logger)
        {
            this.usuarioService = usuarioService;
            this.userManager = userManager; 
            this.signInManager = signInManager;
            this.logger = logger;   
        }

        
        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
              return View();
        }

        [Authorize(Roles = "GestorAdmin")]
        [HttpPost]
        public async Task<ActionResult> CadastrarUsuario(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(usuarioViewModel);
            }

            var user = new IdentityUser
            {
                UserName = usuarioViewModel.Nome,
                PhoneNumber = usuarioViewModel.Telefone,
                Email = usuarioViewModel.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, usuarioViewModel.Senha);


            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return View(usuarioViewModel);
            }

            if (usuarioViewModel.EhGestor)
            {
                var rolesResult = await userManager.AddToRoleAsync(user, "GestorAdmin");

                if (!rolesResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Falha ao atribuir a role ao usuário.");
                    return View(usuarioViewModel);
                }
            }
            else
            {
                var rolesResult = await userManager.AddToRoleAsync(user, "subordinado");

                if (!rolesResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Falha ao atribuir a role ao usuário.");
                    return View(usuarioViewModel);
                }
            }
     
            Usuario usuario = UsuarioMapper.Map(usuarioViewModel);
            
            ServiceResult serviceResult = usuarioService.CadastrarUsuario(usuario);

            if (!serviceResult.Success)
            {
                ModelState.AddModelError(string.Empty, string.Join(",", serviceResult.Erros));
                return View(usuarioViewModel);
            }

            return RedirectToAction("Index", "Usuario");
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


        //[AllowAnonymous]
        //[HttpPost("login")]
        //public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return ValidationProblem(ModelState);
        //    }

        //    IdentityUser? user = await _userManager.FindByEmailAsync(loginUser.Email);
        //    if (user == null)
        //        return Problem("Usuário ou senha incorretos");

        //    var result = await _signInManager.PasswordSignInAsync(user, loginUser.Password, false, true);

        //    if (result.Succeeded)
        //    {
        //        _logger.LogInformation("Login bem-sucedido para o email {Email}", loginUser.Email);
        //        return Ok(await jwtGeneratorService.GenerateJwtAsync(user));
        //    }

        //    _logger.LogWarning("Falha no login: usuário ou senha incorretos para o email {Email}", loginUser.Email);
        //    return Problem("Usuário ou senha incorretos");
        //}
    }
}