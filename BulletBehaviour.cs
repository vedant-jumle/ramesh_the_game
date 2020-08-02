using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	public float bulletSpeed;
	public float bulletEnergy;
	public float runTime;
	public float numberOfBounces;
    public float damageToPlayer;
    public GameObject test;
    public GameObject hitEffect;

    public Rigidbody2D rb;

    private GameObject spawnParent;
    private int shieldHitCount;
    private int playerOverlap;
    void Awake()
    {
        playerOverlap = 2;
        shieldHitCount = 2;
    	rb.velocity = transform.right * bulletSpeed;
    	runTime += Time.time;
        numberOfBounces++;
    }

    void Update()
    {
    	if(Time.time >= runTime)
    	{
    		destroy();
    	}
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        

        if(hitInfo.gameObject == spawnParent)
        {
            playerOverlap--;
        }


        //if player is detected
        if(hitInfo.CompareTag("Player") && playerOverlap <= 0)
        {
            PlayerMain player = hitInfo.GetComponent<PlayerMain>();
            player.DealDamage(damageToPlayer);
            Debug.Log("Damge dealt" + damageToPlayer);
            Instantiate(hitEffect, transform.position, transform.rotation).GetComponent<Transform>().right = -transform.right;
            destroy();
        }
    	numberOfBounces--;
    	if(numberOfBounces == 0)
    	{
            Instantiate(hitEffect, transform.position, transform.rotation).GetComponent<Transform>().right = -transform.right;
            destroy();
    	}


        //if a shield is detected;
        else if(hitInfo.CompareTag("Shield") && shieldHitCount == 0)
        {
            Instantiate(hitEffect, transform.position, transform.rotation).GetComponent<Transform>().right = -transform.right;
            Vector2 normal = -(transform.position - hitInfo.GetComponent<Transform>().position);
            rb.velocity = Vector2.Reflect(rb.velocity, Vector2.Perpendicular(normal)).normalized;
            //Instantiate(test, transform.position, transform.rotation).GetComponent<Transform>().right = Vector2.Perpendicular(-normal.normalized);
            rb.velocity = rb.velocity * bulletSpeed;
        }
        else if(hitInfo.CompareTag("Shield") && shieldHitCount != 0)
        {
            shieldHitCount--;
            Debug.Log(shieldHitCount);
        }
        else if(hitInfo.CompareTag("Platforms") || hitInfo.CompareTag("interactable"))
        {
            Instantiate(hitEffect, transform.position, transform.rotation).GetComponent<Transform>().right = -transform.right;
        }
    }

    //Destroy the bullet
    private void destroy()
    {
    	Destroy(gameObject);
    }
    
    public void SetSpanwer(GameObject spawner)
    {
        spawnParent = spawner;
    }

    public GameObject GetSpawner()
    {
        return spawnParent;
    }
}
