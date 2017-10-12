using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PlcSecurityApp.Core;
using PlcSecurityApp.Core.Lib;
using PlcSecurityApp.Models;
using PlcSecurityApp.Views.Controls;
using PlcSecurityApp.Views.UC;

namespace PlcSecurityApp.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private IPlcSimulator _simulator;
        private SystemState _systemState;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            //_simulator = new PlcSimulator();

            ConnectCommand = new DelegateCommand(ConnectCommandHandler);
            DoorSensorCommand = new DelegateCommand(DoorSensorCommandHandler);
            MotionSensorCommand = new DelegateCommand(MotionSensorCommandHandler);
            GlassSensorCommand = new DelegateCommand(GlassSensorCommandHandler);

            DoorSensorViewModel = new SensorViewModel();
            GlassSensorViewModel = new SensorViewModel();
            MotionSensorViewModel = new SensorViewModel();

            _simulator?.Connect();

            SystemState = new SystemState();
        }

        private void GlassSensorCommandHandler(object obj)
        {
            SwitchSensorState(SystemState.GlassSensor, (x) => SystemState.GlassSensor = x);
            _simulator.ModifySensor(SensorType.Glass, SystemState.GlassSensor);
        }

        private void MotionSensorCommandHandler(object o)
        {
            SwitchSensorState(SystemState.MotionSensor, (x) => SystemState.MotionSensor = x);
            _simulator.ModifySensor(SensorType.Motion, SystemState.MotionSensor);
        }

        
        private void DoorSensorCommandHandler(object o)
        {
            SwitchSensorState(SystemState.DoorSensor, (x) => SystemState.DoorSensor = x);
            _simulator.ModifySensor(SensorType.Door, SystemState.DoorSensor);
        }

        public void ConnectCommandHandler(object obj)
        {
            SystemState.DoorSensor = SystemState.DoorSensor == SensorState.Ok ? SensorState.Alert : SensorState.Ok;
        }

        private void SwitchSensorState(SensorState currentState, Action<SensorState> action)
        {
            action(currentState == SensorState.Ok ? SensorState.Alert : SensorState.Ok);
        }

        private void UpdateSystemState(SystemState state)
        {
            
        }

        private SensorState ConvertBoolToSensorState(bool state)
        {
            return state ? SensorState.Ok : SensorState.Alert;
        }

        public String WindowTitle { get; set; } = "PLC Security App";

        public ICommand ConnectCommand { get; set; }

        public ICommand DoorSensorCommand { get; set; }
        public ICommand MotionSensorCommand { get; set; }
        public ICommand GlassSensorCommand { get; set; }

        public SensorViewModel DoorSensorViewModel { get; set; }
        public SensorViewModel MotionSensorViewModel { get; set; }
        public SensorViewModel GlassSensorViewModel { get; set; }


        public SystemState SystemState
        {
            get { return _systemState; }
            set
            {
                _systemState = value;
                OnPropertyChanged(nameof(SystemState));
            }
        }


        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
