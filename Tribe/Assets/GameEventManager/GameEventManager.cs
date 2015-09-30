using System;
using System.Collections.Generic;
using System.Text;

namespace GameEventManagement
{
    public static class GameEventManager
    {
        #region handlers
        public delegate void GenericEventHandler();
        public delegate void ResultEventHandler(int result);
        public delegate void GetOpponentNameEventHandler(string name);
        public delegate void SendOpponentNameEventHandler(string name);
        public delegate void SendStringEventHandler(string name);
        public delegate void SendBoolEventHandler(bool name);
        public delegate void SendStringBoolEventHandler(string name, bool value);
        public delegate void SendDoubleStringEventHandler(string value,string value1);
        public delegate void SendStringListEventHandler(List<string> name);
        public delegate void LoadXmlForBibliothecaEventHandler(LinkedList<string> xmlList);
        #endregion

        #region events for interface
        public static event GenericEventHandler waitingForOpponent;
        public static event GenericEventHandler opponentDisconnected;
        public static event GenericEventHandler gameStarted;
        public static event ResultEventHandler diceResult;
        public static event ResultEventHandler opponentsDiceResult;
        public static event GenericEventHandler requestXmlForBibliotheca;
        public static event LoadXmlForBibliothecaEventHandler loadXmlForBibliotheca;
        public static event GetOpponentNameEventHandler getOpponentName;
        public static event SendOpponentNameEventHandler sendOpponentName;
        public static event SendStringListEventHandler menuFiltered;
        public static event GenericEventHandler endRoud;
        public static event SendStringEventHandler playCard;
        public static event SendStringEventHandler canPlayCard;
        public static event ResultEventHandler idTarget; //0 e' il player, 1 e' l'opponent
        public static event SendDoubleStringEventHandler manaChosen;
        public static event GenericEventHandler loaded; //notifica che entrambi hanno caricato l'interfaccia
        #endregion

        #region events for communicator
        public static event GenericEventHandler throwDice;
        public static event SendStringEventHandler choseMana; //richiede la selezione del mana da parte dell'utente
        public static event SendStringEventHandler sendMana;    //chiamato da game quando c'e' da visualizzare il mana cambiato
        public static event SendBoolEventHandler setRound;      //per aggiornare lo stato del round
        public static event SendStringListEventHandler menuProcessed;
        public static event SendStringBoolEventHandler canPlayCardChecked;
        public static event GenericEventHandler getAnyTarget;
        public static event GenericEventHandler getPlayersTarget;
        public static event GenericEventHandler getSpiritsTarget;
        public static event GenericEventHandler getElementalTarget;
        public static event GenericEventHandler getAllyElementalTarget;
        public static event GenericEventHandler getEnemyElementalTarget;
        public static event GenericEventHandler unityReady;
        #endregion

        #region methods called from communicator
        public static void OpponentDisconnected()
        {
            opponentDisconnected();
        }
        public static void DiceResult(int result)
        {
            diceResult(result);
        }

        public static void ChoseMana(string param)
        {
            choseMana(param);
        }

        public static void OpponentsDiceResult(int result)
        {
            opponentsDiceResult(result);
        }

        public static void GameStarted()
        {
            gameStarted();
        }

        public static void WaitingForOpponent()
        {
            waitingForOpponent();
        }

        public static void RequestXmlForBibliotheca()
        {
            requestXmlForBibliotheca();
        }



        public static void SendMana(string mana)
        {
            sendMana(mana);
        }
        public static void SetRound(bool round)
        {
            setRound(round);
        }

        public static void MenuProcessed(List<string> card)
        {
            menuProcessed(card);
        }

        public static void CanPlayCardChecked(string name, bool value)
        {
            canPlayCardChecked(name, value);
        }
        public static void GetAnyTarget()
        {
            getAnyTarget();
        }
        public static void GetPlayersTarget()
        {
            getPlayersTarget();
        }
        public static void GetSpiritsTarget()
        {
            getSpiritsTarget();
        }
        public static void GetElementalTarget()
        {
            getElementalTarget();
        }
        public static void GetAllyElementalTarget()
        {
            getAllyElementalTarget();
        }
        public static void GetEnemyElementalTarget()
        {
            getEnemyElementalTarget();
        }
        #endregion

        #region methods called from interface

        public static void Loaded()
        {
                loaded();
        }

        public static void ManaChosen(string mana,string reason)
        {
            manaChosen(mana,reason);
        }

        public static void  UnityReady()
        {
            unityReady();
        }

        public static void ThrowDice()
        {
            throwDice();
        }
        public static void LoadXmlForBibliotheca(LinkedList<string> xmlList)
        {
            loadXmlForBibliotheca(xmlList);
        }
        public static void GetOpponentName(string name)
        {
            getOpponentName(name);
        }
        public static void SendOpponentName(string name)
        {
            sendOpponentName(name);
        }

        public static void MenuFiltered(List<string> param) //lista separata da spazi
        {
            menuFiltered(param);
        }

        public static void EndRound()
        {
            endRoud();
        }
        public static void PlayCard(string card)
        {
            playCard(card);
        }
        public static void CanPlayCard(string card)
        {
            canPlayCard(card);
        }
        public static void IdTarget(int id)
        {
            idTarget(id);
        }
        #endregion
    }
}
