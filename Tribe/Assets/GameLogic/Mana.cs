using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GameLogic
{
    
    public class Mana
    {
        #region variables
        public const int INITMANA = 10;
        public const int MAXMANA  = 20;
        public const int MAXINITMANA = 3;
        public const int COSTFIRSTPOOL = 1;
        public const int COSTSECONDPOOL = 2;
        public Dictionary<Enums.Mana, int> valueList { get; set; }
        public Dictionary<Enums.Mana, int> poolList { get; set; }
        #endregion

        #region methods
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
            for (int i = 0; i < INITMANA; i++)
            {
                int randomNumber = random.Next(1, 6);
                switch (randomNumber)
                {
                    case 1:
                        if (valueList[Enums.Mana.Earth] == MAXINITMANA)
                            i--;
                        else
                            valueList[Enums.Mana.Earth] += 1;
                        break;
                    case 2:
                        if (valueList[Enums.Mana.Fire] == MAXINITMANA)
                            i--;
                        else
                            valueList[Enums.Mana.Fire] += 1;
                        break;
                    case 3:
                        if (valueList[Enums.Mana.Water] == MAXINITMANA)
                            i--;
                        else
                            valueList[Enums.Mana.Water] += 1;
                        break;
                    case 4:
                        if (valueList[Enums.Mana.Life] == MAXINITMANA)
                            i--;
                        else
                            valueList[Enums.Mana.Life] += 1;
                        break;
                    case 5:
                        if (valueList[Enums.Mana.Death] == MAXINITMANA)
                            i--;
                        else
                            valueList[Enums.Mana.Death] += 1;
                        break;
                    default:
                        break;
                }
            }
        }
        public bool PayMana(Dictionary<Enums.Mana, int> param) //se si può pagare il costo di mana lo paga e torna true altrimenti false
        {
            bool canPay = true;
            canPay = CanPay(param);
  
            if(canPay) //se posso pagare pago
                foreach (KeyValuePair<Enums.Mana, int> manaTemp in param)
                    valueList[manaTemp.Key] -= manaTemp.Value;
                         
            return canPay; //ritorno true se posso pagare
        }
        public bool CanPay(Dictionary<Enums.Mana, int> param) //serve per sapere a priori se posso castare qualcosa
        {
            bool canPay = true;
            foreach (KeyValuePair<Enums.Mana, int> manaTemp in param) //controllo se posso pagare
                if (valueList[manaTemp.Key] < manaTemp.Value)
                    canPay = false;
            return canPay;
        }
        public bool AddMana(Dictionary<Enums.Mana, int> param) //se uno dei mana che aggiungiamo è già a maxMana ritorna false
        {
            bool capNotReached = true;
            foreach (KeyValuePair<Enums.Mana, int> manaTemp in param) //controllo se posso pagare
            {
                if (valueList[manaTemp.Key] + manaTemp.Value > MAXMANA)
                    {
                        capNotReached = false;
                        valueList[manaTemp.Key] = MAXMANA;
                    }
                else
                        valueList[manaTemp.Key] += manaTemp.Value;
            }
            return capNotReached;
        }
        public bool incMana(Enums.Mana param)
        {
                Dictionary<Enums.Mana, int> temp = new Dictionary<Enums.Mana, int>();
                temp.Add(param, 1);
                return AddMana(temp);
        } //incrementa il mana di un elemento, se è già a maxMana torna false
        public bool decMana(Enums.Mana param)
        {
            Dictionary<Enums.Mana, int> temp = new Dictionary<Enums.Mana, int>();
            temp.Add(param, 1);
            return PayMana(temp);
        } //decrementa il mana di un elemento, se è già a 0 torna false
        public Enums.Mana addRandomMana() { return Enums.Mana.None; } //Aggiunge un mana random e se sono tutti a maxMana torna None altrimenti il mana aggiunto
        public bool canCreatePool(Enums.Mana param) { return true; } //controlla se posso creare la polla di un tipo (costo/quantità)
        public bool createPool(Enums.Mana param) { return true; }    //crea la polla di mana del tipo specificato, altrimenti torna false
        public bool addManaPool() { return true; } //aggiunge il mana delle polle alla riserva di mana (torna false se non si può sommare anche solo un mana) quindi se stiamo bruciando mana
        
        #endregion
    }
}
