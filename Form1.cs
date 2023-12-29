using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PowerKeys
{
    public partial class mainForm : Form
    {

        #region keyboard
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);
        public const byte KEYBDEVENTF_SHIFTVIRTUAL = 0x10;
        public const byte KEYBDEVENTF_SHIFTSCANCODE = 0x2A;
        public const int KEYBDEVENTF_KEYDOWN = 0;
        public const int KEYBDEVENTF_KEYUP = 2;
        public static void ShiftD()
        {
            keybd_event(KEYBDEVENTF_SHIFTVIRTUAL, KEYBDEVENTF_SHIFTSCANCODE, KEYBDEVENTF_KEYDOWN, 0);
        }
        public static void ShiftU()
        {
            keybd_event(KEYBDEVENTF_SHIFTVIRTUAL, KEYBDEVENTF_SHIFTSCANCODE, KEYBDEVENTF_KEYUP, 0);
        }
        #endregion
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

        public mainForm()
        {
            InitializeComponent();
            ghk = new Hotkeys.GlobalHotkey(Keys.BrowserHome, this);
            ghk.Register();
            AllKeys.Add(ghk);

            ghk = new Hotkeys.GlobalHotkey(Keys.Oemtilde, this);
            ghk.Register();
            AllKeys.Add(ghk);
        }

        #region unregister all keys
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }
        #endregion
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
            mouseClickR(); //Prevent villager from initial shift queue
            SendKeys.Send("q");
            SendKeys.Send("w");
            mouseClickL();
            SendKeys.Send("q");
            SendKeys.Send("a");
            ShiftD();
            for (int i = 0; i < 8; i++)
            {
                mouseClickL();
            }
            ShiftU();
        }

        private void BuildAOE4Houses()
        {
            mouseClickR(); //Prevent villager from initial shift queue
            SendKeys.Send("q");
            SendKeys.Send("q");
            ShiftD();
            for (int i = 0; i < 10; i++)
            {
                mouseClickL();
            }
            ShiftU();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
            {
                Keys key = (Keys)m.WParam;
                //Write(key.ToString());
                switch (key)
                {
                    case Keys.BrowserHome:
                        BuildAOE4Houses();
                        break;
                    case Keys.Oemtilde:
                        BuildAOE4Farms();
                        break;
                }
            }

            base.WndProc(ref m);
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UnRegister();
            Application.Exit();
        }

        private void UnRegister()
        {
            foreach (Hotkeys.GlobalHotkey ghk in AllKeys)
            {
                if (!ghk.Unregister())
                {
                    MessageBox.Show("PowerKeys failed to unregister! Try ending from Task Manager.");
                }
            }
        }

        private void Register()
        {
            foreach (Hotkeys.GlobalHotkey ghk in AllKeys)
            {
                ghk.Register();
            }
        }

        private void Write(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

        private void activeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (active.Checked == true)
            {
                UnRegister();
                active.Checked = false;
            }
            else
            {
                Register();
                active.Checked = true;
            }
        }
    }
}
