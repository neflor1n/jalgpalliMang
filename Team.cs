using System;
using System.Collections.Generic;

namespace jalgpalliMang;

public class Team
{
    public List<Player> Players { get; } = new List<Player>();
    public string Name { get; }
    public ConsoleColor Color { get; }
    public Game Game { get; set; }
    public Team(string name, ConsoleColor color)
    {
        Name = name;
        Color = color;
    }
    public (double, double) GetBallPosition()
    {
        return Game?.Ball != null ? (Game.Ball.X, Game.Ball.Y) : (0.0, 0.0);
    }
    public void StartGame(int width, int height)
    {
        Random rnd = new Random();
        foreach (var player in Players)
        {
            player.SetPosition(rnd.Next(1, width - 1), rnd.Next(1, height - 1));
        }
    }

    public void AddPlayer(Player player)
    {
        Players.Add(player);
        player.Team = this;
    }

    public void Move(Ball ball)
    {
        foreach (var player in Players)
        {
            player.Move(ball);
        }
    }
    public Player GetClosestPlayerToBall(Ball ball)
    {
        Player closestPlayer = null;
        double bestDistance = Double.MaxValue;

        foreach (var player in Players)
        {
            var distance = player.GetDistanceToBall(ball);
            if (distance < bestDistance)
            {
                closestPlayer = player;
                bestDistance = distance;
            }
        }

        return closestPlayer;
    }
    public double GetDistanceToBall()
    {
        var ballPosition = Team!.GetBallPosition(); // Предполагается, что метод GetBallPosition возвращает позицию мяча
        var dx = ballPosition.Item1 - X;
        var dy = ballPosition.Item2 - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    public double GetDistanceToBall(Ball ball)
    {
        double dx = ball.X - X;
        double dy = ball.Y - Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
