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
    public Team? Team { get; set; } = null;
    public bool IsGoalkeeper { get; }
    public ConsoleColor Color { get; }


    // Loome mängija, palli ja selle vahemaa juhusliku kiiruse
    private const double MaxSpeed = 5;
    private const double MaxKickSpeed = 25;
    private const double BallKickDistance = 10; // Расстояние, с которого игрок может ударить мяч
    private Random _random = new Random();

    // Mängija konstruktori loomine
    public Player(string name)
    {
        Name = name;
    }
    
    // Конструктор позиции, команды игрока - Positsiooni konstrueerija, mängija käsud
    public Player(string name, ConsoleColor teamColor, bool isGoalkeeper = false)
    {
        Name = name;
        IsGoalkeeper = isGoalkeeper;
        Color = teamColor;
        
    }


    // Loo positsiooni konstruktor
    public void SetPosition(double x, double y)
    {
        X = x;
        Y = y;
    }

    // получаем изначальную позицию - saame algse positsiooni
    public (double, double) GetAbsolutePosition()
    {
        return (X, Y);
    }



    // Движение игрока к мячу - Mängija liikumine palli suunas
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

            // Отладочные сообщения - Silumissõnumid
            Console.SetCursorPosition(0, 3);
            Console.WriteLine($"{Name} движется к мячу. Позиция игрока: ({X:N2}, {Y:N2})");
        }
    }

    // Loo liikuda konstruktor
    public void Move(Ball ball)
    {

        // Если это не ближайший игрок к мячу, то он не движется - Kui see pole pallile lähim mängija, siis see ei liigu
        if (Team != null && Team.GetClosestPlayerToBall(ball) != this)
        {
            return;
        }

        MoveTowardsBall(ball);

        // Если игрок достаточно близко к мячу, он его ударяет - Kui mängija on pallile piisavalt lähedal, lööb ta seda
        if (GetDistanceToBall(ball) < BallKickDistance && (ball.Vx == 0 && ball.Vy == 0))
        {
            KickBall(ball);
        }

    }
    // Метод получения расстояния до мяча - Palli kauguse saamise meetod
    public double GetDistanceToBall(Ball ball)
    {
        double dx = ball.X - X;
        double dy = ball.Y - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
    
    // Метод пина мяча - Pallilöögi meetod
    private void KickBall(Ball ball)
    {
        // Установка случайной скорости мяча при ударе - Juhusliku palli kiiruse seadistamine kokkupõrkel
        double kickX = (_random.NextDouble() - 0.5) * MaxKickSpeed;
        double kickY = (_random.NextDouble() - 0.5) * MaxKickSpeed;
        ball.SetSpeed(kickX, kickY);

        // Отладочные сообщения - Silumissõnumid
        Console.SetCursorPosition(0, 2);
        Console.Write($"Удар от {Name}: Скорость мяча ({kickX:N2}, {kickY:N2})");
    }
}