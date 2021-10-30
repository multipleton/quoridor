using System.Collections.Generic;
using System;
using Quoridor.Core;
using Quoridor.Player;
using Quoridor.AI;

namespace Quoridor.Console
{
    class Program
    {
        private static readonly GameEngine gameEngine = GameEngine.Instance;

        static void Main(string[] args)
        {
            Dictionary<MenuActionType, Action> actions = new Dictionary<MenuActionType, Action>();
            actions.Add(MenuActionType.PVP, StartPvP);
            actions.Add(MenuActionType.PVA, StartPvA);
            actions.Add(MenuActionType.EXIT, Exit);
            Menu menu = new Menu(actions);
            menu.Bootstrap();
        }

        static void StartPvP()
        {
            gameEngine.Initialize(2);
            PlayerConnection playerConnection1 = new PlayerConnection(gameEngine);
            PlayerConnection playerConnection2 = new PlayerConnection(gameEngine);
            gameEngine.Connect(playerConnection1);
            gameEngine.Connect(playerConnection2);
            gameEngine.Start();
        }

        static void StartPvA()
        {
            gameEngine.Initialize(2);
            PlayerConnection playerConnection = new PlayerConnection(gameEngine);
            RandomBot randomBot = new RandomBot(gameEngine);
            gameEngine.Connect(playerConnection);
            gameEngine.Connect(randomBot);
            gameEngine.Start();
        }

        static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
