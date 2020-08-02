using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_base : MonoBehaviour
{
    [SerializeField] protected bool disableShooting;

	//gun socket info
	protected GameObject socket;
	protected Animator animator;

	//about bullet
	public float fireRate;
	public GameObject bullet;
	public Transform bulletSpawn;
	public Animator gunAnimator;

	//about recoil animation
	protected bool isAnimationPlaying;
	public float recoil_animation_time;
	protected float end_animation_time;

    public int gunId;
    protected string[] triggers = { "isGunEquipt", "gunId"};
    
}
