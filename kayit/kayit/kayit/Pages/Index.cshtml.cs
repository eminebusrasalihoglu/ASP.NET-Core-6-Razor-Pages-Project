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
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager;
        private readonly IFileUploadService fileUploadService;

     
        public IndexModel(ILogger<IndexModel> logger, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager, JsonGonderiService JsonGonderiService, IFileUploadService fileUploadService, JsonProfilService JsonProfilService)
        {
            _logger = logger;
            this.userManager = userManager;
            jsonProfilService = JsonProfilService;
            jsonGonderiService = JsonGonderiService;
            this.fileUploadService = fileUploadService;
        }

        public string FilePath;

        JsonGonderiService jsonGonderiService;
        JsonProfilService jsonProfilService;
        

        [BindProperty]

        public ProfilModel pp { get; set; }

        [BindProperty]
        public GonderiModel gonderim { get; set; }

        public Microsoft.AspNetCore.Identity.UserManager<IdentityUser> UserManager { get; private set; }

        public List<GonderiModel> Projects;
        public List<ProfilModel> Info;
        public string Func()
        {
            string currentUserId = User.Identity.GetUserId();
            return currentUserId;
        }
        

        public void OnGet()
        {
            Projects = jsonGonderiService.GetProjects();
            Info = jsonProfilService.GetProfile(Func());
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

                jsonGonderiService.AddProject(gonderim);
           

            return RedirectToPage("/Index", new { Status = "Success" });

        }

        
        
      
        
       



    }
}