using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;
    public GameObject notch;

    private XRGrabInteractable _bow;
    private bool arrowNotched = false;
    private GameObject currentArrow = null;

    public int totalArrow = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        _bow = GetComponent<XRGrabInteractable>();
        Pull.PullActionReleased += NotchEmpty;
    }

    private void OnDestroy()
    {
        Pull.PullActionReleased -= NotchEmpty;
    }

    // Update is called once per frame
    void Update()
    {
        if (_bow.isSelected && arrowNotched == false && totalArrow > 0)
        {
            totalArrow--;
            Debug.Log("arrow number " +totalArrow);
            arrowNotched = true;
            StartCoroutine("DelayedSpawn");
        }

        if (!_bow.isSelected && currentArrow != null)
        {
            Destroy(currentArrow);
            NotchEmpty(1f);
        }
    }

    private void NotchEmpty(float value)
    {
        arrowNotched = false;
        currentArrow = null;
    }

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(1f);
        currentArrow = Instantiate(arrow, notch.transform);
    }

    public void IncreaseArrow()
    {
        totalArrow += 1;
    }
}
