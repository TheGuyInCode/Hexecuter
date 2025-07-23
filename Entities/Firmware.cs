using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Entities
{
    public class Firmware : BaseEntity
    {
        public string FilePath { get; set; }
        public string Version { get; set; }
        public string? Checksum { get; set; }    
        public string? McuModel { get; set; }       
    }
}
