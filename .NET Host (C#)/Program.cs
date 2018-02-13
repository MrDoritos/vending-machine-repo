using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO.Ports;
using System.IO;

namespace MultiThreadedSerialIO
{
    public class Program
    {
        static string[] rawData;
        static string Data;
        static string incoming = null;
        static SerialPort ArduinoCom = new SerialPort(portName: "COM3");
        
        public static void Main(string[] args)
        {
            Console.WriteLine("Started...");
            Thread IO = new Thread(IOThread);
            if (!ArduinoCom.IsOpen)
            {
                ArduinoCom.Open();
                IO.Start();
            } else {
                Console.WriteLine("An error occurred");
                Console.ReadKey();
                Environment.Exit(0);
            }
                while (true)
            {
                Thread.Sleep(100);
                if (!(incoming is null))
                {
                    Console.WriteLine(incoming);
                }
                Query(
                    Console.ReadLine().ToUpper(),
                    Console.ReadLine()
                    );
                    }
        }
        public static void IOThread()
        {
            Console.WriteLine("Thread Initiated");
                while (true)
            {
                Thread.Sleep(100);
                incoming = ArduinoCom.ReadLine();
            }
        }
        public static void Query(string command, string args)
        {
            if (!(command is null))
            {
                if (!(args is null))
                {
                    switch (command)
                    {
                        case "MOVE":
                            if (File.Exists("C:\\VendingData.txt"))
                            {
                                Data = File.ReadAllText("C:\\VendingData.txt");
                                rawData = Data.Split(',');
                                for (int i = 0; i < 3; i++) {
                                    Console.WriteLine(rawData[i]);
                                }
                                switch (args.ToUpper())
                                {
                                    case "A":
                                        Console.WriteLine("Functioning On Cell 'A'");
                                        ArduinoCom.WriteLine(rawData[1]);
                                        break;
                                    default:
                                        Console.WriteLine("That cell has no available switch!");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("File not found!");
                            }
                            break;
                        case "EXIT":
                            Environment.Exit(0);
                            break;
                        case "IO":
                            ArduinoCom.WriteLine(args);
                            break;
                        default:
                            Console.WriteLine("Ambiguous Command");
                            break;
                    }
                    args = null;
                } else { return; }
                command = null;
            } else { return; }
        }
    }
}
