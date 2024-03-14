using LuumieEngine.Classes;
using LuumieEngine.Structs;
using CharInfo = LuumieEngine.ConsoleHelper.CharInfo;

namespace LuumieEngine.ScreenRendering;

public class ScreenBuffer
{
    private CharInfo[] _buffer = Array.Empty<CharInfo>();
    private short _width;
    private short _height;

    private readonly PriorityQueue<RenderRoutine, int> _renderQueue = new();

    public void ChangeBufferSize(int width, int height)
    {
        _width = (short)width;
        _height = (short)height;
        
        _buffer = new CharInfo[height * width];
        Array.Fill(_buffer, CharInfo.Default);
    }

    public void Write(Vector2Int root, string s, ConsoleColor color)
    {
        var top = root.Y;
        if (top < 0 || top >= _height) return; // Out of frame

        var row = top * _width;
        var left = root.X - 1;
        foreach (var c in s)
        {
            left++;
            if (left < 0 || left >= _width) continue; // Out of frame
            ref var info = ref _buffer[row + left];
            info.Char = c;
            info.Attributes = (short)color;
        }
    }

    public void Background(Vector2Int root, Vector2Int size, ConsoleColor color)
    {
        var top = root.Y - 1;
        var left = root.X - 1;
        
        for (var i = 0; i < size.Y; i++)
        {
            top++;
            if (top < 0 || top >= _height) continue; // Out of frame
            
            
            for (var j = 0; j < size.X; j++)
            {
                left++;
                if (left < 0 || left >= _width) continue; // Out of frame
                ref var info = ref _buffer[top * _width + left];
                info.Char = ' ';
                info.Attributes += (short)((int)color << 4);
            }

            left = root.X - 1;
        }
    }

    public void QueueJob(RenderRoutine r)
    {
        _renderQueue.Enqueue(r, r.Transform.Depth);
    }
    
    public void Render_Extern()
    {
        while (_renderQueue.Count > 0)
        {
            _renderQueue.Dequeue().Render();
        }
        
        ConsoleHelper.WriteToBuffer(_buffer, _width, _height);
        
        Array.Fill(_buffer, CharInfo.Default);
    }
}