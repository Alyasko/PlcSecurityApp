using PlcSecurityApp.Models;
using PlcSecurityApp.Views.UC;

namespace PlcSecurityApp.Core
{
    public interface IPlcSimulator
    {
        void Connect();

        void ModifySensor(SensorType type, SensorState newState);
        void ResetSensors();
        SensorState ReadAlarmState();
    }
}