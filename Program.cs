using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using Spectre.Console;
namespace Stegosaurus
{
/*
 * Stegosaurus
 * 
 *~Jakob Smogawetz (Arwezom)
 * 
 * HTL Hallein
 * 
 */

    class Stegosaurus
    {
        static String LoadImage;
        [DllImport("kernel32.dll", EntryPoint = "GetConsoleWindow", SetLastError = true)]
        private static extern IntPtr GetConsoleHandle();
        static int selectedOption = 1;


        //Text To Binary Convert
        static string TextToBinary(string input)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            string binary = "";

            foreach (byte b in bytes)
            {
                binary += Convert.ToString(b, 2).PadLeft(8, '0');
            }
            return binary;
        }

        //Color Code For Bitmap
        public static int ToConsoleColor(System.Drawing.Color c)
        {
            int index = (c.R > 128 | c.G > 128 | c.B > 128) ? 8 : 0;
            index |= (c.R > 64) ? 4 : 0;
            index |= (c.G > 64) ? 2 : 0;
            index |= (c.B > 64) ? 1 : 0;
            return index;
        }

        //Draw Interface (Main Menu)
        static void DrawUi()
        {
            //Width & Height of Console
            System.Console.WindowWidth = 122;
            System.Console.WindowHeight = 42;

            //Center Anything
            int numSpaces = (Console.WindowWidth - (Console.WindowWidth / 2)) / 2;
            Console.Write(new string(' ', numSpaces));

            //Select
            string[] options =
            {
                "Load Image",
                "Show Bitmap",
                "Write Message",
                "Encrypt",
                "Safe Image",
                "Exit",
            };

            //Clear & Write Title
            Console.Clear();
            var title = new Table();
            title.Border = TableBorder.Heavy;
            title.Centered();
            title.AddColumn(new TableColumn("[yellow3_1]  ______                                                            _     _       \r\n / _____) _                                                        | |   (_)      \r\n( (____ _| |_ _____  ____ _____ ____   ___   ____  ____ _____ ____ | |__  _ _____ \r\n \\____ (_   _) ___ |/ _  (____ |  _ \\ / _ \\ / _  |/ ___|____ |  _ \\|  _ \\| | ___ |\r\n _____) )| |_| ____( (_| / ___ | | | | |_| ( (_| | |   / ___ | |_| | | | | | ____|\r\n(______/  \\__)_____)\\___ \\_____|_| |_|\\___/ \\___ |_|   \\_____|  __/|_| |_|_|_____)\r\n                   (_____|                 (_____|           |_|                  [/]").Centered());
            AnsiConsole.Write(title);

            //Write Options
            Console.WriteLine("                                                   ");
            Console.WriteLine("{0}{1} {2}                  ","                    ", selectedOption == 1 ? "[\x1B[33mx\x1B[97m]" : "[ ]", $"{options[0]}");
            Console.WriteLine("{0}{1} {2}                  ","                    ", selectedOption == 2 ? "[\x1B[33mx\x1B[97m]" : "[ ]", $"{options[1]}");
            Console.WriteLine("{0}{1} {2}                  ","                    ", selectedOption == 3 ? "[\x1B[33mx\x1B[97m]" : "[ ]", $"{options[2]}");
            Console.WriteLine("{0}{1} {2}                  ","                    ", selectedOption == 4 ? "[\x1B[33mx\x1B[97m]" : "[ ]", $"{options[3]}");
            Console.WriteLine("{0}{1} {2}                  ","                    ", selectedOption == 5 ? "[\x1B[33mx\x1B[97m]" : "[ ]", $"{options[4]}");
            Console.WriteLine("{0}{1} {2}                  ","                    ", selectedOption == 6 ? "[\x1B[33mx\x1B[97m]" : "[ ]", $"{options[5]}");
            Console.WriteLine("                                                   ");
            Console.WriteLine("                                                   ");
            Console.WriteLine("                                                   ");
            Console.WriteLine("                                       (Use The Arrow Keys And ENTER)");
        }

        //Select Option From Interface (Main Menu)
        static void RedirectToOption()
        {
            //Redirect From Menu to other Void
            switch (selectedOption)
            {
                case 1:
                    LoadFile();
                    break;
                case 2:
                    ShowImage();
                    break;

                case 3:
                    WriteMSG();
                    break;

                case 4:
                    Encrypt();
                    break;

                case 5:
                    SafeImage();
                    break;

                case 6:
                    Exit();
                    break;

                default:
                    return;
            }
        }

