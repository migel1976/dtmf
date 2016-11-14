using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio;
using NAudio.Wave;
using DtmfDetection;
using DtmfDetection.NAudio;


namespace listen_dtmf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
             analize_dtfm(openFileDialog1.FileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
            openFileDialog1.ShowDialog();

        }

        void analize_dtfm(string filename)
        {
            try
            {
                using (var waveFile = new WaveFileReader(filename))
                //using (var waveFile = new WaveFileReader("dtmf_sample.wav"))
                {
                    foreach (var occurence in waveFile.DtmfTones())
                    {
                        string text = String.Format("TotalSeconds :{0:0.00} Key:{1} Duration:{2:0.000}",
                          occurence.Position.TotalSeconds, occurence.DtmfTone.Key, occurence.Duration.TotalSeconds);

                        

                        listBox1.Items.Add(text);
                        PhoneKey key = occurence.DtmfTone.Key;
                        
                        listBox1.Items.Add(key.ToString());
                        listBox1.Items.Add(occurence.DtmfTone.ToString());
                        listBox1.Items.Add(occurence.DtmfTone.LowTone.ToString());
                        listBox1.Items.Add(occurence.DtmfTone.HighTone.ToString());
                         

                        ////listBox1.Items.Add(       $"{occurence.Position.TotalSeconds:00.000} s: "
                        ////                + $"{occurence.DtmfTone.Key} key "
                        ////                + $"(duration: {occurence.Duration.TotalSeconds:00.000} s)");

                        ////Console.WriteLine($"{occurence.Position.TotalSeconds:00.000} s: "
                        ////                + $"{occurence.DtmfTone.Key} key "
                        ////                + $"(duration: {occurence.Duration.TotalSeconds:00.000} s)");
                    }
                }
            }
            catch (SystemException exc)
            {
                MessageBox.Show("Please select WAV file");
            }
        }
    }
}
