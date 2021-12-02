using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider bar;

    private void Start()
    {
        bar = GetComponent<Slider>();
    }

    public void SetHealthValueOf(float healthValue)
    {
        bar.value = healthValue;
    }
    public void DeactivateHealthBar()
    {
        bar.gameObject.SetActive(false);
    }
}
