using System;
using System.Collections.Generic;
using System.Text;

namespace GameLogic
{
    public class Game
    {

        #region var
        public delegate void GenericEventHandler();
        public delegate void ResultEventHandler(int result);

        public event ResultEventHandler sendDiceResult;
        public event GenericEventHandler requestXmlForBibliotheca;

        private Player shaman;
        private Player opponent;
        private int diceResult;
        private bool myRound = false;
        Communication.Communicator comm;
        private static bool manaChosen; //questo flag ci dice se il mana e' gia' stato scelto

        Bibliotheca bibliotheca;
        #endregion
        public Game(string name)
        {
            shaman = new Player(name);
            comm = Communication.Communicator.getInstance();
        }
        #region setGet
        public bool isMyRound()
        {
            return myRound;
        }

        public string GetOppenentName()
        {
            return opponent.Name;
        }

        public string GetShamanName()
        {
            return shaman.Name;
        }

        public void SetOpponentName(string name) //da tenere
        {
            opponent.Name = name;
        }
        public void SetOpponent(Player param)
        {
            opponent = param;
        }
        public bool getRoundFlag()
        {
            return myRound;
        }
        #endregion
        #region Who start
        public void ThrowDice()
        {
            ThrowDice(Player.ThrowDice(9999999));
        }

        public void ThrowDice(int diceValue)
        {
            diceResult = diceValue;
            if(sendDiceResult != null)
                sendDiceResult(diceResult);
        }



        public void OnOpponentDiceResult(int opponentDiceResult) //in questa funzione viene stabilito di chi e' il turno
        {
                myRound = false;
            if (diceResult == opponentDiceResult)
            {
                ThrowDice();
                comm.game_diceResult(diceResult); //si invia nuovamente il risultato del dado
            }
            else
            {
                if (diceResult > opponentDiceResult)
                {
                    myRound = true;
                }
                FirstRoundStart();
            }
                
                
        }
        #endregion

        #region Metodi chiamati da Comunicator

        public LinkedList<Invocation> MenuFiltered(List<Enums.Filter> param) //questa funzione ritorna una linkedList delle carte filtrate
        {
            return bibliotheca.getCards(param,shaman.mana); 
        }
        public void EndTourn() //viene chiamato quando shaman passa il turno
        {
            myRound = false;
            comm.setRound(myRound);
        }
        public void StartTourn()
        {
            myRound = true;
            comm.setRound(myRound);
            comm.getManaAtStart(); //Chiedo di selezionare il mana che prendo in manaAtStart
        }

        public void ManaAtStart( Enums.Mana param )
        {
            shaman.mana.incMana(param);    //se ha raggiunto il mana max non viene aggiunto il mana
            Enums.Mana manaTemp = shaman.mana.addRandomMana(); //aggiungo il mana random allo shamano
            if (manaTemp != Enums.Mana.None)
            {
                comm.sendMana(shaman.mana);  //invio l'update del mana
            }
            //Aggiungo il mana delle polle
            shaman.mana.addManaPool();
            comm.sendMana(shaman.mana);  //invio l'update del mana
        }

        public void PlayCard(string name)
        {
            shaman.playCard(name);
        }

        public void TargetEvent(Target param) //questa funzione riceve il target richiesto precedentemente
        {
            //shaman.targetList.Clear();
            //shaman.targetList.AddRange(param);
            shaman.target = param;
            shaman.TargetUpdated(); //questo evento viene chiamato per avvertire il player che la carta e' arrivata
        }



        #endregion

        #region Metodi chiamati da altri oggetti a Comunicator
        public void GetAnyTarget()
        {
            //comm.getAnyTarget();
        }

        public void GetPlayerTarget()
        {
            //comm.getPlayerTarget();
        }
        public void GetElementalTarget()
        {
            //comm.GetElementalTarget();
        }

        public void GetSpiritTarget()
        {
            //comm.GetSpiritTarget();
        }

        #endregion
        public void LoadBibliotheca(LinkedList<string> xmlInvocations)
        {
            bibliotheca = new Bibliotheca(xmlInvocations);
        }
        public void FirstRoundStart()
        {
            //invio i miei dati all'opponent
            comm.sendPlayerInfo(shaman);
            comm.setRound(myRound);   //setto il round per la grafica
            if (myRound)
            {

                Enums.Mana manaTemp = shaman.mana.addRandomMana(); //aggiungo il mana random allo shamano se ritorno 
                if (manaTemp != Enums.Mana.None)
                {
                    comm.sendMana(shaman.mana);  //invio il mana random generato al comunicator
                }
                //adesso dovrei aggiungere il mana delle polle ma non e' senso      
            }

        }
        public void GameStarted()
        {

            ThrowDice(); //lancio il dado per vedere chi inizia
            comm.game_diceResult(diceResult);
            requestXmlForBibliotheca();

        }
    }
}
