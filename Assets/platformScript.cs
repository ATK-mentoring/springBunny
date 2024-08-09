using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformScript : MonoBehaviour
{

    public float speed;
    public float distance;
    public bool direction = true;
    private Vector2 startPosition;
    private bool reverse = false;
    private Rigidbody2D myRB;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        if (Camera.main.transform.position.x < transform.position.x)
        {
            // set direction to reverse
            reverse = !reverse;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool updateposition = false;
        if (Mathf.Abs(Vector2.Distance(transform.position, startPosition)) >= distance)
        {
            reverse = !reverse;
            if (transform.position.x > startPosition.x)
            {
                transform.position = new Vector2(startPosition.x + distance, transform.position.y);
            } else
            {
                transform.position = new Vector2(startPosition.x - distance, transform.position.y);
            }
            
            updateposition = true;
        }
        if (!reverse)
        {
            myRB.MovePosition(transform.position + new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
        } else
        {
            myRB.MovePosition(transform.position - new Vector3(speed * Time.deltaTime, 0.0f, 0.0f));
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            Destroy(gameObject);
        }
            
    }
}
