﻿using System;
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
            SystemState = new SystemState();

            ConnectCommand = new DelegateCommand(ConnectCommandHandler);
            DoorSensorCommand = new DelegateCommand(DoorSensorCommandHandler);
            MotionSensorCommand = new DelegateCommand(MotionSensorCommandHandler);
            GlassSensorCommand = new DelegateCommand(GlassSensorCommandHandler);

            DoorSensorViewModel = new SensorViewModel();
            GlassSensorViewModel = new SensorViewModel();
            MotionSensorViewModel = new SensorViewModel();

            _simulator?.Connect();
            var currentState = _simulator?.GetSystemState();
        }

        private void GlassSensorCommandHandler(object obj)
        {
            // MessageBox.Show("Glass Sensor");
        }

        private void MotionSensorCommandHandler(object o)
        {
            // MessageBox.Show("Motion Sensor");
        }

        private void DoorSensorCommandHandler(object o)
        {

        }

        public void ConnectCommandHandler(object obj)
        {
            // _simulator.Connect();
            // For test now.
            SystemState.DoorSensor = SensorState.Alert;
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
            get
            {
                return _systemState;
            }
            set
            {
                _systemState = value; 
                OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
