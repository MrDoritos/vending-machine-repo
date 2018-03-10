using System;
using System.IO.Ports;

namespace SerialHostController
{
    class Program
    {
        static SerialPort arduinoCOM = new SerialPort();
        char[] incoming;
        public static uint selected = 255;
        public static float curBal;

        static void Main(string[] args)
        {
            Console.WriteLine("Started...");
            Thread IO = new Thread(IOThread);
            Thread processIO = new Thread(processIOThread);
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
                //processIO.Start();
                IO.Start();
            }
            else
            {
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
            while (true)
            {
                IOThread.Sleep(10);
                if ((arduinoCOM.available) > 1)
                {
                    for (int i = 1; i <= 2; i++) 
                    {
                        incoming[i] = arduinoCOM.readByte();
                    }
                    processIOThread(incoming[1], incoming[2]);
                }
            }
        }

        public static void processIOThread(char Base, char Arg)
        {
            processIOThread.Sleep(10);
            switch (Base)
            {
                case 'A':
                    selected = (incoming[2] - '0');
                    break;
                case 'B':
                    switch (incoming[2] - '0')
                    {
                        case '0':
                            curBal = curBal + 0.01F;
                            break;
                        case '1':
                            curBal = curBal + 0.05F;
                            break;
                        case '2':
                            curBal = curBal + 0.10F;
                            break;
                        case '3':
                            curBal = curBal + 0.25F;
                            break;
                    }
                    break;
                case 'C':
                    if (selected != 255)
                    {

                    }
                    break;
            }
        }
        
        public static void preformAction(int select, float balance)
        {
            switch (select)
            {
                case 0:
                    if (balance >= 0.25F)
                    {
                        
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
            }
        }

        public static void sendString(char Base, char Arg)
        {
            arduinoCOM.sendRawWhyarentyoucheckingforerrors();
        }
    }
}
