using System.Numerics;
using LuumieEngine.Classes;
using LuumieEngine.Structs;

namespace LuumieEngine.Components;

// No diagonal movement currently supported
public class Movement : Routine
{
    private bool _moving;
    private Vector2 _exactLocation;

    private Vector2Int _ogLocation;
    private Vector2Int _destination;
    private Vector2 _vSpeed;

    protected override void Update()
    {
        if (!_moving) return;
        
        _exactLocation += _vSpeed;
        GameEntity.Transform.Position = _exactLocation.ToVector2Int();
        
        if ((_destination - _ogLocation).SqrMagnitude() > (GameEntity.Transform.Position - _ogLocation).SqrMagnitude()) return;
        
        GameEntity.Transform.Position = _destination;
        _moving = false;
    }
    
    public void MoveTo(Vector2Int position, float speed = 1f)
    {
        _ogLocation = GameEntity.Transform.Position;
        _exactLocation = _ogLocation.ToVector2();
        _destination = position;

        var offset = _destination - _ogLocation;
        var m = Math.Abs(offset.Y) + Math.Abs(offset.X);

        if (m == 0) return;
        
        _vSpeed = new Vector2(speed * offset.X / m, speed * offset.Y / m);
        
        _moving = true;
    }

    public void Offset(Vector2Int offset, float speed = 1f)
    {
        MoveTo((_moving ? _destination : GameEntity.Transform.Position) + offset, speed);
    }
}