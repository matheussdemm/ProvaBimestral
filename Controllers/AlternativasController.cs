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
            List<Alternativas> IsAlternativas = new List<Alternativas>();

            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            var cmd = new MySqlCommand(
                @"SELECT a.id, a.id_pergunta, a.correta, a.alternativa, p.pergunta
          FROM alternativas a
          INNER JOIN perguntas p ON a.id_pergunta = p.id", connection);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Alternativas a = new Alternativas
                {
                    Id = reader.GetInt32("id"),
                    Id_Pergunta = reader.GetInt32("id_pergunta"),
                    Correta = reader.GetBoolean("correta"),
                    AlternativaTexto = reader.GetString("alternativa"),
                    Pergunta = new Perguntas
                    {
                        Id = reader.GetInt32("id_pergunta"),
                        Pergunta = reader.GetString("pergunta")
                    }
                };

                IsAlternativas.Add(a);
            }

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
                    var comando = new MySqlCommand(@"Insert into alternativas (id_pergunta, correta, alternativa) values (@id_pergunta, @correta, @alternativa)", connection);
                    comando.Parameters.AddWithValue("@id_pergunta", objeto.Id_Pergunta);
                    comando.Parameters.AddWithValue("@correta", objeto.Correta);
                    comando.Parameters.AddWithValue("@alternativa", objeto.AlternativaTexto);
                    comando.ExecuteNonQuery();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            Alternativas model = new Alternativas();
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            var comando = new MySqlCommand("SELECT id, id_pergunta, correta, alternativa FROM alternativas where Id = ?", connection);
            comando.Parameters.AddWithValue("?", id);
            using var reader = comando.ExecuteReader();
            while (reader.Read())
            {
                model.Id = reader.GetInt32("id");
                if (!reader.IsDBNull(reader.GetOrdinal("id_pergunta")))
                {
                    model.Id_Pergunta = reader.GetInt32("id_pergunta");
                }
                model.Correta = reader.GetBoolean("correta");
                model.AlternativaTexto = reader.GetString("alternativa");
            }
            connection.Close();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult Delete(Alternativas dados)
        {
            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    var comando = new MySqlCommand(@"delete from alternativas
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
