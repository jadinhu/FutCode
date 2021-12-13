/**
* MonoMatch.cs
* Created by: Jadson Almeida [jadson.sistemas@gmail.com]
* Created on: 31/03/19 (dd/mm/yy)
* Revised on: 12/12/21 (dd/mm/yy)
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the core behavior of a <see cref="Match"/> (score, sounds, time and monobehavior scripts about 
/// ball and player etc.) with all inacessible behavior
/// </summary>
public class MonoMatch : MonoBehaviour
{
    /// <summary>
    /// Ball's monobehavior
    /// </summary>
    [SerializeField]
    MonoBall ball;
    /// <summary>
    /// Text with the score of team A
    /// </summary>
    [SerializeField]
    Text placarTeamA;
    /// <summary>
    /// Text with the score of team B
    /// </summary>
    [SerializeField]
    Text placarTeamB;
    /// <summary>
    /// Text with the name of team A
    /// </summary>
    [SerializeField]
    Text nameTeamA;
    /// <summary>
    /// Text with the name of team B
    /// </summary>
    [SerializeField]
    Text nameTeamB;
    /// <summary>
    /// Text of the time counter
    /// </summary>
    [SerializeField]
    Text placarTime;
    /// <summary>
    /// Image of the Panel Gol with crowd of the Team
    /// </summary>
    [SerializeField]
    Image crowdImage;
    /// <summary>
    /// Image of the red crowd Team
    /// </summary>
    [SerializeField]
    Sprite redCrowdImage;
    /// <summary>
    /// Image of the blue crowd Team
    /// </summary>
    [SerializeField]
    Sprite blueCrowdImage;
    /// <summary>
    /// Panel with the winner message (showed when match ends)
    /// </summary>
    [SerializeField]
    GameObject winnerMessage;
    /// <summary>
    /// Start match sfx
    /// </summary>
    [SerializeField]
    AudioClip startSound;
    /// <summary>
    /// End match sfx
    /// </summary>
    [SerializeField]
    AudioClip endSound;
    /// <summary>
    /// List of the players (1, 2 and 3 are Team A; 4, 5 and 6 are Team B)
    /// </summary>
    [SerializeField]
    List<MonoPlayer> players = new List<MonoPlayer>(6);
    /// <summary>
    /// If match are running
    /// </summary>
    public bool MatchRunning { get; private set; }
    /// <summary>
    /// The original (started) position of the players in <see cref="players"/>
    /// </summary>
    List<Vector3> originalPositions = new List<Vector3>();
    /// <summary>
    /// <see cref="AnimationGol"/> script in the same <see cref="GameObject"/>
    /// </summary>
    AnimationGol animationGolScript;
    /// <summary>
    /// Team A script
    /// </summary>
    Team teamA = new TeamA();
    /// <summary>
    /// Team B script
    /// </summary>
    Team teamB = new TeamB();
    /// <summary>
    /// Match public script
    /// </summary>
    Match match;
    /// <summary>
    /// AudioSource of the game
    /// </summary>
    AudioSource sound;
    /// <summary>
    /// Team A goal counter
    /// </summary>
    int golsTeamA = 0;
    /// <summary>
    /// Team B goal counter
    /// </summary>
    int golsTeamB = 0;
    /// <summary>
    /// Time of the match
    /// </summary>
    float matchTime = 60;
    /// <summary>
    /// If is running the first time of the match
    /// </summary>
    bool isFirstTime = true;

    void FixedUpdate()
    {
        if (!MatchRunning)
            return;
        teamA.Play();
        teamB.Play();
    }

    /// <summary>
    /// Setup the match before start it
    /// </summary>
    public void Setup()
    {
        sound = GetComponent<AudioSource>();
        animationGolScript = GetComponent<AnimationGol>();
        match = new Match(this, new Ball(ball));
        teamA.player1 = new Player(players[0], teamA);
        teamA.player2 = new Player(players[1], teamA);
        teamA.player3 = new Player(players[2], teamA);
        teamB.player1 = new Player(players[3], teamB);
        teamB.player2 = new Player(players[4], teamB);
        teamB.player3 = new Player(players[5], teamB);
        for (int i = 0; i < 6; i++)
        {
            originalPositions.Add(players[i].transform.position);
            if (i < 3)
                players[i].team = teamA;
            else
                players[i].team = teamB;
        }
        teamA.Setup();
        teamB.Setup();
        nameTeamA.text = teamA.name;
        nameTeamB.text = teamB.name;
        teamA.sideLeft = true;
        teamB.sideLeft = false;
        StartCoroutine(StartMatch());
    }

    /// <summary>
    /// Start the match
    /// </summary>
    IEnumerator StartMatch()
    {
        sound.PlayOneShot(startSound);
        yield return new WaitForSeconds(startSound.length);
        foreach (MonoPlayer player in players)
        {
            player.running = true;
        }
        StartCoroutine(TimeCounter());
    }

    /// <summary>
    /// Return the simulated position of the player with <see cref="Player.GetPosition"/> 
    /// </summary>
    /// <param name="yourTeam">your team</param>
    /// <param name="enemyNumber">number of the player (1, 2 or 3)</param>
    public Vector2 GetEnemyPlayerPosition(Team yourTeam, int enemyNumber)
    {
        if (yourTeam == teamB)
        {
            if (enemyNumber == 1)
                return teamA.player1.GetPosition();
            if (enemyNumber == 2)
                return teamA.player2.GetPosition();
            if (enemyNumber == 3)
                return teamA.player3.GetPosition();
        }
        else
        {
            if (enemyNumber == 1)
                return teamB.player1.GetPosition();
            if (enemyNumber == 2)
                return teamB.player2.GetPosition();
            if (enemyNumber == 3)
                return teamB.player3.GetPosition();
        }
        Debug.LogWarning("Enemy not founded");
        return Vector3.zero;
    }

    /// <summary>
    /// Change the <see cref="Time.timeScale"/> to 1 or 10
    /// </summary>
    public void ChangeSpeed()
    {
        if (Time.timeScale == 1)
            Time.timeScale = 10;
        else
            Time.timeScale = 1;
    }

    /// <summary>
    /// Add 1 goal score for a time, stopping the game
    /// </summary>
    /// <param name="isForTimeA">true if the goal is to team A, false if is to B</param>
    public void AddGol(bool isForTimeA)
    {
        if (!MatchRunning)
            return;
        MatchRunning = false;
        foreach (MonoPlayer player in players)
        {
            player.running = false;
        }
        ball.Stop();
        if (isForTimeA)
        {
            if (isFirstTime)
            {
                crowdImage.sprite = redCrowdImage;
                AddGol(ref golsTeamA, placarTeamA);
            }
            else
            {
                crowdImage.sprite = blueCrowdImage;
                AddGol(ref golsTeamB, placarTeamA);
            }
        }
        else
        {
            if (isFirstTime)
            {
                crowdImage.sprite = blueCrowdImage;
                AddGol(ref golsTeamB, placarTeamB);
            }
            else
            {
                crowdImage.sprite = redCrowdImage;
                AddGol(ref golsTeamA, placarTeamB);
            }
        }
        print("A " + golsTeamA + " / B " + golsTeamB);
    }

    /// <summary>
    /// Add a goal to team gol counter, updating the placar and calling <see cref="PlayGol"/>
    /// </summary>
    /// <param name="golCount">gol counter of the team</param>
    /// <param name="placar">placar of the team</param>
    void AddGol(ref int golCount, Text placar)
    {
        golCount++;
        placar.text = golCount.ToString();
        StartCoroutine(PlayGol());
    }

    /// <summary>
    /// Set the players and ball positions to originals
    /// </summary>
    void ResetPositions()
    {
        ball.Stop();
        if(isFirstTime)
        {
            for (int i = 0; i < 6; i++)
                players[i].transform.position = originalPositions[i];
        }
        else
        {
            for (int i = 0; i < 3; i++)
                players[i].transform.position = originalPositions[3 + i];
            for (int i = 3; i < 6; i++)
                players[i].transform.position = originalPositions[i - 3];
        }
        ball.transform.position = new Vector3(0, .25f, 0);
    }

    /// <summary>
    /// Play sounds about goal, call <see cref="ResetPositions"/> and resume the game
    /// </summary>
    IEnumerator PlayGol()
    {
        animationGolScript.Play();
        yield return new WaitForSeconds(animationGolScript.animationSpeed * 5 * 3);
        ResetPositions();
        yield return new WaitForSeconds(1);
        sound.PlayOneShot(startSound);
        yield return new WaitForSeconds(startSound.length);
        StartCoroutine(TimeCounter());
        foreach (MonoPlayer player in players)
        {
            player.running = true;
        }
    }

    /// <summary>
    /// Counter the match time and updates <see cref="placarTime"/>. Calls <see cref="EndFirstTime"/> 
    /// when first time of the match ends and calls <see cref="EndTheMatch"/> when second time match ends
    /// </summary>
    IEnumerator TimeCounter()
    {
        MatchRunning = true;
        while (MatchRunning)
        {
            yield return new WaitForSeconds(1);
            matchTime--;
            if (matchTime < 0)
                matchTime = 0;
            placarTime.text = matchTime.ToString();
            if (matchTime <= 0)
            {
                MatchRunning = false;
                if (isFirstTime)
                    StartCoroutine(EndFirstTime());
                else
                    StartCoroutine(EndTheMatch());
            }
                
        }
    }

    /// <summary>
    /// Stop players in <see cref="players"/>, play <see cref="endSound"/>, calls <see cref="ResetPositions"/>
    /// and release the players restarting <see cref="TimeCounter"/> 
    /// </summary>
    IEnumerator EndFirstTime()
    {
        foreach (MonoPlayer player in players)
        {
            player.running = false;
        }
        sound.PlayOneShot(endSound);
        isFirstTime = false;
        yield return new WaitForSeconds(endSound.length + 1);
        ResetPositions();
        nameTeamA.text = teamB.name;
        nameTeamB.text = teamA.name;
        placarTeamA.text = "" + golsTeamB;
        placarTeamB.text = "" + golsTeamA;
        teamA.sideLeft = false;
        teamB.sideLeft = true;
        yield return new WaitForSeconds(1);
        matchTime = 60;
        StartCoroutine(TimeCounter());
        foreach (MonoPlayer player in players)
        {
            player.running = true;
        }
    }

    /// <summary>
    /// Stop players in <see cref="players"/>, the <see cref="ball"/>, play <see cref="endSound"/>, update
    /// and show <see cref="winnerMessage"/>
    /// </summary>
    IEnumerator EndTheMatch()
    {
        foreach (MonoPlayer player in players)
        {
            player.running = false;
        }
        ball.Stop();
        sound.PlayOneShot(endSound);
        yield return new WaitForSeconds(1);
        if (golsTeamA < golsTeamB)
            winnerMessage.GetComponentInChildren<Text>().text = "Congratulations\n" + teamB.name;
        else if (golsTeamB < golsTeamA)
            winnerMessage.GetComponentInChildren<Text>().text = "Congratulations\n" + teamA.name;
        else
            winnerMessage.GetComponentInChildren<Text>().text = "We have a\nDRAW";
        winnerMessage.SetActive(true);
        yield return null;
    }
}