using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0.3f;
    //public GameObject player;
    private float targetY = 0.0f;

    private Vector3 velocity;

    private void LateUpdate()
    {
        Vector3 targetPos = target.position;
        if (target.gameObject.GetComponent<bunnyController>().is_grounded)
        {
            targetY = target.position.y + 2.5f;
        } else
        {
            //player may be falling
            if (target.position.y < transform.position.y - 2.7f)
            {
                targetY = target.position.y - 2.0f;
            }
        }
        targetPos.y = targetY;
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        newPosition.z = -10.0f;
        
        
        transform.position = newPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
