README

To compile and run this program, you need .NET Framework 3.5 Service Pack 1 (http://www.microsoft.com/en-us/download/details.aspx?id=22) and Microsoft Chart Controls (http://www.microsoft.com/en-us/download/details.aspx?id=14422)

This small program demonstrates how to achieve simple interactivity on Microsoft Chart Controls. The appearance of the graph is modified in accordance to the mouse position and mouse events on the chart, namely MouseMove, MouseDown and MouseUp events.  PixelPositionToValue is used to convert the mouse position to an axis value.

The graph data implements INotifyPropertyChanged, which allows the DataGridView to update when moving a data point on the chart.