using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
	//public Transform nozzel;
	[HideInInspector]public Transform playerTransform;
	[HideInInspector]public CharacterController2D controller;
    
    public float velocity;
    public float fuel = 0f;
    public float resetTime = 0f;
    public float efficiency = 0f;

    private Rigidbody2D player;
    private Animator animator;
    private GameObject go_player;

    public Transform fuelMeter;
    public ParticleSystem[] particles;

    void Start()
    {
        go_player = transform.root.gameObject;
        playerTransform = go_player.GetComponent<Transform>();
        controller = go_player.GetComponent<CharacterController2D>();
        player = go_player.GetComponent<Rigidbody2D>();
    	animator = go_player.GetComponent<Animator>();
        StopParticles();
    }

    void Update()
    {
    	animator.SetBool("isFlying", !controller.IsGrounded());
    	if(Input.GetKey(KeyCode.Space))
    	{
    		player.velocity = playerTransform.up * velocity;
            PlayParticles();
    	}
        else
        {
            StopParticles();
        }
    }

    void FixedUpdate()
    {
        
    }

    void UpdateFuel()
    {

    }

    void StopParticles()
    {
        for(int i = 0;i < particles.Length;i++)
        {
            particles[i].Stop();
        }
    }

    void PlayParticles()
    {
        for(int i = 0;i < particles.Length;i++)
        {
            particles[i].Play();
        }
    }
}
