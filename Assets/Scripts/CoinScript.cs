using UnityEngine;

public class CoinScript : MonoBehaviour
{
    private float yAng;

    void FixedUpdate()
    {
        yAng = transform.rotation.y;
        transform.Rotate(0, yAng + 4, 0);
    }



}
