using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformController : MonoBehaviour
{

    public GameObject platformPrefab;
    public List<GameObject> platforms;
    public int numberOfPlatforms;
    public float platformSpawnRate;
    private float myTimer = 0.0f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            SpawnPlatform();
        }
    }

    void SpawnPlatform()
    {
        GameObject newPlatform = Instantiate(platformPrefab);
        Vector2 newPosition = new Vector2(Camera.main.transform.position.x, player.transform.position.y + 2.0f);
        if (player.GetComponent<Rigidbody2D>().velocity.y < -1.0f && player.transform.position.y > 20.0f)
        {
            newPosition += new Vector2(Random.Range(-7.0f, 7.0f), -15.0f);
        } else
        {
            
            if (Random.Range(0.0f, 1.0f) > 0.5f)
            {
                newPosition += new Vector2(15.0f, 0.0f);
            }
            else
            {
                newPosition -= new Vector2(15.0f, 0.0f);
            }
        }
            
        newPlatform.transform.position = newPosition;
        platforms.Add(newPlatform);
    }

    // Update is called once per frame
    void Update()
    {
        myTimer += Time.deltaTime;
        if (myTimer > platformSpawnRate)
        {
            myTimer = 0.0f;
            SpawnPlatform();
        }
    }
}
