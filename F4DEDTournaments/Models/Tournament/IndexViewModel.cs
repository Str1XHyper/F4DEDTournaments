﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F4DEDTournaments.Models.Tournament
{
    public class IndexViewModel
    {
        public List<Logic.Tournament.Tournament> PlannedTournaments { get; set; }
        public List<Logic.Tournament.Tournament> ActiveTournaments { get; set; }
    }
}
