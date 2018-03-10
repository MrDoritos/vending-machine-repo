using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;


namespace SerialHostController
{
    class Program
    {
        static SerialPort arduinoCOM = new SerialPort();
        public static char[] incoming = new char[255];
        public static uint selected = 255;
        public static float curBal;

        static void Main(string[] args)
        {
            Console.WriteLine("Started...");
            Thread IO = new Thread(IOThread);
            Thread ConsoleToArduino = new Thread(ConsoleToArduinoThread);
            if (!(arduinoCOM.IsOpen))
            {
                Console.WriteLine("Available Ports:");
                foreach (string s in SerialPort.GetPortNames())
                {
                    Console.WriteLine("   {0}", s);
                }
                Console.Write("Which COM Port? ");
                string port = Console.ReadLine().ToUpper();
                arduinoCOM.PortName = port;
                arduinoCOM.Open();
                //processIO.Start();
                IO.Start();
                ConsoleToArduino.Start();
            }
            else
            {
                Console.WriteLine("An error occurred");
                Console.ReadKey();
                Environment.Exit(0);
            }
            while (true)
            {
                //Query(Console.ReadLine().ToUpper());
            }
        }

        public static void IOThread()
        {
            while (true)
            {
                Thread.Sleep(10);
                if ((arduinoCOM.ReadBufferSize) > 1)
                {
                    Console.Write("Arduino --> Host: ");
                    for (int i = 1; i <= 2; i++)
                    {
                        incoming[i] = Convert.ToChar(arduinoCOM.ReadByte());
                        Console.Write(incoming[i]);
                    }
                    Console.WriteLine();
                    ProcessIOThread(incoming[1], incoming[2]);
                }
            }
        }
        
        public static void ProcessIOThread(char Base, char Arg)
        {
            switch (Base)
            {
                case 'A':
                    selected = Convert.ToUInt16(incoming[2] - '0');
                    Console.WriteLine("Host: " + selected + ", Was Selected");
                    break;
                case 'B':
                    Console.Write("Host: Money Recieved, ");
                    switch (incoming[2] - '0')
                    {
                        case 0:
                            curBal = curBal + 0.01F;
                            Console.Write(curBal);
                            break;
                        case 1:
                            curBal = curBal + 0.05F;
                            Console.Write(curBal);
                            break;
                        case 2:
                            curBal = curBal + 0.10F;
                            Console.Write(curBal);
                            break;
                        case 3:
                            curBal = curBal + 0.25F;
                            Console.Write(curBal);
                            break;
                    }
                    Console.WriteLine(", Is Total Balance");
                    Console.Write("Host: ");
                    Console.WriteLine(incoming[2] - '0');
                    break;
                case 'C':
                    if (selected != 255)
                    {
                        Console.WriteLine("Host: Enter Pressed");
                        PreformAction(selected, curBal);
                    } else
                    {
                        Console.WriteLine("Host: Enter Was Pressed, But Nothing Was Selected");
                        SendString('E','-');
                    }
                    break;
                case 'D':
                    GiveChange();
                    break;
                default:
                    return;
            }
        }

        public static void PreformAction(uint select, float balance)
        {
            switch (select)
            {
                case 0:
                    Console.WriteLine("Host: 0 Selected");
                    if (balance >= 0.25F)
                    {
                        Console.WriteLine("Host: Can Afford, Processing");
                        SendString('A', '0');
                        curBal = curBal - 0.25F;
                        GiveChange();
                    } else
                    {
                        Console.WriteLine("Host: Can Not Afford");
                        SendString('F', '0');
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                default:
                    Console.Write("Host: Out of Range: ");
                    Console.WriteLine(select);
                    break;
            }
        }

        public static void SendString(char Base, char Arg)
        {
            Console.Write("Arduino <-- Host: ");
            Console.Write(Base);
            Console.WriteLine(Arg);
            arduinoCOM.Write(Base.ToString());
            arduinoCOM.Write(Arg.ToString());
        }

        public static void ConsoleToArduinoThread()
        {
            while (true)
            {
                Thread.Sleep(10);
                string query = Console.ReadLine();
                if ((query.Length) == 2)
                {
                    arduinoCOM.Write(query);
                }
                else
                {
                    if (query.Length > 2)
                    {
                        query.Substring(0, 1);
                        Console.WriteLine("Host: Too Many Args");
                    }
                    else
                    {
                        Console.WriteLine("Host: Not Enough Args");
                    }
                }
            }
        }

        public static void GiveChange()
        {
            if (curBal > 0.0F)
            {
                Console.Write("Host: ");
                while (curBal > 0.0F)
                {
                    if (curBal >= 0.25F)
                    {
                        Console.Write("Quarter, ");
                        SendString('I', '3');
                        curBal = curBal - 0.25F;
                    }
                    else
                    {
                        if (curBal >= 0.10F)
                        {
                            Console.Write("Dime, ");
                            SendString('I', '2');
                            curBal = curBal - 0.10F;
                        }
                        else
                        {
                            if (curBal >= 0.05F)
                            {
                                Console.Write("Nickel, ");
                                SendString('I', '1');
                                curBal = curBal - 0.05F;
                            }
                            else
                            {
                                if (curBal >= 0.01F)
                                {
                                    Console.Write("Penny, ");
                                    SendString('I', '0');
                                    curBal = curBal - 0.01F;
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("...");
            }
            else
            {
                return;
            }
        }
    }
}
