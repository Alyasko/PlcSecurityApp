using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PlcSecurityApp.Views.Controls
{
    /// <summary>
    /// Interaction logic for Sensor.xaml
    /// </summary>
    public partial class Sensor : UserControl, INotifyPropertyChanged
    {
        public SensorState SensorState
        {
            get { return (SensorState)GetValue(SensorStateProperty); }
            set
            {
                UpdateSensorState(value);
                SetValue(SensorStateProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for SensorState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SensorStateProperty =
            DependencyProperty.Register("SensorState", typeof(SensorState), typeof(Sensor), new PropertyMetadata(SensorState.Ok));



        public ICommand StateChangedCommand
        {
            get { return (ICommand)GetValue(StateChangedCommandProperty); }
            set { SetValue(StateChangedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateChangedCommandProperty =
            DependencyProperty.Register("StateChangedCommand", typeof(ICommand), typeof(Sensor), new PropertyMetadata(null));



        public string SensorName
        {
            get { return (string)GetValue(SensorNameProperty); }
            set { SetValue(SensorNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SensorName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SensorNameProperty =
            DependencyProperty.Register("SensorName", typeof(string), typeof(Sensor), new PropertyMetadata("Sensor"));



        public bool OnlyOutput
        {
            get { return (bool)GetValue(OnlyOutputProperty); }
            set { SetValue(OnlyOutputProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OnlyOutput.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OnlyOutputProperty =
            DependencyProperty.Register("OnlyOutput", typeof(bool), typeof(Sensor), new PropertyMetadata(false));

        private string _sensorText;
        public string SensorText
        {
            get { return _sensorText; }
            set
            {
                _sensorText = value;
                OnPropertyChanged();
            }
        }

        public static readonly DependencyProperty SensorTextProperty =
            DependencyProperty.Register("SensorText", typeof(string), typeof(Sensor), new PropertyMetadata(false));

        private Brush _sensorFill;
        public Brush SensorFill
        {
            get { return _sensorFill; }
            set
            {
                _sensorFill = value;
                OnPropertyChanged();
            }
        }

        public Sensor()
        {
            InitializeComponent();

            SensorState = SensorState.Ok;
        }

        private void Sensor_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (OnlyOutput)
                return;

            if (SensorState == SensorState.Alert)
                SensorState = SensorState.Ok;
            else if (SensorState == SensorState.Ok)
                SensorState = SensorState.Alert;

            StateChangedCommand?.Execute(SensorState);
        }

        private void UpdateSensorState(SensorState state)
        {
            switch (state)
            {
                case SensorState.Ok:
                    SensorText = "OK";
                    SensorFill = Brushes.Green;
                    break;
                case SensorState.Alert:
                    SensorText = "ALERT";
                    SensorFill = Brushes.Red;
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
