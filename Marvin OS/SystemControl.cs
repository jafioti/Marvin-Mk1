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
    class SystemControl
    {
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;

        public const int KEYEVENTF_EXTENTEDKEY = 1;
        public const int KEYEVENTF_KEYUP = 0;
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        public const int VK_MEDIA_PREV_TRACK = 0xB1;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        #region volume
        public void Mute()
        {
            SendMessageW(Form1.ActiveForm.Handle, WM_APPCOMMAND, Form1.ActiveForm.Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        public void VolumeDown()
        {
            SendMessageW(Form1.ActiveForm.Handle, WM_APPCOMMAND, Form1.ActiveForm.Handle, (IntPtr)APPCOMMAND_VOLUME_DOWN);
        }

        public void VolumeUp()
        {
            SendMessageW(Form1.ActiveForm.Handle, WM_APPCOMMAND, Form1.ActiveForm.Handle, (IntPtr)APPCOMMAND_VOLUME_UP);
        }
        #endregion

        #region music
        public void PlayPause()
        {
            keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
        }

        public void NextTrack()
        {
            keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
        }

        public void PrevTrack()
        {
            keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
        }
        #endregion

        #region power
        public void Sleep()
        {
            Application.SetSuspendState(PowerState.Suspend, true, true);
        }

        public void Restart()
        {
            Process.Start("shutdown", "/r /t 0");
        }

        public void Hybernate()
        {
            Application.SetSuspendState(PowerState.Hibernate, true, true);
        }

        public void Shutdown()
        {
            Process.Start("shutdown", "/s /t 0");
        }

        public void Lock()
        {
            Process.Start(@"C:\WINDOWS\system32\rundll32.exe", "user32.dll,LockWorkStation");
        }
        #endregion

    }
}
