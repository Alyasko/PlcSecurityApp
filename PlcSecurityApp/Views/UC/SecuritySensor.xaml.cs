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

namespace PlcSecurityApp.Views.UC
{
    /// <summary>
    /// Interaction logic for SecuritySensor.xaml
    /// </summary>
    public partial class SecuritySensor : UserControl, INotifyPropertyChanged
    {
        public SecuritySensor()
        {
            InitializeComponent();
        }

        public bool ReadOnly
        {
            get { return (bool)GetValue(ReadOnlyProperty); }
            set { SetValue(ReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReadOnlyProperty =
            DependencyProperty.Register("ReadOnly", typeof(bool), typeof(SecuritySensor), new PropertyMetadata(false));



        public SensorState SensorState
        {
            get { return (SensorState)GetValue(SensorStateProperty); }
            set { SetValue(SensorStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SensorState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SensorStateProperty =
            DependencyProperty.Register("SensorState", typeof(SensorState), typeof(SecuritySensor), new PropertyMetadata(SensorState.Ok));

        public ICommand StateChangedCommand
        {
            get { return (ICommand)GetValue(StateChangedCommandProperty); }
            set { SetValue(StateChangedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StateChangedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateChangedCommandProperty =
            DependencyProperty.Register("StateChangedCommand", typeof(ICommand), typeof(SecuritySensor), new PropertyMetadata(null));


        public string SensorName
        {
            get { return (string)GetValue(SensorNameProperty); }
            set { SetValue(SensorNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SensorName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SensorNameProperty =
            DependencyProperty.Register("SensorName", typeof(string), typeof(SecuritySensor), new PropertyMetadata("Default Name"));


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SecuritySensor_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(ReadOnly)
                return;

            StateChangedCommand?.Execute(SensorState);
        }
    }
}
