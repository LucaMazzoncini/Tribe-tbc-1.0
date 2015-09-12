using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    class Enums
    {
        public enum Type
        {
            None, Spell, Elemental, Spirit, Shaman
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
            None, Quickness, Guardian
        }
    }
}
