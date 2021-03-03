using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GoalPointRight : MonoBehaviour
{
    SpriteRenderer mySpriteRenderer;
    public AudioSource WinAudioData;
    public AudioSource GoalAudioData;
    public AudioSource WelcomeAudioData;

    [Tooltip("Target element")]
    public GameObject target;
    public GameObject fight;
    public GameObject goledAnimation;
    private float x = -0.35f;
    private float y = 0.8f;
    public GameObject[] players;
    public GameObject[] balls;
    public GameObject[] points;

    private int point = 0;
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        players = GameObject.FindGameObjectsWithTag("Player");
        balls = GameObject.FindGameObjectsWithTag("Ball");
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Ball")
        {
            playSound();
            addPoint();
            restartPosition();
            StartCoroutine(PauseGame(3f));
        }
    }

    void playSound()
    {
        GoalAudioData.Play();
    }

    void addPoint()
    {
        var instance = Instantiate(target);
        foreach (GameObject player in players)
        {
            if (player.name.Equals("Player 1"))
            {
                instance.transform.SetParent(player.transform);
                instance.transform.localPosition = new Vector3(x, y, 1);
            }
        }
        x = x + 0.15f;
        point++;
        if (point == 5)
        {
            WinAudioData.Play();
            points = GameObject.FindGameObjectsWithTag("Point");
            print(points);
            foreach (GameObject point in points)
            {
                Destroy(point);
            }
            x = -0.35f;
            y = 0.8f;
        }
    }

    void restartPosition()
    {
        restartPositionPlayers();
        restartPositionBalls();
        restartSizeCamera();
    }

    void restartPositionPlayers()
    {
        foreach (GameObject player in players)
        {
            if (player.name.Equals("Player 1"))
            {
                player.transform.position = new Vector3(-0.7f, 0.9f, 0);
            }
            else
            {
                player.transform.position = new Vector3(0.8f, 0.9f, 0);
            }
        }
    }

    void restartPositionBalls()
    {
        foreach (GameObject ball in balls)
        {
            ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            if (ball.name.Equals("Ball 1"))
            {
                ball.transform.position = new Vector3(-0.2f, 0.35f, 0);
            }
            else
            {
                ball.transform.position = new Vector3(0.4f, 0.35f, 0);
            }
        }
    }

    public IEnumerator PauseGame(float pauseTime)
    {
        Time.timeScale = 0f;
        float pauseEndTime = Time.realtimeSinceStartup + pauseTime;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        WelcomeAudioData.Play();
        fight.GetComponent<ParticleSystem>().Play();
    }

    void restartSizeCamera()
    {
        Camera.main.orthographicSize = 4.487658f;
    }
}
