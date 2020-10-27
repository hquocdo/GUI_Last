using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_Last
{
    public partial class Lattepanda_Ehealth : Form
    {
        private ECG ecg;
        public Lattepanda_Ehealth()
        {
            InitializeComponent();
            StyleGraphs();
        }

        private void StartListening()
        {
            ecg = new ECG();

            while (ecg.values == null)
                System.Threading.Thread.Sleep(10);
            scottPlotUC1.plt.Clear();
            scottPlotUC1.plt.PlotSignal(ecg.values, ecg.SAMPLERATE, color: ColorTranslator.FromHtml("#d62728"));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //StartListening();
        }
        #region graphing
        private void StyleGraphs()
        {
            scottPlotUC1.plt.YLabel("Signal (PCM)");
            scottPlotUC1.plt.XLabel("Time (Seconds)");
            scottPlotUC1.plt.Title("ECG Signal");
            scottPlotUC1.Render();

            scottPlotUC2.plt.YLabel("Signal (PCM)");
            scottPlotUC2.plt.XLabel("Time (Seconds)");
            scottPlotUC2.plt.Title("PPG Signal");
            

            scottPlotUC3.plt.YLabel("Signal (PCM)");
            scottPlotUC3.plt.XLabel("Time (Seconds)");
            scottPlotUC3.plt.Title("SP02 Value");
            
        }

        #endregion

        private void lblBPM_Click(object sender, EventArgs e)
        {

        }
    }
}
