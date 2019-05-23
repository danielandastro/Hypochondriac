using System;
namespace Hypochondriac
{
    public class DefaultSettings
    {
        public DataTypes.CharacterINFO DefaultCharacter()
        {
            var returndata = new DataTypes.CharacterINFO();
            var rand = new Random();
            returndata.age = rand.Next(1, 99);
            returndata.money = rand.Next(1, 50);
            returndata.name = "John Smith";
            return returndata;
        }
        public DataTypes.PlayerINFO DefaultPlayer()
        {
            var returndata = new DataTypes.PlayerINFO();
            returndata.name = "Player 1";
            return returndata;
        }
    }
}
