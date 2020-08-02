using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [SerializeField]private float health = 100f;
    [SerializeField] private Transform shildSocket;

    public Transform jetPackSocket;
    public GameObject JetPack;
    public Camera mainCamra;

    public Transform gunSocket;
    public Transform armTarget;
    public GameObject[] guns;
    //public float handMinConstraint;
    //public float handMaxConstraint;

    public Animator animator;
    


    public int defaultGun = 0;
    private Transform gunBarrel;
    private GameObject activeShield;
    private int activeGunId;
    private Transform activeGun;

    void Start()
    {
    	Instantiate(guns[defaultGun], gunSocket.position, gunSocket.rotation);
        Instantiate(JetPack, jetPackSocket.position, jetPackSocket.rotation).GetComponent<Transform>().SetParent(jetPackSocket);
        activeShield = null;
    }

    void Update()
    {
        if(health <= 0)
        { KillPlayer(); }
    	rotateHand(true);
    }

    void rotateHand(bool isGunEquipt)
    {
    	if(isGunEquipt)
    	{
            Vector3 mousePos;
    		mousePos = Input.mousePosition;
    		mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            mousePos.z = 0f;
            
            armTarget.localPosition = transform.InverseTransformPoint(mousePos);
            Debug.Log("pistol");
        }
    }

    public void DealDamage(float damage)
    {
        if(activeShield != null)
        { health -= damage;}
    }

    public void Heal(float healAmt)
    {
        health += healAmt;
    }

    void KillPlayer()
    {
        mainCamra.transform.SetParent(null);
        Destroy(gameObject);
    }

    public Transform getShieldSocket()
    {
        return shildSocket;
    }

    public GameObject GetActiveSield()
    {
        return activeShield;
    }

    public void SetActiveShield(GameObject shield)
    {
        activeShield = shield;
    }

    public void ToggleAnimation(int gunId, Transform gun)
    {
        animator.SetBool("isGunEquipt", true);
        animator.SetInteger("gunId", gunId);
        activeGunId = gunId;
        activeGun = gun;
    }

    public void SetActiveId(int id)
    {
        activeGunId = id;
    }
}
