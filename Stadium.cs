using System;
using System.Collections.Generic;
using System.Linq;

namespace jalgpalliMang;

public class Stadium
{
    public int Width { get; }
    public int Height { get; }

    public Stadium(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public bool IsIn(double x, double y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    public void Draw(List<Player> players, Ball ball)
    {
        Console.Clear(); // Очищаем консоль перед отрисовкой - Konsooli tühjendamine enne renderdamist

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                if (IsIn(x, y))
                {
                    // Рисуем игроков
                    var player = players.FirstOrDefault(p => (int)p.X == x && (int)p.Y == y);
                    if (player != null)
                    {
                        Console.ForegroundColor = player.Color;
                        Console.Write("P"); // Отображаем игрока - Mängija kuvamine
                    }
                    else if ((int)ball.X == x && (int)ball.Y == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("B"); // Отображаем мяч - Palli näitamine
                    }
                    else
                    {
                        Console.Write(" "); // Пустое пространство - tühi ruum
                    }
                }
            }
            Console.WriteLine(); // Переход на новую строку - Uus rida
        }
    }

    private bool IsGoal(int x, int y)
    {
        return (x == 1 && y >= Height / 2 - 1 && y <= Height / 2 + 1) ||
               (x == Width - 2 && y >= Height / 2 - 1 && y <= Height / 2 + 1);
    }

    private bool IsCenterCircle(int x, int y)
    {
        int centerX = Width / 2;
        int centerY = Height / 2;
        int radius = 4; // Радиус центрального круга - Keskringi raadius
        return (x - centerX) * (x - centerX) + (y - centerY) * (y - centerY) <= radius * radius;
    }
}
