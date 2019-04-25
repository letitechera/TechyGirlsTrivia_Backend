using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using TechyGirlsTrivia.Models.Helpers;
using TechyGirlsTrivia.Models.Hubs;
using TechyGirlsTrivia.Models.Models;
using TechyGirlsTrivia.Models.Storage;
using TechyGirlsTrivia.Models.Storage.Tables;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechyGirls.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private IHubContext<GameHub> _hub;
        private IDataAccess _accessData;

        public GameController(IHubContext<GameHub> hub, IDataAccess accessData)
        {
            _hub = hub;
            _accessData = accessData;
        }

        [Route("timer")]
        [HttpGet]
        public IActionResult Get()
        {
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("startTimer", 1));
            return Ok(new { Message = "Request Completed" });
        }

        [Route("question/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetQuestion(int id)
        {
            var question = _accessData.GetQuestion(id);
            await _hub.Clients.All.SendAsync("getQuestion", question);
            return Ok(question);
        }

        [Route("uploadimage")]
        [HttpPost]
        public async Task<IActionResult> PostSpeakerImage()
        {
            try
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    var newFile = await _accessData.LoadUserImage(file);

                    return Ok(new { newFile });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
