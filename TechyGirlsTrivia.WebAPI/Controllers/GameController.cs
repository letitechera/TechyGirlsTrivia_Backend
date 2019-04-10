using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TechyGirlsTrivia.Models.Helpers;
using TechyGirlsTrivia.Models.Hubs;
using TechyGirlsTrivia.Models.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechyGirls.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private IHubContext<GameHub> _hub;

        public GameController(IHubContext<GameHub> hub)
        {
            _hub = hub;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferdata", new Question { QuestionId = 0, QuestionText = "What's your Name?" }));
            return Ok(new { Message = "Request Completed" });
        }
    }
}
