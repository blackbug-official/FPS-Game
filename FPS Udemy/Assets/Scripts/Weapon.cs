using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float Range = 100f;
    [SerializeField] float Damage = 25f;
    [SerializeField] ParticleSystem MuzzleFlashVFX;
    [SerializeField] GameObject HitVFX;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRayCast();
    }

    private void PlayMuzzleFlash()
    {
        MuzzleFlashVFX.Play();
    }

    private void ProcessRayCast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, Range))
        {
            CreateHitFx(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) { return; } // if ray hit something other than enemy , then we return
            target.TakeDamage(Damage);

        }
        else
        {
            return;
        }
    }

    private void CreateHitFx(RaycastHit hit)
    {
        GameObject HitImpact = Instantiate(HitVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(HitImpact, 0.1f);
    }
}
