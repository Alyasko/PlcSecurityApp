using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using PlcSecurityApp.Models;
using PlcSecurityApp.Views.UC;
using S7PROSIMLib;

namespace PlcSecurityApp.Core
{
    class PlcSimulator : IPlcSimulator
    {
        private S7ProSim _proSim;

        private static readonly PinConfig OutputConfig = new PinConfig() { ByteAddress = 4, PinAddress = 0};

        private static readonly PinConfig DoorConfig = new PinConfig() { ByteAddress = 0, PinAddress = 0};
        private static readonly PinConfig GlassConfig = new PinConfig() { ByteAddress = 0, PinAddress = 1};
        private static readonly PinConfig MotionConfig = new PinConfig() { ByteAddress = 0, PinAddress = 2};

        public PlcSimulator()
        {
            _proSim = new S7ProSim();
            //_proSim.ConnectionError += ProSimOnConnectionError;
        }

        private void ProSimOnConnectionError(string controlEngine, int error)
        {
            MessageBox.Show($"Engine: {controlEngine}, Error: {error}");
        }

        public void Connect()
        {
            _proSim.Connect();
            _proSim.BeginScanNotify();
            _proSim.SetScanMode(ScanModeConstants.ContinuousScan);
        }

        private SensorState ConvertToSensorState(bool state)
        {
            return !state ? SensorState.Ok : SensorState.Alert;
        }

        private bool ConvertFromSensorState(SensorState state)
        {
            return state == SensorState.Alert;
        }

        private bool ReadDataBlock(PinConfig config)
        {
            object pData = new int();
            _proSim.ReadFlagValue(config.ByteAddress, config.PinAddress, PointDataTypeConstants.S7_Bit, ref pData);

            return (int)pData == 1;
        }

        private bool ReadOutputPin(PinConfig config)
        {
            var pData = new Object();
            _proSim.ReadOutputPoint(config.ByteAddress, config.PinAddress, PointDataTypeConstants.S7_Bit, ref pData);

            return (bool)pData;
        }

        private void WriteInputPin(PinConfig config, bool value)
        {
            _proSim.WriteInputPoint(config.ByteAddress, config.PinAddress, value);
        }

        public void ModifySensor(SensorType type, SensorState newState)
        {
            var boolState = ConvertFromSensorState(newState);
            switch (type)
            {
                case SensorType.Door:
                    WriteInputPin(DoorConfig, boolState);
                    break;
                case SensorType.Motion:
                    WriteInputPin(MotionConfig, boolState);
                    break;
                case SensorType.Glass:
                    WriteInputPin(GlassConfig, boolState);
                    break;
            }
        }

        public void ResetSensors()
        {
            WriteInputPin(DoorConfig, false);
            WriteInputPin(MotionConfig, false);
            WriteInputPin(GlassConfig, false);
        }

        public SensorState ReadAlarmState()
        {
            Thread.Sleep(50);
            return ConvertToSensorState(ReadOutputPin(OutputConfig));
        }
    }
}
