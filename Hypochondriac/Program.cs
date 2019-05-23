using System;
using Newtonsoft.Json;
using System.IO;
namespace Hypochondriac
{
    public class Workings
    {
        public static DataTypes.GameSettings GlobalSettings;
        public static int score;
        public static DataTypes.EventData gevent;
        public static DataTypes.PlayerEvent playerevent;
        public static int money = Workings.GlobalSettings.chardata.money;
        public static bool doesDie()
        {
            var rand = new Random();
            if (rand.Next(1, 10) <= playerevent.killChance) return true;
            return false;
        }
        public static bool isCured()
        {
            var rand = new Random();
            if (rand.Next(1, 10) <= playerevent.killChance) return true;
            return false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to hypochondriac");
            Console.WriteLine("You are " + Workings.GlobalSettings.playerdata.name + " and your character's name is " + Workings.GlobalSettings.chardata.name);
            try { Workings.GlobalSettings = JsonConvert.DeserializeObject<DataTypes.GameSettings>(File.ReadAllText("gamesettings.json")); }
            catch (Exception)
            {
                Console.WriteLine("please ensure a valid gamesettings.json file is present, you may have to build one with the editor");
                Console.ReadKey();
                return;
            }
            var gamestuff = new MainGame(Workings.GlobalSettings);
            while (true) {
                if (Workings.GlobalSettings.chardata.money <=0)
                {
                    Console.WriteLine("You ran out of money, visits to the doctor cost money, you know");
                    Console.WriteLine("Score: " + Workings.score);
                    Console.ReadKey();
                    return;
                }
                try
                {
                    Workings.gevent = Workings.GlobalSettings.Events[gamestuff.eventCalc()];
                    Console.WriteLine(Workings.gevent.announce);
                    Workings.GlobalSettings.chardata.money += Workings.gevent.moneyEffect;
                    if (Workings.gevent.killsChar)
                    {
                        Console.WriteLine("Score: " + Workings.score + " your greedy SOB kid's inheritance is " + Workings.GlobalSettings.chardata.money);
                        Console.ReadKey();
                        return;
                    }
                    if (Workings.gevent.killsDisease)
                    {
                        Workings.score++;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("No event this frame");
                }
                Console.WriteLine("You have "+Workings.GlobalSettings.chardata.money+" money left");
                Console.WriteLine("enter command");
                try
                {
                    Workings.playerevent = Workings.GlobalSettings.PlayerEvents.Find(x => x.command.Contains(Console.ReadLine()));
                    if (Workings.doesDie())
                    {
                        Console.WriteLine(Workings.playerevent.killReply);
                        Console.WriteLine("Score: " + Workings.score + " your greedy SOB kid's inheritance is "+Workings.money);
                        Console.ReadKey();
                        return;
                    }
                    else if (Workings.isCured())
                    {
                        Console.WriteLine(Workings.playerevent.cureReply);
                        Workings.score++;
                    }
                    else Console.WriteLine(Workings.playerevent.reply);
                }
                catch (Exception)
                {
                    Console.WriteLine("Unknown command, skipping...");
                }
            }

        }
    }
}
