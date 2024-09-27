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
    public bool IsGoalkeeper { get; }
    public ConsoleColor Color { get; }

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
    public Player(string name, bool isGoalkeeper = false)
    {
        Name = name;
        IsGoalkeeper = isGoalkeeper;
        Color = isGoalkeeper ? ConsoleColor.Black : ConsoleColor.White; //
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
        return Team?.Game.GetPositionForTeam(Team, X, Y) ?? (X, Y);
    }

    // Loo vahemaa konstruktor
    public double GetDistanceToBall()
    {
        var ballPosition = Team?.GetBallPosition() ?? (0.0, 0.0);
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }


    // Движение игрока к мячу
    public void MoveTowardsBall(Ball ball)
    {
        double dx = ball.X - X;
        double dy = ball.Y - Y;
        double distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance > 0)
        {
            double moveX = (dx / distance) * MaxSpeed;
            double moveY = (dy / distance) * MaxSpeed;

            X += moveX;
            Y += moveY;
        }
    }

    // Loo liikuda konstruktor
    public void Move(Ball ball)
    {

        // Проверяем, является ли игрок ближайшим к мячу
        if (Team.GetClosestPlayerToBall(ball) != this)
        {
            return; // Не делаем ничего, если не самый близкий
        }

        // Двигаемся к мячу
        MoveTowardsBall(ball);

        // Если игрок в пределах расстояния для удара, бьем по мячу
        if (GetDistanceToBall(ball) < BallKickDistance)
        {
            KickBall(ball);
        }

    }
    private double GetDistanceToBall(Ball ball)
    {
        double dx = ball.X - X;
        double dy = ball.Y - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    private void KickBall(Ball ball)
    {
        double kickX = (new Random().NextDouble() - 0.5) * MaxKickSpeed;
        double kickY = (new Random().NextDouble() - 0.5) * MaxKickSpeed;
        ball.SetSpeed(kickX, kickY);
    }
}