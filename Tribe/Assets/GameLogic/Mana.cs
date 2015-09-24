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
        public int TOTALMANA;
        public Dictionary<Enums.Mana, int> valueList { get; set; } //riserva di mana
        public Dictionary<Enums.Mana, int> poolList { get; set; } // polle
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
                        if (valueList[Enums.Mana.Earth] == MAXINITMANA )
                            i--;
                        else
                            valueList[Enums.Mana.Earth] += 1;
                            ++TOTALMANA;

                        break;
                    case 2:
                        if (valueList[Enums.Mana.Fire] == MAXINITMANA)
                            i--;
                        else
                            valueList[Enums.Mana.Fire] += 1;
                            ++TOTALMANA;
                        break;
                    case 3:
                        if (valueList[Enums.Mana.Water] == MAXINITMANA)
                            i--;
                        else
                            valueList[Enums.Mana.Water] += 1;
                            ++TOTALMANA;
                        break;
                    case 4:
                        if (valueList[Enums.Mana.Life] == MAXINITMANA)
                            i--;
                        else
                            valueList[Enums.Mana.Life] += 1;
                            ++TOTALMANA;
                        break;
                    case 5:
                        if (valueList[Enums.Mana.Death] == MAXINITMANA)
                            i--;
                        else
                            valueList[Enums.Mana.Death] += 1;
                            ++TOTALMANA;
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

            if (canPay) //se posso pagare pago
                foreach (KeyValuePair<Enums.Mana, int> manaTemp in param)
                {
                    valueList[manaTemp.Key] -= manaTemp.Value;
                    TOTALMANA -= manaTemp.Value;
                }
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
        public bool AddMana(Dictionary<Enums.Mana, int> param) //se TOTALMANA è già a maxMana ritorna false
        {
            bool capNotReached = true;
            foreach (KeyValuePair<Enums.Mana, int> manaTemp in param) //controllo se posso aggiungere
                if (TOTALMANA + manaTemp.Value > MAXMANA)
                {
                    capNotReached = false;
                }
                else
                {
                    valueList[manaTemp.Key] += manaTemp.Value; // aggiungo
                    TOTALMANA += manaTemp.Value;
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
        public Enums.Mana addRandomMana()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 6);
            if (TOTALMANA >= MAXMANA)
                return Enums.Mana.None;
            else
            {
                switch (randomNumber)
                {
                    case 1:
                       
                            valueList[Enums.Mana.Earth] += 1;
                            ++TOTALMANA;
                            return Enums.Mana.Earth;

                    case 2:
                       
                            valueList[Enums.Mana.Fire] += 1;
                            ++TOTALMANA;
                            return Enums.Mana.Fire;
                    
                    case 3:
                       
                            valueList[Enums.Mana.Water] += 1;
                            ++TOTALMANA;
                            return Enums.Mana.Water;
                      
                    case 4:
                       
                            valueList[Enums.Mana.Life] += 1;
                            ++TOTALMANA;
                            return Enums.Mana.Life;
                      
                    case 5:
                        
                            valueList[Enums.Mana.Death] += 1;
                            ++TOTALMANA;
                            return Enums.Mana.Death;
                      
                    default:
                        break;
                }
            }
                        return Enums.Mana.None;
        } //Se TOTALMANA >= MAXMANA torna None, altrimenti aggiunge un mana random e ritorna il tipo di mana aggiunto

        public bool canCreatePool(Enums.Mana thatMana)
        {
            bool canPay = false;
            if (poolList[thatMana] < 2)//se ho già 2 polle di quel tipo ritorno false
            {
               
                int costPool = 0;
                if (poolList[thatMana] == 0)//controllo quante polle del mana richiesto ho già
                    costPool = COSTFIRSTPOOL;
                else
                    costPool = COSTSECONDPOOL;
                           
                    if (valueList[thatMana] >= costPool)//controllo se posso pagare
                    canPay = true;
            }
            return canPay; 

        } //controlla se posso creare la polla di un tipo (costo/quantità)
        public bool createPool(Enums.Mana param)
        {
            bool canPool = false;
            canPool = canCreatePool(param);
            if (canPool)
                valueList[param] -= (poolList[param] + 1);
            return canPool;
        }    //crea la polla di mana del tipo specificato, altrimenti torna false
        public bool addManaPool() 
        {
            Boolean canAdd = false;
            if (TOTALMANA < MAXMANA)
            {
                var items = from pair in valueList
                            orderby pair.Value ascending
                            select pair; 
                            valueList = (Dictionary<Enums.Mana,int>)items; // sorta la riserva di mana in base al valore
                foreach (KeyValuePair<Enums.Mana, int> manaTemp in valueList)// aggiunge il mana delle polle alla riserva di mana
                    if (TOTALMANA + poolList[manaTemp.Key] <= MAXMANA)
                    {
                        valueList[manaTemp.Key] += poolList[manaTemp.Key];
                        TOTALMANA += poolList[manaTemp.Key];
                        canAdd = true;
                    }
                    else
                        canAdd = false;                  
                
            }                               
            return canAdd;} //aggiunge il mana delle polle alla riserva di mana (torna false se non si può sommare anche solo un mana) quindi se stiamo bruciando mana
        
        #endregion
    }
}
