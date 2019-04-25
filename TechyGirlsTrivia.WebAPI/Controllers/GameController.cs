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


        [HttpGet]
        public IActionResult GetQuestionTimer()
        {
            var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("startTimer", GetQuestion()));
            return Ok(new { Message = "Request Completed" });
        }


        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody]Participant p)
        {
            try
            {
                p.ParticipantId = Guid.NewGuid().ToString();
                var pEntity = new ParticipantsTableEntity(p);

                if (_accessData.AlreadyExists(p.ParticipantName))
                {
                    return NoContent();
                }

                //save
                await _accessData.StoreEntity(pEntity, "Participants");

                //broadcast list:
                var returnList = _accessData.GetParticipants(p.GameId);
                await _hub.Clients.All.SendAsync("registerUser", returnList);

                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("question")]

        [HttpGet]
        public async Task<IActionResult> GetQuestion()
        {
            var question = _accessData.GetQuestion();
            if (question != null)
            {
                var update = _accessData.UpdateIsAnsweredAsync(question);
            }
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

        [Route("deleteUsers")]
        [HttpPost]
        public  IActionResult DeleteAllUsers()
        {
            try
            {
                var question = _accessData.DeleteAllUsers();

                return Ok("Users deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("ResetQuestions")]
        [HttpPost]
        public IActionResult ResetIsAnsweredQuestions()
        {
            try
            {
                var question = _accessData.ResetAllIsAnswered();

                return Ok("Questions updated");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
