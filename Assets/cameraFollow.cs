using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 0.3f;
    //public GameObject player;
    private float targetY = 0.0f;

    private Vector3 velocity;
    public GameObject Background;
    private Material BGMaterial;
    public float downCameraTrigger;
    private bool downCamera = false;
    private bool spiritJump = false;
    public TMP_Text text;

    private void LateUpdate()
    {
        BGMaterial = Background.GetComponent<SpriteRenderer>().material;
        Vector3 targetPos = target.position;
        if (target.gameObject.GetComponent<bunnyController>().is_grounded)
        {
            targetY = target.position.y + 2.5f;
            if (downCamera)
            {
                targetY = target.position.y;
            }
        } else
        {
            //player may be falling
            if (target.position.y < transform.position.y - 2.7f)
            {
                targetY = target.position.y - 2.0f;
            }
            
        }
        if (spiritJump)
        {
            targetY = target.position.y + 2.5f;
            spiritJump = false;
        }
        targetPos.y = targetY;
        Vector3 newPosition = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
        newPosition.z = -10.0f;

        Vector2 newOffset = BGMaterial.mainTextureOffset;
        Vector3 cameraMovement = newPosition - transform.position;
        cameraMovement /= 8.0f;
        newOffset += new Vector2(cameraMovement.x, cameraMovement.y);
        //if (newOffset.x > 1.0f)
        BGMaterial.mainTextureOffset = newOffset;

        transform.position = newPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            downCameraTrigger += Time.deltaTime;
        } else
        {
            downCameraTrigger -= Time.deltaTime;
        }
        downCameraTrigger = Mathf.Clamp(downCameraTrigger, 0.0f, 1.0f);
        if (downCameraTrigger == 1.0f)
        {
            downCamera = true;
        } else
        {
            downCamera = false;
        }
    }

    public void SpiritJump()
    {
        spiritJump = true;
    }
}
