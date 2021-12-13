/**
* Time.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 31/03/19 (dd/mm/yy)
* Revised on: 19/10/19 (dd/mm/yy)
*/

/// <summary>
/// This classe cannot be extended or implements anything. You cannot use Monobehavior, as well.
/// All your team code must be in this class.
/// Here, you can create functions, variables and all code strategy that you want.
/// </summary>
public abstract class Team
{
    public Ball ball;
    public Match match;
    public Player player1;
    public Player player2;
    public Player player3;
    public string name;
    public bool sideLeft;

    public abstract void Setup();
    public abstract void Play();

}