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
        private readonly UserManager<IdentityUser<Guid>> userManager;
        private readonly SignInManager<IdentityUser<Guid>> signInManager; 
        private readonly ILogger<UsuarioController> logger;

        public UsuarioController(UsuarioService usuarioService,
                                 UserManager<IdentityUser<Guid>> userManager,
                                 SignInManager<IdentityUser<Guid>> signInManager, 
                                 ILogger<UsuarioController> logger)
        {
            this.usuarioService = usuarioService;
            this.userManager = userManager; 
            this.signInManager = signInManager;
            this.logger = logger;   
        }

        [Authorize(Roles = "GestorAdmin, Gestor")]
        [HttpGet]
        public ActionResult CadastrarUsuario()
        {
              return View();
        }

        [Authorize(Roles = "GestorAdmin, Gestor")]
        [HttpPost]
        public async Task<ActionResult> CadastrarUsuario(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(usuarioViewModel);
            }

            var identityUser = new IdentityUser<Guid>
            {   
                Id = Guid.NewGuid(),
                UserName = usuarioViewModel.Nome,
                PhoneNumber = usuarioViewModel.Telefone,
                Email = usuarioViewModel.Email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(identityUser, usuarioViewModel.Senha);

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
                var rolesResult = await userManager.AddToRoleAsync(identityUser, "GestorAdmin");

                if (!rolesResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Falha ao atribuir a role ao usuário.");
                    return View(usuarioViewModel);
                }
            }
            else
            {
                var rolesResult = await userManager.AddToRoleAsync(identityUser, "subordinado");

                if (!rolesResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Falha ao atribuir a role ao usuário.");
                    return View(usuarioViewModel);
                }
            }

            //Salvar a foto e obter o caminho relativo
            var fotoPath = await SalvarFoto(usuarioViewModel.Foto);

            // Mapear o ViewModel para a entidade do domínio
            Usuario usuario = UsuarioMapper.Map(usuarioViewModel, fotoPath);
            usuario.Id = identityUser.Id;
            //usuario.Foto = fotoPath; // Salva o caminho relativo na entidade

            ServiceResult serviceResult = usuarioService.CadastrarUsuario(usuario);

            if (!serviceResult.Success)
            {
                ModelState.AddModelError(string.Empty, string.Join(",", serviceResult.Erros));
                return View(usuarioViewModel);
            }

            return RedirectToAction("CadastrarUsuario", "Usuario");
        }

        [Authorize(Roles = "GestorAdmin, Gestor")]
        [HttpGet]
        public ActionResult<Usuario> ConsultarUsuario(UsuarioViewModel usuarioViewModel) 
        {
            ServiceResult<Usuario> serviceResult = usuarioService.ConsultarUsuario(usuarioViewModel.Id);

            if (serviceResult.Success)
            {
                if(serviceResult.Value == null)
                    return View(usuarioViewModel);
                else
                    return Ok(serviceResult.Value);
            }
            else
            {
                return View(usuarioViewModel);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(loginUser);
            }
            //Verificação do Email!
            IdentityUser<Guid>? user = await userManager.FindByEmailAsync(loginUser.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Usuário ou senha incorretos");
                return View(loginUser); 
            }
            //Verificação da Senha!
            var result = await signInManager.PasswordSignInAsync(user, loginUser.Senha, true, true);

            if(result.Succeeded)
            { //Autenticação concluida!
                logger.LogInformation("Login bem-sucedido para o email {Email}", loginUser.Email);

                if(returnUrl == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return Redirect(returnUrl);
                }
                
            }
            else
            { //Autenticação Falha!
                logger.LogWarning("Falha no login: usuário ou senha incorretos para o email {Email}", loginUser.Email);
                ModelState.AddModelError( string.Empty, "Usuário ou senha incorretos");
            }

            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult>  Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }

        [HttpGet]
        public ActionResult accessdenied()
        {
            return View();  
        }

        public async Task<string?> SalvarFoto(IFormFile foto)
        {
            if (foto == null || foto.Length == 0)
            {
                return null;
            }

            //Caminho para a pasta de uploads
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            Directory.CreateDirectory(uploadsFolder); // Garante que a pasta exista

            //Nome único para o arquivo  // Extrai a extensão do arquivo enviado
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(foto.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            //Salva o arquivo no sistema
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await foto.CopyToAsync(fileStream); 
            }

            // Retorna o caminho relativo Ex: uploads/arquivo.jpg
            return Path.Combine("uploads", fileName).Replace("\\", "/");
        }
    }
}