using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 30f;
 
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void EnemyAttackHitEvent()
    {
        if(target == null) { return; }
        target.TakeDamage(damage);
        print("enemy attacking player");
    }
}
