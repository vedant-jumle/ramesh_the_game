using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public bool anim;
    public AI_base ai;

    void Update()
    {
        ai.anim = anim;
    }
}
