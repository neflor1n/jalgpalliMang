using jalgpalliMang;
using System.Threading;
using System;

namespace jalgpalliMang {
    public class Program
    {
        public static void Main(string[] args)
        {
            // Создание нового экземпляра игры
            var game = new Game();

            // Создание двух команд, задание цветов и привязка их к игре
            var teamA = new Team("Team A", ConsoleColor.Red) { Game = game };
            var teamB = new Team("Team B", ConsoleColor.Blue) { Game = game };

            // Добавление команд в список команд игры
            game.Teams.Add(teamA);
            game.Teams.Add(teamB);

            // Задание начальных позиций игроков для всех команд
            teamA.StartGame(100, 30);
            teamB.StartGame(100, 30);

            // Бесконечный игровой цикл
            while (true)
            {
                // Обновление состояния игры (положение мяча и счет)
                game.Update();

                // Обновление позиций игроков на основе положения мяча
                teamA.Move(game.Ball);
                teamB.Move(game.Ball);

                // Отрисовка поля, включая игроков, мяч и ворота
                game.DrawField();

                // Задержка на 500 миллисекунд перед следующим обновлением
                Thread.Sleep(500);
            }
        }
    }
}
