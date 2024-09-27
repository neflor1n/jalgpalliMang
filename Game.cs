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
    
    public Ball Ball { get; } = new Ball();
    public Goal GoalA { get; } // Ворота команды A
    public Goal GoalB { get; } // Ворота команды B
    public int ScoreA { get; private set; } // Счет команды A
    public int ScoreB { get; private set; } // Счет команды B
    
    public List<Team> Teams { get; } = new List<Team>();

    public int HomeScore { get; private set; } = 0; // Счет домашней команды
    public int AwayScore { get; private set; } = 0; // Счет выездной команды


    public Game()
    {
        GoalA = new Goal(10, 20, 0, 10);
        GoalB = new Goal(10, 20, 90, 100);

        ScoreA = 0;
        ScoreB = 0;
    }
    // Обновление счета
    public void UpdateScore()
    {
        if (GoalA.IsBallInGoal(Ball.X, Ball.Y))
        {
            ScoreB++;
            ResetBall();
        }
        else if (GoalB.IsBallInGoal(Ball.X, Ball.Y))
        {
            ScoreA++;
            ResetBall();
        }
    }
    // Mängu konstruktori
    public Game(Team homeTeam, Team awayTeam, Stadium stadium)
    {
        HomeTeam = homeTeam;
        AwayTeam = awayTeam;
        Stadium = stadium;
    }

    // Konstruktor mängu algus
    

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
    // Сброс позиции мяча после гола
    private void ResetBall()
    {
        Ball.X = 50;
        Ball.Y = 50;
        Ball.SetSpeed(0, 0); // Остановка мяча
    }
    
    // Отрисовка поля, ворот, игроков и счета
    public void DrawField()
    {
        Console.Clear();

        DrawBoundary(); // Отрисовка границ поля.
        DrawCenterCircle(); // Отрисовка окружности в центре поля.
        DrawGoals(); // Отрисовка ворот.

        foreach (var team in Teams)
        {
            foreach (var player in team.Players)
            {
                var position = player.GetAbsolutePosition();
                SafeSetCursorPosition((int)position.Item1, (int)position.Item2, player.Color);
                Console.Write('P'); // Представление игрока
            }
        }

        SafeSetCursorPosition((int)Ball.X, (int)Ball.Y, ConsoleColor.Yellow);
        Console.Write('B'); // Представление мяча

        SafeSetCursorPosition(0, 0, ConsoleColor.White);
        Console.Write($"Score A: {ScoreA} - Score B: {ScoreB}"); // Вывод текущего счета

        Console.ResetColor();
    }
    
    // Метод рисования ворот
    private void DrawGoals()
    {
        DrawGoal(GoalA, ConsoleColor.Red);
        DrawGoal(GoalB, ConsoleColor.Blue);
    }
    
    // Метод рисования окружности в центре поля
    private void DrawCenterCircle()
    {
        int centerX = 50;
        int centerY = 15;
        int radius = 5;

        for (int i = 0; i < 360; i++)
        {
            double angle = i * Math.PI / 180;
            int x = centerX + (int)(radius * Math.Cos(angle));
            int y = centerY + (int)(radius * Math.Sin(angle));
            Console.SetCursorPosition(x, y);
            Console.Write('O');
        }
    }
    // Метод рисования границ поля
    private void DrawBoundary()
    {
        for (int i = 0; i < 100; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write('-');
            Console.SetCursorPosition(i, 29);
            Console.Write('-');
        }

        for (int i = 0; i < 30; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write('|');
            Console.SetCursorPosition(99, i);
            Console.Write('|');
        }
    }
    
    // Метод рисования одной цели (ворот)
    private void DrawGoal(Goal goal, ConsoleColor color)
    {
        Console.ForegroundColor = color;

        for (int y = (int)goal.Top; y <= (int)goal.Bottom; y++)
        {
            for (int x = (int)goal.Left; x <= (int)goal.Right; x++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write('G');
            }
        }
        Console.ResetColor();
    }
    public void DisplayScore()
    {
        Console.SetCursorPosition(0, Stadium.Height + 1); // Позиция для отображения счета
        Console.WriteLine($"Score: Home {HomeScore} - Away {AwayScore}");
    }
    
    public void Update()
    {
        Ball.UpdatePosition(); // Обновление позиции мяча
        UpdateScore(); // Обновление счета
    }
    // Безопасная установка позиции курсора и цвета текста.
    private void SafeSetCursorPosition(int left, int top, ConsoleColor color)
    {
        if (left >= 0 && left < Console.WindowWidth && top >= 0 && top < Console.WindowHeight)
        {
            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = color;
        }
    }
}