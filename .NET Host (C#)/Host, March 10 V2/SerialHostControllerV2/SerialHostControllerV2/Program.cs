using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.IO;

namespace SerialHostController
{
    class Program
    {
        static SerialPort arduinoCOM = new SerialPort();
        public static char[] incoming = new char[255];
        public static uint selected = 255;
        public static float curBal;
        public static string Location;
        public static string port = "COM3";
        public static float A0 = 0.0F;
        public static float A1 = 0.0F;
        public static float A2 = 0.0F;
        public static float A3 = 0.0F;
        public static float A4 = 0.0F;
        public static float A5 = 0.0F;
        public static float A6 = 0.0F;
        public static float A7 = 0.0F;

        static void Main(string[] args)
        {
            Console.WriteLine("Started...");
            Thread IO = new Thread(IOThread);
            Thread ConsoleToArduino = new Thread(ConsoleToArduinoThread);
            Console.Write("Location of Configuration File (No specification will find and/or create one on the root of the drive): ");
            while (!Directory.Exists(Location))
            {
                Location = Console.ReadLine();
                if (!Directory.Exists(Location))
                {
                    //File.CreateText("VMConfig.txt"); //C:\\VM\\
                    Location = "";        //C:\\VM
                    //ParseConfig("C:\\VM");
                    //ParseConfig(Location, false);
                    //break;
                }
                ParseConfig(Location, true);
            }
            if (!(arduinoCOM.IsOpen))
            {
                //Console.WriteLine("Available Ports:");
                //foreach (string s in SerialPort.GetPortNames())
                //{
                //   Console.WriteLine("   {0}", s);
                //}
                //Console.Write("Which COM Port? ");
                //string port = Console.ReadLine().ToUpper();
                if (!port.StartsWith("COM"))
                {
                    Console.WriteLine("COM Port: " + port + ", is not valid, or does not exist");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
                else
                {
                    arduinoCOM.PortName = port;
                }
                try
                {
                    arduinoCOM.Open();
                }
                catch (IOException)
                {
                    Console.WriteLine("COM Port: " + port + ", does not exist");
                    Console.ReadKey();
                    Environment.Exit(0);
                }
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
                    if (balance >= A0)
                    {
                        Console.WriteLine("Host: Can Afford, Processing");
                        SendString('A', '0');
                        curBal = curBal - A0;
                        GiveChange();
                    } else
                    {
                        Console.WriteLine("Host: Can Not Afford");
                        SendString('F', '0');
                    }
                    break;
                case 1:
                    Console.WriteLine("Host: 1 Selected");
                    if (balance >= A1)
                    {
                        Console.WriteLine("Host: Can Afford, Processing");
                        SendString('A', '1');
                        curBal = curBal - A1;
                        GiveChange();
                    }
                    else
                    {
                        Console.WriteLine("Host: Can Not Afford");
                        SendString('F', '0');
                    }
                    break;
                case 2:
                    Console.WriteLine("Host: 2 Selected");
                    if (balance >= A2)
                    {
                        Console.WriteLine("Host: Can Afford, Processing");
                        SendString('A', '2');
                        curBal = curBal - A2;
                        GiveChange();
                    }
                    else
                    {
                        Console.WriteLine("Host: Can Not Afford");
                        SendString('F', '0');
                    }
                    break;
                case 3:
                    Console.WriteLine("Host: 3 Selected");
                    if (balance >= A3)
                    {
                        Console.WriteLine("Host: Can Afford, Processing");
                        SendString('A', '3');
                        curBal = curBal - A3;
                        GiveChange();
                    }
                    else
                    {
                        Console.WriteLine("Host: Can Not Afford");
                        SendString('F', '0');
                    }
                    break;
                case 4:
                    Console.WriteLine("Host: 4 Selected");
                    if (balance >= A4)
                    {
                        Console.WriteLine("Host: Can Afford, Processing");
                        SendString('A', '4');
                        curBal = curBal - A4;
                        GiveChange();
                    }
                    else
                    {
                        Console.WriteLine("Host: Can Not Afford");
                        SendString('F', '0');
                    }
                    break;
                case 5:
                    Console.WriteLine("Host: 5 Selected");
                    if (balance >= A5)
                    {
                        Console.WriteLine("Host: Can Afford, Processing");
                        SendString('A', '5');
                        curBal = curBal - A5;
                        GiveChange();
                    }
                    else
                    {
                        Console.WriteLine("Host: Can Not Afford");
                        SendString('F', '0');
                    }
                    break;
                case 6:
                    Console.WriteLine("Host: 6 Selected");
                    if (balance >= A6)
                    {
                        Console.WriteLine("Host: Can Afford, Processing");
                        SendString('A', '6');
                        curBal = curBal - A6;
                        GiveChange();
                    }
                    else
                    {
                        Console.WriteLine("Host: Can Not Afford");
                        SendString('F', '0');
                    }
                    break;
                case 7:
                    Console.WriteLine("Host: 7 Selected");
                    if (balance >= A7)
                    {
                        Console.WriteLine("Host: Can Afford, Processing");
                        SendString('A', '7');
                        curBal = curBal - A7;
                        GiveChange();
                    }
                    else
                    {
                        Console.WriteLine("Host: Can Not Afford");
                        SendString('F', '0');
                    }
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

        static public void ParseConfig(string Path, bool Exist) //using (TextReader sr = new StreamReader("VMConfig.txt", Encoding.Default))
        {
            int i = 0;
            string[] contents = new string[255];
            if (Exist)
            {
                using (TextReader sr = new StreamReader("VMConfig.txt", Encoding.Default))
                {
                    while (sr.Peek() >= 0)
                    {
                        //Console.WriteLine(sr.ReadLine());
                        i++;
                        contents[i] = sr.ReadLine();
                        //Console.WriteLine(contents[i]);
                        //string[] bad = contents[i].Split('=');
                        //if (bad.Length > 1)
                        //{
                        //    Console.WriteLine(bad[0]);
                        //}
                    }
                    for (int b = 1; b <= i; b++)
                    {
                        string[] line = contents[b].Split('=', '#');
                        if (contents[b].StartsWith("#"))
                        {
                            Console.WriteLine(contents[b]);
                        }
                            if (line.Length > 1)
                            {
                                switch (line[0].ToUpper())
                                {
                                    case "PORT":
                                        port = line[1].ToUpper();
                                        Console.Write("New Port: ");
                                        Console.WriteLine(line[1].ToUpper());
                                        break;
                                case "A0":
                                    A0 = Convert.ToSingle(line[1]);
                                    Console.WriteLine("New price defined for " + line[0] + ", " + line[1]);
                                    break;
                                case "A1":
                                    A1 = Convert.ToSingle(line[1]);
                                    Console.WriteLine("New price defined for " + line[0] + ", " + line[1]);
                                    break;
                                case "A2":
                                    A2 = Convert.ToSingle(line[1]);
                                    Console.WriteLine("New price defined for " + line[0] + ", " + line[1]);
                                    break;
                                case "A3":
                                    A3 = Convert.ToSingle(line[1]);
                                    Console.WriteLine("New price defined for " + line[0] + ", " + line[1]);
                                    break;
                                case "A4":
                                    A4 = Convert.ToSingle(line[1]);
                                    Console.WriteLine("New price defined for " + line[0] + ", " + line[1]);
                                    break;
                                case "A5":
                                    A5 = Convert.ToSingle(line[1]);
                                    Console.WriteLine("New price defined for " + line[0] + ", " + line[1]);
                                    break;
                                case "A6":
                                    A6 = Convert.ToSingle(line[1]);
                                    Console.WriteLine("New price defined for " + line[0] + ", " + line[1]);
                                    break;
                                case "A7":
                                    A7 = Convert.ToSingle(line[1]);
                                    Console.WriteLine("New price defined for " + line[0] + ", " + line[1]);
                                    break;
                            }
                            }
                    }
                }
            }
            else
            {

            }
            Location = @"C:\Windows\Temp\";
        }
    }
}
