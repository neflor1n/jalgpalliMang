using jalgpalliMang;

namespace jalgpalliMang;

public class Game
{
    // kodumeeskond omaduste
    public Team HomeTeam { get; }
    // võõrsil meeskond omaduste
    public Team AwayTeam { get; }
    // stadiomi omaduste
    public Stadium Stadium { get; }
    // Palli omaduste
    public Ball Ball { get; private set; }

    public int HomeScore { get; private set; } = 0; // Счет домашней команды
    public int AwayScore { get; private set; } = 0; // Счет выездной команды



    // Mängu konstruktori
    public Game(Team homeTeam, Team awayTeam, Stadium stadium)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        Stadium = stadium;
    }

    // Konstruktor mängu algus
    public void Start()
    {
        // Создаем новый объект мяча, устанавливая его в центр стадиона
        Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
        // Запускаем игру для домашней команды, передавая координаты стартовой позиции
        HomeTeam.StartGame(Stadium.Width / 2, Stadium.Height);
        // Запускаем игру для выездной команды, передавая координаты стартовой позиции
        AwayTeam.StartGame(Stadium.Width / 2, Stadium.Height);
    }

    // Meeskonna positsiooni määramine
    private (double, double) GetPositionForAwayTeam(double x, double y)
    {
        return (Stadium.Width - x, Stadium.Height - y);
    }

    // установка позиций команд
    public (double, double) GetPositionForTeam(Team team, double x, double y)
    {
        return team == HomeTeam ? (x, y) : GetPositionForAwayTeam(x, y);
    }
    // Устанавливаем позицию мяча для команд
    public (double, double) GetBallPositionForTeam(Team team)
    {
        // Возвращаем позиции по осям, и мяча
        return GetPositionForTeam(team, Ball.X, Ball.Y);
    }

    // Установка скорости мяча для команд 
    public void SetBallSpeedForTeam(Team team, double vx, double vy)
    {
        // если домашняя команда, то она вызывает скорость у мяча по оси Х и У
        if (team == HomeTeam)
        {
            Ball.SetSpeed(vx, vy);
        }
        // Это означает, что скорость мяча устанавливается в противоположном направлении:
        else
        {
            Ball.SetSpeed(-vx, -vy);
        }
    }
    // Движение команд и мяча
    public void Move()
    {
        HomeTeam.Move(Ball); // Передаем мяч в метод движения команды
        AwayTeam.Move(Ball); // Передаем мяч в метод движения команды
        Ball.Move();

        CheckGoals();
    }
    private void CheckGoals()
    {
        // Проверка, забит ли гол
        if (Ball.X <= 0) // Если мяч зашел в ворота домашней команды
        {
            AwayScore++;
            ResetBall();
        }
        else if (Ball.X >= Stadium.Width - 1) // Если мяч зашел в ворота выездной команды
        {
            HomeScore++;
            ResetBall();
        }
    }
    private void ResetBall()
    {
        Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this); // Сбрасываем мяч в центр
    }

    public void DisplayScore()
    {
        Console.SetCursorPosition(0, Stadium.Height + 1); // Позиция для отображения счета
        Console.WriteLine($"Score: Home {HomeScore} - Away {AwayScore}");
    }
}