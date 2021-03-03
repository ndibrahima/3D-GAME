using System;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    public float MaxSpeed = 1;
    SpriteRenderer mySpriteRenderer;
    DateTime currentTime = DateTime.Now;
    Vector3 randomDirection;

    void Start()
    {
        randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = Vector3.zero;
        var maxDisancePerFrame = MaxSpeed * Time.deltaTime;

        if (DateTime.Now > currentTime.AddSeconds(1))
        {
            randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0f);
            currentTime = DateTime.Now;
        }

        move += randomDirection.normalized * maxDisancePerFrame;

        this.transform.position = this.transform.position + move;

        transform.Rotate(0f, 0f, Time.deltaTime * 360);
    }
}
