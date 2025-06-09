using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProvaBimestral.Models;

namespace ProvaBimestral.Controllers
{
    public class PerguntasController : Controller
    {
        private readonly string connectionString = "Server=localhost;Database=bdbimestre2;Uid=root;Pwd=;";
        public static 
        public IActionResult Index()
        {
            List<Perguntas> IsPerguntas = new List<Perguntas>();
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            var cmd = new MySqlCommand("SELECT id, nome FROM perguntas", connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Perguntas u = new Perguntas();
                u.Id = reader.GetInt32("id");
                u.Nome = reader.GetString("nome");
                model.Add(u);
            }
            connection.Close();

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Perguntas objeto)
        {
            IsPerguntas.Add(objeto);
            return RedirectToAction("Index");
        }
    }
}
