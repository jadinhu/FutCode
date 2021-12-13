/**
* MonoBall.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 31/03/19 (dd/mm/yy)
* Revised on: 12/12/21 (dd/mm/yy)
*/
using UnityEngine;

/// <summary>
/// Handles the core behavior of a ball
/// </summary>
public class MonoBall : MonoBehaviour
{
    /// <summary>
    /// Sound of player's touching this ball
    /// </summary>
    [SerializeField]
    AudioClip audioKickLow;
    /// <summary>
    /// The movement speed of this ball when touched
    /// </summary>
    [SerializeField]
    float speed = 1;
    /// <summary>
    /// The <see cref="Rigidbody"/> of this gameobject
    /// </summary>
    Rigidbody rig;
    /// <summary>
    /// AudioSource of the game
    /// </summary>
    AudioSource sound;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Checks if a tag Player have touch this ball to calls <see cref="Rigidbody.AddForce(Vector3)"/>
    /// </summary>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Vector3 direction = (transform.position - collision.transform.position).normalized * speed;
            direction.y = 0;
            rig.AddForce(direction, ForceMode.Force);
            PlayKickLow();
        }
    }

    /// <summary>
    /// Play oneshot the sound <see cref="audioKickLow"/>
    /// </summary>
    void PlayKickLow()
    {
        sound.PlayOneShot(audioKickLow);
    }

    /// <summary>
    /// Stops the ball's <see cref="Rigidbody"/>
    /// </summary>
    public void Stop()
    {
        rig.velocity = rig.angularVelocity = Vector3.zero;
    }
}