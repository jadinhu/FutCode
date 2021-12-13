/**
* Match.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 24/04/19 (dd/mm/yy)
* Revised on: 13/12/21 (dd/mm/yy)
*/
using UnityEngine;

/// <summary>
/// Futcode API for <see cref="MonoMatch"/>. Handles every method that programming student can use in him team
/// </summary>
public class Match
{
    /// <summary>
    /// The ball used in this match
    /// </summary>
    public Ball Ball { get; private set; }
    /// <summary>
    /// The Singleton instance of this class 
    /// </summary>
    static Match instance;
    /// <summary>
    /// The <see cref="MonoBehaviour"/> of the match with not accessible code
    /// </summary>
    MonoMatch match;
    /// <summary>
    /// The center point of the left-side gol
    /// </summary>
    Vector2 leftGolPoint = new Vector2(-10, 0);
    /// <summary>
    /// The center point of the right-side gol
    /// </summary>
    Vector2 rightGolPoint = new Vector2(10, 0);

    /// <summary>
    /// Returns the Singleton <see cref="instance"/>
    /// </summary>
    public static Match Instance { get { return instance; } private set { } }

    public Match(MonoMatch match, Ball ball)
    {
        instance = this;
        this.match = match;
        Ball = ball;
    }

    /// <summary>
    /// Retorna a posição de um jogador adversário (pelo número dele)
    /// </summary>
    /// <param name="yourTeam">seu time</param>
    /// <param name="enemyNumber">o número do jogador adversário</param>
    /// <returns></returns>
    public Vector2 GetEnemyPlayerPosition(Team yourTeam, int enemyNumber)
    {
        return match.GetEnemyPlayerPosition(yourTeam, enemyNumber);
    }

    /// <summary>
    /// Retorna o ponto central do Gol esquerdo (X e Y)
    /// </summary>
    public Vector2 LeftGolPoint()
    {
        return leftGolPoint;
    }

    /// <summary>
    /// Retorna o ponto central do Gol direito (X e Y)
    /// </summary>
    public Vector2 RightGolPoint()
    {
        return rightGolPoint;
    }
}