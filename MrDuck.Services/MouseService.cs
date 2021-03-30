

namespace MrDuck.Services
{
    public class MouseService
    {
        // emulate mouse movements
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point point);
        public struct Point
        {
            public int x;
            public int y;
        }

        public static void MoveMouse(int moveX, int moveY)
        {
            Point p = new Point();
            int x = 0, y = 0;

            // get mouse position
            if (GetCursorPos(out p))
            {
                x = p.x;
                y = p.y;
            }

            // move postion 

            SetCursorPos(x + moveX, y + moveY);

        }
    }
}
