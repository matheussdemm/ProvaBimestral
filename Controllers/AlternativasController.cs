using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using ProvaBimestral.Models;

namespace ProvaBimestral.Controllers
{
    public class AlternativasController : Controller
    {
        private readonly string connectionString = "Server=localhost;Database=bdbimestre2;Uid=root;Pwd=;";
        public static List<Alternativas> IsAlternativas = new List<Alternativas>();
        public IActionResult Index()
        {
            List<Alternativas> IsPerguntas = new List<Alternativas>();
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            var cmd = new MySqlCommand("SELECT id, alternativas FROM perguntas", connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Alternativas u = new Alternativas();
                u.Id = reader.GetInt32("id");
                u.Pergunta = reader.GetString("alternativas");
                IsPerguntas.Add(u);
            }
            connection.Close();
            return View(IsAlternativas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Alternativas objeto)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var comando = new MySqlCommand(@"Insert into perguntas (Nome) values (?)", connection);
                    comando.Parameters.AddWithValue("?", objeto.Pergunta);
                    comando.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
