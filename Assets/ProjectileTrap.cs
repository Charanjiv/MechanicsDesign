using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileTrap : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectile;
    private float cooldownTimer;

    private void attack()
    {
        cooldownTimer = 0;

        projectile[FindProjectile()].transform.position = firePoint.position;
        projectile[FindProjectile()].GetComponent<Projectile>().ActivateProjectile();
    }

    private int FindProjectile()
    {
        for (int i = 0; i < projectile.Length; i++)
        {
            if (!projectile[i].activeInHierarchy) return i;
        }
        return 0;
    }

    private void Update()
    {
        if(cooldownTimer>=attackCooldown)
        {
            attack();

        }
    }
}
