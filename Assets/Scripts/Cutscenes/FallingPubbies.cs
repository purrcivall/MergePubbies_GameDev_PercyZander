using UnityEngine;

//<Summary> This script is used for the falling Pubbies in the loading screen. - Zander :3 <Summary>
public class FallingPubbies : MonoBehaviour
{
    private float minX = -5f;
    private float maxX = 5f;

    private float startingMinY = -13;
    private float startingMaxY = 14;

    private float minY = 15;
    private float maxY = 16;

    private Rigidbody2D rb;

    //Not making this a method because I need to set Y differently at start, so these 2 methods are different
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(startingMinY, startingMaxY));
        transform.Rotate(0, 0, Random.Range(0, 360));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.linearVelocity /= 2; //Halfs velocity to prevent fast falling
        transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        transform.Rotate(0, 0, Random.Range(0, 360));
    }
}