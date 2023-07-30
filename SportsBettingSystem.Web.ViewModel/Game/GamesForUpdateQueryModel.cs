using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsBettingSystem.Web.ViewModels.Game
{
    public class GamesForUpdateQueryModel
    {
        public GamesForUpdateQueryModel()
        {
            CurrentPage = 1;
            GamesForUpdate = new HashSet<GameUpdateServiceModel>();
            Leagues = new HashSet<string>();
        }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int GamesCount { get; set; }
        public int CurrentPage { get; set; }
        
        [Display(Name = "Search by team name")]
        public string? SearchString { get; set; }

        public string? League { get; set; }
        public IEnumerable<string> Leagues { get; set; }
        public IEnumerable<GameUpdateServiceModel> GamesForUpdate { get; set; }
    }
}
