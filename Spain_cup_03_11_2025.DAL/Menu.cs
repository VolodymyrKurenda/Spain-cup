using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spain_cup_03_11_2025.DAL.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Spain_cup_03_11_2025
{
    public class Menu
    {


        public void menu()
        {
                while (true)
                {
                    Console.WriteLine("=== Spain Cup Management Menu ===\n1 - Show All Clubs\n2 - Add Clubs\n3 - Update Club\n4 - Remove Club\n5 - Search Club with parametrs\n6 - Club Goals comparison\n7 - Show Match Info\n8 - Show Mathc at date\n9 - show All matches of team\n10 - Show all ScoredPlayers at date\n11 - Add Match\n12 - Update Match\n0 - Exit");
                    switch (getint("Your Choice: "))
                    {
                        case 0:
                            Console.WriteLine("Exiting...");
                            return;
                        case 1:
                            ShowAllClubs();
                            break;
                        case 2:
                            AddClub();
                            break;
                        case 3:
                            UpdateClub();
                            break;
                        case 4:
                            RemoveClubAndMatches(getstring("Enter Club Name: "));
                            break;
                        case 5:
                            SearchClub();
                            break;
                        case 6:
                            ClubGoalscomparison();
                            break;
                        case 7:
                            ShowMatchInfo(FindMatchByID(getint("Enter Match Id")));
                            break;
                        case 8:
                            ShowMatchWithDate(GetDate("Enter Match Date"));
                            break;
                        case 9:
                            ShowAllClubMathces(getstring("Enter Club Name: "));
                            break;
                        case 10:
                            ShowAllScoredPlayersAtDate(GetDate("Enter Date")); 
                            break;
                        case 11:
                            AddMatch();
                            break;
                        case 12:
                            UpdateMatch();
                            break;
                        default:
                            Console.WriteLine("Choose a number from 0 to 5");
                            break;
                    }
                }
        }


        public Spain_cup_03_11_2025.DAL.Entities.Match FindMatchByID(int id)
        {
            using (var db = new AppDbContext())
            {
                return db.Matches.FirstOrDefault(m => m.ID == id);
            }
        }



        public void RemoveMatch(DateOnly Date,string club1,string club2)
        {
            using (var db = new AppDbContext())
            {
                var matches = db.Matches
                    .Include(m => m.Club_Match)
                        .ThenInclude(cm => cm.Club1)
                    .Include(m => m.Club_Match)
                        .ThenInclude(cm => cm.Club2)
                    .ToList();

                foreach(var m in matches)
                {
                    foreach(var cm in m.Club_Match)
                    {
                        if(cm.Club1.Club_Name == club1 && cm.Club2.Club_Name == club2 && cm.Match.Date == Date)
                        {
                            Console.Write($"Do you want to Remove {cm.Club1.Club_Name} VS {cm.Club2.Club_Name} at {cm.Match.Date} ?\n1 - Yes\t2 - No");
                            switch (getint("Your choice"))
                            {
                                case 1:
                                    db.Club_Matches.Remove(cm);

                                    db.Matches.Remove(m);
                                    db.SaveChanges();
                                    return;
                                case 2:
                                    return;
                                default:
                                    Console.WriteLine("Choose 1 or 2");
                                    break;
                            }
                        }
                    }
                }
            }
        }
        


        public void UpdateMatch()
        {
            using (var db = new AppDbContext())
            {
                var searched_Match = db.Matches.FirstOrDefault(m => m.ID == getint("Enter Id: "));

                if (searched_Match != null)
                {
                    while (true)
                    {
                        Console.WriteLine($"Change:\n1 - Date\n2 - Club1\n3 - Club2\n4 -Club1 Scored goals\n5 - Club2 Scored Goals\n0 - Stop");
                        switch (getint("Your Choice: "))
                        {
                            case 0:
                                db.SaveChanges();
                                return;
                            case 1:
                                searched_Match.Date = GetDate("Enter Match Date: ");
                                break;
                            case 2:
                                var searched_Club_Match1 = db.Club_Matches.FirstOrDefault(cm => cm.MatchID == searched_Match.ID);
                                int club1id = getint("Enter new Id:");
                                if (db.Clubs.Find(club1id) != null) { searched_Club_Match1.Club1_id = club1id; }
                                break;
                            case 3:
                                var searched_Club_Match2 = db.Club_Matches.FirstOrDefault(cm => cm.MatchID == searched_Match.ID);
                                int club2id = getint("Enter new Id:");
                                if (db.Clubs.Find(club2id) != null) { searched_Club_Match2.Club2_id = club2id; }
                                break;
                            case 4:
                                searched_Match.club1_scored = getint("Enter how much club1 scored goals: ");
                                break;
                            case 5:
                                searched_Match.club2_scored = getint("Enter how much club2 scored goals: ");
                                break;
                            default:
                                Console.WriteLine("Choose from 0 to 5");
                                break;
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Match not found!");
                }
            }
        }


        public void AddMatch()
        {
            using (var db = new AppDbContext())
            {
              
                DateOnly date = GetDate("Enter Match Date: ");
                string name1 = getstring("Enter club1 Name: ");
                string name2 = getstring("Enter club2 Name: ");

                var matches = db.Matches
                    .Include(m => m.Club_Match)
                        .ThenInclude(cm => cm.Club1)
                    .Include(m => m.Club_Match)
                        .ThenInclude(cm => cm.Club2)
                    .ToList();

                foreach(var m in matches)
                {
                    foreach(var cm in m.Club_Match)
                    {
                        if (cm.Club1.Club_Name == name1 && cm.Club2.Club_Name == name2 && m.Date == date) { Console.WriteLine("This Match already exist!"); return; }
                    }
                }

                
               
                var newMatch = new Spain_cup_03_11_2025.DAL.Entities.Match
                {
                    club1_scored = getint("Enter how much Club 1 scored: "),
                    club2_scored = getint("Enter how much Club 2 scored: "),
                    Date = date
                };

                db.Matches.Add(newMatch);
                db.SaveChanges();

                int club1_id = getint("Enter Club1 ID: ");
                int club2_id = getint("Enter Club2 ID: ");

                var club1 = db.Clubs.Find(club1_id);
                var club2 = db.Clubs.Find(club2_id);
                if (club1 == null || club2 == null) { Console.WriteLine("Club not found"); return; }


                db.Club_Matches.Add(new Club_Match
                {
                    Club1_id = club1_id,
                    Club2_id = club2_id,
                    MatchID = newMatch.ID
                });



                db.SaveChanges();
            }
        }


        public void ShowAllScoredPlayersAtDate(DateOnly date)
        {
            using (var db = new AppDbContext())
            {
                var Goals = db.Goals
                    .Include(g => g.Player)
                        .ThenInclude(c => c.Club)
                    .Include(g => g.Match)
                    .Where(g => g.Match.Date == date)
                    .ToList();

                if(Goals.Count == 0)
                {
                    Console.WriteLine($"0 Goals At {date} Match");
                }
                else
                {
                    Console.WriteLine($"Goals At {date}:");

                    foreach(var g in Goals)
                    {
                        Console.WriteLine($"{g.Player.Club.Club_Name}\t{g.Player.FullName}    {g.Minute} minute ");
                    }
                }
            }
        }


        public void ShowAllClubMathces(string Name)
        {
            using (var db = new AppDbContext())
            {
                var matches = db.Matches
                    .Include(m => m.Club_Match)
                        .ThenInclude(cm => cm.Club1)
                    .Include(m => m.Club_Match)
                        .ThenInclude(cm => cm.Club2)
                        .ToList();

                if (!matches.Any()) { Console.WriteLine("Match not found"); return; }

                foreach (var match in matches)
                {
                    foreach (var cm in match.Club_Match)
                    {
                        if(cm.Club1.Club_Name == Name || cm.Club2.Club_Name == Name)
                        {
                           ShowMatchInfo(match);
                        }
                    }
                }
            }
        }


        public void ShowMatchWithDate(DateOnly date)
        {
            using (var db = new AppDbContext())
            {
                var match = db.Matches.Where(m => m.Date == date).FirstOrDefault();

                if (match == null) { Console.WriteLine("Match not found"); return; }

                ShowMatchInfo(match);
            }
        }


        public void ShowMatchInfo(Spain_cup_03_11_2025.DAL.Entities.Match obj)
        {
            using (var db = new AppDbContext())
            {
                if (obj == null) { Console.WriteLine("Match not found"); return; }

                var match = db.Matches
                    .Include(m => m.Club_Match)
                        .ThenInclude(cm => cm.Club1)
                    .Include(m => m.Club_Match)
                        .ThenInclude(cm => cm.Club2)
                    .FirstOrDefault(m => m.ID == obj.ID);

                foreach (var cm in match.Club_Match)
                {
                    string name1 = cm.Club1.Club_Name;
                    string name2 = cm.Club2.Club_Name;

                    Console.WriteLine($"Match Date: {obj.Date}");
                    Console.WriteLine($"{name1} {obj.club1_scored} -- {obj.club2_scored} {name2}");
                }
            }
        }


        public void ClubGoalscomparison()
        {
            using ( var db = new AppDbContext())
            {
                var Clubs = db.Clubs.ToList();

                var club1 = Clubs.FirstOrDefault(c => c.Club_Name == getstring("Enter Club Name"));

                var club2 = Clubs.FirstOrDefault(c => c.Club_Name == getstring("Enter Club Name"));

                Console.WriteLine("=== Club Goals Comparison Menu ===\n1 - Goal Lost Difference\t\t2 -Goals Win Difference");
                switch (getint("Your Choice: "))
                {
                    case 1:
                        Console.WriteLine($"{club1.Club_Name} Lost {club1.Goals_Lost} ");
                        Console.WriteLine($"{club2.Club_Name} Lost {club2.Goals_Lost} ");
                        if (club1.Goals_Lost > club2.Goals_Lost)
                        {
                            Console.WriteLine($"{club1.Club_Name} Lost More on {club1.Goals_Lost - club2.Goals_Lost} Goals Then {club2.Club_Name} ");
                        }
                        else if (club1.Goals_Lost < club2.Goals_Lost)
                        {
                            Console.WriteLine($"{club2.Club_Name} Lost More on {club2.Goals_Lost - club1.Goals_Lost} Goals Then {club1.Club_Name} ");
                        }
                        else
                        {
                            Console.WriteLine($"{club1.Club_Name} and {club1.Club_Name} lost same amount of goals ");
                        }
                        break;
                    case 2:
                        Console.WriteLine($"{club1.Club_Name} Scored {club1.Goals_scored} ");
                        Console.WriteLine($"{club2.Club_Name} Scored {club2.Goals_scored} ");
                        if (club1.Goals_scored > club2.Goals_scored)
                        {
                            Console.WriteLine($"{club1.Club_Name} Scored More on {club1.Goals_scored - club2.Goals_scored} Goals Then {club2.Club_Name} ");
                        }
                        else if (club1.Goals_Lost < club2.Goals_Lost)
                        {
                            Console.WriteLine($"{club2.Club_Name} Scored More on {club2.Goals_scored - club1.Goals_scored} Goals Then {club1.Club_Name} ");
                        }
                        else
                        {
                            Console.WriteLine($"{club1.Club_Name} and {club1.Club_Name} Scored same amount of goals ");
                        }
                        break;
                    default:
                        Console.WriteLine("Choose from 1 or 2");
                        break;
                }
            }
        }

        public void RemoveClubAndMatches(string clubName)
        {
            using var db = new AppDbContext();

            var club = db.Clubs
                .Include(c => c.Club1_Matches)
                    .ThenInclude(cm => cm.Match)
               .Include(c => c.Club2_Matches)
                    .ThenInclude(cm => cm.Match)
                .FirstOrDefault(c => c.Club_Name == clubName);

            if (club == null)
            {
                Console.WriteLine("Club not found!");
                return;
            }

            Console.WriteLine($"Do you want to remove {club.Club_Name} and all its matches? 1-Yes 2-No");
            int choice = getint("Your choice: ");
            if (choice != 1) return;


            foreach (var cm in club.Club1_Matches.ToList())
            {
                var match = cm.Match;
                db.Matches.Remove(match);
            }

            foreach (var cm in club.Club2_Matches.ToList())
            {
                var match = cm.Match;
                db.Matches.Remove(match);
            }

            db.Clubs.Remove(club);
            db.SaveChanges();

            Console.WriteLine($"{club.Club_Name} and all its matches were removed.");
        }


        public void UpdateClub()
        {
            using (var db = new AppDbContext())
            {
                var searched_Club = db.Clubs.FirstOrDefault(c => c.ID == getint("Enter Id: "));

                if (searched_Club != null)
                {
                    while (true)
                    {
                        Console.WriteLine($"Change:\n1 - Name\n2 - Ciy\n3 - Wins\n4 -Lose\n5 - Ties\n6 - Goals scored\n7 - Goals lost\n0 - Stop");
                        switch (getint("Your Choice: "))
                        {
                            case 0:
                                db.SaveChanges();
                                return;
                            case 1:
                                searched_Club.Club_Name = getstring("Enter Club Name: ");
                                break;
                            case 2:
                                searched_Club.City = getstring("Enter city: ");
                                break;
                            case 3:
                                searched_Club.Wins = getint("Enter Club Wins: ");
                                break;
                            case 4:
                                searched_Club.Lose = getint("Enter Club Lose: ");
                                break;
                            case 5:
                                searched_Club.Tie = getint("Enter Club Ties: ");
                                break;
                            case 6:
                                searched_Club.Goals_scored = getint("Enter Goals scored: ");
                                break;
                            case 7:
                                searched_Club.Goals_Lost = getint("Enter Goals Lost: ");
                                break;
                            default:
                                Console.WriteLine("Choose from 0 to 7");
                                break;
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Club not found!");
                }
            }
        }

        public void SearchClub()
        {
            var db = new AppDbContext();

            var Clubs = db.Clubs.ToList();

            Console.WriteLine($"Searhc club with:\n1 - Most wins\n2 - Most lost\n3 - Most Tie\n4 -Most goals scored\n5 - Most goals lost");
            switch (getint("Enter Your Choice: "))
            {
                case 1:
                    var Most_wins = Clubs.OrderByDescending(c => c.Wins).FirstOrDefault();
                    showclub(Most_wins);
                    break;
                case 2:
                    var Most_Lost = Clubs.OrderByDescending(c => c.Lose).FirstOrDefault();
                    showclub(Most_Lost);
                    break;
                case 3:
                    var Most_Tie = Clubs.OrderByDescending(c => c.Tie).FirstOrDefault();
                    showclub(Most_Tie);
                    break;
                case 4:
                    var Most_Goals_scored = Clubs.OrderByDescending(c => c.Goals_scored).FirstOrDefault();
                    showclub(Most_Goals_scored);
                    break;
                case 5:
                    var Most_Goals_Lost = Clubs.OrderByDescending(c => c.Goals_Lost).FirstOrDefault();
                    showclub(Most_Goals_Lost);
                    break;
                default:
                    Console.WriteLine("Choose from 1 to 5");
                    break;
            }
        }






        public void AddClub()
        {
            using (var db = new AppDbContext())
            {
                var Clubs = db.Clubs.ToList();

                var check = db.Clubs.FirstOrDefault(c => c.Club_Name == getstring("Enter Club Name: "));
                if (check == null)
                {
                    db.Clubs.Add(new Club
                    {
                        Club_Name = getstring("Enter Club Name: "),
                        City = getstring("Enter City: "),
                        Wins = getint("Enter Wins: "),
                        Lose = getint("Enter Lose: "),
                        Tie = getint("Enter Tie: "),
                        Goals_scored = getint("Enter Goals scored: "),
                        Goals_Lost = getint("Enter Goals lost: "),
                    });
                }
                db.SaveChanges();
            }
        }


        public void showclub(Club obj)
        {
            Console.WriteLine($"ID: {obj.ID}");
            Console.WriteLine($"Name: {obj.Club_Name}");
            Console.WriteLine($"City: {obj.City}");
            Console.WriteLine($"Win: {obj.Wins}");
            Console.WriteLine($"Lose: {obj.Lose}");
            Console.WriteLine($"Tie: {obj.Tie}");
            Console.WriteLine($"Coals Scored: {obj.Goals_scored}");
            Console.WriteLine($"Coals Lost: {obj.Goals_Lost}");
            Console.WriteLine("********************************************************");

        }


        public void ShowAllClubs()
        {
            using (var db = new AppDbContext())
            {
                var Clubs = db.Clubs.ToList();

                Console.WriteLine("\n=== Club List ===\n");

                foreach (var c in Clubs)
                {
                    showclub(c);
                }
            }
        }


        public DateOnly GetDate(string message)
        {
            while(true)
            {
                Console.WriteLine($"{message} DD.MM.YYYY: ");
                if (DateOnly.TryParse(Console.ReadLine(), out DateOnly date))
                {
                    return date;
                }
                else
                {
                    Console.WriteLine("Wrong Data format!");
                }
            }
            
        }


        public string getstring(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public int getint(string message)
        {
            while (true)
            {
                Console.Write(message);
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter a number!");
                }
            }
        }
    }

}
