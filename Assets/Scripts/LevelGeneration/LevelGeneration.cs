using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    private void Start()
    {
        int random = Random.Range(0, objects.Length);
        Instantiate(objects[random], transform.position, Quaternion.identity);
    }
}
