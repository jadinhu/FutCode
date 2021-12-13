/**
* Match.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 24/04/19 (dd/mm/yy)
* Revised on: 19/10/19 (dd/mm/yy)
*/
using UnityEngine;

public class Match
{
    private static Match instance;
    private MonoMatch match;
    public Ball Ball { get; private set; }
    private Vector2 leftGolPoint = new Vector2(-10, 0);
    private Vector2 rightGolPoint = new Vector2(10, 0);

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