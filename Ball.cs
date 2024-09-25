using jalgpalliMang;

namespace jalgpalliMang;

public class Ball
{
    public double X { get; private set; }
    public double Y { get; private set; }

    private double _vx, _vy;

    private Game _game;

    public Ball(double x, double y, Game game)
    {
        _game = game;
        X = x;
        Y = y;
    }

    public void SetSpeed(double vx, double vy)
    {
        _vx = vx;
        _vy = vy;
    }

    public void Move()
    {
        double newX = X + _vx;
        double newY = Y + _vy;
        if (_game.Stadium.IsIn(newX, newY))
        {
            X = newX;
            Y = newY;
        }
        else
        {
            _vx = 0;
            _vy = 0;
        }
    }

}