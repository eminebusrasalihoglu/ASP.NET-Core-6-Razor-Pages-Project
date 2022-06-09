using kayit.Model;
using System.Text.Json;

namespace kayit.Services
{
    public class JsonProfilService
    {
        public JsonProfilService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment;

        public string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "profil.json"); }

        }



        public List<ProfilModel> GetProjects()
        {
            using var json = File.OpenText(JsonFileName);

            return JsonSerializer.Deserialize<ProfilModel[]>(json.ReadToEnd()).ToList();

        }
        

        public void AddProject(ProfilModel newproject)
        {
            List<ProfilModel> projects = GetProjects();
            newproject.id = projects.Max(x => x.id) + 1;
            projects.Add(newproject);

            using var json = File.OpenWrite(JsonFileName);
            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<ProfilModel>>(jsonwriter, projects);
        }
       
       

        public List<ProfilModel> GetProfile(string userId)
        {
            using var json = File.OpenText(JsonFileName);

            List<ProfilModel> projects = JsonSerializer.Deserialize<ProfilModel[]>(json.ReadToEnd()).ToList();
            List<ProfilModel> result = projects.FindAll(x => x.user == userId);
            return result;

        }
        



        public void JsonWriter(List<ProfilModel> projects, bool status)
        {
            FileStream json;

            if (status)
                json = File.Create(JsonFileName);
            else
                json = File.OpenWrite(JsonFileName);

            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<ProfilModel>>(jsonwriter, projects);
            json.Close();
        }

    }
}
