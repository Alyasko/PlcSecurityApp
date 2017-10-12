using PlcSecurityApp.Models;
using PlcSecurityApp.Views.UC;

namespace PlcSecurityApp.Core
{
    public interface IPlcSimulator
    {
        void Connect();
        void ReadSystemState(SystemState state);

        void ModifySensor(SensorType type, SensorState newState);
    }
}