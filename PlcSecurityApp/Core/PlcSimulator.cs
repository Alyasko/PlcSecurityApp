using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using PlcSecurityApp.Models;
using S7PROSIMLib;

namespace PlcSecurityApp.Core
{
    class PlcSimulator : IPlcSimulator
    {
        private S7ProSim _proSim;

        public PlcSimulator()
        {
            _proSim = new S7ProSim();
        }

        public void Connect()
        {
            var pData = new Object();

            _proSim.Connect();
            _proSim.BeginScanNotify();
            _proSim.SetScanMode(ScanModeConstants.ContinuousScan);

            _proSim.ReadOutputPoint(4, 0, PointDataTypeConstants.S7_Bit, ref pData);

            var state = (bool) pData;

            MessageBox.Show($"Output pin state: {state}");

            _proSim.WriteInputPoint(0, 0, true);
        }

        public SystemState GetSystemState()
        {
            throw new NotImplementedException();
        }
    }
}
