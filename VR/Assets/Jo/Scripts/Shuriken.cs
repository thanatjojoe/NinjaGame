using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public static float damageMultiplier = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            float swordSpeed = CalculateSpeed();
            float damage = swordSpeed * damageMultiplier;
            if (damage > 2)
            {
                SoundManager.instance.Play(SoundManager.SoundName.enemyHurt);
                other.GetComponent<EnemyHP>().Dead();
                Destroy(gameObject);
            }
            else
            {
                other.GetComponent<EnemyHP>().Stun();
            }
        }
    }
    private float CalculateSpeed()
    {
        
        // คำนวณความเร็วของการเหวี่ยงดาบ

        float swordSpeed = Mathf.Abs(OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch).y);
        return swordSpeed;
    }
}
