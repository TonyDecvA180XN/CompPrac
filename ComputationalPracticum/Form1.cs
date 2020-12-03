using System;
using System.Windows.Forms;

namespace ComputationalPracticum
{
    public partial class MainFrame : Form
    {
        FunctionView[] Solutions;
        Error[] Errors;
        GlobalError GE;

        public MainFrame()
        {
            InitializeComponent();
            Solutions = new FunctionView[] { new EulerMethod(), new EulerMethodImproved(), new RungeKutta(), new Exact() };
            Errors = new Error[] {new Error(Solutions[0], Solutions[3]),
                new Error(Solutions[1], Solutions[3]),
                new Error(Solutions[2], Solutions[3])
            };
            GE = new GlobalError(new Error[] { Errors[0], Errors[1], Errors[2] });
            UpdateSolutions();
            ErrorsChart.Series[1].Enabled = false;
            ErrorsChart.Series[2].Enabled = false;
            SolutionsChart.ChartAreas[0].AxisX.Minimum = (double)x_0Field.Value;
            SolutionsChart.ChartAreas[0].AxisX.Maximum = (double)XField.Value;
        }

        private void UpdateSolutions()
        {
            BorderRegion borderRegion;
            borderRegion.y_min = 0.0;
            borderRegion.y_max = 0.0;

            for (int i = 3; i >= 0; i--)
            {
                borderRegion = BorderRegion.Extend(borderRegion, UpdateSolution(i));
            }

            if (borderRegion.y_min < borderRegion.y_max)
            {
                SolutionsChart.ChartAreas[0].AxisY.Minimum = borderRegion.y_min;
                SolutionsChart.ChartAreas[0].AxisY.Maximum = borderRegion.y_max;
            }
        }

        private void MainMenuFileStripExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void x_0Field_ValueChanged(object sender, EventArgs e)
        {
            if (x_0Field.Value < XField.Value)
            {
                SolutionsChart.ChartAreas[0].AxisX.Minimum = (double)x_0Field.Value;
                UpdateSolutions();
                StatusLabel.Text = "Ready";
                StatusLabel.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                StatusLabel.Text = "Range is negative!";
                StatusLabel.ForeColor = System.Drawing.Color.Red;
            }
        }

        private BorderRegion UpdateSolution(int index)
        {
            double x0 = (double)x_0Field.Value;
            double y0 = (double)y_0Field.Value;
            double X = (double)XField.Value;
            int N = (int)NField.Value;
            BorderRegion br = Solutions[index].Update(x0, y0, X, N);
            SolutionsChart.Series[0].Points.DataBindXY(Solutions[index].GetXPoints(), Solutions[index].GetYPoints());
            if (index != 3)
            {
                Errors[index].Update();
                ErrorsChart.Series[index].Points.DataBindXY(Solutions[index].GetXPoints(), Errors[index].GetErrors());
            }
            return br;
        }


        private void y_0Field_ValueChanged(object sender, EventArgs e)
        {
            UpdateSolutions();
        }

        private void XField_ValueChanged(object sender, EventArgs e)
        {
            if (x_0Field.Value < XField.Value)
            {
                SolutionsChart.ChartAreas[0].AxisX.Maximum = (double)XField.Value;
                UpdateSolutions();
                StatusLabel.Text = "Ready";
                StatusLabel.ForeColor = System.Drawing.Color.Black;

            }
            else
            {
                StatusLabel.Text = "Range is negative!";
                StatusLabel.ForeColor = System.Drawing.Color.Red;

            }
        }

        private void NField_ValueChanged(object sender, EventArgs e)
        {
            UpdateSolutions();
        }

        private void x_0Bar_ValueChanged(object sender, EventArgs e)
        {
            x_0Field.Value = (decimal)(x_0Bar.Value / 100.0);
        }

        private void NBar_ValueChanged(object sender, EventArgs e)
        {
            NField.Value = NBar.Value;
        }

        private void XBar_ValueChanged(object sender, EventArgs e)
        {
            XField.Value = (decimal)(XBar.Value / 100.0);
        }

        private void y_0Bar_ValueChanged(object sender, EventArgs e)
        {
            y_0Field.Value = (decimal)(y_0Bar.Value / 100.0);
        }

        private void ExactCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            SolutionsChart.Series[3].Enabled = ExactCheckbox.Checked;
        }

        private void EulerCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            SolutionsChart.Series[0].Enabled = EulerCheckbox.Checked;
        }

        private void EulerLECheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            ErrorsChart.Series[0].Enabled = EulerLECheckbox.Checked;
        }

        private void EulerImprovedLECheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            ErrorsChart.Series[1].Enabled = EulerImprovedLECheckbox.Checked;
        }

        private void RungeKuttaLECheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            ErrorsChart.Series[2].Enabled = RungeKuttaLECheckbox.Checked;
        }

        private void TabSet_Selecting(object sender, TabControlCancelEventArgs e)
        {
            UpdateGE();
        }

        private void UpdateGE()
        {
            double[][] values = GE.Compute(n_0Field.Value, NUpField.Value, NField);
            double[] x_values = new double[(int)(NUpField.Value - n_0Field.Value + 1)];
            for (int i = (int)n_0Field.Value; i <= NUpField.Value; i++)
            {
                x_values[i - (int)n_0Field.Value] = i;
            }

            for (int i = 0; i < 3; i++)
            {

                GlobalErrorChart.Series[i].Points.DataBindXY(x_values, values[i]);
            }
        }

        private void n_0Field_ValueChanged(object sender, EventArgs e)
        {
            UpdateGE();
        }

        private void NUpField_ValueChanged(object sender, EventArgs e)
        {
            UpdateGE();
        }

        private void NUpBar_ValueChanged(object sender, EventArgs e)
        {
            NUpField.Value = NUpBar.Value;
        }

        private void n_0Bar_ValueChanged(object sender, EventArgs e)
        {
            n_0Field.Value = n_0Bar.Value;
        }

        private void EulerGECheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            GlobalErrorChart.Series[0].Enabled = EulerGECheckbox.Checked;
        }

        private void ImprovedGECheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            GlobalErrorChart.Series[1].Enabled = ImprovedGECheckbox.Checked;
        }

        private void RungeGECheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            GlobalErrorChart.Series[2].Enabled = RungeGECheckbox.Checked;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Computational Practicum App\nCreated by Anton Dospekhov (BS19-01)\nTelegram: @tonydecva180xn");
        }

        private void RungeKuttaCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            SolutionsChart.Series[2].Enabled = RungeKuttaCheckbox.Checked;
        }

        private void ImprovedCheckbox_CheckStateChanged(object sender, EventArgs e)
        {
            SolutionsChart.Series[1].Enabled = ImprovedCheckbox.Checked;
        }
    }
}
