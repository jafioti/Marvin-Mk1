using System.Windows.Forms;
using System;
using System.Linq;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;
using System.Xml;
using System.IO;
using System.ServiceModel.Syndication;


namespace Marvin_OS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region variables
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        Weather weatherClass = new Weather();
        Utility utilityClass = new Utility();
        Question questionClass = new Question();
        Prelim prelimClass = new Prelim();
        SystemControl systemClass = new SystemControl();
        Smarthome smarthomeClass = new Smarthome();

        MathClass math = new MathClass();
        public static string prelim = "";
        int mathProb = 1;
        bool hasReturned = false;
        SpeechSynthesizer speech = new SpeechSynthesizer();

        bool muteMic = true;
        
        //speech
        

        //temp vars
        public string mathAns = "";


        #endregion
        
        #region volume controls
        
        
        #endregion

        private void button1_Click(object sender, System.EventArgs e)
        {
            analyze("");
        }

        public void analyze(string specIn)
        {
            #region setup
            if(speech.State == SynthesizerState.Speaking)
            {
                if (inputBox.Text.ToLower().Contains("stop"))
                {
                    speech.SpeakAsyncCancelAll();
                    return;
                }
                else
                {
                    return;
                }
            }
            hasReturned = false;
            //refine input
            string input = "";
            if (specIn == "")
            {
                input = inputBox.Text.ToLower();
            }
            else
            {
                input = specIn;
            }
            if (input[input.Length - 1] == '.' || input[input.Length - 1] == '?')
            {
                input = input.Remove(input.Length - 1, 1);
            }
            #endregion

            string temp = "";
            #region prelims
            if (prelim != "")
            {
                //special cases
                temp = prelimClass.analyzePrelim(prelim, input);
                if(temp == "done")
                {
                    return;
                }else if(temp != "")
                {
                    ret(temp);
                    return;
                }
            }
            #endregion

            #region run through questions
            //run through questions
            if (input.Contains("what") || input.Contains("when") || input.Contains("will") || input.Contains("who") || input.Contains("how") || input.Contains("can") || input.Contains("are") || input.Contains("is"))
            {
                temp = questionClass.analyzeQuestion(input);
            }

            if (temp == "done")
            {
                return;
            }
            else if (temp == "volumeup")
            {
                systemClass.VolumeUp();
                return;
            }
            else if (temp == "volumedown")
            {
                systemClass.VolumeDown();
                return;
            }
            else if (temp == "mute")
            {
                systemClass.Mute();
                return;
            }
            else if (temp == "play")
            {
                systemClass.PlayPause();
                return;
            }
            else if (temp != "")
            {
                ret(temp);
                return;
            }
            #endregion

            #region run through utility
            //run through utility
            temp = utilityClass.analyzeUtility(input);
            if(temp == "done")
            {
                return;
            }else if(temp == "volumeup")
            {
                systemClass.VolumeUp();
                return;
            }else if(temp == "volumedown")
            {
                systemClass.VolumeDown();
                return;
            }else if(temp == "mute")
            {
                systemClass.Mute();
                return;
            }else if(temp == "play")
            {
                systemClass.PlayPause();
                return;
            }
            else if(temp != "")
            {
                ret(temp);
                return;
            }
            #endregion

            //if all else fails
            input.Replace(" ", "");
            if (input == "")
            {
                ret("You didn't say anything!");
                return;
            }
            temp = questionClass.wolfram(input);
            if(temp != "done" && temp != "")
            {
                ret(temp);
            }
        }

        #region other functions
        public void ret(string output)
        {
            hasReturned = true;
            richTextBox1.Text = output;
            if (inputBox.Text.Contains("math") || inputBox.Text.Contains("square root") || inputBox.Text.Contains("squared") || (inputBox.Text.Any(char.IsDigit) && (inputBox.Text.Contains("+") || inputBox.Text.Contains("-") || inputBox.Text.Contains("*") || inputBox.Text.Contains("/"))))
            {
                output = output.Replace("-", "minus ");
                output = output.Replace("*", "times ");
                output = output.Replace("/", "divided by ");
            }
            //speech.SpeakAsync(output);
        }

        //allows enter to be pressed
        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                inputBox.Text = "";
                button1.PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (muteMic)
            {
                //unmute
                muteMic = false;
                button2.Image = Properties.Resources._6083_200;
                recEngine.SetInputToDefaultAudioDevice();
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
                recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
            }
            else
            {
                //mute
                muteMic = true;
                button2.Image = Properties.Resources._6083_201;
                recEngine.RecognizeAsyncStop();
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            recEngine.LoadGrammarAsync(new DictationGrammar());            
            if (!muteMic)
            {
                recEngine.SetInputToDefaultAudioDevice();
                recEngine.RecognizeAsync(RecognizeMode.Multiple);
                recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
            }
            */
        }
        
        private void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //if (e.Result.Text.ToLower().Contains("marvin"))
            //{
                inputBox.Text = e.Result.Text;
                //analyze("");
            //}
        }

        void google(string query)
        {
            string url = "https:\\www.google.com/search?q=" + System.Uri.EscapeDataString(query);
            System.Diagnostics.Process.Start("chrome.exe", url);
        }
        #endregion
    }
}