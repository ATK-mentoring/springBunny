using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorController : MonoBehaviour
{

    public GameObject BaseSprite;
    public GameObject floors;

    // Start is called before the first frame update
    void Start()
    {
        //spawn 20 on either side
        Vector2 middle = Camera.main.transform.position;
        for (int i = 0; i < 40; i++)
        {
            GameObject newPlatform = Instantiate(BaseSprite, floors.transform );
            newPlatform.transform.position = new Vector3(Mathf.Floor(middle.x) + i - 19.5f, -4.5f, 0.0f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
