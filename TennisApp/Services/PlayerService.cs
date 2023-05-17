using TennisApp.Helpers;
using TennisApp.Models;
using TennisApp.Models.Dtos;

namespace TennisApp.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly List<Player> _players;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _players = playerRepository.LoadPlayers();
        }

        public IEnumerable<PlayerDto?> GetPlayers()
        {
            return _players.OrderBy(p => p.Data.Rank).Select(p => p.ToPlayerDto());
        }

        public PlayerDto? GetPlayerById(int id)
        {
            return _players.SingleOrDefault(p => p.Id == id).ToPlayerDto();
        }

        public PlayerStatisticsDto GetStatistics()
        {
            return new PlayerStatisticsDto()
            {
                CountryCode = getCountryCode(),
                AvgImc = getAvgImc(),
                MedianHeight = getMedianHeight(),
            };
        }

        private double getMedianHeight()
        {
            var sortedHeights = _players.Select(player => player.Data.Height).OrderBy(h => h).ToList();
            double medianHeight = sortedHeights[_players.Count / 2];
            if (_players.Count % 2 == 0)
            {
                medianHeight += sortedHeights[(_players.Count / 2) - 1];
                medianHeight /= 2;
            }

            return medianHeight;
        }

        private double getAvgImc()
        {
            // We Need To Multiply By Ten because we need to Do Weight/1000 and height * 100 twice
            return _players.Select(player => player.Data.Weight * 10 / (double)(player.Data.Height * player.Data.Height)).Average();
        }

        private string getCountryCode()
        {
            return _players.GroupBy(player => player.Country.Code)
                           .ToDictionary(c => c.Key, c => c.Sum(player => player.Data.Last.Sum()))
                           .MaxBy(kvp => kvp.Value).Key;
        }
    }

}
