using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;

namespace Graphs
{
    public class GraphXYData : INotifyPropertyChanged
    {
        private double x;
        private double y;

        public event PropertyChangedEventHandler PropertyChanged;

        public GraphXYData(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X
        {
            get { return this.x; }
            set
            {
                this.x = value;
                this.NotifyPropertyChanged("X");
            }
        }

        public double Y
        {
            get { return this.y; }
            set
            {
                this.y = value;
                this.NotifyPropertyChanged("Y");
            }
        }

        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
