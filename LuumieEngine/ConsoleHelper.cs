using System.Runtime.InteropServices;
using LuumieEngine.Structs;
using static System.Console;

namespace LuumieEngine;

public static partial class ConsoleHelper
{
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWind, int nCmdShow);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    public static void Maximize()
    {
        const int swMaximize = 3;
        ShowWindow(GetConsoleWindow(), swMaximize);
    }

    private const int StdOutputH = -11;

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    private static readonly IntPtr ConsoleOutputH = GetStdHandle(StdOutputH);
    
    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, FontInfo lpConsoleCurrentFontEx);
    
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct FontInfo
    {
        internal int cbSize;
        internal int FontIndex;
        internal short FontWidth;
        public short FontSize;
        public int FontFamily;
        public int FontWeight;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FontName;
    }

    public static void SetCurrentFont(string font = "Consolas", short fontSize = 16)
    {
        var info = new FontInfo
        {
            cbSize = Marshal.SizeOf<FontInfo>(),
            FontIndex = 0,
            FontFamily = 54,
            FontName = font,
            FontWeight = 400,
            FontSize = fontSize
        };

        SetCurrentConsoleFontEx(ConsoleOutputH, false, info);
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteConsoleOutput(
        IntPtr hConsoleOutput,
        CharInfo[] lpBuffer,
        Coord dwBufferSize,
        Coord dwBufferCoord,
        ref SmallRect lpWriteRegion
        );
    
    [StructLayout(LayoutKind.Sequential)]
    public struct CharInfo
    {
        public char Char;
        public short Attributes;

        public static CharInfo Default => new() { Char = ' ', Attributes = 15 };
    }
    
    [StructLayout(LayoutKind.Sequential)]
    private struct Coord
    {
        public short X;
        public short Y;

        public Coord(short x, short y)
        {
            X = x;
            Y = y;
        }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    private struct SmallRect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;

        public SmallRect(short left, short top, short right, short bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    // https://learn.microsoft.com/en-us/windows/console/writeconsoleoutput
    public static void WriteToBuffer(CharInfo[] buffer, short width, short height)
    {
        var rect = new SmallRect(0, 0, width, height);
        
        WriteConsoleOutput(
            ConsoleOutputH,
            buffer,
            new Coord(width, height),
            new Coord(),
            ref rect
            );
    }
}