        //Main Programm (Interface)
        static void Main()
        {

        LabelMethodEntry:
            Console.Title = "Steganography Tool";
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.Clear();

        LabelDrawUi:

            //Set Cursor Position
            Console.SetCursorPosition(0, 4);

            //DrawUi
            DrawUi();

        LabelReadKey:

            //Read Key
            ConsoleKey pressedKey = Console.ReadKey(true).Key;

            //Check which Key pressed
            switch (pressedKey)
            {
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;

                case ConsoleKey.DownArrow:
                    selectedOption = (selectedOption + 1 <= 6) ? selectedOption + 1 : selectedOption;
                    goto LabelDrawUi;

                case ConsoleKey.UpArrow:
                    selectedOption = (selectedOption - 1 >= 1) ? selectedOption - 1 : selectedOption;
                    goto LabelDrawUi;

                case ConsoleKey.Enter:
                    RedirectToOption();
                    break;

                default:
                    goto LabelReadKey;
            }

            goto LabelMethodEntry;
        }

        //Load a Png or Jpg File
        static void LoadFile()
        {

            //Clear & Write Title
            Console.Clear();
            var title = new Table();
            title.Border = TableBorder.Heavy;
            title.Centered();
            title.AddColumn(new TableColumn("[yellow3_1]  ______                                                            _     _       \r\n / _____) _                                                        | |   (_)      \r\n( (____ _| |_ _____  ____ _____ ____   ___   ____  ____ _____ ____ | |__  _ _____ \r\n \\____ (_   _) ___ |/ _  (____ |  _ \\ / _ \\ / _  |/ ___|____ |  _ \\|  _ \\| | ___ |\r\n _____) )| |_| ____( (_| / ___ | | | | |_| ( (_| | |   / ___ | |_| | | | | | ____|\r\n(______/  \\__)_____)\\___ \\_____|_| |_|\\___/ \\___ |_|   \\_____|  __/|_| |_|_|_____)\r\n                   (_____|                 (_____|           |_|                  [/]").Centered());
            AnsiConsole.Write(title);
            Console.WriteLine();

            //Type In Path
            string textToEnter = "Please Write The Name Of The File:";
            Console.WriteLine("                    " + textToEnter);
            Console.WriteLine();
            Console.Write("                    "+ "[\u001b[33m>:\u001b[97m]");

            //Search File
            string DummyFile = Console.ReadLine();
            string directoryPath = Directory.GetCurrentDirectory();
            LoadImage = Path.Combine(directoryPath, DummyFile);
            Console.WriteLine(LoadImage);



            //Wait (Loading)
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            var Wait = new Table();
            Wait.Centered();
            Wait.Border = TableBorder.Markdown;
            Wait.AddColumn(new TableColumn("Wait ..?").Centered());
            AnsiConsole.Write(Wait);
            Thread.Sleep(2000);

            //Clear
            Console.Clear();

            //Write Title
            AnsiConsole.Write(title);

            //Check if File exists
            if (File.Exists(LoadImage))
            {
                //Accept
                var path = new TextPath(LoadImage);

                var Exist = new Table();
                Exist.Border = TableBorder.Heavy;
                Exist.Centered();
                Exist.AddColumn(new TableColumn("[green]File Loaded Succesfull[/]").Centered());
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                AnsiConsole.Write(Exist);

                //Back
                BackToMain();
            }
            else
            {
                //Deny
                var path = new TextPath(LoadImage);

                var NotExist = new Table();
                NotExist.Border = TableBorder.Heavy;
                NotExist.Centered();
                NotExist.AddColumn(new TableColumn("[red]File Does Not Exist[/]").Centered());
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                AnsiConsole.Write(NotExist);

                //Back
                BackToMain();

            }
        }

