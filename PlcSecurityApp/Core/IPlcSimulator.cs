using PlcSecurityApp.Models;

namespace PlcSecurityApp.Core
{
    public interface IPlcSimulator
    {
        void Connect();
        SystemState GetSystemState();
    }
}