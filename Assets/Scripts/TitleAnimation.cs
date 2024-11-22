using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleAnimation : MonoBehaviour
{
    float inc = 0;
    float init_y;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        init_y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        inc = (inc + Time.deltaTime * speed) % (2*Mathf.PI);
        transform.SetPositionAndRotation(new Vector3(transform.position.x, init_y + Mathf.Sin(inc), transform.position.z), transform.rotation);
    }
}
