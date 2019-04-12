using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
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

        [Route("timer")]
        [HttpGet]
        public IActionResult Get()
        {
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("getTimer", 1));
            return Ok(new { Message = "Request Completed" });
        }

        [Route("register")]
        [HttpPost]
        public Participant RegisterUser([FromBody]Participant p)
        {
            p.ParticipantId = Guid.NewGuid().ToString();
            _hub.Clients.All.SendAsync("registerUser", p);
            return p;
        }
    }
}
