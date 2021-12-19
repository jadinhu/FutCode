/**
* TeamB.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 31/03/19 (dd/mm/yy)
* Revised on: 18/12/21 (dd/mm/yy)
*/

/// <summary>
/// Simple Test
/// </summary>
public class TeamB : Team
{
    public override void Setup()
    {
        name = "TIME B";
    }

    public override void Play()
    {
        player1.RegisterGoToBall();
        player2.RegisterMarkOpponent(3);
        player3.RegisterStayQuiet();
    }
}