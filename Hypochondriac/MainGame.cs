using System;
namespace Hypochondriac
{
    public class MainGame
    {
        public int eventCount = 0, ease = 0;
        public MainGame(DataTypes.GameSettings settings)
        {
            eventCount = settings.Events.Count;
            ease = settings.ease;
        }
        public int eventCalc()
        {
            Random rand = new Random();
            return rand.Next(eventCount+ease);
        }

    }
}