using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Leadtools.ImageProcessing;
using Spectre.Console;

namespace Stegosaurus
{

    class Stegosaurus
    {
        static String LoadImage;
        [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow", SetLastError = true)]
        private static extern IntPtr GetConsoleHandle();
        static async Task Main()
        {
            System.Console.WindowWidth = 125;
            System.Console.WindowHeight = 40;
            //Clear
            Console.Clear();
            var title = new Table();
            title.Border = TableBorder.Heavy;
            title.Centered();
            title.AddColumn(new TableColumn("[blue]  __  ______  ____   ___   ___  __  __   ___     ___  ____   ___  ____  __  __ __  ____\r\n (( \\ | || | ||     // \\\\ // \\\\ ||\\ ||  // \\\\   // \\\\ || \\\\ // \\\\ || \\\\ ||  || || ||   \r\n  \\\\    ||   ||==  (( ___ ||=|| ||\\\\|| ((   )) (( ___ ||_// ||=|| ||_// ||==|| || ||== \r\n \\_))   ||   ||___  \\\\_|| || || || \\||  \\\\_//   \\\\_|| || \\\\ || || ||    ||  || || ||___\r\n                                                                                       [/]").Centered());

            //Select Title
            var select = new Table();
            select.Border = TableBorder.SimpleHeavy;
            select.AddColumn("Selection:");
            select.AddRow(" ");

            //Write Tiltle / Select 
            AnsiConsole.Write(title);
            Console.WriteLine();
            Console.WriteLine();
            AnsiConsole.Write(select);

            //Terminal Select Mode
            string Select = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                        .PageSize(10)
                        .AddChoices(new[] {
                            "Load File", "Show BitMap", "Write Message",
                            "Encrypt", "Save", "Exit",
                        }));

            //Select Interface
            switch (Select)
            {
                case "Load File":
                    LoadFile(); break;
                case "Show BitMap":
                    await ShowImage(); break;
                case "WriteMessage":
                    WriteMSG(); break;
                case "Encrypt":
                    Encrypt(); break;
                case "Save":
                    SafeImage(); break;
                case "Exit":
                    Exit(); break;
            }
        }
        //Load Image
        static void LoadFile()
        {
            Console.Clear();

            //Title
            var title = new Table();
            title.Border = TableBorder.Heavy;
            title.Centered();
            title.AddColumn(new TableColumn("[blue]  __  ______  ____   ___   ___  __  __   ___     ___  ____   ___  ____  __  __ __  ____\r\n (( \\ | || | ||     // \\\\ // \\\\ ||\\ ||  // \\\\   // \\\\ || \\\\ // \\\\ || \\\\ ||  || || ||   \r\n  \\\\    ||   ||==  (( ___ ||=|| ||\\\\|| ((   )) (( ___ ||_// ||=|| ||_// ||==|| || ||== \r\n \\_))   ||   ||___  \\\\_|| || || || \\||  \\\\_//   \\\\_|| || \\\\ || || ||    ||  || || ||___\r\n                                                                                       [/]").Centered());
            AnsiConsole.Write(title);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //Type In Path
            Console.WriteLine("Please Write The Name Of The Data File:");
            Console.WriteLine();
            LoadImage = @"C:\Users\jakob\OneDrive\Desktop\Stegosaurus\Stegosaurus\" + Console.ReadLine();

            //Loading Animation
            Console.WriteLine();
            AnsiConsole.Status()
            .Start("Searching...", ctx =>
                 {
                     // Simulate some work
                     Thread.Sleep(3000);
                 });


            if (File.Exists(LoadImage))
            {
                //Accept
                var path = new TextPath(LoadImage);

                var Exist = new Table();
                Exist.Border = TableBorder.Heavy;
                Exist.AddColumn(new TableColumn("[green]File Loaded Succesfull[/]"));
                AnsiConsole.Write(Exist);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                //Back
                BackToMain();
            }
            else
            {
                //Deny
                var path = new TextPath(LoadImage);

                var NotExist = new Table();
                NotExist.Border = TableBorder.Heavy;
                NotExist.AddColumn(new TableColumn("[red]File Does Not Exist[/]"));
                AnsiConsole.Write(NotExist);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                //Back
                BackToMain();

            }
        }
        static void BackToMain()
        {
            //Back Terminal
            string Back = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .AddChoices(new[] {
                        "Back", "Exit",
                    }));

