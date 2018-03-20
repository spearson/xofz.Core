namespace xofz.Framework.Plc
{
    using System;
    using System.Collections.Generic;
    using System.IO.Ports;
    using System.Linq;
    using xofz.Framework.Materialization;
    using xofz.Framework.Plc.Implementation;

    public class FpxFinder
    {
        private const string statusCommand = "%01#RT**";

        public virtual Fpx CreateSerial(
            string portName, int baudRate = 57600)
        {
            return new SerialFpx(
                portName,
                baudRate);
        }

        public virtual Fpx CreateTcp(
            string host, int port = 9094)
        {
            return new TcpFpx(host, port);
        }

        public virtual MaterializedEnumerable<Fpx> FindSerial(
            IEnumerable<string> excludedPortNames = null)
        {
            // step1
            IEnumerable<string> availablePortNames;
            if (excludedPortNames != null)
            {
                availablePortNames = SerialPort.GetPortNames()
                    .Except(excludedPortNames);
                goto step2;
            }
            availablePortNames = SerialPort.GetPortNames();

            step2:
            var fpxes = new LinkedListMaterializedEnumerable<Fpx>();
            foreach (var portName in availablePortNames)
            {
                Fpx possibility = new SerialFpx(
                    portName,
                    57600);
                try
                {
                    possibility.Request(statusCommand);
                }
                catch (TimeoutException)
                {
                    goto testHigherBaudRate;
                }
                catch
                {
                    continue;
                }
                fpxes.AddLast(possibility);
                continue;

                testHigherBaudRate:
                possibility = new SerialFpx(
                    portName,
                    115200);
                try
                {
                    possibility.Request(statusCommand);
                }
                catch
                {
                    continue;
                }
                fpxes.AddLast(possibility);
            }

            return fpxes;
        }
    }
}