        //Show Bitmap of Png
        static async Task ShowImage()
        {
            //Clear & Write Title
            Console.Clear();
            var title = new Table();
            title.Border = TableBorder.Heavy;
            title.Centered();
            title.AddColumn(new TableColumn("[yellow3_1]  ______                                                            _     _       \r\n / _____) _                                                        | |   (_)      \r\n( (____ _| |_ _____  ____ _____ ____   ___   ____  ____ _____ ____ | |__  _ _____ \r\n \\____ (_   _) ___ |/ _  (____ |  _ \\ / _ \\ / _  |/ ___|____ |  _ \\|  _ \\| | ___ |\r\n _____) )| |_| ____( (_| / ___ | | | | |_| ( (_| | |   / ___ | |_| | | | | | ____|\r\n(______/  \\__)_____)\\___ \\_____|_| |_|\\___/ \\___ |_|   \\_____|  __/|_| |_|_|_____)\r\n                   (_____|                 (_____|           |_|                  [/]").Centered());
            AnsiConsole.Write(title);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //Check if Image was Loaded
            if (LoadImage == null || !File.Exists(LoadImage))
            {
                var NotExist = new Table();
                NotExist.Border = TableBorder.Heavy;
                NotExist.Centered();
                NotExist.AddColumn(new TableColumn("[red]Please First Load File[/]").Centered());
                AnsiConsole.Write(NotExist);
                BackToMain();
            }
            else
            {

                //Clear
                Console.Clear();

                //Title
                AnsiConsole.Write(title);

                //Wait
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                var Wait = new Table();
                Wait.Centered();
                Wait.Border = TableBorder.Markdown;
                Wait.AddColumn(new TableColumn("Wait ..?").Centered());
                AnsiConsole.Write(Wait);
                Thread.Sleep(2000);

                //Clear
                Console.Clear();

                //Title
                AnsiConsole.Write(title);


                //Write BitMap And Show Image
                Bitmap bmpSrc = new Bitmap(LoadImage, true);
                ConsoleWriteImage(bmpSrc);
            }
            //Back
            BackToMain();
        }

        //Make Bitmap form Png
        public static void ConsoleWriteImage(Bitmap bmpSrc)
        {
            //Size Of Bitmap (Can be Changed)
            int sMax = 41;


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

        //Wirte Message to Hide
        static void WriteMSG()
        {
            //Clear & Write Title
            Console.Clear();
            var title = new Table();
            title.Border = TableBorder.Heavy;
            title.Centered();
            title.AddColumn(new TableColumn("[yellow3_1]  ______                                                            _     _       \r\n / _____) _                                                        | |   (_)      \r\n( (____ _| |_ _____  ____ _____ ____   ___   ____  ____ _____ ____ | |__  _ _____ \r\n \\____ (_   _) ___ |/ _  (____ |  _ \\ / _ \\ / _  |/ ___|____ |  _ \\|  _ \\| | ___ |\r\n _____) )| |_| ____( (_| / ___ | | | | |_| ( (_| | |   / ___ | |_| | | | | | ____|\r\n(______/  \\__)_____)\\___ \\_____|_| |_|\\___/ \\___ |_|   \\_____|  __/|_| |_|_|_____)\r\n                   (_____|                 (_____|           |_|                  [/]").Centered());
            AnsiConsole.Write(title);

            //Text to Binary
            Console.WriteLine();
            string textToEnter = "Please Write The Message You Want to Encrypt:";
            Console.WriteLine("                    " + textToEnter);
            Console.Write("                    " + "[\u001b[33m>:\u001b[97m]");
            string userinput = Console.ReadLine();
            string inputBinary = TextToBinary(userinput);

            //Clear & Write Title
            Console.Clear();
            AnsiConsole.Write(title);

            //Wait
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            var Wait = new Table();
            Wait.Centered();
            Wait.Border = TableBorder.Markdown;
            Wait.AddColumn(new TableColumn("Wait ..?").Centered());
            AnsiConsole.Write(Wait);
            Thread.Sleep(3000);

            //Clear & Write Title
            Console.Clear();
            AnsiConsole.Write(title);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            //Succesfull
            var Exist = new Table();
            Exist.Border = TableBorder.Heavy;
            Exist.Centered();
            Exist.AddColumn(new TableColumn("[green]Message Loaded Succesfull[/]").Centered());
            AnsiConsole.Write(Exist);

            //Back
            BackToMain();
        }

        //TEST
        static void Encrypt()
        {
            Console.Clear();
        }

        //TEST
        static void SafeImage()
        {
            Console.Clear();
        }

        //Go Back to Main
        static void BackToMain()
        {
            Console.ReadKey();
        }

        //Exit Application
        static void Exit()
        {
            Environment.Exit(0);
        }
    }
    }


