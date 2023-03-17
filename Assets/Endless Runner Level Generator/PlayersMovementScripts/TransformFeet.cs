using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformFeet : MonoBehaviour
{
    [SerializeField] float jumpHeight = 5;
    [SerializeField] float gravityScale = 5;

    float velocity;

    [SerializeField] float floorHeight = 0.5f;
    [SerializeField] Transform feet;
    [SerializeField] ContactFilter2D filter;

    bool isGrounded;

    Collider2D[] results = new Collider2D[1];

    // Update is called once per frame
    void Update()
    {
        //Add gravity to objects velocity
        velocity += Physics2D.gravity.y * gravityScale * Time.deltaTime;

        //If the object isGrounde reset the object to zero and snap it to the floor
        //if (Physics2D.OverlapBox(feet.position, feet.localScale, 0, filter, results) > 0 && velocity < 0)
        if (Physics2D.OverlapCircle(feet.position, floorHeight, filter, results) > 0 && velocity < 0)
        {
            velocity = 0;
            Vector2 surface = Physics2D.ClosestPoint(transform.position, results[0]) + Vector2.right * floorHeight;
            transform.position = new Vector3(transform.position.x, surface.y, transform.position.z);

            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if(Input.GetMouseButtonDown(0) && isGrounded)
        {
            velocity = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * gravityScale));
        }

        transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);
    }
}
