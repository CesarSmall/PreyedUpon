using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ShooterGun : MonoBehaviour
{
    [SerializeField] private InputActionReference LeftShoot, RightShoot;
    [SerializeField] private Transform barrel;
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private int bullets = 6;

    [SerializeField] private AudioSource emptyMag, shootSound;
    [SerializeField] private ShooterManager manager;

    internal RaycastHit hit;
    internal int shotsremaining;
    private bool loaded = true, pickedUp = false;


    private void Awake()
    {
        manager = GameObject.FindObjectOfType<ShooterManager>();
        LeftShoot.action.performed += OnShoot;
        RightShoot.action.performed += OnShoot;
    }
    private void Start()
    {
        shotsremaining = bullets;
    }
    private void Update()
    {
        #region debuggers

        Debug.DrawRay(barrel.position, (barrel.forward * maxDistance), Color.white);
        #endregion

        VictoryCheck();
    }

    private void OnShoot(InputAction.CallbackContext obj)
    {
        if (IsPickedUp()) CheckForBulletsInMag();
    }

    #region shoot functions

    public bool IsPickedUp()
    {
        return true;
    }

    private void VictoryCheck()
    {
        if (manager.reset && !loaded)
        {
            reset();
        }
    }

    private void CheckForBulletsInMag()
    {
        if (shotsremaining > 0)
        {
            Shoot();
        }
        else if (shotsremaining <= 0)
        {
            PlayEmptyMagSound();
        }
    }

    private void Shoot()
    {
        Physics.Raycast(barrel.position, barrel.forward, out hit, maxDistance);
        shotsremaining--;
        shootSound.PlayOneShot(shootSound.clip);
    }

    private void PlayEmptyMagSound()
    {
        emptyMag.Play();
        loaded = false;
    }

    private void reset()
    {
        shotsremaining = bullets;
        loaded = true;
        Debug.Log($"Reset revolver, {shotsremaining} shots in gun");
    }


    #endregion
}
