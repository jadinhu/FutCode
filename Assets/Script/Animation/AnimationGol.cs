/**
* Gol.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 04/05/19 (dd/mm/yy)
* Revised on: 13/12/21 (dd/mm/yy)
*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the gol event (animation, sound and texts)
/// </summary>
public class AnimationGol : MonoBehaviour
{
    /// <summary>
    /// The gol panel
    /// </summary>
    [SerializeField]
    GameObject golPanel;
    /// <summary>
    /// The gol text of <see cref="golPanel"/>
    /// </summary>
    [SerializeField]
    Text golText;
    /// <summary>
    /// The crowd sound when gol happens
    /// </summary>
    [SerializeField]
    AudioClip crowdSound;
    /// <summary>
    /// The animation speed of the gol UI event
    /// </summary>
    public float animationSpeed;
    /// <summary>
    /// The audio source used in gol UI event
    /// </summary>
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays a gol coroutine event (<see cref="PlayGol"/>)
    /// </summary>
    public void Play()
    {
        StartCoroutine(PlayGol());
    }

    /// <summary>
    /// Plays all gol event: playing <see cref="crowdSound"/>, displaying <see cref="golPanel"/> 
    /// and <see cref="golText"/> animation
    /// </summary>
    IEnumerator PlayGol()
    {
        audioSource.PlayOneShot(crowdSound);
        golPanel.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            golText.text = "G";
            yield return new WaitForSeconds(animationSpeed);
            golText.text = "GO";
            yield return new WaitForSeconds(animationSpeed);
            golText.text = "GOO";
            yield return new WaitForSeconds(animationSpeed);
            golText.text = "GOOL";
            yield return new WaitForSeconds(animationSpeed);
            golText.text = "";
            yield return new WaitForSeconds(animationSpeed);
        }
        audioSource.Stop();
        golPanel.SetActive(false);
    }
}