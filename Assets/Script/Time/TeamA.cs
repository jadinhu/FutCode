/**
* TimeA.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 31/03/19 (dd/mm/yy)
* Revised on: 28/04/19 (dd/mm/yy)
*/

public class TeamA : Team
{
    public override void Setup()
    {
        name = "AMERICA";
    }

    public override void Play()
    {
        player1.RegisterGoToBall();

    }
}