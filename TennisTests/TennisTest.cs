using Moq;
using TennisApp.Models;
using TennisApp.Services;

namespace TennisTests
{
    public class TennisTest
    {
        private IPlayerService _playerService;

        public TennisTest()
        {
            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(p => p.LoadPlayers()).Returns(LoadTestPlayers);
            _playerService = new PlayerService(playerRepositoryMock.Object);
        }

        private List<Player> LoadTestPlayers()
        {
            var players = new List<Player>();
            string[] countries = { "ESP", "USA", "SUI", "SRB" };
            for (int i = 1; i <= 20; i++)
            {
                players.Add(new Player()
                {
                    Id = i,
                    Firstname = $"Test__{i}",
                    Lastname = $"BC__{i}",
                    Shortname = $"ABC__{i}",
                    Sex = i % 2 == 0 ? "F" : "M",
                    Country = new Country()
                    {
                        Picture = "https://data.latelier.co/training/tennis_stats/resources/i.png",
                        Code = countries[i % 4]
                    },
                    Picture = "https://data.latelier.co/training/tennis_stats/resources/i.png",
                    Data = new Data()
                    {
                        Rank = i,
                        Points = 1500 + 10 * i,
                        Weight = 75000 + 200 * i,
                        Height = 170 + i,
                        Age = 25 + i,
                        Last = new List<int>() { i % 2, (i % 3) % 2, (i % 4) % 5, (i % 6) % 2, (i % 7) % 2 }
                    }
                });
            }
            return players;
        }

        [Fact]
        public void GetPlayersTests()
        {
            var players = _playerService.GetPlayers().ToList();
            Assert.Equal(20, players.Count());

            for (int i = 0; i < players.Count() - 1; i++)
            {
                Assert.NotEmpty(players[i].Firstname);
                Assert.NotEmpty(players[i].Lastname);
                Assert.NotEmpty(players[i].Shortname);
                Assert.NotEmpty(players[i].Sex);
                Assert.NotEmpty(players[i].Picture);
                Assert.NotEmpty(players[i].CountryCode);

                Assert.NotEqual(default, players[i].Id);
                Assert.NotEqual(default, players[i].Height);
                Assert.NotEqual(default, players[i].Weight);
                Assert.NotEqual(default, players[i].Rank);
                Assert.NotEqual(default, players[i].Age);

                //Sorted By Rank 
                Assert.True(players[i].Rank < players[i + 1].Rank);
            }
        }

        [Fact]
        public void GetPlayerByIdTests()
        {
            var player = _playerService.GetPlayerById(37);
            Assert.Null(player);

            player = _playerService.GetPlayerById(13);
            Assert.Equal(13, player.Id);
            Assert.Equal("Test__13", player.Firstname);
            Assert.Equal("BC__13", player.Lastname);
            Assert.Equal("ABC__13", player.Shortname);
            Assert.Equal("M", player.Sex);
            Assert.Equal("USA", player.CountryCode);
            Assert.Equal(183, player.Height);
            Assert.Equal(77600, player.Weight);
            Assert.Equal(13, player.Rank);
            Assert.Equal(38, player.Age);
        }

        [Fact]
        public void GetPlayerStatisticsTests()
        {
            var statistics = _playerService.GetStatistics();
            Assert.NotNull(statistics);

            Assert.Equal("SRB", statistics.CountryCode);
            Assert.Equal(23.71, statistics.AvgImc, 2);
            Assert.Equal(180.5, statistics.MedianHeight);
        }
    }
}