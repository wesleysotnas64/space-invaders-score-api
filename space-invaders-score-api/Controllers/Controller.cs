using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using space_invaders_score_api.DBContext;
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
                DBManager db = new();

                List<Player> players = db.GetPlayerList();

                return Ok(players);
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

            Player player = new();
            player.Name = submitScoreRequest.Name;
            player.Score = submitScoreRequest.Score;

            try
            {
                new DBManager().AddPlayer(player);
                return Ok(player);
            }
            catch
            {
                return BadRequest("Error when add database.");
            }
        }
    }
}
