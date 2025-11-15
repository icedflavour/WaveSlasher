using UnityEngine;

public class FindNearEnemy : MonoBehaviour
{
    public GameObject Player;
    private Transform closestEnemy;
    private float closestDistance;

    void Update()
    {
        FindNearestEnemy();
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {

            float distance = Vector2.Distance(Player.transform.position, enemy.transform.position);
          
            if(distance > closestDistance)
            { 
            closestDistance = distance;
            closestEnemy = enemy.transform;
            }

        }
        if (closestEnemy != null)
        {
            Debug.Log("Closest Enemy: " + closestEnemy.name);
        }
    }
}