using jalgpalliMang;

public class Program
{
    public static void Main(string[] args)
    {
        Console.SetWindowSize(200, 50);
        Stadium stadium = new Stadium(80, 25);

        // Создаем команды
        Game game = new Game(new Team("Home", ConsoleColor.Yellow), new Team("Away", ConsoleColor.Red), stadium);

        // Добавляем игроков (10 полевых + 1 вратарь для каждой команды)
        for (int i = 1; i <= 10; i++)
        {
            game.HomeTeam.AddPlayer(new Player($"Home Player {i}"));
            game.AwayTeam.AddPlayer(new Player($"Away Player {i}"));
        }

        // Добавляем вратарей
        game.HomeTeam.AddPlayer(new Player("Home Goalkeeper", true));
        game.AwayTeam.AddPlayer(new Player("Away Goalkeeper", true));

        // Начинаем игру
        game.Start();

        // Игровой цикл
        while (true)
        {
            game.Move(); // Двигаем команды и мяч
            stadium.Draw(game.HomeTeam.Players.Concat(game.AwayTeam.Players).ToList(), game.Ball); // Рисуем стадион
            game.DisplayScore(); // Отображаем счет
            System.Threading.Thread.Sleep(100); // Контроль скорости игрового цикла
        }
    }
}
