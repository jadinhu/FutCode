/**
* Gol.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 26/04/19 (dd/mm/yy)
* Revised on: 13/12/21 (dd/mm/yy)
*/
using UnityEngine;

/// <summary>
/// Handles the crossbar of each <see cref="Team"/>
/// </summary>
public class Crossbar : MonoBehaviour
{
    /// <summary>
    /// If the team A is the owner of this crossbar
    /// </summary>
    [SerializeField]
    bool defTimeA;
    /// <summary>
    /// The current match 
    /// </summary>
    [SerializeField]
    MonoMatch match;

    /// <summary>
    /// Starts a gol event against the team, calling <see cref="MonoMatch.AddGol(bool)"/>
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
            match.AddGol(!defTimeA);
    }
}