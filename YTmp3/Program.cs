using System;
using System.IO;
using VideoLibrary;
using MediaToolkit;
using MediaToolkit.Model;

namespace YT_Downloader {
    class Program {
        static void Main() {
            var mainMenu = new Menu("cyprus's youtube downloader", new string[] { "Download Audio and Video", "Download Audio Only", "Download Video Only", "Exit" });
            
            while (true)
            {
                string destination, url;
                
                int selectedIndex = mainMenu.Run();
                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        GetInfo(out destination, out url);
                        SaveBoth(@destination, url);
                        break;
                    case 1:
                        Console.Clear();
                        GetInfo(out destination, out url);
                        SaveMp3(@destination, url);
                        break;
                    case 2:
                        Console.Clear();
                        GetInfo(out destination, out url);
                        SaveMp4(@destination, url);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }

                SendMessage("Press 'q' to quit or any other key to download again...\n");
                var r = Console.ReadKey();
                if (r.KeyChar == 'q') break;
            }
        }

        private static void GetInfo(out string destination, out string url) {
            SendMessage("Paste the video URL: ");
            url = Console.ReadLine();
            
            SendMessage("Paste the location where you want the file saved: ");
            destination = Console.ReadLine();
            if (!Directory.Exists(@destination))
            {
                SendMessage("Invalid File Path.\n");
                return;
            }
        }

        private static void SaveBoth(string destination, string url) {
            Console.Clear();
            SendMessage("Searching for video...");

            if (!ValidURL(url)) return;

            var yt = YouTube.Default;
            var vid = yt.GetVideo(url);
            SendMessage($"\"{vid.FullName}\" found, downloading...");
            File.WriteAllBytes(destination + vid.FullName, vid.GetBytes());

            var inputFile = new MediaFile { Filename = destination + vid.FullName };
            var outputFile = new MediaFile { Filename = destination + vid.FullName + ".mp3" };

            using (var engine = new Engine())
            {
                SendMessage("Extracting mp3...");
                engine.Convert(inputFile, outputFile);
            }

            SendMessage($"Audio (.mp3) and video (.mp4) saved at: {destination} successfilly.");
        }
        private static void SaveMp3(string destination, string url) {
            Console.Clear();
            SendMessage("Searching for video...");

            if (!ValidURL(url)) return;

            var yt = YouTube.Default;
            var vid = yt.GetVideo(url);
            SendMessage($"\"{vid.FullName}\" found, downloading...");
            File.WriteAllBytes(destination + vid.FullName, vid.GetBytes());

            var inputFile = new MediaFile { Filename = destination + vid.FullName };
            var outputFile = new MediaFile { Filename = destination + vid.FullName + ".mp3" };

            using (var engine = new Engine())
            {
                SendMessage("Converting to mp3...");
                engine.Convert(inputFile, outputFile);
                File.Delete(destination + vid.FullName);
            }

            SendMessage($"Audio (.mp3) saved at: {destination} successfilly.\n");
        }
        private static void SaveMp4(string destination, string url) {
            Console.Clear();
            SendMessage("Searching for video...");

            if (!ValidURL(url)) return;

            var yt = YouTube.Default;
            var vid = yt.GetVideo(url);
            SendMessage($"\"{vid.FullName}\" found, downloading...");
            File.WriteAllBytes(destination + vid.FullName, vid.GetBytes());

            SendMessage($"Video (.mp4) saved at: {destination} successfilly.\n");
        }

        private static bool ValidURL(string url) {
            var yt = YouTube.Default;

            try
            {
                yt.GetVideo(url);
            }
            catch (Exception ex)
            {
                SendMessage($"Invalid URL. (Error: {ex.Message})");
                return false;
            }
            return true;
        }

        private static void SendMessage(string message) {
            Console.WriteLine($"[{DateTime.Now}] {message}");
        }
    }
}