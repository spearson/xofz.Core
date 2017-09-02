// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="SerialFpxFinder.cs" company="Care Controls">
//   Copyright (c) Care Controls Inc. All rights reserved.
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

namespace xofz.Framework.Plc
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Ports;
    using System.Linq;

    public class SerialFpxFinder
    {
        public virtual IList<SerialFpx> Find()
        {
            var portNames = SerialPort.GetPortNames().OrderBy(s => s);
            return portNames
                .Select(findAt)
                .Where(fpx => fpx != null)
                .ToList();
        }

        public virtual IList<SerialFpx> FindExcludingPorts(
            IEnumerable<string> portNames)
        {
            return SerialPort.GetPortNames()
                .Where(portName => !portNames.Contains(portName))
                .Select(findAt)
                .Where(fpx => fpx != null)
                .ToList();
        }

        private static SerialFpx findAt(string portName)
        {
            var possibleFpx = new SerialFpx(portName, 57600)
            {
                ReadTimeout = 150,
                WriteTimeout = 150
            };
            string response;
            try
            {
                response = possibleFpx.Do(statusCommand);
            }
            catch (UnauthorizedAccessException)
            {
                return null;
            }
            catch (IOException)
            {
                return null;
            }
            catch (TimeoutException)
            {
                // test at higher baud rate
                possibleFpx = new SerialFpx(portName, 115200)
                {
                    ReadTimeout = 150,
                    WriteTimeout = 150
                };
                try
                {
                    response = possibleFpx.Do(statusCommand);
                }
                catch (UnauthorizedAccessException)
                {
                    return null;
                }
                catch (IOException)
                {
                    return null;
                }
                catch (TimeoutException)
                {
                    return null;
                }
            }

            if (!response.StartsWith("%01$RT"))
            {
                return null;
            }

            possibleFpx.ReadTimeout = 2500;
            possibleFpx.WriteTimeout = 2500;
            return possibleFpx;
        }

        private const string statusCommand = "%01#RT**";
    }
}
