using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : MonoBehaviour
{
    //Rotation player;
    CameraFollowPlayer player;

    [SerializeField] float groundHeight;
    [SerializeField] float groundRight;
    [SerializeField] float screenRight;
    //private BoxCollider2D collider;
    private CircleCollider2D collider;

    bool didGenerateGround = false;

    // Name of the game object to use as the level part
    private string levelPartName = "level";
    // New list to hold spawned level parts
    [SerializeField] List<Transform> spawnedLevelParts;

    private void Awake()
    {
        player = GameObject.Find("Nut").GetComponent<CameraFollowPlayer>();

        //collider = GetComponent<BoxCollider2D>();
        collider = GetComponent<CircleCollider2D>();

        //groundHeight = transform.position.y + (collider.size.y / 2);
        groundHeight = transform.position.y + (collider.radius / 2);
        screenRight = Camera.main.transform.position.x * 2;

        // Initialize the list
        spawnedLevelParts = new List<Transform>();
    }

    
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.playerPosition.x * Time.fixedDeltaTime;
        //pos.x -= player.velocity.x * Time.fixedDeltaTime;

        //groundRight = transform.position.x + (collider.size.x / 2);
        groundRight = transform.position.x + (collider.radius / 2);

        if (groundRight < 0)
        {
            Destroy(gameObject);
            return;
        }

        if (!didGenerateGround)
        {
            if (groundRight < screenRight)
            {
                didGenerateGround = true;
                generateGround();
            }
        }

        transform.position = pos;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object has a name that contains "LevelPart"
        if (other.gameObject.name.Contains("LevelPart"))
        {
            // Update the level part name
            levelPartName = other.gameObject.name;

            // Add its transform to the list
            spawnedLevelParts.Add(other.transform);
        }
    }
    void generateGround()
    {
        // Get a random index from the list
        int randomIndex = Random.Range(0, spawnedLevelParts.Count);
        // Get the transform component of the randomly selected game object
        Transform spawnPoint = spawnedLevelParts[randomIndex];

        GameObject go = Instantiate(gameObject);
        //BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        CircleCollider2D goCollider = go.GetComponent<CircleCollider2D>();
        Vector2 pos;
        pos.x = screenRight + 30;
        pos.y = transform.position.y;
        go.transform.position = pos;
    }
}
