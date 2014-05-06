using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using TicTacToe.Models;

namespace TicTacToe.Hubs
{
    public class TicTacToeHub : Hub
    {
        public static Dictionary<String, Game> Games = new Dictionary<String, Game>();

        public void Update(String name, String gameName, int x = 0, int y = 0)
        {
            Game game = null;
            if (!String.IsNullOrEmpty(gameName))
            {
                if (TicTacToeHub.Games.ContainsKey(gameName))
                {
                    game = TicTacToeHub.Games[gameName];
                }
                else
                {
                    game = new Game(gameName);
                    TicTacToeHub.Games.Add(gameName, game);
                }

                if (game.Status == GameStatus.INPROGRESS && x > 0 && y > 0)
                {
                   game.MakePlay(name, x, y);
                }
            }

            Clients.All.updateGameOnPage(game);
        }
    }
}