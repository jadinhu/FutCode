/**
* TeamB.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 31/03/19 (dd/mm/yy)
* Revised on: 28/04/19 (dd/mm/yy)
*/

public class TeamB : Team
{
    public override void Setup()
    {
        name = "TIME B";
    }

    public override void Play()
    {
        player1.RegisterMoveToPoint(Direction.NorthEast);
        player2.RegisterMarkOpponent(3);
        player3.RegisterStayQuiet();
    }
}