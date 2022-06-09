using kayit.Models;
using System.Text.Json;

namespace kayit.Services
{
    public class JsonGonderiService
    {
        public JsonGonderiService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment;

        public string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "gonderi.json"); }

        }
        


        public List<GonderiModel> GetProjects()
        {
            using var json = File.OpenText(JsonFileName);

            return JsonSerializer.Deserialize<GonderiModel[]>(json.ReadToEnd()).ToList();

        }
        public List<GonderiModel> GetQuestions(string userId)
        {
            using var json = File.OpenText(JsonFileName);

            List<GonderiModel> projects = JsonSerializer.Deserialize<GonderiModel[]>(json.ReadToEnd()).ToList();
            List<GonderiModel> questions = projects.FindAll(x => x.user == userId);
            questions.Reverse();
            return questions;

        }
        
        public void AddProject(GonderiModel newproject)
        {
            List<GonderiModel> projects = GetProjects();
            newproject.id = projects.Max(x => x.id) + 1;
            projects.Add(newproject);

            using var json = File.OpenWrite(JsonFileName);
            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<GonderiModel>>(jsonwriter, projects);
        }
       
        


        public void JsonWriter(List<GonderiModel> projects, bool status)
        {
            FileStream json;

            if (status)
                json = File.Create(JsonFileName);
            else
                json = File.OpenWrite(JsonFileName);

            Utf8JsonWriter jsonwriter = new Utf8JsonWriter(json, new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize<List<GonderiModel>>(jsonwriter, projects);
            json.Close();
        }

    }
}
