using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public enum GameStatus
    {
        FINISHED,
        INPROGRESS,
        DRAW
    }

    public class Game
    {
        public int Size { get; set; }
        public String Name { get; set; }
        public List<Play> Plays { get; set; }
        /// <summary>
        /// Used to keep track of which player owns which mark
        /// </summary>
        public Dictionary<String, Mark> PlayerMarkLegend { get; set; }
        public String Winner { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public GameStatus Status { get; set; }

        public Game(String name)
        {
            this.Size = 3;
            this.Status = GameStatus.INPROGRESS;
            this.Name = name;
            this.Plays = new List<Play>();
            this.PlayerMarkLegend = new Dictionary<String, Mark>();
        }

        public void MakePlay(String playerName, int x, int y)
        {
            // Evaluate what player's mark should be
            Mark mark = this.GetPlayerMark(playerName);

            this.Plays.Add(new Play(playerName, x, y, mark));

            // Check to see if a player has won by checking "row, column & diagonal scores"
            foreach (KeyValuePair<String, Mark> entry in PlayerMarkLegend)
            {
                List<Play> plays = this.Plays.Where(p => p.PlayerName.Equals(playerName)).ToList();
                if (plays != null && plays.Count > 0)
                {
                    // Check row score
                    int rowScore = plays.Count(p => p.X == x);
                    if(rowScore == this.Size)
                    {
                        this.Winner = playerName;
                        this.Status = GameStatus.FINISHED;
                        break;
                    }

                    // Check column score
                    int columnScore = plays.Count(p => p.Y == y);
                    if (columnScore == this.Size)
                    {
                        this.Winner = playerName;
                        this.Status = GameStatus.FINISHED;
                        break;
                    }

                    // Check 1st diagonal score
                    int firstDiagScore = plays.Count(p => p.X == p.Y);
                    if (firstDiagScore == this.Size)
                    {
                        this.Winner = playerName;
                        this.Status = GameStatus.FINISHED;
                        break;
                    }

                    // Check 2nd diagonal score
                    int secondDiagScore = plays.Count(p => (p.X + p.Y) == this.Size + 1);
                    if (secondDiagScore == this.Size)
                    {
                        this.Winner = playerName;
                        this.Status = GameStatus.FINISHED;
                        break;
                    }
                }
            }

            // If there is no winner, check for a draw
            if (this.Plays.Count == 9 && this.Status == GameStatus.INPROGRESS)
            {
                this.Status = GameStatus.DRAW;
            }
        }

        private Mark GetPlayerMark(String playerName)
        {
            if (this.PlayerMarkLegend.ContainsKey(playerName))
            {
                return this.PlayerMarkLegend[playerName];
            }
            else
            {
                // Default to X
                Mark mark = Mark.X;

                // Find out what first player's mark was, if any
                if (this.Plays.Count > 0)
                {
                    Play firstPlay = this.Plays.FirstOrDefault();
                    if (firstPlay.Mark.Equals(Mark.X))
                    {
                        mark = Mark.O;
                    }
                    else
                    {
                        mark = Mark.X;
                    }
                }

                this.PlayerMarkLegend.Add(playerName, mark);
                return mark;
            }
        }
    }
}