            //Select Interface
            switch (Back)
            {
                case "Back":
                    Main(); break;
                case "Exit":
                    Exit(); break;
            }
        }

        //Draw Image
        static async Task ShowImage()
        {
            Console.Clear();
            //Title
            var title = new Table();
            title.Border = TableBorder.Heavy;
            title.Centered();
            title.AddColumn(new TableColumn("[blue]  __  ______  ____   ___   ___  __  __   ___     ___  ____   ___  ____  __  __ __  ____\r\n (( \\ | || | ||     // \\\\ // \\\\ ||\\ ||  // \\\\   // \\\\ || \\\\ // \\\\ || \\\\ ||  || || ||   \r\n  \\\\    ||   ||==  (( ___ ||=|| ||\\\\|| ((   )) (( ___ ||_// ||=|| ||_// ||==|| || ||== \r\n \\_))   ||   ||___  \\\\_|| || || || \\||  \\\\_//   \\\\_|| || \\\\ || || ||    ||  || || ||___\r\n                                                                                       [/]").Centered());
            AnsiConsole.Write(title);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            if (LoadImage == null)
            {
                Console.WriteLine("Please First Load an Image");
                Console.WriteLine();
                Console.WriteLine();
                BackToMain();
            }
            else
            {
                await AnsiConsole.Progress()
                .StartAsync(async ctx =>
            {
                // Define tasks
                var task1 = ctx.AddTask("-----------------------------[white]Justify Image[/]");
                var task2 = ctx.AddTask("-----------------------------[white]Create BitMap[/]");


                while (!ctx.IsFinished)
                        {
                            //Clear
                            Console.Clear();

                            //Title
                            AnsiConsole.Write(title);



                    // Simulate some work
                    Thread.Sleep(500);


                            //Increase Bar
                            task1.Increment(15);
                            task2.Increment(10);

                }
            });
                //Clear
                Console.Clear();

                //Title
                AnsiConsole.Write(title);

                //Write BitMap And Show Image
                Bitmap bmpSrc = new Bitmap(LoadImage, true);
                ConsoleWriteImage(bmpSrc);
            }
            Thread.Sleep(3000);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            BackToMain();
        }
        static void WriteMSG()
        {
            Console.Clear();
        }
        static void Encrypt()
        {
            Console.Clear();
        }
        static void SafeImage()
        {
            Console.Clear();
        }

        static void Exit()
        {
            Environment.Exit(0);
        }

        //Color Code for BitMap
        public static int ToConsoleColor(System.Drawing.Color c)
        {
            int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0;
            index |= (c.R > 64) ? 4 : 0;
            index |= (c.G > 64) ? 2 : 0;
            index |= (c.B > 64) ? 1 : 0;
            return index;
        }

        //Create Bitmap 
        //https://stackoverflow.com/questions/33538527/display-an-image-in-a-console-application
        public static void ConsoleWriteImage(Bitmap bmpSrc)
        {
            int sMax = 40;
            decimal percent = Math.Min(decimal.Divide(sMax, bmpSrc.Width), decimal.Divide(sMax, bmpSrc.Height));
            System.Drawing.Size resSize = new System.Drawing.Size((int)(bmpSrc.Width * percent), (int)(bmpSrc.Height * percent));
            Func<System.Drawing.Color, int> ToConsoleColor = c =>
            {
                int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0;
                index |= (c.R > 64) ? 4 : 0;
                index |= (c.G > 64) ? 2 : 0;
                index |= (c.B > 64) ? 1 : 0;
                return index;
            };
            Bitmap bmpMax = new Bitmap(bmpSrc, resSize.Width * 2, resSize.Height * 2);
            for (int i = 0; i < resSize.Height; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write("                    ");

                for (int j = 0; j < resSize.Width; j++)
                {
                    Console.ForegroundColor = (ConsoleColor)ToConsoleColor(bmpMax.GetPixel(j * 2, i * 2));
                    Console.BackgroundColor = (ConsoleColor)ToConsoleColor(bmpMax.GetPixel(j * 2, i * 2 + 1));
                    Console.Write("▀");

                    Console.ForegroundColor = (ConsoleColor)ToConsoleColor(bmpMax.GetPixel(j * 2 + 1, i * 2));
                    Console.BackgroundColor = (ConsoleColor)ToConsoleColor(bmpMax.GetPixel(j * 2 + 1, i * 2 + 1));
                    Console.Write("▀");
                }
                System.Console.WriteLine();
            }
        }
    }
}


