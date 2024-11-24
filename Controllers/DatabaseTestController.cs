using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DreamApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseTestController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DatabaseTestController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    return Ok("бд работает!");
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, $"Ошибка подключения к базе данных: {ex.Message}");
            }
        }
    }
}