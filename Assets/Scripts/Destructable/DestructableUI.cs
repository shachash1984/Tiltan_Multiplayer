using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Destructable))]
public class DestructableUI : MonoBehaviour {

    public Slider HUDSlider;
    public Slider extSlider;
    public Transform canvas;
    private Destructable destructable;
    


    private void Awake()
    {
        destructable = GetComponent<Destructable>();
        
        if (!canvas)
            canvas = extSlider.transform.parent;
        extSlider.value = extSlider.maxValue;
        HUDSlider.value = HUDSlider.maxValue;
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
        extSlider.value = newHP;
        HUDSlider.value = newHP;
        canvas.LookAt(shotDir);
    }

}
