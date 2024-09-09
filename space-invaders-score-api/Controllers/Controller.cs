using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using space_invaders_score_api.Entities;
using space_invaders_score_api.Utilities;

namespace space_invaders_score_api.Controllers
{
    [ApiController]
    [Route("space-invader-api/")]
    public class Controller : ControllerBase
    {

        public Controller() { }

        [HttpGet("get-player-list/{key}")]
        public IActionResult GetPlayerList(string key)
        {
            ReturnValidation validation = new();
            validation = new Validation().KeyValidation(key);

            if (validation.Result == false) return BadRequest(validation.Message);

            try
            {
                //Vai ao banco e retorna os players
                List<Player> players = [];

                players.Add(new Player() { Name = "Pixel Man", Score = 10530 });
                players.Add(new Player() { Name = "Bug Boy", Score = 120450 });

                return Ok(players.OrderByDescending(player => player.Score));
            }
            catch
            {
                return BadRequest("Error when request database.");
            }
        }

        [HttpPost("add-player")]
        public IActionResult AddPlayer([FromBody] SubmitPlayerRequest submitScoreRequest)
        {

            ReturnValidation validation = new();
            validation = new Validation().KeyValidation(submitScoreRequest.Key);

            if (validation.Result == false) return BadRequest(validation.Message);

            //Aqui adiciona o score atual

            return Ok(submitScoreRequest);
        }
    }
}
