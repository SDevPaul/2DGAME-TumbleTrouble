using UnityEngine;

public class EnemyBat : MonoBehaviour
{

    // Variables that can be accessed outside the script
    public float speed; // Speed at which the Bat moves between points
    public int startingPoint; // Index of the starting point in the points array
    public Transform[] points; // Array of points (Transform) that the Bat will move between, these are game objects from the unity editor

    // Private variable used internally by the script
    private SpriteRenderer sprite; // Variable that contains the Bat sprite
    private int i; // Index to track the current target point in the points array

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial position of the platform to the specified starting point
        transform.position = points[startingPoint].position;

        // Fetching the Bat sprite compononet for modification 
        sprite = GetComponent<SpriteRenderer>();

        // Making sure that the Bat is looking at the right direction
        sprite.flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the bat is very close to the current target point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            // Move to the next point in the array
            i++;

            // If the end of the array is reached, loop back to the first point
            if (i == points.Length)
            {
                i = 0;
                 
            }
            // Flip the Bat sprite to face at where the next point is
            sprite.flipX = !sprite.flipX;  
        }

        // Move the Bat towards the current target point at the specified speed
        // Time.deltaTime ensures smooth movement regardless of frame rate
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

    }

}
