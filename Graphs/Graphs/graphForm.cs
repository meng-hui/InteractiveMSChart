using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

namespace Graphs
{
    public partial class graphForm : Form
    {
        //data structure to bind to both table and chart
        private BindingList<GraphXYData> graphData;

        //used for mouse over chart events
        private bool isDraggingPoint = false;
        private int draggedPointIndex = -1;

        public graphForm()
        {
            InitializeComponent();
            this.initializeGraphDataBindingList();
            this.customTableAndGraphInitialization();
        }

        private void initializeGraphDataBindingList()
        {
            this.graphData = new BindingList<GraphXYData>();
            this.graphData.Add(new GraphXYData(0, 5));
            this.graphData.Add(new GraphXYData(1, 6));
            this.graphData.Add(new GraphXYData(2, 7));
            this.graphData.Add(new GraphXYData(3, 8));
            this.graphData.Add(new GraphXYData(4, 9));
        }

        private void customTableAndGraphInitialization()
        {
            //set the DataPropertyName, can be set in designer as well
            this.dataGridView.Columns[0].DataPropertyName = "X";
            this.dataGridView.Columns[1].DataPropertyName = "Y";

            //similarly for graph, set the value members
            this.chart.Series[0].XValueMember = "X";
            this.chart.Series[0].YValueMembers = "Y";

            //bind data to table
            this.dataGridView.DataSource = this.graphData;
            //bind data to chart
            this.chart.DataSource = this.graphData;
            this.chart.DataBind();

            //set default appearance of graph
            this.chart.Series[0].Color = Color.DarkBlue;
            this.chart.Series[0].MarkerStyle = MarkerStyle.Cross;
            this.chart.Series[0].MarkerColor = Color.Brown;
            this.chart.Series[0].MarkerSize = 5;
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.chart.DataBind(); //refresh chart when table value changes
        }

        #region chart mouse event handling

        private void chart_MouseMove(object sender, MouseEventArgs e)
        {
            //call HitTest
            HitTestResult result = this.chart.HitTest(e.X, e.Y);

            // reset all data point attributes
            foreach (DataPoint point in this.chart.Series[0].Points)
            {
                this.dataPointResetAppearance(point);
            }

            //if mouse is over a data point
            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                //find mouse-over data point
                DataPoint point = this.chart.Series[0].Points[result.PointIndex];

                //change appearance of that data point 
                this.dataPointSetMouseOverAppearance(point);
            }

            //additionally, if mouse is dragging a data point
            if (this.isDraggingPoint)
            {
                //change appearance of the graph
                this.chart.Series[0].Color = Color.Lime;

                //change mouse position values to X and Y values
                this.chart.Series[0].Points[this.draggedPointIndex].XValue = this.chart.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                this.chart.Series[0].Points[this.draggedPointIndex].YValues[0] = this.chart.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);

                //update the values for the table to display
                this.graphData[this.draggedPointIndex].X = this.chart.Series[0].Points[this.draggedPointIndex].XValue;
                this.graphData[this.draggedPointIndex].Y = this.chart.Series[0].Points[this.draggedPointIndex].YValues[0];
            }
        }

        private void chart_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = this.chart.HitTest(e.X, e.Y);

            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                // store the index of the point being dragged
                this.draggedPointIndex = result.PointIndex;
                this.isDraggingPoint = true;
            }
        }

        private void chart_MouseUp(object sender, MouseEventArgs e)
        {
            this.draggedPointIndex = -1;
            this.isDraggingPoint = false;
            //set appearance of graph back to normal
            this.chart.Series[0].Color = Color.DarkBlue;
        }

        private void dataPointResetAppearance(DataPoint point)
        {
            point.MarkerColor = Color.Brown;
            point.MarkerSize = 5;
        }

        private void dataPointSetMouseOverAppearance(DataPoint point)
        {
            point.MarkerColor = Color.Red;
            point.MarkerSize = 10;
        }

        #endregion

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
