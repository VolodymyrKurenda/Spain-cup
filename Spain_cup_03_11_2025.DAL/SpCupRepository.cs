using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spain_cup_03_11_2025.DAL.Entities;

namespace Spain_cup_03_11_2025.DAL
{
    public class SpCupRepository
    {
        private AppDbContext db;

        public SpCupRepository()
        {
            db = new AppDbContext();
        }
        public void SaveChanges() => db.SaveChanges();
        public List<Club> GetAllClubs() => db.Clubs.ToList();
        public List<Match> GetAllMatches() => db.Matches.ToList();
        public List<Club_Match> GetAllClub_Matches() => db.Club_Matches.ToList();
        public List<Goal> GetAllGoals() => db.Goals.ToList();
        public List<Player> GetAllPlayers() => db.Players.ToList();
        public void AddClub(Club club) => db.Add(club);
        public void AddMatch(Match match) => db.Add(match);
        public void AddClub_Match(Club_Match club_Match) => db.Add(club_Match);
        public void AddGoal(Goal goal) => db.Add(goal);
        public void AddPlayer(Player player) => db.Add(player);
        public void RemoveClub(Club club) => db.Remove(club);
        public void RemoveMatch(Match match) => db.Remove(match);
        public void RemoveClub_Match(Club_Match club_Match) => db.Remove(club_Match);
        public void RemoveGoal(Goal goal) => db.Remove(goal);
        public void RemovePlayer(Player player) => db.Remove(player);
        public List<Match> GetAllMatchesIncludeClubs()
        {
            return db.Matches
                .Include(m => m.Club_Match)
                    .ThenInclude(cm => cm.Club1)
                .Include(m => m.Club_Match)
                    .ThenInclude(cm => cm.Club2)
                .ToList();
        }


        public List<Goal> Goals_Include_ClubsAndMatches()
        {
            return db.Goals
                 .Include(g => g.Player)
                     .ThenInclude(c => c.Club)
                 .Include(g => g.Match)
                 .ToList();
        }


        public List<Club> GetAllClubsIncludeMatches()
        {
            return db.Clubs
                .Include(c => c.Club1_Matches)
                    .ThenInclude(cm => cm.Match)
               .Include(c => c.Club2_Matches)
                    .ThenInclude(cm => cm.Match)
               .ToList();
        }

    }
}
