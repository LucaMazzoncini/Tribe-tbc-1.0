﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    class SelfDamagePower : ArmorAction
    {
        private int cooldown = 0; //il cooldown del potere della creatura
        public SelfDamagePower(int n,int cd) : base(n)
        {
            cooldown = cd;
            value = n;
        }
        public override void Execute(string target)
        {
            throw new NotImplementedException();
        }

    }
}