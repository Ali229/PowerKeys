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

        #region keyboard
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void keybd_event(uint bVk, uint bScan, uint dwFlags);
        private const int VK_Key_Q = 0x51;
        #endregion

        private readonly Hotkeys.GlobalHotkey ghk;

        public Form1()
        {
            InitializeComponent();
            ghk = new Hotkeys.GlobalHotkey(Keys.BrowserHome, this);
            ghk.Register();
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
            keybd_event(VK_Key_Q, 0, 0);
            //SendKeys.Send("q");
            //SendKeys.Send("w");
            //mouseClickL();
            //SendKeys.Send("q");
            //SendKeys.Send("a");
            //mouseClickL();
            //SendKeys.Send("q");
            //SendKeys.Send("a");
            //mouseClickL();
            //SendKeys.Send("q");
            //SendKeys.Send("a");
            //mouseClickL();
            //SendKeys.Send("q");
            //SendKeys.Send("a");
            //mouseClickL();
            //SendKeys.Send("q");
            //SendKeys.Send("a");
            //mouseClickL();
            //SendKeys.Send("q");
            //SendKeys.Send("a");
            //mouseClickL();
            //SendKeys.Send("q");
            //SendKeys.Send("a");
            //mouseClickL();
            //SendKeys.Send("q");
            //SendKeys.Send("a");
            //mouseClickL();
            //mouseClickR();
            //mouseClickL();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
            {
                BuildAOE4Farms();
            }

            base.WndProc(ref m);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ghk.Unregiser())
            {
                MessageBox.Show("PowerKeys failed to unregister! Try ending from Task Manager.");
            }
        }

        private void Write(string msg)
        {
            System.Diagnostics.Debug.WriteLine(msg);
        }

    }
}
