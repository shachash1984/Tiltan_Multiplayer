using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Destructable))]
public class DestructablePlayer : MonoBehaviour {

    public Slider slider;
    private Destructable destructable;


    private void Awake()
    {
        destructable = GetComponent<Destructable>();
        
        slider.value = slider.maxValue;

    }

    private void OnEnable()
    {
        destructable.EventOnTakeDamage += UpdateHPValue;
    }

    private void OnDisable()
    {
        destructable.EventOnTakeDamage -= UpdateHPValue;
    }

    void UpdateHPValue(float newHP, Vector3 shotDir)
    {
        slider.value = newHP;
    }

}
