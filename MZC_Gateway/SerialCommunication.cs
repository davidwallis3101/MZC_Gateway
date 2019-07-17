using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Text;

namespace MZC_Gateway
{
    public static class SerialCommunication
    {
        private static SerialPort sp;

        public static void Initialize(string portName)
        {
            Console.WriteLine($"Initialising Serial port {portName}");
            sp = new SerialPort(portName);
            sp.Encoding = Encoding.UTF8;
            sp.BaudRate = 57600;
            sp.Handshake = Handshake.XOnXOff;

            sp.DataBits = 8;
            sp.Parity = Parity.None;
            sp.StopBits = StopBits.One;

            //var availablePorts = SerialPort.GetPortNames();
            //foreach (var port in availablePorts)
            //{
            //    Console.WriteLine(port);
            //}
            //sp.ReadTimeout = 1000;
            //sp.WriteTimeout = 1000;
            sp.Open();
        }

        private static byte[] GenerateCRC(byte[] command)
        {
            var total = command.Aggregate(default(byte), (current, b) => (byte)(current + b));

            var crc = (byte)(256 - total);

            // Append CRC byte to command
            var commandWithCRC = new byte[command.Length + 1];
            command.CopyTo(commandWithCRC, 0);
            commandWithCRC[commandWithCRC.Length - 1] = crc;

            return commandWithCRC;
        }

        private static bool SendCommand(byte[] command)
        {
            int retryCount = 10;
            for (int i = 0; i < retryCount; i++)
            {
                Console.WriteLine($"Sending Command: {BitConverter.ToString(command)}");
                System.Threading.Thread.Sleep(250);
                sp.Write(command, 0, command.Length);
                int length = sp.BytesToRead;
                byte[] buf = new byte[length];
                sp.Read(buf, 0, length);
                Console.WriteLine($"Length {length} CommandLen: {command.Length}");
                Console.WriteLine(BitConverter.ToString(buf));
                if (length > 3)
                {
                    if (buf[2] == 0x95 && buf[4] == 0x01)
                    {
                        Console.WriteLine("ACK resp");
                        return true;
                    }
                    //else
                    //{

                    //    Console.WriteLine(BitConverter.ToString(buf));
                    //}
                }
            }
            return false;


        }

        public static bool SendOffCommand(int Zone)
        {
            Console.WriteLine($"Send Off Command Zone {Zone}");
            return SendCommand(GenerateCRC(new byte[] { 0x55, 0x04, 0xA1, (byte)Zone }));
        }

        public static bool SendOnCommand(int Zone)
        {
            Console.WriteLine($"Send On Command Zone {Zone}");
            return SendCommand(GenerateCRC(new byte[] { 0x55, 0x04, 0xA0, (byte)Zone }));
        }

        public static bool SendSelectSourceCommand(int Zone, int Source)
        {
            Console.WriteLine($"Send Select Source Command {Zone}");
            return SendCommand(GenerateCRC(new byte[] { 0x55, 0x05, 0xA3, (byte)Zone, (byte)Source }));
        }

    }
}
