using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
        private string _windowTitle;

        public event PropertyChangedEventHandler PropertyChanged;

        private const string AppName = "PLC Security App";
        private const string NotConnected = " - Not Connected";

        public MainWindowViewModel()
        {
            WindowTitle = AppName + NotConnected;

            ConnectCommand = new DelegateCommand(ConnectCommandHandler);
            DoorSensorCommand = new DelegateCommand(DoorSensorCommandHandler);
            MotionSensorCommand = new DelegateCommand(MotionSensorCommandHandler);
            GlassSensorCommand = new DelegateCommand(GlassSensorCommandHandler);

            DoorSensorViewModel = new SensorViewModel();
            GlassSensorViewModel = new SensorViewModel();
            MotionSensorViewModel = new SensorViewModel();

            SystemState = new SystemState();
        }

        private void Initialize()
        {
            try
            {
                CreateSimulator();
                InitializeSensors();

                WindowTitle = AppName;
            }
            catch (Exception e)
            {
                MessageBox.Show($"Unable to connect to simulator.\n{e.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //Application.Current.Shutdown();
            }
        }

        private void InitializeSensors()
        {
            SystemState.Reset();
            _simulator.ResetSensors();
        }

        private void CreateSimulator()
        {
            _simulator = new PlcSimulator();
            _simulator.Connect();
        }

        private void GlassSensorCommandHandler(object obj)
        {
            SwitchSensorState(SystemState.GlassSensor, (x) => SystemState.GlassSensor = x);
            _simulator?.ModifySensor(SensorType.Glass, SystemState.GlassSensor);

            UpdateOutput();
        }

        private void MotionSensorCommandHandler(object o)
        {
            SwitchSensorState(SystemState.MotionSensor, (x) => SystemState.MotionSensor = x);
            _simulator?.ModifySensor(SensorType.Motion, SystemState.MotionSensor);

            UpdateOutput();
        }


        private void DoorSensorCommandHandler(object o)
        {
            SwitchSensorState(SystemState.DoorSensor, (x) => SystemState.DoorSensor = x);
            _simulator?.ModifySensor(SensorType.Door, SystemState.DoorSensor);

            UpdateOutput();
        }

        private void UpdateOutput()
        {
            SystemState.AlarmState = _simulator != null ? _simulator.ReadAlarmState() : SensorState.Ok;
        }

        public void ConnectCommandHandler(object obj)
        {
            Initialize();
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

        public String WindowTitle
        {
            get { return _windowTitle; }
            set
            {
                _windowTitle = value;
                OnPropertyChanged(nameof(WindowTitle));
            }
        }

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
