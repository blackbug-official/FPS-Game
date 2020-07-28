using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float Range = 100f;
    [SerializeField] float Damage = 25f;
    [SerializeField] ParticleSystem MuzzleFlashVFX;
    [SerializeField] GameObject HitVFX;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShoot = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;
    AudioSource FiringAudio;
    

    bool canShoot = true;

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Start()
    {
        FiringAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButton(0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            FiringAudio.Play();
            ProcessRayCast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShoot);
        canShoot = true;
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
