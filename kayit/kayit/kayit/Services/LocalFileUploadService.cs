using Microsoft.AspNetCore.Mvc;

namespace kayit.Services
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment environment;
        public LocalFileUploadService(Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            this.environment = environment;
        }
        [BindProperty]
        public IFormFile file { get; set; }


        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }



        public async Task<string> UploadFileAsync(IFormFile file)
        {
            string uzanti = file.FileName;
          
            string temp = RandomString(32);
            string temp2 = file.FileName;
            if(temp2.Contains(".png") == true)
            {
                temp = temp + ".png";
            }
            else if (temp2.Contains(".PNG") == true)
            {
                temp = temp + ".PNG";
            }
            else if (temp2.Contains(".jpg") == true)
            {
                temp = temp + ".jpg";
            }
            else if (temp2.Contains(".JPG") == true)
            {
                temp = temp + ".JPG";
            }
            else if (temp2.Contains(".JPEG") == true)
            {
                temp = temp + ".JPEG";
            }
            else if (temp2.Contains(".jpeg") == true)
            {
                temp = temp + ".jpeg";
            }

            var filePath = Path.Combine(environment.ContentRootPath,@"wwwroot/images",temp);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            filePath = "images/"+temp;
            return filePath;

        }
    }
}
