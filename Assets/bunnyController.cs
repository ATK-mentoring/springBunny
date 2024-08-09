using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunnyController : MonoBehaviour
{
    public bool is_grounded;
    public float move_speed;
    public float jump_force;
    private Rigidbody2D my_rigidbody;
    private Animator my_animator;
    public float max_speed;

    // Start is called before the first frame update
    void Start()
    {
        my_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        my_animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //my_animator.ResetTrigger("jump");
        //my_animator.ResetTrigger("landed");
        if (is_grounded && Input.GetKeyDown(KeyCode.Space))
        {
            // can jump
            my_animator.SetTrigger("jump");
            //my_animator.ResetTrigger("landed");

            
        }
        if (!is_grounded)
        {
            // check if moving left or right
            float move_input = 0.0f;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                move_input += -1.0f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                move_input += 1.0f;
            }
            my_rigidbody.AddForce(transform.right * move_input * move_speed);
        }
        //flip sprite to face movement
        if (Mathf.Abs(my_rigidbody.velocity.x) > 0.0f)
        {
            if (my_rigidbody.velocity.x > 0.0f)
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            } else
            {
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * -1.0f, transform.localScale.y);
            }
        }
        if (Mathf.Abs(my_rigidbody.velocity.x) > max_speed)
        {
            float new_speed = max_speed;
            if (my_rigidbody.velocity.x < 0.0f)
            {
                new_speed *= -1.0f;
            }
            my_rigidbody.velocity = new Vector2(new_speed, my_rigidbody.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            is_grounded = true;
            my_rigidbody.velocity = Vector2.zero;
            my_animator.SetTrigger("landed");
            transform.parent = collision.transform;
            my_rigidbody.isKinematic = true;
        }
    }

    private void Jump()
    {
        transform.parent = null;
        my_rigidbody.isKinematic = false;
        my_rigidbody.AddForce(transform.up * jump_force);
        is_grounded = false;
        my_animator.ResetTrigger("landed");
    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
