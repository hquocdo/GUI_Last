using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EsploraPulse.Model;
using LattePanda.Firmata;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace GUI_Last
{
    public partial class Lattepanda_Ehealth : Form
    {
        private ECG ecg = new ECG();
        private bool checkConnect = false;
        MqttClient client = new MqttClient("13.229.80.211");
        public Lattepanda_Ehealth()
        {
            InitializeComponent();
            try
            {
                this.ecg.checkMQTT(false);
                client.ProtocolVersion = MqttProtocolVersion.Version_3_1_1;
                byte code = client.Connect(Guid.NewGuid().ToString(), "hquocdo", "hung11898");
                client.MqttMsgPublished += Client_MqttMsgPublished;
                //client.ConnectionClosed += Client_ConnectionClosed;
            }
            catch(Exception e)
            {
                this.ecg.checkMQTT(true);
            }
            StyleGraphs();
        }

        private void StartListening()
        {
            //ecg = new ECG();
            while (ecg.values == null && ecg.ppg_values == null)
                System.Threading.Thread.Sleep(10);

            scottPlotUC1.plt.Clear();
            scottPlotUC2.plt.Clear();

            scottPlotUC2.plt.PlotSignal(ecg.ppg_values, ecg.SAMPLERATE, color: ColorTranslator.FromHtml("#d62728"));
            scottPlotUC1.plt.PlotSignal(ecg.values, ecg.SAMPLERATE, color: ColorTranslator.FromHtml("#d62728"));
            
            //scottPlotUC1.plt.AxisAuto();
            //scottPlotUC2.plt.AxisAuto();
            //scottPlotUC1.plt.Axis(y1: -Math.Pow(2, 16) / 2, y2: Math.Pow(2, 16) / 2);

            //scottPlotUC1.Render();
            //scottPlotUC2.Render();
            
            timerRenderGraph.Enabled = true;
            timerMqttPublish.Enabled = true;
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
            scottPlotUC2.Render();
            

            scottPlotUC3.plt.YLabel("Signal (PCM)");
            scottPlotUC3.plt.XLabel("Time (Seconds)");
            scottPlotUC3.plt.Title("SP02 Value");
            scottPlotUC3.Render();
            
        }
        bool busyRendering = false;
        bool useLowpassFilter = false;
     
        #endregion

        private void lblBPM_Click(object sender, EventArgs e)
        {

        }
        private bool displayHeartbeats = false;
        private void timerRenderGraph_Tick(object sender, EventArgs e)
        {
            if (busyRendering)
                return;

            busyRendering = false;

            if (useLowpassFilter)
            {
                scottPlotUC1.plt.Clear();
                scottPlotUC1.plt.PlotSignal(ecg.GetFilteredValues(ecg.values,ecg.lastPointUpdated), ecg.SAMPLERATE);
                scottPlotUC1.Render();

                scottPlotUC2.plt.Clear();
                scottPlotUC2.plt.PlotSignal(ecg.GetFilteredValues(ecg.ppg_values, ecg.lastPPGUpdated), ecg.SAMPLERATE);
                scottPlotUC2.Render();

            }
            else
            {
                scottPlotUC1.plt.Clear(signalPlots: false);
                scottPlotUC2.plt.Clear(signalPlots: false);

                scottPlotUC1.plt.PlotVLine((double)ecg.lastPointUpdated / ecg.SAMPLERATE, color: ColorTranslator.FromHtml("#636363"));
                scottPlotUC2.plt.PlotVLine((double)ecg.lastPPGUpdated / ecg.SAMPLERATE, color: ColorTranslator.FromHtml("#636363"));

                if (displayHeartbeats)
                    scottPlotUC1.plt.PlotHLine(ecg.beatThreshold);

                scottPlotUC1.Render();
                scottPlotUC2.Render();
            }
            //scottPlotUC2.plt.PlotSignal(ecg.data.SampleCounter, ecg.data.Signal);
            //Application.DoEvents();
            busyRendering = false;
        }



        private void timerMqttPublish_Tick(object sender, EventArgs e)
        {
            string data = this.ecg.data.BPM + "-" + this.ecg.spo2;
            
            ushort msgId = client.Publish("mqtt1", Encoding.UTF8.GetBytes(data), MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE, false);
            lblBPM.Text = ecg.data.BPM + " BPM";
            lblSPO2.Text = ecg.spo2 + " %";
        }

        private void Client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("MessageId = " + e.MessageId + " Published = " + e.IsPublished);
            
        }
        private void Client_ConnectionClosed(object sender, EventArgs e)
        {
            this.ecg.checkMQTT(true);
            checkConnect = true;
        }

        private void AutoConnect_Tick(object sender, EventArgs e)
        {
            if (checkConnect)
            {
                try
                {
                    checkConnect = false;
                    byte code = client.Connect(Guid.NewGuid().ToString(), "hquocdo", "hung11898");
                    this.ecg.checkMQTT(false);
                }
                catch (Exception exce)
                {
                    checkConnect = true;
                }
            }
        }
    }
}
