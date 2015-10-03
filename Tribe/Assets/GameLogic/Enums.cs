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
            None, Water, Earth, Fire, Life, Death
        }

        public enum ManaEvent //questo enum definisco per quali eventi puo' essere richiesto il mana
        {
            None,NewRound
        }

        public enum Properties
        {
            None, Quickness, Guardian ,Penetrate ,Thunderborn ,Thorns , Armor
        }
        public enum Buff
        {
            None, Shield, Immortal, Quickness, Guardian, Penetrate, Thunderborn, Thorns1, IncreasedStr, IncreasedCon
        }
        public enum Debuff
        {
            None, Incurable, Asleep, Poison, DecreasedStr, DecreasedCon
        }
        public enum Filter
        {
            None, Water, Earth, Fire, Magma, Vapor, Flora, Ancestral, Ritual, Elemental, Spirit, Tank, Healer, Dps, Playable, Rank1, Rank2, Rank3
        }
        public enum Target
        {
            None, Player,Elemental,Spirit,Mana
        }
    }
}
