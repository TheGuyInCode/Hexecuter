using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexecuter.Enums
{
    public enum DeviceCategory
    {  
        SdCard ,
        ZifPic,         // ZIF soketli DIP PIC
        JigPic,         // Jig ile bağlı SMD PIC
        JigStm32
    }
}
