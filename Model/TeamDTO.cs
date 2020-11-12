using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum Games
    {
        CounterStrike = 1,
        CallOfDuty = 2,
        RainbowSixSiege = 3,
        Valorant = 4,
    }

    public enum Countries
    {
        UnitedKingdom = 1,
        UnitedStates = 2,
        Canada = 3,
        Netherlands = 4,
        Belgium = 5,
        France = 6,
        Germany = 7,
        Japan = 8,
        China = 9
    }

    public enum Languages
    {
        English = 1,
        Dutch = 2,
        French = 3,
        German = 4,
        Japanese = 5,
        Chinees = 6
    }

    public class TeamDTO
    {
        public string TeamID { get; set; }
        public string TeamName { get; set; }
        public int MinimumElo { get; set; }
        public bool IsPrivate { get; set; }
        public string Description { get; set; }
        public Countries Country { get; set; }
        public Languages Language { get; set; }
        public int MinimumAge { get; set; }
        public Games PlayedGame { get; set; }
    }
}
