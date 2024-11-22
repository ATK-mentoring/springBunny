using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritScript : MonoBehaviour
{

    public float horizontalSpeed;
    public float amplitude;
    public float frequency;
    private Vector2 startPosition;
    private bool reverse = false;
    private Rigidbody2D myRB;
    private Animator myAnimator;
    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRB = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        if (Camera.main.transform.position.x < transform.position.x)
        {
            // set direction to reverse
            reverse = !reverse;
            horizontalSpeed *= -1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            float new_x = transform.position.x + horizontalSpeed * Time.deltaTime;
            float new_y = startPosition.y + (Mathf.Sin(new_x * frequency) * amplitude);
            myRB.MovePosition(new Vector2(new_x, new_y));
        } else
        {
            float new_x = transform.position.x + horizontalSpeed * Time.deltaTime;
            float new_y = transform.position.y + horizontalSpeed * Time.deltaTime;
            myRB.MovePosition(new Vector2(new_x, new_y));
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, GetComponent<SpriteRenderer>().color.a - 0.01f);
            if (GetComponent<SpriteRenderer>().color.a <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.tag == "Player")
        //{
        //    myAnimator.SetTrigger("death");
        //    alive = false;
        //    Camera.main.GetComponent<cameraFollow>().SpiritJump();
        //}
    }

    public void death()
    {
        myAnimator.SetTrigger("death");
        alive = false;
        Camera.main.GetComponent<cameraFollow>().SpiritJump();
    }
}
