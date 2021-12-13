/**
* MonoPlayer.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 31/03/19 (dd/mm/yy)
* Revised on: 19/10/19 (dd/mm/yy)
*/
using UnityEngine;

public enum Action { StayQuiet, GoToBall, GoToGol, GoToDefense, MoveToPoint, MarkOpponent }
public enum Direction { North, South, East, West, NorthEast, NorthWest, SouthEast, SouthWest }

public class MonoPlayer : MonoBehaviour
{
    [SerializeField]
    float speed = 1;
    public bool Running { get; set; }
    public Team Team { get; set; }
    Rigidbody rig;
    Action action = Action.StayQuiet;
    Vector3 moveToPoint;
    int enemyToMark;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        UpdateRandomMass();
        if (!Running)
            return;
        switch (action )
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
            default: 
                break;
        }
    }

    public void RegisterStayQuiet()
    {
        action = Action.StayQuiet;
    }

    public void RegisterGoToBall()
    {
        action = Action.GoToBall;
    }

    public void RegisterGoToGol()
    {
        action = Action.GoToGol;
    }

    public void RegisterGoToDefense()
    {
        action = Action.GoToDefense;
    }

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

    public void RegisterMoveToPoint(Vector2 direction)
    {
        moveToPoint = new Vector3(direction.x, 0f, direction.y);
        action = Action.MoveToPoint;
    }

    public void RegisterMarkOpponent(int enemyNumber)
    {
        enemyToMark = enemyNumber;
        action = Action.MarkOpponent;
    }

    void Move()
    {
        transform.Translate(moveToPoint * speed * Time.deltaTime, Space.World);
    }

    void Move(Vector2 simulatedDirection)
    {
        Vector3 realDirection = new Vector3(simulatedDirection.x, 0, simulatedDirection.y);
        transform.Translate(realDirection * speed * Time.deltaTime, Space.World);
    }

    void GoToBall()
    {
        var heading = Match.Instance.Ball.GetPosition() - new Vector2(transform.position.x, transform.position.z);
        var distance = heading.magnitude;
        var direction = heading / distance;
        Move(direction);
    }

    void GoToGol()
    {
        Vector2 heading;
        if (Team.sideLeft)
            heading = Match.Instance.RightGolPoint() - new Vector2(transform.position.x, transform.position.z);
        else
            heading = Match.Instance.LeftGolPoint() - new Vector2(transform.position.x, transform.position.z);
        var distance = heading.magnitude;
        var direction = heading / distance;
        Move(direction);
    }

    void GoToDefense()
    {
        Vector2 heading;
        if (Team.sideLeft)
            heading = Match.Instance.LeftGolPoint() - new Vector2(transform.position.x, transform.position.z);
        else
            heading = Match.Instance.RightGolPoint() - new Vector2(transform.position.x, transform.position.z);
        var distance = heading.magnitude;
        var direction = heading / distance;
        Move(direction);
    }

    void MarkEnemy()
    {
        Vector2 enemyPlayerPosition = Match.Instance.GetEnemyPlayerPosition(Team, enemyToMark);
        var heading = enemyPlayerPosition - new Vector2(transform.position.x, transform.position.z);
        var distance = heading.magnitude;
        var direction = heading / distance;
        Move(direction);
    }

    void UpdateRandomMass()
    {
        rig.mass = Random.Range(1, 200);
    }

}