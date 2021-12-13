/**
* Ball.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 24/04/19 (dd/mm/yy)
* Revised on: 19/10/19 (dd/mm/yy)
*/
using UnityEngine;

public class Ball
{
    private MonoBall ball;

    public Ball(MonoBall ball)
    {
        this.ball = ball;
    }

    /// <summary>
    /// Retorna a posi��o X e Y da bola
    /// </summary>
    public Vector2 GetPosition()
    {
        return new Vector2(ball.transform.position.x, ball.transform.position.z);
    }

    /// <summary>
    /// Retorna a dist�ncia (float) entre a bola e a defesa de um time (a defesa � X:-10/Y:0 ou X:10/Y:0)
    /// </summary>
    /// <param name="team">time em quest�o</param>
    public float DistanceOfDefense(Team team)
    {
        if (team.sideLeft)
            return Vector2.Distance(GetPosition(), Match.Instance.LeftGolPoint());
        else
            return Vector2.Distance(GetPosition(), Match.Instance.RightGolPoint());
    }

    /// <summary>
    /// Retorna TRUE se a bola est� entre o jogador e a defesa advers�ria (a defesa � X:-10/Y:0 ou X:10/Y:0)
    /// </summary>
    /// <param name="player">jogador em quest�o</param>
    public bool IsBallForwardOfPlayer(Player player)
    {
        if (player.GetTeam().sideLeft)
        {
            if (Vector2.Distance(GetPosition(), Match.Instance.RightGolPoint()) <
                Vector2.Distance(player.GetPosition(), Match.Instance.RightGolPoint()))
                return true;
            else
                return false;
        }
        else
        {
            if (Vector2.Distance(GetPosition(), Match.Instance.LeftGolPoint()) <
                Vector2.Distance(player.GetPosition(), Match.Instance.LeftGolPoint()))
                return true;
            else
                return false;
        }
    }
}