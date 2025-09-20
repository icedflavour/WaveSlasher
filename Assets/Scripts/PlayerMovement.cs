using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public int Score;

    void Update()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        transform.position = transform.position + new Vector3(h * movementSpeed * Time.deltaTime, v * movementSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectable")
        {
            Score += 1;
            Destroy(other);
        }
    }
}
