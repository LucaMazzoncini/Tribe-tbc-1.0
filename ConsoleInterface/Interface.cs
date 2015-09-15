using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEventManagement;
using Communication;
using System.IO;
using System.Reflection;

namespace ConsoleInterface
{
    class Interface
    {
        private static Communicator communicator;
        private static LinkedList<string> xmlList;

        static void Main(string[] args)
        {
            Console.WriteLine("Name?");

            GameEventManager.waitingForOpponent += gameEventManager_waitingForOpponent;
            GameEventManager.gameStarted += gameEventManager_gameStarted;
            GameEventManager.diceResult += gameEventManager_diceResult;
            GameEventManager.opponentsDiceResult += GameEventManager_opponentsDiceResult;
            GameEventManager.requestXmlForBibliotheca += GameEventManager_requestXmlForBibliotheca;

            communicator = new Communicator(Console.ReadLine(),Server.Default.Ip,Server.Default.Port);
            Console.WriteLine("Waiting..");

            Console.ReadKey();
        }

        private static void GameEventManager_opponentsDiceResult(int result)
        {
            Console.WriteLine("Opponent's dice result is " + result);
        }

        static void GameEventManager_requestXmlForBibliotheca()
        {
            xmlList = new LinkedList<string>();
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (string file in Directory.EnumerateFiles(currentPath + "/cards_xml/", "*.xml"))
            {
                xmlList.AddLast(File.ReadAllText(file));
            }
            GameEventManager.LoadXmlForBibliotheca(xmlList);
        }

        static void gameEventManager_diceResult(int result)
        {
            Console.WriteLine("Your dice result is " + result);
        }

        private static void gameEventManager_gameStarted()
        {
            Console.WriteLine("GameStarted!");

            #region commandSwitch
            switch (Console.ReadLine())
            {
                case "throw":
                    GameEventManager.ThrowDice();
                    break;
                default:
                    break;
            }
            #endregion
            
        }

        static void gameEventManager_waitingForOpponent()
        {
            Console.WriteLine("Waiting for opponent..");
        }

    }
}
