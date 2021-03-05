using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class AnimateGirl : MonoBehaviour
{
    [Tooltip("Vitesse max en unité par seconde")]
    public int MaxSpeed = 4;
    Animator animator;
    public AudioSource WelcomeAudioData;
    public GameObject fight;
    SpriteRenderer mySpriteRenderer;
    Rigidbody2D rigidbody2d;
    public float forceApplied = 10;

    private void Start()
    {
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    static readonly int Speed = Animator.StringToHash("Speed");
    static readonly int Roll = Animator.StringToHash("Roll");
    static readonly int Jump = Animator.StringToHash("Jump");

    void FixedUpdate()
    {
        var maxDisancePerFrame = MaxSpeed;
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right * maxDisancePerFrame;
            mySpriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            move += Vector3.left * maxDisancePerFrame;
            mySpriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            move += Vector3.up * maxDisancePerFrame;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.down * maxDisancePerFrame;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            GetComponent<CircleCollider2D>().isTrigger = true;
            animator.SetBool(Jump, true);
        }
        else
        {
            GetComponent<CircleCollider2D>().isTrigger = false;
            animator.SetBool(Jump, false);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool(Roll, true);
        }
        else
        {
            animator.SetBool(Roll, false);
        }

        animator.SetFloat(Speed, move.magnitude * 10f);
        rigidbody2d.velocity = move;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Ball")
        {
            if (animator.GetBool(Roll))
            {
                collision2D.gameObject.GetComponent<Rigidbody2D>().AddForce(collision2D.contacts[0].normal * -forceApplied, ForceMode2D.Impulse);
            }
        }
    }
}
