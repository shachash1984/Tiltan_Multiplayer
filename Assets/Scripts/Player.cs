using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Events;
using UnityEngine.UI;

public enum WeaponType { M16 = 0, AK47, Shotgun}

[System.Serializable]
public class ToggleEvent : UnityEvent<bool> { }

public class Player : NetworkBehaviour {
    
    [SyncVar (hook = "OnNameChanged")] public string crushemPlayerName;
    [SyncVar (hook = "OnColorChanged")] public Color crushemPlayerColor;

    [SerializeField] private ToggleEvent OnToggleShared;
    [SerializeField] private ToggleEvent OnToggleLocal;
    [SerializeField] private ToggleEvent OnToggleRemote;
    [SerializeField] private float _respawnTime = 5f;
    private GameObject mainCamera;
    private NetworkAnimator _anim;

    private void Start()
    {
        _anim = GetComponent<NetworkAnimator>();
        mainCamera = Camera.main.gameObject;
        EnablePlayer();
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;

        _anim.animator.SetFloat("Speed", Input.GetAxis("Vertical"));
        _anim.animator.SetFloat("Strafe", Input.GetAxis("Horizontal"));
    }

    private void DisablePlayer()
    {
        if (isLocalPlayer)
        {
            PlayerCanvas.S.HideReticle();
            mainCamera.SetActive(true);
        }
            

        OnToggleShared.Invoke(false);

        if (isLocalPlayer)
            OnToggleLocal.Invoke(false);
        else
            OnToggleRemote.Invoke(false);
    }

    private void EnablePlayer()
    {

        if (isLocalPlayer)
        {
            PlayerCanvas.S.Initialize();
            mainCamera.SetActive(false);
        }
            

        OnToggleShared.Invoke(true);

        if (isLocalPlayer)
            OnToggleLocal.Invoke(true);
        else
            OnToggleRemote.Invoke(true);
    }

    public void Die()
    {
        if(isLocalPlayer)
        {
            PlayerCanvas.S.WriteGameStatusText("<color=red>You Died!</color>");
            PlayerCanvas.S.PlayDeathAudio();
            _anim.SetTrigger("Died");
        }

        DisablePlayer();
        Invoke("Respawn", _respawnTime);
    }

    public void Respawn()
    {
        if (isLocalPlayer)
        {
            Transform spawn = NetworkManager.singleton.GetStartPosition();
            transform.position = spawn.position;
            transform.rotation = spawn.rotation;
            _anim.SetTrigger("Restart");
        }

        EnablePlayer();
    }

    public void OnNameChanged(string n)
    {
        Debug.Log(name + " OnNameChanged " + n);
        crushemPlayerName = n;
        gameObject.name = crushemPlayerName;
        GetComponentInChildren<Text>(true).text = crushemPlayerName;
    }

    public void OnColorChanged(Color col)
    {
        Debug.Log(name + " OnColorChanged " + col);
        crushemPlayerColor = col;
        GetComponentInChildren<RendererToggler>(true).ChangeColor(crushemPlayerColor);
    }

    /*private Camera _playerCamera;
    private FirstPersonController _FPSController;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Transform _weaponBarrel;
    [SyncVar] public string playerName = "N/A";
    [SyncVar] public Color playerColor = Color.white;
    public WeaponType weaponType;
    public GameObject[] weapons;

    private void Awake()
    {
        _playerCamera = GetComponentInChildren<Camera>();
        _FPSController = GetComponentInChildren<FirstPersonController>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
        _playerCamera.gameObject.SetActive(isLocalPlayer);
        _FPSController.enabled = isLocalPlayer;
        //Debug.Log(playerName);
        //GetComponent<Renderer>().material.color = playerColor;
        //SetWeapon(weaponType);
        
    }

    private void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(_playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)), _playerCamera.transform.forward);
            _weapon.Attack(ray);
        }
    }

    public void SetWeapon(WeaponType wt)
    {
        int weaponIndex = 0;
        switch (wt)
        {
            case WeaponType.M16:
                weaponIndex = 0;
                break;
            case WeaponType.AK47:
                weaponIndex = 1;
                break;
            case WeaponType.Shotgun:
                weaponIndex = 2;
                break;
            default:
                break;
        }
        GameObject go = Instantiate(weapons[weaponIndex], transform.position, Quaternion.identity ,transform);
        _weapon = go.GetComponent<Weapon>();
    }*/

}
