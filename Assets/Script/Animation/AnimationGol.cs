/**
* Gol.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 04/05/19 (dd/mm/yy)
* Revised on: 04/05/19 (dd/mm/yy)
*/
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnimationGol : MonoBehaviour
{
    [SerializeField]
    GameObject golPanel;
    [SerializeField]
    Text golText;    
    [SerializeField]
    AudioClip crowdSound;
    public float animationSpeed;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        StartCoroutine(PlayGol());
    }

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