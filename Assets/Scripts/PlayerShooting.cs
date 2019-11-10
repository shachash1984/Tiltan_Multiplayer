using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShooting : NetworkBehaviour {

    [SerializeField] private float _shotCoolDown = 0.3f;
    [SerializeField] private Transform _firePosition;
    [SerializeField] private ShotFX _shotFX;
    [SerializeField] private Transform FPSController;

    [SyncVar (hook = "OnScoreChanged")] private int _score;

    private float elapsedTime;
    private bool _canShoot;

    private void Start()
    {
        _shotFX.Initialize();
        if (isLocalPlayer)
            _canShoot = true;
        FPSController = transform.GetChild(1);
    }

    [ServerCallback]
    private void OnEnable()
    {
        _score = 0;
    }



    private void Update()
    {
        if (!_canShoot)
            return;

        elapsedTime += Time.deltaTime;
        if(Input.GetButtonDown("Fire1") && elapsedTime > _shotCoolDown)
        {
            elapsedTime = 0;

            CmdFireShot(_firePosition.position, FPSController.forward * 50f);
        }

    }

    [Command]
    void CmdFireShot(Vector3 origin, Vector3 direction)
    {
        RaycastHit hit;
        Ray ray = new Ray(origin, direction);
        Debug.DrawRay(ray.origin, ray.direction * 3f, Color.blue, 1f);
        bool result = Physics.Raycast(ray, out hit, 50f);
        if (result)
        {
            PlayerHealth enemy = hit.transform.GetComponent<PlayerHealth>();
            if (enemy)
            {
                bool wasKillShot = enemy.TakeDamge(25f);
                if (wasKillShot)
                    _score++;

            }
                
        }
        RpcProcessShotFX(result, hit.point);
    }



    [ClientRpc]
    void RpcProcessShotFX(bool playImpact, Vector3 point)
    {
        _shotFX.PlayShotEffects();
        if (playImpact)
            _shotFX.PlayImpactEffect(point);
    }

    private void OnScoreChanged(int value)
    {
        _score = value;
        if (isLocalPlayer)
            PlayerCanvas.S.SetKills(value);

    }
}
