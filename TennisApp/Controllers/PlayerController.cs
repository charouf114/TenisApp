using Microsoft.AspNetCore.Mvc;
using TennisApp.Models.Dtos;
using TennisApp.Services;

namespace TennisApp.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("Players")]
        public ActionResult<IEnumerable<PlayerDto>> Get()
        {
            return Ok(_playerService.GetPlayers());
        }

        [HttpGet("Player/{Id}")]
        public ActionResult<PlayerDto> GetPlayer(int Id)
        {
            var player = _playerService.GetPlayerById(Id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpGet("Players/Statistics")]
        public ActionResult<PlayerStatisticsDto> GetStatistics()
        {
            return Ok(_playerService.GetStatistics());
        }
    }
}