using jalgpalliMang;
using System;

namespace jalgpalliMang;

public class Player
{
    // Mängija omaduste kirjutamine
    // Nimi 
    public string Name { get; }
    // Координаты позиций
    public double X { get; private set; }
    public double Y { get; private set; }
    // Скорость шага движения
    private double _vx, _vy;
    
    public Team? Team { get; set; } = null;

    // Loome mängija, palli ja selle vahemaa juhusliku kiiruse
    private const double MaxSpeed = 5;
    private const double MaxKickSpeed = 25;
    private const double BallKickDistance = 10;


    private Random _random = new Random();

    // Mängija konstruktori loomine
    public Player(string name)
    {
        Name = name;
    }
    
    // Конструктор позиции, команды игрока 
    public Player(string name, double x, double y, Team team)
    {
        Name = name;
        X = x;
        Y = y;
        Team = team;
    }


    // Loo positsiooni konstruktor
    public void SetPosition(double x, double y)
    {
        X = x;
        Y = y;
    }

    // получаем изначальную позицию
    public (double, double) GetAbsolutePosition()
    {
        return Team!.Game.GetPositionForTeam(Team, X, Y);
    }
    
    // Loo vahemaa konstruktor
    public double GetDistanceToBall()
    {

        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    // Движение игрока к мячу
    public void MoveTowardsBall()
    {

        var ballPosition = Team!.GetBallPosition();
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
        _vx = dx / ratio;
        _vy = dy / ratio;
    }

    // Loo liikuda konstruktor
    public void Move()
    {

        // See kood peatab mängija liikumise, kui ta ei ole oma meeskonnas pallile kõige lähemal.
        if (Team.GetClosestPlayerToBall() != this)
        {
            _vx = 0;
            _vy = 0;
        }

        
        if (GetDistanceToBall() < BallKickDistance)
        {
            Team.SetBallSpeed(
                MaxKickSpeed * _random.NextDouble(),
                MaxKickSpeed * (_random.NextDouble() - 0.5)
                );
        }

        var newX = X + _vx;
        var newY = Y + _vy;
        var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);
        if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2))
        {
            X = newX;
            Y = newY;
        }
        else
        {
            _vx = _vy = 0;
        }
    }
}