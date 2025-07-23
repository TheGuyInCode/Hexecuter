using Hexecuter.Win32;
using ICSharpCode.SharpZipLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
namespace Hexecuter.Services.AbstractServices
{
    public interface IDiskAccess
    {      
        event ProgressHandler OnProgress;
        Handle? Open(string drivePath);
        bool LockDrive(string drivePath);
        void UnlockDrive();
        int Read(byte[] buffer, int readMaxLength, out int readBytes);
        int Write(byte[] buffer, int bytesToWrite, out int wroteBytes);
        void Close();
        string GetPhysicalPathForLogicalPath(string logicalPath);
        long GetDriveSize(string drivePath);
    }
}
