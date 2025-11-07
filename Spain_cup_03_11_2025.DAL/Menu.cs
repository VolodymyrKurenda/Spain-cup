using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Spain_cup_03_11_2025.DAL.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Spain_cup_03_11_2025.DAL
{
    public class Menu
    {
        SpCupRepository rep = new SpCupRepository();

        public void menu()
        {
            Updatepts();
            while (true)
            {
                Console.WriteLine("=== Spain Cup Main Menu ===\n1 - Club Menu\n2 - Match Menu\n3 - Show all players that scored at date\n4 - Pts info\n5 - Player menu\n0 - Exit");
                switch (getint("Your Choice: "))
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Exiting...");
                        return;
                    case 1:
                        Console.Clear();
                        ClubMenu();
                        break;
                    case 2:
                        Console.Clear();
                        MatchMenu();
                        break;
                    case 3:
                        Console.Clear();
                        ShowAllScoredPlayersAtDate(GetDate("Enter Date: "));
                        break;
                    case 4:
                        Console.Clear();
                        PtsMenu();
                        break;
                    case 5:
                        Console.Clear();
                        PlayerMenu();
                        break;
                    default:
                        Console.WriteLine("Choose a number from 0 to 5");
                        break;
                }
            }
        }



        public void PlayerMenu()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("===Player Managment Menu===\n1 - Add Player\n2 - Update Player\n3 - Remove Player\n4 - Remove player\n5 - Add Goal\n0 - Back to main menu");
                switch (getint("Your Choice: "))
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Going back To Main Menu...");
                        return;
                    case 1:
                        Console.Clear();
                        AddPlayer();
                        break;
                    case 2:
                        Console.Clear();
                        updatePlayer();
                        break;
                    case 3:
                        Console.Clear();
                        AddMatch();
                        break;
                    case 4:
                        Console.Clear();
                        RemovePlayer();
                        break;
                    case 5:
                        Console.Clear();
                        AddGoal();
                        break;
                    default:
                        Console.WriteLine("Enter number from 0 to 4");
                        break;
                }
            }
        }


        public void AddGoal()
        {
            int playerId = getint("Enter player that scored id: ");
            int matchId = getint("Enter match id: ");
            int minute = getint("Enter goal minute: ");

           
            var player = rep.GetAllPlayers().FirstOrDefault(p => p.ID == playerId);
            if (player == null)
            {
                Console.WriteLine("Player not found!");
                return;
            }

            var match = rep.GetAllMatches().FirstOrDefault(m => m.ID == matchId);
            if (match == null)
            {
                Console.WriteLine("Match not found!");
                return;
            }

            var goal = new Goal
            {
                PlayerID = playerId,
                MatchID = matchId,
                Minute = minute
            };

            rep.AddGoal(goal);
            rep.SaveChanges();

            Console.WriteLine($"Goal added successfully! Player: {player.FullName}, Minute: {minute}");
        }



        public void RemovePlayer()
        {
            int id = getint("Enter player id: ");

            var player = rep.GetAllPlayers().FirstOrDefault(p => p.ID == id);

            if (player == null) { Console.WriteLine("Player is not found");return; }

            rep.RemovePlayer(player);

            rep.SaveChanges();
        }


        public void updatePlayer()
        {
            Console.Clear();
            int id = getint("Enter player id: ");
            var searched_player = rep.GetAllPlayers().FirstOrDefault(c => c.ID == id);

            if (searched_player != null)
            {
                while (true)
                {
                    Console.WriteLine($"Change:\n1 - FullName\n2 - Country\n3 - Number\n4 - Position\n5 - Club\n0 - Stop");
                    switch (getint("Your Choice: "))
                    {
                        case 0:
                            Console.Clear();
                            rep.SaveChanges();
                            return;
                        case 1:
                            Console.Clear();
                            searched_player.FullName = getstring("Enter player name: ");
                            break;
                        case 2:
                            Console.Clear();
                            searched_player.Country = getstring("Enter country: ");
                            break;
                        case 3:
                            Console.Clear();
                            searched_player.Number = getint("Enter player number: ");
                            break;
                        case 4:
                            Console.Clear();
                            searched_player.Position = getstring("Enter positin: ");
                            break;
                        default:
                            Console.WriteLine("Choose from 0 to 4");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Club is not found!");
            }
        }


        public void AddPlayer()
        {
            Console.Clear();
            rep.AddPlayer(new Player
            {
                FullName = getstring("Enter player name: "),
                Country = getstring("Enter player country: "),
                Number = getint("Enter player number: "),
                Position = getstring("Enter position: "),
                Club = GetClubByName()
            });
            rep.SaveChanges();
        }


        public void ClubMenu()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("===Club Managment Menu===\n1 - Show all clubs\n2 - Add club\n3 - Update club\n4 - Remove club and matches\n5 - Search club\n6 - Club goals comparison\n7 - Show all club mathces\n0 - Back to main menu");
                switch (getint("Your Choice: "))
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Going back To Main Menu...");
                        return;
                    case 1:
                        Console.Clear();
                        ShowAllClubs();
                        break;
                    case 2:
                        Console.Clear();
                        AddClub();
                        break;
                    case 3:
                        Console.Clear();
                        UpdateClub();
                        break;
                    case 4:
                        Console.Clear();
                        RemoveClubAndMatches(getstring("Enter Club Name: "));
                        break;
                    case 5:
                        Console.Clear();
                        SearchClubgoals();
                        break;
                    case 6:
                        Console.Clear();
                        ClubGoalscomparison();
                        break;
                    case 7:
                        Console.Clear();
                        ShowAllClubMathces(getstring("Enter Club Name: "));
                        break;
                    default:
                        Console.WriteLine("Enter number from 0 to 7");
                        break;
                }
            }
        }


        public void MatchMenu()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("===Match Managment Menu===\n1 - Show match info\n2 - Show match with date\n3 - Add match\n4 - Update match\n0 - Back to main menu");
                switch (getint("Your Choice: "))
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Going back To Main Menu...");
                        return;
                    case 1:
                        Console.Clear();
                        ShowMatchInfo(FindMatchByID(getint("Enter match id: ")));
                        break;
                    case 2:
                        Console.Clear();
                        ShowMatchWithDate(GetDate("Enter match date: "));
                        break;
                    case 3:
                        Console.Clear();
                        AddMatch();
                        break;
                    case 4:
                        Console.Clear();
                        UpdateMatch();
                        break;
                    default:
                        Console.WriteLine("Enter number from 0 to 4");
                        break;
                }
            }
        }


        public void Updatepts()
        {
                var clubs = rep.GetAllClubs();

                foreach (var club in clubs)
                {
                    club.UpdatePoints();
                }
                rep.SaveChanges();
        }



        public void PtsMenu()
        {
            Console.Clear();
            while(true)
            {
                Console.WriteLine("===Pts Managment Menu===\n1 - Show top 3 teams\n2 - Show top 1 team\n3 - Show 3 worst teams\n4 - Show worst team\n0 - Back to main menu");
                switch (getint("Your Choice: "))
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Going back to Main Menu...");
                        return;
                    case 1:
                        Console.Clear();
                        Top3Pts();
                        break;
                    case 2:
                        Console.Clear();
                        Top1Pts();
                        break;
                    case 3:
                        Console.Clear();
                        Worst3Pts();
                        break;
                    case 4:
                        Console.Clear();
                        WorstPts();
                        break;
                    default:
                        Console.WriteLine("Enter number from 0 to 4");
                        break;
                }
            }

            
  
        }


        public void WorstPts()
        {
            Updatepts();

            rep.SaveChanges();

            var worst = rep.GetAllClubs().OrderBy(c => c.Pts).Take(1);

            foreach (var club in worst)
            {
                showclub(club);
            }
        }


        public void Worst3Pts()
        {
            Updatepts();

            rep.SaveChanges();
            
            var worst3 = rep.GetAllClubs()
                .OrderBy(c => c.Pts)
                .Take(3)
                .ToList();
            
            foreach (var club in worst3)
            {
                showclub(club);
            }
        }


        public void Top1Pts()
        {
            Updatepts();

            rep.SaveChanges();

            var top1 = rep.GetAllClubs()
                .OrderByDescending(c => c.Pts)
                .Take(1);

            foreach (var club in top1)
            {
                showclub(club);
            }
        }
                    


        public void Top3Pts()
        {
            Updatepts();

            rep.SaveChanges();

            var top3 = rep.GetAllClubs()
                .OrderByDescending(c => c.Pts)
                .Take(3)
                .ToList();

            foreach (var club in top3)
            {
                showclub(club);
            }
        }


        public Spain_cup_03_11_2025.DAL.Entities.Match FindMatchByID(int id)
        {
            return rep.GetAllMatches().FirstOrDefault(m => m.ID == id);            
        }



        public void RemoveMatch(DateOnly Date, string club1, string club2)
        {
            Console.Clear();
            var matches = rep.GetAllMatchesIncludeClubs();

            foreach (var m in matches)
            {
                foreach (var cm in m.Club_Match)
                {
                    if (cm.Club1.Club_Name == club1 && cm.Club2.Club_Name == club2 && cm.Match.Date == Date)
                    {
                        Console.Write($"Do you want to remove {cm.Club1.Club_Name} VS {cm.Club2.Club_Name} at {cm.Match.Date} ?\n1 - Yes\t2 - No");
                        switch (getint("Your choice: "))
                        {
                            case 1:
                                rep.RemoveClub_Match(cm);
                                rep.RemoveMatch(m);
                                rep.SaveChanges();
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
        


        public void UpdateMatch()
        {
            Console.Clear();
            int id = getint("Enter Id: ");
            var searched_Match = rep.GetAllMatches().FirstOrDefault(m => m.ID == id);

            if (searched_Match != null)
            {
                while (true)
                {
                    Console.WriteLine($"Change:\n1 - Date\n2 - Club 1\n3 - Club 2\n4 - Club 1 scored goals\n5 - Club 2 scored Goals\n0 - Stop");
                    switch (getint("Your Choice: "))
                    {
                        case 0:
                            Console.Clear();
                            rep.SaveChanges();
                            return;
                        case 1:
                            Console.Clear();
                            searched_Match.Date = GetDate("Enter match date: ");
                            break;
                        case 2:
                            Console.Clear();
                            var searched_Club_Match1 = rep.GetAllClub_Matches().FirstOrDefault(cm => cm.MatchID == searched_Match.ID);
                            int club1id = getint("Enter new id:");
                            if (rep.GetAllClubs().FirstOrDefault(c => c.ID == club1id) != null) { searched_Club_Match1.Club1_id = club1id; }
                            break;
                        case 3:
                            Console.Clear();
                            var searched_Club_Match2 = rep.GetAllClub_Matches().FirstOrDefault(cm => cm.MatchID == searched_Match.ID);
                            int club2id = getint("Enter new id:");
                            if (rep.GetAllClubs().FirstOrDefault(c => c.ID == club2id) != null) { searched_Club_Match2.Club2_id = club2id; }
                            break;
                        case 4:
                            Console.Clear();
                            searched_Match.club1_scored = getint("Enter how much club 1 scored goals: ");
                            break;
                        case 5:
                            Console.Clear();
                            searched_Match.club2_scored = getint("Enter how much club 2 scored goals: ");
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


        public void AddMatch()
        {
            Console.Clear();
            DateOnly date = GetDate("Enter match date: ");
            string name1 = getstring("Enter club 1 name: ");
            string name2 = getstring("Enter club 2 name: ");
            
            var matches = rep.GetAllMatchesIncludeClubs();
                    
            
            foreach(var m in matches)
            {
                foreach(var cm in m.Club_Match)
                {
                    if (cm.Club1.Club_Name == name1 && cm.Club2.Club_Name == name2 && m.Date == date) { Console.WriteLine("This match already exist!"); return; }
                }
            }
 
            var newMatch = new Spain_cup_03_11_2025.DAL.Entities.Match
            {
                club1_scored = getint("Enter how much club 1 scored: "),
                club2_scored = getint("Enter how much club 2 scored: "),
                Date = date
            };
            
            rep.AddMatch(newMatch);
            rep.SaveChanges();
            
            int club1_id = getint("Enter club 1 id: ");
            int club2_id = getint("Enter club 2 id: ");
            
            var club1 = rep.GetAllClubs().FirstOrDefault(c => c.ID == club1_id);
            var club2 = rep.GetAllClubs().FirstOrDefault(c => c.ID == club2_id);
            if (club1 == null || club2 == null) { Console.WriteLine("Club is not found"); return; }
            
            rep.AddClub_Match(new Club_Match
            {
                Club1_id = club1_id,
                Club2_id = club2_id,
                MatchID = newMatch.ID
            });
            rep.SaveChanges();
        }


        public void ShowAllScoredPlayersAtDate(DateOnly date)
        {
            Console.Clear();
            var Goals = rep.Goals_Include_ClubsAndMatches()
                .Where(g => g.Match.Date == date)
                .ToList();
            
            if(Goals.Count == 0)
            {
                Console.WriteLine($"0 goals at {date} match");
            }
            else
            {
                Console.WriteLine($"Goals at {date}:");
            
                foreach(var g in Goals)
                {
                    Console.WriteLine($"{g.Player.Club.Club_Name}({g.Player.FullName}) -- {g.Minute} minute!");
                }
            }
        }


        public void ShowAllClubMathces(string Name)
        {
            Console.Clear();
            var matches = rep.GetAllMatchesIncludeClubs();
            
            if (!matches.Any()) { Console.WriteLine("Match is not found"); return; }
            
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


        public void ShowMatchWithDate(DateOnly date)
        {
            Console.Clear();
            var match = rep.GetAllMatches().Where(m => m.Date == date).FirstOrDefault();
            
            if (match == null) { Console.WriteLine("match is not found"); return; }
            
            ShowMatchInfo(match);
        }


        public void ShowMatchInfo(Spain_cup_03_11_2025.DAL.Entities.Match obj)
        {
            Console.Clear();
            if (obj == null) { Console.WriteLine("Match is not found"); return; }
            
            var match = rep.GetAllMatchesIncludeClubs()
                .FirstOrDefault(m => m.ID == obj.ID);
            
            foreach (var cm in match.Club_Match)
            {
                string name1 = cm.Club1.Club_Name;
                string name2 = cm.Club2.Club_Name;
            
                Console.WriteLine($"Match date: {obj.Date}");
                Console.WriteLine($"{name1} {obj.club1_scored} -- {obj.club2_scored} {name2}");
            }
        }


        public void ClubGoalscomparison()
        {
            Console.Clear();
            var Clubs = rep.GetAllClubs();

            string name1 = getstring("Enter club 1 Name: ");

            string name2 = getstring("Enter club 2 Name: ");

            var club1 = Clubs.FirstOrDefault(c => c.Club_Name == name1);
            
            var club2 = Clubs.FirstOrDefault(c => c.Club_Name == name2);

            if (club1 == null || club2 == null) { Console.WriteLine("Wrong name:"); return; }
            
            Console.WriteLine("=== Club Goals Comparison Menu ===\n1 - Goal lost difference\n2 - Goals win difference");
            switch (getint("Your Choice: "))
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine($"{club1.Club_Name} Lost {club1.Goals_Lost} ");
                    Console.WriteLine($"{club2.Club_Name} Lost {club2.Goals_Lost} ");
                    if (club1.Goals_Lost > club2.Goals_Lost)
                    {
                        Console.WriteLine($"{club1.Club_Name} Lost more on {club1.Goals_Lost - club2.Goals_Lost} goals then {club2.Club_Name} ");
                    }
                    else if (club1.Goals_Lost < club2.Goals_Lost)
                    {
                        Console.WriteLine($"{club2.Club_Name} Lost more on {club2.Goals_Lost - club1.Goals_Lost} goals then {club1.Club_Name} ");
                    }
                    else
                    {
                        Console.WriteLine($"{club1.Club_Name} and {club1.Club_Name} lost same amount of goals ");
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine($"{club1.Club_Name} Scored {club1.Goals_scored} ");
                    Console.WriteLine($"{club2.Club_Name} Scored {club2.Goals_scored} ");
                    if (club1.Goals_scored > club2.Goals_scored)
                    {
                        Console.WriteLine($"{club1.Club_Name} Scored more on {club1.Goals_scored - club2.Goals_scored} goals then {club2.Club_Name} ");
                    }
                    else if (club1.Goals_Lost < club2.Goals_Lost)
                    {
                        Console.WriteLine($"{club2.Club_Name} Scored more on {club2.Goals_scored - club1.Goals_scored} goals then {club1.Club_Name} ");
                    }
                    else
                    {
                        Console.WriteLine($"{club1.Club_Name} and {club1.Club_Name} Scored same amount of goals ");
                    }
                    break;
                default:
                    Console.WriteLine("Choose 1 or 2");
                    break;
            }
        }

        public void RemoveClubAndMatches(string clubName)
        {
            Console.Clear();
            var club = rep.GetAllClubsIncludeMatches()
                .FirstOrDefault(c => c.Club_Name == clubName);
            
            if (club == null)
            {
                Console.WriteLine("Club is not found!");
                return;
            }
            
            Console.WriteLine($"Do you want to remove {club.Club_Name} and all its matches? 1-Yes 2-No");
            if (getint("Your choice: ") != 1) return;
            
            foreach (var cm in club.Club1_Matches.ToList())
            {
                var match = cm.Match;
                rep.RemoveMatch(match);
            }
            
            foreach (var cm in club.Club2_Matches.ToList())
            {
                var match = cm.Match;
                rep.RemoveMatch(match);
            }
            
            rep.RemoveClub(club);
            rep.SaveChanges();
            
            Console.WriteLine($"{club.Club_Name} and all its matches were removed.");
        }


        public void UpdateClub()
        {
            Console.Clear();
            int id = getint("Enter id: ");
            var searched_Club = rep.GetAllClubs().FirstOrDefault(c => c.ID == id);

            if (searched_Club != null)
            {
                while (true)
                {
                    Console.WriteLine($"Change:\n1 - Name\n2 - City\n3 - Wins\n4 - Lose\n5 - Ties\n6 - Goals scored\n7 - Goals lost\n0 - Stop");
                    switch (getint("Your Choice: "))
                    {
                        case 0:
                        rep.SaveChanges();
                            return;
                        case 1:
                            searched_Club.Club_Name = getstring("Enter club name: ");
                            break;
                        case 2:
                            searched_Club.City = getstring("Enter city: ");
                            break;
                        case 3:
                            searched_Club.Wins = getint("Enter club wins: ");
                            break;
                        case 4:
                            searched_Club.Lose = getint("Enter club lose: ");
                            break;
                        case 5:
                            searched_Club.Tie = getint("Enter club ties: ");
                            break;
                        case 6:
                            searched_Club.Goals_scored = getint("Enter goals scored: ");
                            break;
                        case 7:
                            searched_Club.Goals_Lost = getint("Enter goals Lost: ");
                            break;
                        default:
                            Console.WriteLine("Choose from 0 to 7");
                            break;
                    }
                }
            
            }
            else
            {
                Console.WriteLine("Club is not found!");
            }
        }

        public void SearchClubgoals()
        {
            Console.Clear();
            var Clubs = rep.GetAllClubs();

            Console.WriteLine($"Search club with:\n1 - Most wins\n2 - Most lost\n3 - Most tie\n4 - Most goals scored\n5 - Most goals lost");
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
            Console.Clear();
            var Clubs = rep.GetAllClubs();
            
            string name = getstring("Enter club name: ");
            
            var check = Clubs.FirstOrDefault(c => c.Club_Name == name);
            if (check == null)
            {
                rep.AddClub(new Club
                {
                    Club_Name = name,
                    City = getstring("Enter city: "),
                    Wins = getint("Enter wins: "),
                    Lose = getint("Enter lose: "),
                    Tie = getint("Enter tie: "),
                    Goals_scored = getint("Enter goals scored: "),
                    Goals_Lost = getint("Enter goals lost: "),
                });
            }
            rep.SaveChanges();
            
        }


        public void showclub(Club obj)
        {
            Console.WriteLine($"ID: {obj.ID}");
            Console.WriteLine($"Name: {obj.Club_Name}");
            Console.WriteLine($"City: {obj.City}");
            Console.WriteLine($"Win: {obj.Wins}");
            Console.WriteLine($"Lose: {obj.Lose}");
            Console.WriteLine($"Tie: {obj.Tie}");
            Console.WriteLine($"Goals scored: {obj.Goals_scored}");
            Console.WriteLine($"Goals lost: {obj.Goals_Lost}");
            Console.WriteLine("********************************************************");

        }


        public void ShowAllClubs()
        {
            Console.Clear();
            var Clubs = rep.GetAllClubs();
            
            Console.WriteLine("\n=== Club List ===\n");
            
            foreach (var c in Clubs)
            {
                showclub(c);
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
                    Console.WriteLine("Wrong Date format!");
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


        public Club GetClubByName()
        {
            string name = getstring("Enter club name: ");
            return rep.GetAllClubs().FirstOrDefault(c => c.Club_Name == name);
        }
    }

}
