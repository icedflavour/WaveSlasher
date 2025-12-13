using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootInterval = 1.5f;
    public float shootRange = 10f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Transform target = FindNearestNPC();

            if (target != null)
            {
                Shoot(target);
                timer = 0f;
            }
        }
    }

    void Shoot(Transform target)
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            transform.position,
            Quaternion.identity
        );

        projectile.GetComponent<Projectile>().SetTarget(target);
    }

    Transform FindNearestNPC()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        Transform nearest = null;
        float minDistance = shootRange;

        foreach (GameObject npc in npcs)
        {
            float dist = Vector3.Distance(transform.position, npc.transform.position);

            if (dist < minDistance)
            {
                minDistance = dist;
                nearest = npc.transform;
            }
        }

        return nearest;
    }
}
