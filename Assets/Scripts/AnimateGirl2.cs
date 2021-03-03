using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class AnimateGirl2 : MonoBehaviour
{
    [Tooltip("Vitesse max en unité par seconde")]
    public int MaxSpeed = 4;
    Animator animator;
    SpriteRenderer mySpriteRenderer;
    Rigidbody2D rigidbody2d;
    public float forceApplied = 10;


    private void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    static readonly int Speed = Animator.StringToHash("Speed");
    static readonly int Jump = Animator.StringToHash("Jump");
    static readonly int Roll = Animator.StringToHash("Roll");

    void Update()
    {
        var maxDisancePerFrame = MaxSpeed;
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            move += Vector3.right * maxDisancePerFrame;
            mySpriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            move += Vector3.left * maxDisancePerFrame;
            mySpriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            move += Vector3.up * maxDisancePerFrame;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            move += Vector3.down * maxDisancePerFrame;
        }

        if (Input.GetKey(KeyCode.RightControl))
        {
            animator.SetBool(Roll, true);
        }
        else
        {
            animator.SetBool(Roll, false);
        }

        if (Input.GetKey(KeyCode.RightShift))
        {
            animator.SetBool(Jump, true);
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
        else
        {
            animator.SetBool(Jump, false);
            GetComponent<CircleCollider2D>().isTrigger = false;
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
