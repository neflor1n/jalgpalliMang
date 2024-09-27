using jalgpalliMang;
using System.Threading;
using System;

namespace jalgpalliMang {
    public class Program
    {
        public static void Main(string[] args)
        {
            // Создание нового экземпляра игры - Uue mängu eksemplari loomine
            var game = new Game();

            // Создание двух команд, задание цветов и привязка их к игре - Kahe meeskonna loomine, värvide määramine ja nende sidumine mänguga
            var teamA = new Team("Team A", ConsoleColor.Red) { Game = game };
            var teamB = new Team("Team B", ConsoleColor.Blue) { Game = game };

            // Добавление команд в список команд игры - Käskude lisamine mängukäskude loendisse
            game.Teams.Add(teamA);
            game.Teams.Add(teamB);

            // Задание начальных позиций игроков для всех команд - Kõigi võistkondade algmängijate positsioonide määramine
            teamA.StartGame(100, 30);
            teamB.StartGame(100, 30);

            // Бесконечный игровой цикл - Lõputu mänguring
            while (true)
            {
                // Обновление состояния игры (положение мяча и счет) - Mängu seisu värskendus (palli asukoht ja skoor)
                game.Update();

                // Обновление позиций игроков на основе положения мяча - Mängijate positsioonide värskendamine pallipositsiooni alusel
                teamA.Move(game.Ball);
                teamB.Move(game.Ball);

                // Отрисовка поля, включая игроков, мяч и ворота - Väljaku renderdamine, sealhulgas mängijad, pall ja värav
                game.DrawField();

                // Задержка на 500 миллисекунд перед следующим обновлением - Enne järgmist värskendust viivitage 500 millisekundit
                Thread.Sleep(500);
            }
        }
    }
}
