using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicTacToe.Models
{
    public enum Mark
    {
        X,
        O
    }

    public class Play
    {
        public String PlayerName { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Mark Mark { get; set; }

        public Play(String playerName, int x, int y, Mark mark)
        {
            this.PlayerName = playerName;
            this.X = x;
            this.Y = y;
            this.Mark = mark;
        }
    }
}