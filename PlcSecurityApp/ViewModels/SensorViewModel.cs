using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace PlcSecurityApp.ViewModels
{
    class SensorViewModel
    {
        public SensorViewModel()
        {
            FillColor = Brushes.Green;
            Text = "OK";
        }

        public Brush FillColor { get; set; }
        public String Text { get; set; }
    }
}
