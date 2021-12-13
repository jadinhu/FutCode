/**
* Player.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 24/04/19 (dd/mm/yy)
* Revised on: 19/10/19 (dd/mm/yy)
*/
using UnityEngine;

public class Player
{
    private MonoPlayer player;
    private Team team;

    public Player(MonoPlayer player, Team team)
    {
        this.player = player;
        this.team = team;
    }

    /// <summary>
    /// Retorna o <see cref="Team"/> o qual este jogador defende
    /// </summary>
    public Team GetTeam()
    {
        return team;
    }

    /// <summary>
    /// N�o faz nada (fica parado)
    /// </summary>
    public void RegisterStayQuiet()
    {
        player.RegisterStayQuiet();
    }

    /// <summary>
    /// Corre em dire��o � bola se chocando com ela
    /// </summary>
    public void RegisterGoToBall()
    {
        player.RegisterGoToBall();
    }

    /// <summary>
    /// Corre em dire��o � defesa advers�ria
    /// </summary>
    public void RegisterGoToGol()
    {
        player.RegisterGoToGol();
    }

    /// <summary>
    /// Corre em dire��o � pr�pria defesa
    /// </summary>
    public void RegisterGoToDefense()
    {
        player.RegisterGoToDefense();
    }

    /// <summary>
    /// O jogador se move em uma determinada dire��o (<see cref="Direction"/>)
    /// </summary>
    /// <param name="direction"></param>
    public void RegisterMoveToPoint(Direction direction)
    {
        player.RegisterMoveToPoint(direction);
    }

    /// <summary>
    /// Vai em dire��o a um jogador advers�rio espec�fico (pelo n�mero) e fica se chocando com ele
    /// </summary>
    /// <param name="enemyNumber">n�mero do jogador advers�rio a ser marcado</param>
    public void RegisterMarkOpponent(int enemyNumber)
    {
        player.RegisterMarkOpponent(enemyNumber);
    }

    /// <summary>
    /// Retorna a posi��o X e Y do jogador
    /// </summary>
    public Vector2 GetPosition()
    {
        return new Vector2(player.transform.position.x, player.transform.position.z);
    }

    /// <summary>
    /// retorna a dist�ncia entre este jogador e a bola
    /// </summary>
    public float DistanceOfBall()
    {
        return Vector2.Distance(GetPosition(), Match.Instance.Ball.GetPosition());
    }

    /// <summary>
    /// Retorna a posi��o da defesa do time desse jogador
    /// </summary>
    public Vector2 GetDefenseGolPoint()
    {
        if (player.Team.sideLeft)
            return Match.Instance.LeftGolPoint();
        else
            return Match.Instance.RightGolPoint();
    }

    /// <summary>
    /// Retorna a dist�ncia entre o jogador e a defesa do time dele
    /// </summary>
    public float DistanceOfDefenseGolPoint()
    {
        return Vector2.Distance(GetDefenseGolPoint(), GetPosition());
    }
}