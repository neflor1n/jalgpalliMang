﻿using System;

namespace jalgpalliMang;

public class Ball
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Vx { get; private set; }
    public double Vy { get; private set; }
    
    private const double Friction = 0.99;
    
    // Размеры поля и ворот
    private const int FieldWidth = 100; // Ширина поля
    private const int FieldHeight = 30; // Высота поля
    private const int GoalHeight = 10; // Высота ворот
    
    private double _vx, _vy;
    private Game _game;

    public Ball()
    {
        // Инициализируем мяч в центре поля
        X = FieldWidth / 2;
        Y = FieldHeight / 2;
        Vx = 0;
        Vy = 0;
    }
    
    // Установка скорости мяча
    public void SetSpeed(double vx, double vy)
    {
        Vx = vx;
        Vy = vy;
    }
    
    public void UpdatePosition()
    {
        // Обновление позиции мяча // Värskenda palli asendit
        X += Vx;
        Y += Vy;

        // Применение трения к скорости // Rakenda kiirusele hõõrdumist
        Vx *= Friction;
        Vy *= Friction;

        // Проверка отскока от штанги ворот // Tagalöögi kontrollimine väravapostist
        // Левая штанга ворот // Vasak väravapost
        if (X <= 1 && (Y < (FieldHeight - GoalHeight) / 2 || Y > (FieldHeight + GoalHeight) / 2))
        {
            Vx = -Vx; // Инвертировать скорость по X для отскока // Pöörake X kiirus põrke jaoks
            X = 1;    // Корректировка позиции мяча // Reguleerige palli asendit
        }
        // Правая штанга ворот // Parem väravapost
        if (X >= FieldWidth - 1 && (Y < (FieldHeight - GoalHeight) / 2 || Y > (FieldHeight + GoalHeight) / 2))
        {
            Vx = -Vx; // Инвертировать скорость по X для отскока // Pöörake X kiirus põrke jaoks
            X = FieldWidth - 1; // Корректировка позиции мяча // Reguleerige palli asendit
        }

        // Отскок мяча от верхней и нижней границ поля // Palli põrgatamine väljaku ülemiselt ja alumiselt piirilt
        // Верхняя граница поля // Välja ülemine piir
        if (Y < 1)
        {
            Vy = -Vy; // Инвертировать скорость по Y для отскока // Pöörake Y kiirus põrke jaoks
            Y = 1;    // Корректировка позиции мяча // Reguleerige palli asendit
        }
        // Нижняя граница поля
        if (Y > FieldHeight - 1)
        {
            Vy = -Vy; // Инвертировать скорость по Y для отскока // Pöörake Y kiirus põrke jaoks
            Y = FieldHeight - 1; // Корректировка позиции мяча // Reguleerige palli asendit
        }

        // Отладочные сообщения для отображения позиции и скорости мяча
        // Silumissõnumid palli asukoha ja kiiruse kuvamiseks
        Console.SetCursorPosition(0, 1);
        Console.WriteLine($"Ball Position: ({X:N2}, {Y:N2}), Speed: ({Vx:N2}, {Vy:N2}) ");
    }

    public void Move()
    {
        X += _vx;
        Y += _vy;

        // Убедимся, что мяч остается внутри стадиона // Veenduge, et pall jääks staadionile
        if (!_game.Stadium.IsIn(X, Y))
        {
            _vx = 0;
            _vy = 0;
        }
    }
}
