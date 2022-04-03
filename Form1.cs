using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PowerKeys
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        const int KEY_DOWN_EVENT = 0x0001; //Key down flag
        const int KEY_UP_EVENT = 0x0002; //Key up flag

        private Hotkeys.GlobalHotkey ghk;

        public Form1()
        {
            InitializeComponent();
            //ghk = new Hotkeys.GlobalHotkey(Constants.SHIFT, Keys.BrowserHome, this);
            ghk = new Hotkeys.GlobalHotkey(Keys.OemPeriod, this);
            ghk.Register();
        }

        private void mouseClickL()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;

            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            System.Threading.Thread.Sleep(1);
        }

        private void mouseClickR()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;

            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
            System.Threading.Thread.Sleep(1);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);

        private const int VK_LSHIFT = 0xA0;

        public static void LeftArrow()
        {
            keybd_event(VK_LSHIFT, 0, KEY_DOWN_EVENT | KEY_UP_EVENT, 0);
        }

        private const int KEYEVENTF_KEYDOWN = 0x0;
        private const int KEYEVENTF_KEYUP = 0x2;
        private void BuildAOE4Farms()
        {
            SendKeys.Send("q");
            SendKeys.Send("w");
            mouseClickL();
            SendKeys.Send("q");
            SendKeys.Send("a");
            mouseClickL();
            SendKeys.Send("q");
            SendKeys.Send("a");
            mouseClickL();
            SendKeys.Send("q");
            SendKeys.Send("a");
            mouseClickL();
            SendKeys.Send("q");
            SendKeys.Send("a");
            mouseClickL();
            SendKeys.Send("q");
            SendKeys.Send("a");
            mouseClickL();
            SendKeys.Send("q");
            SendKeys.Send("a");
            mouseClickL();
            SendKeys.Send("q");
            SendKeys.Send("a");
            mouseClickL();
            SendKeys.Send("q");
            SendKeys.Send("a");
            mouseClickL();
            mouseClickR();
            mouseClickL();
        }

        //click left mouse button



        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Hotkeys.Constants.WM_HOTKEY_MSG_ID)
                BuildAOE4Farms();
            base.WndProc(ref m);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ghk.Unregiser())
                MessageBox.Show("Hotkey failed to unregister!");
        }
    }
}
