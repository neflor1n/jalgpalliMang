namespace jalgpalliMang;

public class Goal
{
    // Координаты топа и высоты ворот
    public double Top { get; }
    public double Bottom { get; }
    // Ширина ворот
    public double Left { get; }
    public double Right { get; }

    public Goal(double top, double bottom, double left, double right)
    {
        Top = top;
        Bottom = bottom;
        Left = left;
        Right = right;
    }

    // Проверка, находится ли мяч в воротах
    public bool IsBallInGoal(double ballX, double ballY)
    {
        return ballX >= Left && ballX <= Right && ballY >= Top && ballY <= Bottom;
    }
    
}