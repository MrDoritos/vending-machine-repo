using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.IO;

/// <summary>
/// To Do:
/// 1) The system for giving change
/// 2) Map all cells
/// 3) Create a config file for the cost of each cell
/// </summary>

namespace MultiThreadedSerialIO
{
    public class Program
    {
        //static string[] rawData;
        //static string Data;
        //static string incoming = null;
        static SerialPort ArduinoCom = new SerialPort();
        public static string incoming = null;
        public static int selectedSlot = 254;
        public static float curBalance = 0F;
        public static float activeCost = 0F;

        public static void Main(string[] args)
        {
            Console.WriteLine("Started...");
            Thread IO = new Thread(IOThread);
            if (!ArduinoCom.IsOpen)
            {
                Console.WriteLine("Available Ports:");
                foreach (string s in SerialPort.GetPortNames())
                {
                    Console.WriteLine("   {0}", s);
                }
                Console.Write("Which COM Port? ");
                string port = Console.ReadLine().ToUpper();
                ArduinoCom.PortName = port;
                ArduinoCom.Open();
                IO.Start();
            } else {
                Console.WriteLine("An error occurred");
                Console.ReadKey();
                Environment.Exit(0);
            }
                while (true)
            {
                Query(Console.ReadLine().ToUpper());
            }
        }
        public static void IOThread()
        {
            Console.WriteLine("Thread Initiated");
                while (true)
            {
                Thread.Sleep(100);
                incoming = ArduinoCom.ReadLine();
                Console.WriteLine(incoming);
                switch (incoming)
                {
                    case "REQCHG":
                        Selector(79);
                        break;
                    case "REQMOVA":
                        Selector(0);
                        break;
                    case "REQMOVB":
                        Selector(1);
                        break;
                    case "ENTER":
                        Selector(255);
                        break;
                    case "BAL1":
                        Selector(80);
                        break;
                    case "BAL5":
                        Selector(81);
                        break;
                    case "BAL10":
                        Selector(82);
                        break;
                    case "BAL25":
                        Selector(83);
                        break;
                    default:
                        break;
                }
            }
        }

        static public void Query(string command)
        {
            if (!(command == null))
            {
                string[] command2 = command.Split(' ');
                string actcommand = command2[0];
                string args = command.Substring(command.IndexOf(' ') + 1);
                switch (actcommand.ToUpper())
                {
                    case "EXIT":
                        Environment.Exit(0);
                        break;
                    case "MOVE":
                        ArduinoCom.Write(args.ToUpper());
                        break;
                    case "STAT":
                        Console.WriteLine(curBalance);
                        Console.WriteLine(selectedSlot);
                        Console.WriteLine(activeCost);
                        break;
                    default:
                        break;
                }
                return;
            } else { return; }
        }

        static public void Selector(int option)
        {
            switch (option)
            {
                case 0:
                    selectedSlot = 0;
                    activeCost = 0.25F;
                    SendString("SEL" + option);
                    break;
                case 1:
                    selectedSlot = 1;
                    activeCost = 0.25F;
                    SendString("SEL" + option);
                    break;
                case 79:
                    calculateChange(curBalance);
                    break;
                case 80:
                    curBalance = curBalance + 0.01F;
                    SendString("CHBAL" + curBalance.ToString());
                    break;
                case 81:
                    curBalance = curBalance + 0.05F;
                    SendString("CHBAL" + curBalance.ToString());
                    break;
                case 82:
                    curBalance = curBalance + 0.10F;
                    SendString("CHBAL" + curBalance.ToString());
                    break;
                case 83:
                    curBalance = curBalance + 0.25F;
                    SendString("CHBAL" + curBalance.ToString());
                    break;
                case 255:
                    PreformAction(selectedSlot, activeCost);
                    break;
                default:
                    Console.WriteLine("Error in Selector Method");
                    break;
            }
            return;
        }

        static public void SendString(string send)
        {
            ArduinoCom.Write(send.ToUpper());
            return;
        }

        static public void PreformAction(int caseNum, float cost)
        {
            if (cost <= curBalance)
            {
                if (caseNum == 254) { SendString("NOSEL"); } else {
                    curBalance = curBalance - cost;
                    SendString("MOV" + caseNum.ToString() + '\n');
                    SendString("CHBAL" + curBalance.ToString());
                }
            } else { SendString("NOBAL"); }
            return;
        }

        static public void calculateChange(float change)
        {
            return;
        }
    }
}
