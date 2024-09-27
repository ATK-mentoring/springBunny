using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bunnyController : MonoBehaviour
{
    public bool is_grounded;
    public float move_speed;
    public float jump_force; 
    public float side_jump_force; 
    private Rigidbody2D my_rigidbody;
    private Animator my_animator;
    public float max_speed;
    public float spiritJumpForce;
    public float deathHeight;
    private float fallHeight;
    private bool isFalling = false;
    private bool canDieToFalling = false;
    private bool gameover = false;
    private float cameraShrinkTimer = 0.0f;
    private float cameraVelocity;
    public GameObject gameOverUI;
    public bool can_sideJump = false;
    public HighscoreController HSController;
    private bool isGameOverProcessed = false;
    public audioController myAC;

    // Start is called before the first frame update
    void Start()
    {
        my_rigidbody = gameObject.GetComponent<Rigidbody2D>();
        my_animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            //my_animator.ResetTrigger("jump");
            //my_animator.ResetTrigger("landed");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (is_grounded)
                {
                    isFalling = false;
                    // can jump
                    my_animator.SetTrigger("jump");
                    //my_animator.ResetTrigger("landed");
                    Jump(jump_force);
                    
                } else if (can_sideJump)
                {
                    isFalling = false;
                    // can jump
                    my_animator.SetTrigger("climb");
                    //my_animator.ResetTrigger("landed");
                    Jump(side_jump_force);
                    //land
                    myAC.playSound("land");
                }

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
            if (Mathf.Abs(my_rigidbody.velocity.x) > 0.0f && !can_sideJump)
            {
                if (my_rigidbody.velocity.x > 0.0f)
                {
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
                }
                else
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
            //check if falling and store height
            if (my_rigidbody.velocity.y < 0.0f && !isFalling)
            {
                //we are falling
                fallHeight = transform.position.y;
                isFalling = true;
            }
            if (isFalling)
            {
                if (fallHeight - transform.position.y > deathHeight)
                {
                    canDieToFalling = true;
                }
            }
        } else
        {
            //game over code
            
            //shink camera size to zoom in
            float cameraTargetSize = 1.5f;
            float cameraShrinkDuration = 3.0f;
            if (cameraShrinkTimer < cameraShrinkDuration)
            {
                //shrint a little bit
                Camera.main.orthographicSize = Mathf.SmoothDamp(Camera.main.orthographicSize, cameraTargetSize, ref cameraVelocity, cameraShrinkDuration / cameraShrinkTimer);
                cameraShrinkTimer += Time.deltaTime;
            }
            //single function calls
            if (!isGameOverProcessed) {
                //display game over text
                myAC.playSound("gameOver");
                gameOverUI.SetActive(true);

                HSController.isHighScore();
                isGameOverProcessed = true;
                //show text field for player name input
                //show button for saving high score
            }

        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            if (canDieToFalling)
            {
                //game over
                my_animator.SetTrigger("dead");
                gameover = true;
            } else
            {
                myAC.playSound("land");
                isFalling = false;
                is_grounded = true;
                my_rigidbody.velocity = Vector2.zero;
                my_animator.SetTrigger("landed");
                transform.parent = collision.transform;
                my_rigidbody.isKinematic = true;
            }
            
        } else if (collision.gameObject.tag == "spirit")
        {
            isFalling = false;
            canDieToFalling = false;
            is_grounded = false;
            my_rigidbody.velocity = new Vector2(my_rigidbody.velocity.x, 0.0f);
            my_rigidbody.AddForce(transform.up * spiritJumpForce);
            myAC.playSound("spiritJump");

        } else if (collision.gameObject.tag == "side")
        {
            //Debug.Log("side detected");
            can_sideJump = true;
            canDieToFalling = false;
            isFalling = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "side")
        {

            can_sideJump = false;
        } else if (collision.gameObject.tag == "spirit")
        {
            collision.gameObject.GetComponent<SpiritScript>().death();
        }
    }

    private void Jump(float this_jump_force)
    {
        isFalling = false;
        transform.parent = null;
        my_rigidbody.isKinematic = false;
        my_rigidbody.AddForce(transform.up * this_jump_force);
        is_grounded = false;
        my_animator.ResetTrigger("landed");
        myAC.playSound("jump");
    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spirit")
        {
            //Debug.Log("spirit hit");
            my_rigidbody.isKinematic = false;
            myAC.playSound("spiritHit");
        }
    }
}
