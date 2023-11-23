using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToPlayer : MonoBehaviour
{
    private int enemyDmg = 30;

    public NinjaEnemy _ninjaEnemy;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void damageToPlayer()
    {
        Player.playerHP = Player.playerHP - enemyDmg;
    }

    private void OnEnable()
    {
        if (_ninjaEnemy.onAttack == true)
        {
            Player.OnCollisionWithEnemy += damageToPlayer;
        }
    }
    private void OnDisable()
    {
        Player.OnCollisionWithEnemy -= damageToPlayer;
    }
}
