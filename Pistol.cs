using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon_base
{
    void Start()
    {
        gunId = 0;
        animator = transform.root.GetComponent<Animator>();
        
        socket = GameObject.FindWithTag("GunSocket");
        transform.SetParent(socket.GetComponent<Transform>());
        transform.SetPositionAndRotation(socket.GetComponent<Transform>().position, socket.GetComponent<Transform>().rotation);
        transform.root.GetComponent<PlayerMain>().ToggleAnimation(gunId, transform);
        Debug.Log(transform.root.name);
    }

    void Update()
    {

        if (Time.time >= end_animation_time)
        {
            gunAnimator.SetBool("Fire", false);
            isAnimationPlaying = false;
        }

        if (Input.GetButtonDown("Fire1") && !isAnimationPlaying && !disableShooting) { Shoot(); }


    }

    private void Shoot()
    {
        Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation).GetComponent<BulletBehaviour>().SetSpanwer(transform.root.gameObject);
        gunAnimator.SetBool("Fire", true);
        end_animation_time = Time.time + recoil_animation_time;
        isAnimationPlaying = true;
    }
}
