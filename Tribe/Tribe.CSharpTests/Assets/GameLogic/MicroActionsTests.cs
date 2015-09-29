using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLogic.Tests
{
    [TestClass()]
    public class MicroActionsTests
    {
        [TestMethod()]
        public void getTargetsTest()
        {
            string action = "Armor.2 Heal.2 Damage.1 AddMana Asleep";
            List<Enums.Target> targets = MicroActions.getTargets(action);

            Assert.IsTrue(targets.Count > 0);
        }
    }
}