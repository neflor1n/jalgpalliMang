using jalgpalliMang;

namespace jalgpalliMang;

public class Game
{
    public Team HomeTeam { get; }
    public Team AwayTeam { get; }
    public Stadium Stadium { get; }
    public Ball Ball { get; private set; }

    public Game(Team homeTeam, Team awayTeam, Stadium stadium)
    {
        HomeTeam = homeTeam;
        homeTeam.Game = this;
        AwayTeam = awayTeam;
        awayTeam.Game = this;
        Stadium = stadium;
    }

    public void Start()
    {
        Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
        HomeTeam.StartGame(Stadium.Width / 2, Stadium.Height);
        AwayTeam.StartGame(Stadium.Width / 2, Stadium.Height);
    }
    private (double, double) GetPositionForAwayTeam(double x, double y)
    {
        return (Stadium.Width - x, Stadium.Height - y);
    }

    public (double, double) GetPositionForTeam(Team team, double x, double y)
    {
        return team == HomeTeam ? (x, y) : GetPositionForAwayTeam(x, y);
    }

    public (double, double) GetBallPositionForTeam(Team team)
    {
        return GetPositionForTeam(team, Ball.X, Ball.Y);
    }

    public void SetBallSpeedForTeam(Team team, double vx, double vy)
    {
        if (team == HomeTeam)
        {
            Ball.SetSpeed(vx, vy);
        }
        else
        {
            Ball.SetSpeed(-vx, -vy);
        }
    }

    public void Move()
    {
        HomeTeam.Move();
        AwayTeam.Move();
        Ball.Move();
    }
}