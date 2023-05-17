using System.Text.Json;
using TennisApp.Models;

namespace TennisApp.Services
{
    public class PlayerRepository : IPlayerRepository
    {
        public List<Player> LoadPlayers()
        {
            string jsonData = File.ReadAllText("Data/Players.json");
            var PlayersResponse = JsonSerializer.Deserialize<PlayersResponse>(jsonData, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });

            return PlayersResponse.Players;
        }
    }

}
