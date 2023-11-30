using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using UnityEngine;

public class SoulShop : MonoBehaviour
{
    public static SoulShop instance;
    [SerializeField] private int pointInShop;
    [SerializeField] private TextAnimator_TMP soulText;
    [SerializeField] private Fire _fire;
    private float soulPointCounter;
    public Transform dropPosition;

    private int arrowPrise;
    private int campFirePrise;
    private int shurikanPrise;
    
    public Transform stick;
    public Transform shuriken;

    public int PointInShop
    {
        get { return pointInShop; }
    }
    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (soulPointCounter < pointInShop)
        {
            soulPointCounter += 100 * Time.deltaTime;
            if (soulPointCounter > pointInShop)
            {
                soulPointCounter = pointInShop;
            }
        }
        else if (soulPointCounter > pointInShop)
        {
            soulPointCounter -= 100 * Time.deltaTime;
            if (soulPointCounter < pointInShop)
            {
                soulPointCounter = pointInShop;
            }
        }

        _fire.fireTime = Convert.ToSingle(pointInShop);
        
        soulText.SetText(Convert.ToInt32(soulPointCounter).ToString());
    }

    public void AddSoul(int soul)
    {
        pointInShop += soul;
    }

    public void BuyStickItem()
    {
        if (pointInShop >= campFirePrise)
        {
            Instantiate(stick, dropPosition.position, Quaternion.identity);
            pointInShop -= campFirePrise;
        }
    }
    
    public void BuyShurikanItem()
    {
        if (pointInShop >= shurikanPrise)
        {
            Instantiate(shuriken, dropPosition.position, Quaternion.identity);
            pointInShop -= shurikanPrise;
        }
    }

    public void BuyArrow()
    {
        if (pointInShop >= arrowPrise)
        {
            ArrowSpawner arrowSpawner = GameObject.FindGameObjectWithTag("Bow").GetComponent<ArrowSpawner>();
            arrowSpawner.IncreaseArrow();
            pointInShop -= arrowPrise;
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.onShop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.onShop = false;
        }
    }
}
