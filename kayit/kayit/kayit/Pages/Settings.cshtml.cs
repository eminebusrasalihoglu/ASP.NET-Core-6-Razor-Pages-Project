using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kayit.Models;
using kayit.Services;
using System.Security.Claims;
using kayit.Model;

namespace kayit.Pages
{
    [Authorize]
    public class SettingsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager;
        private readonly IFileUploadService fileUploadService;

        public SettingsModel(ILogger<IndexModel> logger, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager, JsonProfilService JsonProfilService, IFileUploadService fileUploadService)
        {
            _logger = logger;
            this.userManager = userManager;
            jsonProfilService = JsonProfilService;
            this.fileUploadService = fileUploadService;
        }

        public string FilePath;

        JsonProfilService jsonProfilService;

        [BindProperty]
        public ProfilModel gonderiw { get; set; }

        public Microsoft.AspNetCore.Identity.UserManager<IdentityUser> UserManager { get; private set; }

        public string Func()
        {
            string currentUserId = User.Identity.GetUserId();
            return currentUserId;
        }
        public string Func2()
        {
            string currentUserId = User.Identity.GetUserName();
            return currentUserId;
        }
        public List<ProfilModel> Projects;

        public void OnGet()
        {
            Projects = jsonProfilService.GetProjects();
        }
        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }

        public async void OnPost(IFormFile file)
        {
            if (file != null)
            {
                FilePath = await fileUploadService.UploadFileAsync(file);
            }
        }

        public IActionResult OnPostForm()
        {

                jsonProfilService.AddProject(gonderiw);

            return RedirectToPage("/Index", new { Status = "Success" });

        }


       





    }
}