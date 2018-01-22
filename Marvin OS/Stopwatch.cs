using System;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Marvin_OS
{
    public partial class Stopwatch : Form
    {
        System.Threading.Thread t;
        String currentTime = "nah";
        bool isRunning = true;
        public Stopwatch()
        {
            InitializeComponent();
        }

        public System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

        private void button1_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                button1.Text = "Resume";
                StopWatch();
            }
            else
            {
                button1.Text = "Stop";
                StartWatch();
            }
        }

        public void StartWatch()
        {
            watch.Start();
        }

        public void StopWatch()
        {
            watch.Stop();
        }

        public string CheckWatch()
        {
            string elapsedTime = "";
            if(watch.Elapsed.Hours < 0)
            {
                elapsedTime += watch.Elapsed.Hours.ToString() + " hours ";
            }
            if(watch.Elapsed.Minutes < 0)
            {
                elapsedTime += watch.Elapsed.Minutes.ToString() + " minutes and "; 
            }
            elapsedTime += watch.Elapsed.Seconds.ToString() + "." + (watch.Elapsed.Milliseconds / 10).ToString() + " seconds";
            Debug.WriteLine(elapsedTime);
            return (elapsedTime);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (watch.IsRunning)
            {              
                TimeSpan ts = watch.Elapsed;
                label1.Text = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                currentTime = label1.Text;
            }
        }
    }
}
