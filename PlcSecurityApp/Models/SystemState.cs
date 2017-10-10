﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PlcSecurityApp.Views.Controls;

namespace PlcSecurityApp.Models
{
    public class SystemState : INotifyPropertyChanged
    {
        private SensorState _doorSensor = SensorState.Alert;

        public SensorState DoorSensor
        {
            get { return _doorSensor; }
            set
            {
                _doorSensor = value;
                OnPropertyChanged(nameof(DoorSensor));
            }
        }

        public SensorState MotionSensor { get; set; } = SensorState.Ok;
        public SensorState GlassSensor { get; set; } = SensorState.Ok;



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}