﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Hotkeys
{
    public class GlobalHotkey
    {
        private readonly int modifier;
        private readonly int key;
        private readonly IntPtr hWnd;
        private readonly int id;

        public GlobalHotkey(int modifier, Keys key, Form form)
        {
            this.modifier = modifier;
            this.key = (int)key;
            hWnd = form.Handle;
            id = GetHashCode();
        }

        public GlobalHotkey(Keys key, Form form)
        {
            this.key = (int)key;
            hWnd = form.Handle;
            id = GetHashCode();
        }

        public bool Register()
        {
            return RegisterHotKey(hWnd, id, modifier, key);
        }

        public bool Unregiser()
        {
            return UnregisterHotKey(hWnd, id);
        }

        public override int GetHashCode()
        {
            return modifier ^ key ^ hWnd.ToInt32();
        }

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
