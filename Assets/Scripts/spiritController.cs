using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiritController : MonoBehaviour
{
    public GameObject spiritPrefab;
    public List<GameObject> spirits;
    public int numberOfSpirits;
    public float spiritSpawnRate;
    private float myTimer = 0.0f;
    public GameObject player;

    void SpawnSpirit()
    {
        GameObject newSpirit = Instantiate(spiritPrefab);
        Vector2 newPosition = new Vector2(Camera.main.transform.position.x, player.transform.position.y + 0.0f);
        //if player not falling
        if (player.GetComponent<Rigidbody2D>().velocity.y < -1.0f && player.transform.position.y > 20.0f)
        {
            newPosition += new Vector2(Random.Range(-5.0f, 5.0f), -15.0f);
            
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
        
        
        
        newSpirit.transform.position = newPosition;
        spirits.Add(newSpirit);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myTimer += Time.deltaTime;
        if (myTimer > spiritSpawnRate)
        {
            myTimer = 0.0f;
            if (player.transform.position.y > 2.0f)
            {
                SpawnSpirit();
            }
            
        }
    }
}
