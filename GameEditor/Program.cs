using System;
using Hypochondriac;
using System.Collections.Generic;
using System.IO;
namespace GameEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the hypochondriac game editor");
            Console.WriteLine("Would you like to 1) edit game events, 2) edit player information, 3) edit player events, 4) edit character information or 5) compile a gamesettings file");
            switch (Console.ReadLine())
            {
                case "1":
                    EventsEditor();
                    break;
                case "2":
                    playereditor();
                    break;
                case "3":
                    PlayerEventsEditor();
                    break;
                case "4":
                    charactereditor();
                    break;
                case "5":
                    gameSettingCreator();
                        break;

                default:
                    Console.WriteLine("invalid input");
                    break;


            }
        }
        private static void gameSettingCreator()
        {
            var data = new DataTypes.GameSettings();
            data.chardata = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTypes.CharacterINFO>(File.ReadAllText("charinfo.json"));
            Console.WriteLine("Read Character Data");
            data.Events = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataTypes.EventData>>(File.ReadAllText("gameevents.json"));
            Console.WriteLine("Read Game events");
            data.playerdata= Newtonsoft.Json.JsonConvert.DeserializeObject<DataTypes.PlayerINFO>(File.ReadAllText("playerinfo.json"));

            Console.WriteLine("Read player Data");
            data.PlayerEvents = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataTypes.PlayerEvent>>(File.ReadAllText("playerevents.json"));
            Console.WriteLine("Read Player Events");
            Console.WriteLine("Enter ease (0 - 100 recommended)");
            data.ease = int.Parse(Console.ReadLine());
            File.WriteAllText("gamesettings.json", Newtonsoft.Json.JsonConvert.SerializeObject(data));

        }
        private static void playereditor()
        {
            var info = new DataTypes.PlayerINFO();
            if (File.Exists("playerinfo.json"))
            {
                Console.WriteLine("The current player file contains: ");
                info = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTypes.PlayerINFO>(File.ReadAllText("playerinfo.json"));
                Console.WriteLine("Name: "+info.name);
            }
            else File.Create("playerinfo.json");
            Console.WriteLine("Enter new name");
            info.name = Console.ReadLine();
            File.WriteAllText("playerinfo.json", Newtonsoft.Json.JsonConvert.SerializeObject(info));
        }
        private static void charactereditor()
        {
            var info = new DataTypes.CharacterINFO();
            if (File.Exists("charinfo.json"))
            {
                Console.WriteLine("The current character file contains: ");
                info = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTypes.CharacterINFO>(File.ReadAllText("charinfo.json"));
                Console.WriteLine("Name: " + info.name);
                Console.WriteLine("Age: " + info.age);
                Console.WriteLine("Money: " + info.money);

            }
            else File.Create("charinfo.json");
            Console.WriteLine("Enter new name");
            info.name = Console.ReadLine();
            Console.WriteLine("Enter new age");
            info.age = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter new starting money");
            info.money = int.Parse(Console.ReadLine());
            File.WriteAllText("charinfo.json", Newtonsoft.Json.JsonConvert.SerializeObject(info));
        }
        private static void EventsEditor()
        {
            var list1 = new List<DataTypes.EventData>();
            if (File.Exists("gameevents.json"))
            {
                Console.WriteLine("The current gameevents file contains: ");
                list1 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataTypes.EventData>>(File.ReadAllText("gameevents.json"));
                foreach (var eventt in list1)
                {
                    Console.WriteLine("Name: " + eventt.name);
                    Console.WriteLine("The game says: " + eventt.announce);
                    Console.WriteLine("Money is increased by: " + eventt.moneyEffect);
                    if (eventt.killsChar) { Console.WriteLine("It kills the character"); }
                    else Console.WriteLine("It does not kill the character");
                    if (eventt.killsDisease) { Console.WriteLine("It kills the disease"); }
                    else Console.WriteLine("It does not kill the disease");
                    Console.WriteLine("________________");
                    Console.WriteLine("");
                }
            }
            else File.Create("gameevents.json");

            var eventData1 = new DataTypes.EventData();
            var end = false;
            while (!end)
            {
                eventData1.killsChar = false;
                eventData1.killsDisease = false;
                Console.WriteLine("Add event name");
                eventData1.name = Console.ReadLine();
                Console.WriteLine("What should the game say?");
                eventData1.announce = Console.ReadLine();
                Console.WriteLine("How much money should be added (or removed)");
                eventData1.moneyEffect = int.Parse(Console.ReadLine());
                Console.WriteLine("Does the event kill the player");
                if (Console.ReadLine().Contains('y')) eventData1.killsChar = true;
                Console.WriteLine("Does the event kill the disease");
                if (Console.ReadLine().Contains('y')) eventData1.killsDisease = true;
                Console.WriteLine("Would you like to add more events");
                if (Console.ReadLine().Contains('n')) end = true;
                list1.Add(eventData1);
            }
            File.WriteAllText("gameevents.json", Newtonsoft.Json.JsonConvert.SerializeObject(list1));
        }
        private static void PlayerEventsEditor()
        {
            var list1 = new List<DataTypes.PlayerEvent>();
            if (File.Exists("playerevents.json"))
            {
                Console.WriteLine("The current playerevents file contains: ");
                list1 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataTypes.PlayerEvent>>(File.ReadAllText("playerevents.json"));
                foreach (var eventt in list1)
                {
                    Console.WriteLine("Name: " + eventt.command);
                    Console.WriteLine("The game says: " + eventt.reply);
                    Console.WriteLine("The game says if you die: " + eventt.killReply);
                    Console.WriteLine("The game says if you are cured: " + eventt.cureReply);
                    Console.WriteLine("Kill chance is" + eventt.killChance);
                    Console.WriteLine("Cure chance is" + eventt.cureChance);
                    Console.WriteLine("________________");
                    Console.WriteLine("");
                }
            }
            else File.Create("playerevents.json");

            var eventData1 = new DataTypes.PlayerEvent();
            var end = false;
            while (!end)
            {
                Console.WriteLine("Add event command");
                eventData1.command = Console.ReadLine();

                Console.WriteLine("What should the game say?");
                eventData1.reply = Console.ReadLine();

                Console.WriteLine("What should the game say if you die?");
                eventData1.killReply = Console.ReadLine();

                Console.WriteLine("What should the game say if you are cured?");
                eventData1.cureReply = Console.ReadLine();

                Console.WriteLine("Kill chance (0-10)");
                eventData1.killChance = int.Parse(Console.ReadLine());

                Console.WriteLine("Cure chance (0-10)");
                eventData1.cureChance = int.Parse(Console.ReadLine());

                Console.WriteLine("Would you like to add more events");
                if (Console.ReadLine().Contains('n')) end = true;
                list1.Add(eventData1);
            }
            File.WriteAllText("playerevents.json", Newtonsoft.Json.JsonConvert.SerializeObject(list1));
        }
    }
}