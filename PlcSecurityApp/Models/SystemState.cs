using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using PlcSecurityApp.Views.Controls;
using PlcSecurityApp.Views.UC;

namespace PlcSecurityApp.Models
{
    public class SystemState : INotifyPropertyChanged
    {
        private SensorState _doorSensor;
        private SensorState _motionSensor;
        private SensorState _glassSensor;
        private SensorState _alarmState;

        public SystemState()
        {
            Reset();
        }

        public void Reset()
        {
            DoorSensor = SensorState.Ok;
            MotionSensor = SensorState.Ok;
            GlassSensor = SensorState.Ok;

            AlarmState = SensorState.Ok;
        }

        public SensorState DoorSensor
        {
            get { return _doorSensor; }
            set
            {
                _doorSensor = value;
                OnPropertyChanged(nameof(DoorSensor));
            }
        }

        public SensorState MotionSensor
        {
            get { return _motionSensor; }
            set
            {
                _motionSensor = value;
                OnPropertyChanged(nameof(MotionSensor));
            }
        }

        public SensorState GlassSensor
        {
            get { return _glassSensor; }
            set
            {
                _glassSensor = value;
                OnPropertyChanged(nameof(GlassSensor));
            }
        }

        public SensorState AlarmState
        {
            get { return _alarmState; }
            set
            {
                _alarmState = value;
                OnPropertyChanged(nameof(AlarmState));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"Door: {DoorSensor}, Glass: {GlassSensor}, Motion: {MotionSensor} Alarm: {AlarmState}";
        }
    }
}
