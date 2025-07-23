using Hexecuter.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Entities
{
    public class Device : BaseEntity
    {
        public string DeviceName { get; set; }
        public string UsbIdentifier { get; set; }
        public string? RootPath { get; set; }
        public string? SerialPortName { get; set; }
        public DeviceCategory? Category { get; set; }        
        public List<Firmware> Firmwares { get; set; } = new List<Firmware>();
        public List<ProgrammingLog> ProgrammingLogs { get; set; } = new List<ProgrammingLog>();
    }
}
