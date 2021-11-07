using static System.Console;
using System;
using System.Collections.Generic;
using Quoridor.Core;
using Quoridor.Core.Models;
using Quoridor.Player;
using Quoridor.AI;
using Quoridor.ExternalInterface;

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
            actions.Add(MenuActionType.CONTROLS, Controls);
            menu = new Menu(actions);
        }

        static void Main(string[] args)
        {

            if (args.Length > 0 && args[0] == "--external")
            {
                StartAvA();
            }
            else
            {
                menu.Show();
            }
        }

        private static void StartPvP()
        {
            int playersCount = 2;
            PlayerConnection[] players = new PlayerConnection[playersCount];
            for (int i = 0; i < playersCount; i++)
            {
                Write("Name for Player " + (i + 1) + ": ");
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

        private static void StartPvA()
        {
            gameEngine.Initialize(2);
            PlayerConnection playerConnection = new PlayerConnection(gameEngine);
            RandomBot randomBot = new RandomBot(gameEngine);
            gameEngine.Connect(playerConnection);
            gameEngine.Connect(randomBot);
            gameEngine.Start();
        }

        private static void StartAvA()
        {
            gameEngine.Initialize(2);
            Connection first;
            Connection second;
            Write("-> ");
            string color = ReadLine();
            if (color == "black")
            {
                first =  new RandomBot(gameEngine); // Replace with improved bot
                second = new ExternalConnection(gameEngine);
            }
            else if (color == "white")
            {
                first = new ExternalConnection(gameEngine);
                second = new RandomBot(gameEngine); // Replace with improved bot
            }
            else
            {
                throw new ArgumentException("Invalid color!");
            }
            gameEngine.Connect(first);
            gameEngine.Connect(second);
            gameEngine.Start();
        }

        private static void Controls()
        {
            WriteLine("Controls:");
            WriteLine("'move A1' - move to position A1");
            WriteLine("'wall A1 A2 B1 B2' - place wall between A1 A2 and B1 B2");
            WaitAnyKey();
            menu.Show();
        }

        private static void Exit()
        {
            Environment.Exit(0);
        }

        private static void OnFinish()
        {
            WaitAnyKey();
            menu.Show();
        }

        private static void WaitAnyKey()
        {
            WriteLine();
            WriteLine("Press any key to continue...");
            ReadKey();
        }
    }
}
