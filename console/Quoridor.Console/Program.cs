using static System.Console;
using System;
using System.Collections.Generic;
using Quoridor.Core;
using Quoridor.Player;
using Quoridor.AI;

namespace Quoridor.Console
{
    class Program
    {
        private static readonly GameEngine gameEngine;
        private static readonly Menu menu;

        static Program()
        {
            gameEngine = GameEngine.Instance;
            gameEngine.OnFinish += OnFinish;
            Dictionary<MenuActionType, Action> actions = new Dictionary<MenuActionType, Action>();
            actions.Add(MenuActionType.PVP, StartPvP);
            actions.Add(MenuActionType.PVA, StartPvA);
            actions.Add(MenuActionType.EXIT, Exit);
            menu = new Menu(actions);
        }

        static void Main(string[] args)
        {
            menu.Bootstrap();
        }

        static void StartPvP()
        {
            int playersCount = 2;
            PlayerConnection[] players = new PlayerConnection[playersCount];
            for (int i = 0; i < playersCount; i++)
            {
                Write("Name for player " + (i + 1) + ": ");
                string name = ReadLine();
                players[i] = new PlayerConnection(gameEngine, name);
            }
            gameEngine.Initialize(2);
            foreach (var player in players)
            {
                gameEngine.Connect(player);
            }
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

        static void OnFinish()
        {
            WriteLine("Press any key to continue...");
            ReadKey();
            menu.Bootstrap();
        }
    }
}
