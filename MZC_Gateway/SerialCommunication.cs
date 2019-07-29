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
            //var ports = SerialPort.GetPortNames();
            //foreach (var port in ports)
            //{
            //    Console.WriteLine($"Debug: Found Serial Port: {port}");
            //}

            Console.WriteLine($"Initialising Serial port {portName}");
            sp = new SerialPort(portName)
            {
                Encoding = Encoding.UTF8,
                BaudRate = 57600,
                Handshake = Handshake.XOnXOff,

                DataBits = 8,
                Parity = Parity.None,
                StopBits = StopBits.One
            };

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
            int retryCount = 20;
            var sleepDuration = 250;

            for (int i = 0; i < retryCount; i++)
            {
                Console.WriteLine($"Sending Command [{i+1}/{retryCount}]: {BitConverter.ToString(command)}");
                
            
                System.Threading.Thread.Sleep(sleepDuration);
                sp.Write(command, 0, command.Length);
                int length = sp.BytesToRead;
                byte[] buf = new byte[length];
                sp.Read(buf, 0, length);
                Console.WriteLine($"Response: {BitConverter.ToString(buf)} Response Length: {length}");
                if (length > 3)
                {
                    if (buf[2] == 0x95 && buf[4] == 0x01)
                    {
                        Console.WriteLine("ACK response received");
                        return true;
                    }
                }
                sleepDuration += 50;
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
