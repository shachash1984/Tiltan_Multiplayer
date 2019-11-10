using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    [SerializeField] [Range(10, 500)] protected int _damage;
    [SerializeField] [Range(0.05f, 5f)] protected float _attackRate;
    [SerializeField] private GameObject VFX;
    [SerializeField] private AudioSource SFX;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Vector3 _carryPosition;
    [SerializeField] private Vector3 _carryRotation;
    private float _timer;

    private void Awake()
    {
        if (VFX)
            VFX.SetActive(false);
    }

    public void Attack(Ray directionRay)
    {
        if((Time.time - _timer) > _attackRate)
        {
            //if (VFX)
            //    VFX.SetActive(false);
            RaycastHit hit;
            if(Physics.Raycast(directionRay, out hit, 150f, _layerMask))
            {
                Debug.Log(hit.collider.name);
                Destructable d = hit.collider.gameObject.GetComponent<Destructable>();
                //VFX.SetActive(true);
                if (d)
                    d.TakeDamage(_damage, transform.position);
                _timer = Time.time;
            }
        }
    }

    public void SetPosition()
    {
        transform.position = _carryPosition;
    }

    public void SetRotation()
    {
        transform.rotation = Quaternion.Euler(_carryRotation);
    }
}
