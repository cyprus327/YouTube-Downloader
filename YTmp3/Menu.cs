using System;

namespace YT_Downloader {
    public class Menu {
        public Menu(string title, string[] contents) {
            Title = title;
            Contents = contents;
            Selected = 0;
        }
        private readonly string Title;
        private readonly string[] Contents;
        private int Selected;

        private void RefreshContents() {
            Console.WriteLine(Title + "\n");
            for (int i = 0; i < Contents.Length; i++)
            {
                string selectedVisual;
                if (i == Selected)
                {
                    selectedVisual = " >";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    selectedVisual = "  ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                string current = Contents[i];
                Console.WriteLine($"{selectedVisual} [ {current} ]");
            }
            Console.ResetColor();
        }

        public int Run() {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                RefreshContents();
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    Selected--;
                    if (Selected == -1)
                    {
                        Selected = Contents.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    Selected++;
                    if (Selected == Contents.Length)
                    {
                        Selected = 0;
                    }
                }
            }
            while (keyPressed != ConsoleKey.Enter);
            return Selected;
        }
    }
}
