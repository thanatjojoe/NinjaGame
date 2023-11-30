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

    public void BuyItem(Transform item, int prise)
    {
        if (pointInShop >= prise)
        {
            Instantiate(item, dropPosition.position, Quaternion.identity);
            pointInShop -= prise;
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
