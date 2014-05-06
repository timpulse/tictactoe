using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicTacToe.Hubs;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Play(String name, String gameName)
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
            }
            return View(game);
        }
	}
}