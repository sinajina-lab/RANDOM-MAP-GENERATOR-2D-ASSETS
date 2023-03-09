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

    private void Awake()
    {
        player = GameObject.Find("Nut").GetComponent<CameraFollowPlayer>();

        //collider = GetComponent<BoxCollider2D>();
        collider = GetComponent<CircleCollider2D>();

        //groundHeight = transform.position.y + (collider.size.y / 2);
        groundHeight = transform.position.y + (collider.radius / 2);
        screenRight = Camera.main.transform.position.x * 2;
    }

    
    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.x -= player.playerPosition.x * Time.fixedDeltaTime;

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
    void generateGround()
    {
        GameObject go = Instantiate(gameObject);
        //BoxCollider2D goCollider = go.GetComponent<BoxCollider2D>();
        CircleCollider2D goCollider = go.GetComponent<CircleCollider2D>();
        Vector2 pos;
        pos.x = screenRight + 30;
        pos.y = transform.position.y;
        go.transform.position = pos;
    }
}
