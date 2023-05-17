using TennisApp.Models;

namespace TennisApp.Services
{
    public interface IPlayerRepository
    {
        List<Player> LoadPlayers();
    }
}
