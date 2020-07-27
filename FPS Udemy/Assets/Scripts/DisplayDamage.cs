 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] Canvas bloodCanvas;
    [SerializeField] float bloodTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        bloodCanvas.enabled = false;
    }

    // Update is called once per frame
    public void ShowDamageDisplay()
    {
        StartCoroutine(ShowBloodCanvas());
    }

    IEnumerator ShowBloodCanvas()
    {
        bloodCanvas.enabled = true;
        yield return new WaitForSeconds(bloodTime);
        bloodCanvas.enabled = false;
    }
}
