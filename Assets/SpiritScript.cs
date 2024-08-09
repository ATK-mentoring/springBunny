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

    // Start is called before the first frame update
    void Start()
    {
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
        float new_x = transform.position.x + horizontalSpeed * Time.deltaTime;
        float new_y = startPosition.y + (Mathf.Sin(new_x * frequency) * amplitude);
        myRB.MovePosition(new Vector2(new_x, new_y));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera")
        {
            //Destroy(gameObject);
        }

    }
}
