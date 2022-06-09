using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using kayit.Models;
using kayit.Services;
using System.Security.Claims;

namespace kayit.Pages
{
    [Authorize]
    public class SorModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager;
        private readonly IFileUploadService fileUploadService;


        public SorModel(ILogger<IndexModel> logger, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager, JsonGonderiService JsonGonderiService, IFileUploadService fileUploadService)
        {
            _logger = logger;
            this.userManager = userManager;
            jsonGonderiService = JsonGonderiService;
            this.fileUploadService = fileUploadService;
        }

        public string FilePath;

        JsonGonderiService jsonGonderiService;


        [BindProperty]
        public GonderiModel proje { get; set; }

        public Microsoft.AspNetCore.Identity.UserManager<IdentityUser> UserManager { get; private set; }

        public List<GonderiModel> Projects;
        public string Func()
        {
            string currentUserId = User.Identity.GetUserId();
            return currentUserId;
        }


        public void OnGet()
        {
            Projects = jsonGonderiService.GetProjects();
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

                jsonGonderiService.AddProject(proje);

            return RedirectToPage("/Index", new { Status = "Success" });

        }

       

       

     





    }
}