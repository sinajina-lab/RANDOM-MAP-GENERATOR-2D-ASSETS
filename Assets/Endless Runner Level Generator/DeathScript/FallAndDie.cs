using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallAndDie : MonoBehaviour
{
    [SerializeReference] GameObject player;

    [SerializeField] Transform spawnPoint;

    [SerializeField] float spawnValue;

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < -spawnValue)
        {
            //ResetGame();
            //RespawnPoint();
        }
    }
    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void RespawnPoint()
    {
        //transform.position = spawnPoint.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "death")
        {
            //transform.position = spawnPoint.position;
            ResetGame();
        }
    }
}
