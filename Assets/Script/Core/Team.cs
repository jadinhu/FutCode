/**
* Time.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 31/03/19 (dd/mm/yy)
* Revised on: 13/12/21 (dd/mm/yy)
*/

/// <summary>
/// This classe cannot be extended or implements anything. You cannot use Monobehavior, as well.
/// All your team code must be in a template of this class.
/// Here, you can create functions, variables and all code strategy that you want.
/// </summary>
public abstract class Team
{
    /// <summary>
    /// The ball of the match
    /// </summary>
    public Ball ball;
    /// <summary>
    /// The current match
    /// </summary>
    public Match match;
    /// <summary>
    /// The player number 1 of yout team
    /// </summary>
    public Player player1;
    /// <summary>
    /// The player number 2 of yout team
    /// </summary>
    public Player player2;
    /// <summary>
    /// The player number 3 of yout team
    /// </summary>
    public Player player3;
    /// <summary>
    /// Your team's name
    /// </summary>
    public string name;
    /// <summary>
    /// If your team have started in left-side of stadium
    /// </summary>
    public bool sideLeft;

    /// <summary>
    /// Works like a constructor for you setup your team (set the <see cref="name"/> and something else), 
    /// called when the match begins
    /// </summary>
    public abstract void Setup();

    /// <summary>
    /// Here you must put all your team behavior algorithm. Is called in every <see cref="MonoMatch.FixedUpdate"/>
    /// </summary>
    public abstract void Play();

}