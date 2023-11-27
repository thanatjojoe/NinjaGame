using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine;

public class SoulShop : MonoBehaviour
{
    public static SoulShop instance;
    [SerializeField] private int pointInShop;
    [SerializeField] private TextAnimator_TMP soulText;
    [SerializeField] private Fire _fire;
    private float soulPointCounter;

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
