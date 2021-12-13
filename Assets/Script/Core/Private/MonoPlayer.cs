/**
* MonoPlayer.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 31/03/19 (dd/mm/yy)
* Revised on: 13/12/21 (dd/mm/yy)
*/
using UnityEngine;

/// <summary>
/// List of possible actions for <see cref="MonoPlayer"/>
/// </summary>
public enum Action { StayQuiet, GoToBall, GoToGol, GoToDefense, MoveToPoint, MarkOpponent }

/// <summary>
/// List of 8 directions that <see cref="MonoPlayer"/> can move
/// </summary>
public enum Direction { North, South, East, West, NorthEast, NorthWest, SouthEast, SouthWest }

/// <summary>
/// Handles the core behavior of a <see cref="Player"/> with all inacessible behavior
/// </summary>
public class MonoPlayer : MonoBehaviour
{
    /// <summary>
    /// The player's speed to move
    /// </summary>
    [SerializeField]
    float speed = 1;
    /// <summary>
    /// If the player are runnig
    /// </summary>
    public bool running;
    /// <summary>
    /// The team of this player
    /// </summary>
    public Team team;
    /// <summary>
    /// The <see cref="Rigidbody"/> of this player
    /// </summary>
    Rigidbody rig;
    /// <summary>
    /// The current action of this player
    /// </summary>
    Action action = Action.StayQuiet;
    /// <summary>
    /// The point that this player are going in next movement
    /// </summary>
    Vector3 moveToPoint;
    /// <summary>
    /// The number of enemy player to mark individually if <see cref="action"/> are setted to <see cref="Action.MarkOpponent"/>
    /// </summary>
    int enemyToMark;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Act();
    }

    /// <summary>
    /// Sort a random mass to this player (<see cref="UpdateRandomMass"/>) and do his <see cref="action"/>
    /// </summary>
    void Act()
    {
        UpdateRandomMass();
        if (!running)
            return;
        switch (action)
        {
            case Action.GoToBall:
                GoToBall();
                break;
            case Action.GoToGol:
                GoToGol();
                break;
            case Action.GoToDefense:
                GoToDefense();
                break;
            case Action.MoveToPoint:
                Move();
                break;
            case Action.MarkOpponent:
                MarkEnemy();
                break;
        }
    }

    /// <summary>
    /// Sets his <see cref="action"/> to <see cref="Action.StayQuiet"/>
    /// </summary>
    public void RegisterStayQuiet()
    {
        action = Action.StayQuiet;
    }

    /// <summary>
    /// Sets his <see cref="action"/> to <see cref="Action.GoToBall"/>
    /// </summary>
    public void RegisterGoToBall()
    {
        action = Action.GoToBall;
    }

    /// <summary>
    /// Sets his <see cref="action"/> to <see cref="Action.GoToGol"/>
    /// </summary>
    public void RegisterGoToGol()
    {
        action = Action.GoToGol;
    }

    /// <summary>
    /// Sets his <see cref="action"/> to <see cref="Action.GoToDefense"/>
    /// </summary>
    public void RegisterGoToDefense()
    {
        action = Action.GoToDefense;
    }

    /// <summary>
    /// Sets his <see cref="action"/> to <see cref="Action.MoveToPoint"/> and sets <see cref="moveToPoint"/> 
    /// based in a <see cref="Direction"/>
    /// </summary>
    /// <param name="direction">direction used to set the <see cref="moveToPoint"/></param>
    public void RegisterMoveToPoint(Direction direction)
    {
        switch (direction)
        {
            case Direction.North:
                moveToPoint = Vector3.forward;
                break;
            case Direction.South:
                moveToPoint = Vector3.back;
                break;
            case Direction.East:
                moveToPoint = Vector3.right;
                break;
            case Direction.West:
                moveToPoint = Vector3.left;
                break;
            case Direction.NorthEast:
                moveToPoint = Vector3.right + Vector3.forward;
                break;
            case Direction.NorthWest:
                moveToPoint = Vector3.left + Vector3.forward;
                break;
            case Direction.SouthEast:
                moveToPoint = Vector3.right + Vector3.back;
                break;
            case Direction.SouthWest:
                moveToPoint = Vector3.left + Vector3.back;
                break;
        }
        action = Action.MoveToPoint;
    }

    /// <summary>
    /// Sets his <see cref="action"/> to <see cref="Action.MoveToPoint"/> and sets <see cref="moveToPoint"/>
    /// to specified <see cref="Direction"/>
    /// </summary>
    /// <param name="direction">direction used to set the <see cref="moveToPoint"/></param>
    public void RegisterMoveToPoint(Vector2 direction)
    {
        moveToPoint = new Vector3(direction.x, 0f, direction.y);
        action = Action.MoveToPoint;
    }

    /// <summary>
    /// Sets his <see cref="action"/> to <see cref="Action.MarkOpponent"/> and <see cref="enemyToMark"/>
    /// </summary>
    /// <param name="enemyNumber">number of enemy player to be marked</param>
    public void RegisterMarkOpponent(int enemyNumber)
    {
        enemyToMark = enemyNumber;
        action = Action.MarkOpponent;
    }

    /// <summary>
    /// Translate the player to <see cref="moveToPoint"/> with his <see cref="speed"/>
    /// </summary>
    void Move()
    {
        transform.Translate(moveToPoint * speed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Translate the player to a specified point with his <see cref="speed"/>. Used for
    /// <see cref="MarkEnemy"/> and "GoTo" methods
    /// </summary>
    /// <param name="simulatedDirection"></param>
    void Move(Vector2 simulatedDirection)
    {
        Vector3 realDirection = new Vector3(simulatedDirection.x, 0, simulatedDirection.y);
        transform.Translate(realDirection * speed * Time.deltaTime, Space.World);
    }

    /// <summary>
    /// Calls <see cref="Move(Vector2)"/> to direct the player to the <see cref="Ball"/>
    /// </summary>
    void GoToBall()
    {
        var heading = Match.Instance.Ball.GetPosition() - new Vector2(transform.position.x, transform.position.z);
        var distance = heading.magnitude;
        var direction = heading / distance;
        Move(direction);
    }

    /// <summary>
    /// Calls <see cref="Move(Vector2)"/> to direct the player to the enemy's crossbar
    /// </summary>
    void GoToGol()
    {
        Vector2 heading;
        if (team.sideLeft)
            heading = Match.Instance.RightGolPoint() - new Vector2(transform.position.x, transform.position.z);
        else
            heading = Match.Instance.LeftGolPoint() - new Vector2(transform.position.x, transform.position.z);
        var distance = heading.magnitude;
        var direction = heading / distance;
        Move(direction);
    }

    /// <summary>
    /// Calls <see cref="Move(Vector2)"/> to direct the player to the own crossbar (his defense)
    /// </summary>
    void GoToDefense()
    {
        Vector2 heading;
        if (team.sideLeft)
            heading = Match.Instance.LeftGolPoint() - new Vector2(transform.position.x, transform.position.z);
        else
            heading = Match.Instance.RightGolPoint() - new Vector2(transform.position.x, transform.position.z);
        var distance = heading.magnitude;
        var direction = heading / distance;
        Move(direction);
    }

    /// <summary>
    /// Calls <see cref="Move(Vector2)"/> to direct the player to an enemy player with number equals <see cref="enemyToMark"/>
    /// </summary>
    void MarkEnemy()
    {
        Vector2 enemyPlayerPosition = Match.Instance.GetEnemyPlayerPosition(team, enemyToMark);
        var heading = enemyPlayerPosition - new Vector2(transform.position.x, transform.position.z);
        var distance = heading.magnitude;
        var direction = heading / distance;
        Move(direction);
    }

    /// <summary>
    /// Random a <see cref="Rigidbody.mass"/> for this player with range between 1 and 200
    /// </summary>
    void UpdateRandomMass()
    {
        rig.mass = Random.Range(1, 200);
    }

}