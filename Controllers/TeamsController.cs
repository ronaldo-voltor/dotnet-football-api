using FootballAPI.Models;
using FootballAPI.Services;
using Microsoft.AspNetCore.Mvc;
using FootballAPI.DTOs;

namespace FootballAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly TeamService _teamService;

        public TeamsController(TeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Team>>> Get() =>
            await _teamService.GetAsync();

        [HttpGet("{id:length(24)}", Name = "GetTeam")]
        public async Task<ActionResult<Team>> Get(string id)
        {
            var team = await _teamService.GetAsync(id);
            if (team is null) return NotFound();
            return team;
        }

        [HttpPost]
        public async Task<ActionResult<Team>> Create(TeamCreateDto teamDto)
        {
            var team = new Team
            {
                Nombre = teamDto.Nombre,
                Liga = teamDto.Liga

            };
            await _teamService.CreateAsync(team);
            return CreatedAtRoute("GetTeam", new { id = team.Id }, team);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, TeamCreateDto teamDto)
        {
            var existingTeam = await _teamService.GetAsync(id);
            if (existingTeam is null)
                return NotFound();

            // Construir objeto actualizado SIN permitir que el cliente toque el Id
            var updatedTeam = new Team
            {
                Id = existingTeam.Id,
                Nombre = teamDto.Nombre,
                Liga = teamDto.Liga
            };

            await _teamService.UpdateAsync(id, updatedTeam);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var team = await _teamService.GetAsync(id);
            if (team is null) return NotFound();

            await _teamService.RemoveAsync(id);
            return NoContent();
        }
    }
}
