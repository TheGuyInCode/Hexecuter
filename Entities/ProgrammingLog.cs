using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Entities
{
    public class ProgrammingLog : BaseEntity    
    {
        public Guid DeviceId { get; set; }
        public Hexecuter.Entities.Device Device { get; set; }
        public string? UserName { get; set; }
        public string? DeviceName { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public string? LogOutput { get; set; }
        
    }
}
