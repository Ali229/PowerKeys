using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Hotkeys
{
    public class GlobalHotkey
    {
        private readonly int modifier;
        private readonly int key;
        private readonly IntPtr hWnd;
        private readonly Keys id;

        public GlobalHotkey(int modifier, Keys key, Form form)
        {
            this.modifier = modifier;
            this.key = (int)key;
            hWnd = form.Handle;
            id = key;
        }

        public GlobalHotkey(Keys key, Form form)
        {
            this.key = (int)key;
            hWnd = form.Handle;
            id = key;
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, id, modifier, key);
        }

        public bool Unregister()
        {
            return UnregisterHotKey(hWnd, id);
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, Keys id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, Keys id);
    }
}
