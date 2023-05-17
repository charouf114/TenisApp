using TennisApp.Models;
using TennisApp.Models.Dtos;

namespace TennisApp.Helpers
{
    public static class PlayerExtension
    {
        public static PlayerDto? ToPlayerDto(this Player? player)
        {
            return player == null ? null : new PlayerDto()
            {
                Id = player.Id,
                Firstname = player.Firstname,
                Lastname = player.Lastname,
                Shortname = player.Shortname,
                Sex = player.Sex,
                Picture = player.Picture,
                Height = player.Data.Height,
                Weight = player.Data.Weight,
                Age = player.Data.Age,
                CountryCode = player.Country.Code
            };
        }
    }
}
