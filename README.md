# MrDuck

![MrDuck](https://github.com/MoonMan42/MrDuck/blob/MrDuckAndGifs/MrDuck.Wpf/Images/Gifs/Idle.gif?raw=true)

Light program used to keep your computer up and running. 

Belowe is the code for keeping the computer up and running and added ontop are a set of images/gifs and sound effects.

```
 public class PowerHelper
    {
        // keep program running and system up.
        public static void ForceSystemAwake()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.EXECUTION_STATE.ES_CONTINUOUS |
                                                  NativeMethods.EXECUTION_STATE.ES_DISPLAY_REQUIRED |
                                                  NativeMethods.EXECUTION_STATE.ES_SYSTEM_REQUIRED |
                                                  NativeMethods.EXECUTION_STATE.ES_AWAYMODE_REQUIRED);
        }

        // call when program is closed. 
        public static void ResetSystemDefault()
        {
            NativeMethods.SetThreadExecutionState(NativeMethods.EXECUTION_STATE.ES_CONTINUOUS);
        }
    }

    internal static partial class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern EXECUTION_STATE SetThreadExecutionState(EXECUTION_STATE esFlags);

        [FlagsAttribute]
        public enum EXECUTION_STATE : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001

        }
    }
```

credit for the images goes to [here](https://opengameart.org/content/character-spritesheet-duck)
