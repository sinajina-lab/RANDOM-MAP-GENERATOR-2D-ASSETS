using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    static ItemCollector instance;

    [SerializeField] int collectable = 0;

    [SerializeField] TMP_Text landedNuts;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        landedNuts.text = "" + collectable;
    }
    void IncreasePoints()
    {
        
        landedNuts.text = "" + collectable;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("nut"))
        {
            collectable++;
            landedNuts.text = "" + collectable;
            Debug.Log("Nut:" + collectable);
            //collectable = collectable + 1;
            //Destroy(collision.gameObject);
        }
    }
}
