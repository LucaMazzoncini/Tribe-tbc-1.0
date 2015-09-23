using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class Enums
    {
        public enum Type
        {
            None, Ritual, Elemental, Spirit, Shaman
        }

        public enum SubType
        {
            None, Water, Earth, Fire, Magma, Vapor, Flora, Ancestral
        }

        public enum Role
        {
            None, Tank, Healer, Dps
        }

        public enum Mana
        {
            None, Earth, Water, Fire, Life, Death
        }

        public enum Properties
        {
            None, Quickness, Guardian ,Penetrate , Rage , Thorns1
        }
        public enum Debuff
        {
            None, Quickness, Guardian, Penetrate, Rage, Thorns1
        }
        public enum Filter
        {
            None, Water, Earth, Fire, Magma, Vapor, Flora, Ancestral, Ritual, Elemental, Spirit, Shaman, Tank, Healer, Dps, Playable
        }
        public enum Target
        {
            None, Player,Elemental,Spirit
        }
    }
}
