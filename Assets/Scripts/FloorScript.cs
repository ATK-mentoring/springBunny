using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 camPos = Camera.main.transform.position;
        if (Mathf.Abs(transform.position.x - camPos.x) > 20.0f)
        {
            if (camPos.x > transform.position.x)
            {
                transform.position = transform.position + new Vector3(40.0f, 0.0f, 0.0f);
            }
            else
            {
                transform.position = transform.position - new Vector3(40.0f, 0.0f, 0.0f);
            }
        }
    }

    
}
