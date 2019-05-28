using System;
using System.Collections.Generic;
namespace Hypochondriac
{
    public class DataTypes
    {
        
            public struct PlayerINFO
        {
            public string name;
        }
        public struct CharacterINFO
        {
            public int age;
            public string name;
            public int money;
        }
        public struct GameSettings
        {
            public List<EventData> Events;
            public List<PlayerEvent> PlayerEvents;
            public int ease;
            public CharacterINFO chardata;
            public PlayerINFO playerdata;
            public string version;
        }
        public struct EventData
        {
            public string name;
            public string announce;
            public int moneyEffect;
            public bool killsChar;
            public bool killsDisease;
        }
        public struct PlayerEvent
        {
            public string command;
            public int cureChance;
            public int killChance;
            public string reply;
            public string killReply;
            public string cureReply;
        }
        
    }
}
