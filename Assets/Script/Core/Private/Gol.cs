/**
* Gol.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 26/04/19 (dd/mm/yy)
* Revised on: 28/04/19 (dd/mm/yy)
*/
using UnityEngine;

public class Gol : MonoBehaviour
{
    [SerializeField]
    bool defTimeA;
    [SerializeField]
    MonoMatch match;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Ball"))
        {
            match.AddGol(!defTimeA);
        }
    }
}