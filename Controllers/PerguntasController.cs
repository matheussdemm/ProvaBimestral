using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using ProvaBimestral.Models;

namespace ProvaBimestral.Controllers
{
    public class PerguntasController : Controller
    {
        private readonly string connectionString = "Server=localhost;Database=bdbimestre2;Uid=root;Pwd=;";
        public static List<Perguntas> IsAlternativas = new List<Perguntas>();
        public IActionResult Index()
        {
            List<Perguntas> IsPerguntas = new List<Perguntas>();
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            var cmd = new MySqlCommand("SELECT id, pergunta FROM perguntas", connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Perguntas u = new Perguntas();
                u.Id = reader.GetInt32("id");
                u.Pergunta = reader.GetString("pergunta");
                IsPerguntas.Add(u);
            }
            connection.Close();

            return View(IsPerguntas);
        }
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Perguntas objeto)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var comando = new MySqlCommand(@"INSERT INTO perguntas (pergunta) VALUES (@pergunta)", connection);
                    comando.Parameters.AddWithValue("@pergunta", objeto.Pergunta);
                    comando.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Você pode adicionar uma mensagem de erro aqui
                ViewBag.Erro = "Erro ao salvar a pergunta: " + ex.Message;
                return View(objeto);
            }
        }

        public ActionResult Delete(int id)
        {
            Perguntas model = new Perguntas();
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            var comando = new MySqlCommand("SELECT id, pergunta FROM perguntas where Id = ?", connection);
            comando.Parameters.AddWithValue("?", id);
            using var reader = comando.ExecuteReader();
            while (reader.Read())
            {
                model.Id = reader.GetInt32("id");
                model.Pergunta = reader.GetString("nome");
            }
            connection.Close();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult Delete(Perguntas dados)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var comando = new MySqlCommand(@"delete from perguntas
                        where Id = @id", connection);

                    comando.Parameters.AddWithValue("@id", dados.Id);
                    comando.ExecuteNonQuery();

                    connection.Close();
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
