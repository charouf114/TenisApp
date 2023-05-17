using TennisApp.Models.Dtos;

namespace TennisApp.Services
{
    public interface IPlayerService
    {
        IEnumerable<PlayerDto?> GetPlayers();
        PlayerDto? GetPlayerById(int id);
        PlayerStatisticsDto GetStatistics();

    }
}
