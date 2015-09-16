using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GameLogic
{
    
    class Mana
    {
        public const int initMana = 10;
        Dictionary<Enums.Mana, int> valueList;
        Dictionary<Enums.Mana, int> poolList;
        public Dictionary<Enums.Mana, int> Init()
        {

            Dictionary<Enums.Mana, int> param = new Dictionary<Enums.Mana, int>();
            param.Add(Enums.Mana.Earth, 0);
            param.Add(Enums.Mana.Fire, 0);
            param.Add(Enums.Mana.Water, 0);
            param.Add(Enums.Mana.Life, 0);
            param.Add(Enums.Mana.Death, 0);
            return param;
        }
        public Mana()
        {
            valueList = Init();
            poolList = Init();
            Random random = new Random();
            for (int i = 0; i < initMana; i++)
            {
                int randomNumber = random.Next(1, 6);
                switch (randomNumber)
                {
                    case 1:
                        if (valueList[Enums.Mana.Earth] > 2)
                            i--;
                        else
                            valueList[Enums.Mana.Earth] += 1;
                        break;
                    case 2:
                        if (valueList[Enums.Mana.Fire] > 2)
                            i--;
                        else
                            valueList[Enums.Mana.Fire] += 1;
                        break;
                    case 3:
                        if (valueList[Enums.Mana.Water] > 2)
                            i--;
                        else
                            valueList[Enums.Mana.Water] += 1;
                        break;
                    case 4:
                        if (valueList[Enums.Mana.Life] > 2)
                            i--;
                        else
                            valueList[Enums.Mana.Life] += 1;
                        break;
                    case 5:
                        if (valueList[Enums.Mana.Death] > 2)
                            i--;
                        else
                            valueList[Enums.Mana.Death] += 1;
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
