using UnityEngine;

[CreateAssetMenu(fileName = "NewScriptableObjectScript", menuName = "Scriptable Objects/NewScriptableObjectScript")]
public class NewScriptableObjectScript : ScriptableObject
{
    public float damage;
    public float attackSpeed;
    public float range;
    public GameObject projectilePrefab; 
    
    public float splashRadius; 
    public int bounceCount;
    
    public bool isMelee; 
    public bool isProjectile;
    public bool isTargeted;
}
