﻿using System;
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
        for (int i = 0; i < 10; i++) // 10 полевых игроков - 10 väljakumängijat
        {
            Players.Add(new Player($"Player {i + 1}", color));
        }

        Players.Add(new Player("Goalkeeper", color, true)); // Добавляем вратаря - Väravavahi lisamine
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
            player.Team = this; // Устанавливаем команду игрока - Mängija meeskonna seadistamine
        }
    }

    public void AddPlayer(Player player)
    {
        Players.Add(player);
        player.Team = this;
    }
    
    // Метод обновления позиции каждого игрока - Iga mängija positsiooni värskendamise meetod
    public void Move(Ball ball)
    {
        foreach (var player in Players)
        {
            player.Move(ball);
        }
    }
    
    // Метод получения ближайшего к мячу игрока - Meetod mängija pallile lähemale viimiseks
    public Player GetClosestPlayerToBall(Ball ball)
    {
        Player closestPlayer = null;
        double bestDistance = double.MaxValue;

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
}