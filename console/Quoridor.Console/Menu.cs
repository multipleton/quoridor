using static System.Console;
using static System.ConsoleKey;
using System.Collections.Generic;
using System;

namespace Quoridor.Console
{
    class Menu
    {
        private MenuActionType currentMenuActionType;
        private Dictionary<MenuActionType, Action> actions;

        public Menu(Dictionary<MenuActionType, Action> actions)
        {
            currentMenuActionType = MenuActionType.PVP;
            this.actions = actions;
        }

        public void Bootstrap()
        {
            PrintMenu();
            HandleKeyInput();
        }

        private void PrintMenu()
        {
            string[] itemsList =
            {
                "Player vs. Player",
                "Player vs. AI",
                "Exit"
            };
            Clear();
            WriteLine("Welcome to Quoridor Game!");
            WriteLine("Use up / down arrows to navigate and 'Enter' to select:");
            for (int i = 0; i < itemsList.Length; i++)
            {
                string prefix = "  ";
                string postfix = "  ";
                if ((int)currentMenuActionType == i)
                {
                    prefix = "> ";
                    postfix = " <";
                }
                Write(prefix);
                Write(itemsList[i]);
                Write(postfix);
                WriteLine();
            }
            HandleKeyInput();
        }

        private void HandleKeyInput()
        {
            var key = ReadKey().Key;
            switch (key)
            {
                case UpArrow:
                    ChangeMenuItem(false);
                    return;
                case DownArrow:
                    ChangeMenuItem(true);
                    return;
                case Enter:
                    PickMenuItem();
                    return;
            }
        }

        private void ChangeMenuItem(bool direction)
        {
            if (direction)
            {
                currentMenuActionType++;
            }
            else
            {
                currentMenuActionType--;
            }
            if ((int)currentMenuActionType < 0)
            {
                currentMenuActionType = MenuActionType.EXIT;
            }
            else if ((int)currentMenuActionType > (int)MenuActionType.EXIT)
            {
                currentMenuActionType = MenuActionType.PVP;
            }
            PrintMenu();
        }

        private void PickMenuItem()
        {
            actions[currentMenuActionType]();
        }
    }
}
