using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float offset;
    [SerializeField] float offsetSmoothing;
    
    internal Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, 0, -10f);
        //playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        if(player.transform.localScale.x > 0.0f)
        {
            playerPosition = new Vector3(playerPosition.x + 0, 0,-10f);
            //playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y, playerPosition.z);
        }

        else
        {
            playerPosition = new Vector3(playerPosition.x - 0, 0,-10f);
            //playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        }
        transform.position = Vector3.Lerp(playerPosition, playerPosition, 0);
        //transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
