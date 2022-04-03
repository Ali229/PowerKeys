using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PowerKeys
{
    public partial class Form1 : Form
    {
        #region mouse
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        private const int MOUSEEVENTF_MIDDLEUP = 0x40;
        private const int MOUSEEVENTF_WHEEL = 0x800;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        #endregion

        List<Hotkeys.GlobalHotkey> AllKeys = new List<Hotkeys.GlobalHotkey>();
        private readonly Hotkeys.GlobalHotkey ghk;

        public Form1()
        {
            InitializeComponent();
            ghk = new Hotkeys.GlobalHotkey(Keys.BrowserHome, this);
            ghk.Register();
            AllKeys.Add(ghk);

            ghk = new Hotkeys.GlobalHotkey(Keys.LaunchMail, this);
            ghk.Register();
            AllKeys.Add(ghk);
        }

        private void mouseClickL()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP);
        }

        private void mouseClickR()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP);
        }

        private void BuildAOE4Farms()
        {
            SendKeys.Send("q");
            SendKeys.Send("w");
            mouseClickL();
            for (int i = 0; i < 8; i++)
            {
                SendKeys.Send("q");
                SendKeys.Send("a");
                mouseClickL();
            }
            mouseClickR();
            mouseClickL();
        }

        private void BuildAOE4Houses()
        {
            for (int i = 0; i < 9; i++)
            {
                SendKeys.Send("q");
                SendKeys.Send("q");
                mouseClickL();
            }
            mouseClickL();
        }

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
            {
                Keys key = (Keys)m.WParam;
                switch (key)
                {
                    case Keys.BrowserHome:
                        BuildAOE4Farms();
                        break;
                    case Keys.LaunchMail:
                        BuildAOE4Houses();
                        break;
                }
            }

            base.WndProc(ref m);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Hotkeys.GlobalHotkey ghk in AllKeys)
            {
                if (!ghk.Unregiser())
                {
                    MessageBox.Show("PowerKeys failed to unregister! Try ending from Task Manager.");
                }
            }
        }

        private void Write(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

    }
}
