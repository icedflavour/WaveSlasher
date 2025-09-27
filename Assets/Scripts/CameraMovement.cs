using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;

    void FixedUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z - 10);
    }
}
