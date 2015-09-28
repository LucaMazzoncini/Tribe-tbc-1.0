﻿using System;
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

        public enum ManaEvent //questo enum definisco per quali eventi puo' essere richiesto il mana
        {
            None,NewRound
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
            None, Water, Earth, Fire, Magma, Vapor, Flora, Ancestral, Ritual, Elemental, Spirit, Tank, Healer, Dps, Playable, Rank1, Rank2, Rank3
        }
        public enum Target
        {
            None, Player,Elemental,Spirit
        }
    }
}
