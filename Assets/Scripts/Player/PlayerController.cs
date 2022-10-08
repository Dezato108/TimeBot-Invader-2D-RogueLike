using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigidbody;    
    [SerializeField] Transform weaponsArm;
    private Camera mainCamera;

    [SerializeField] int movementSpeed;

    private Vector2 movementInput;

    private Animator playerAnimator;    

    private float currentMovementSpeed;
    private bool canDash;

    [SerializeField] float dashSpeed = 20f, dashLength = 0.5f, dashCoolDown = 1f;

    [SerializeField] List<WeaponsSystem> availableWeapons = new List<WeaponsSystem>();
    private int currentGun;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        playerAnimator = GetComponent<Animator>();
        currentMovementSpeed = movementSpeed;
        canDash = true;

        for (int i = 0; i < availableWeapons.Count; i++)
        {
            if (availableWeapons[i].gameObject.activeInHierarchy)
            {
                currentGun = i;
            }
        }
        SettingWeaponsUI();
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoving();
        PointingGunAtMouse();
        AnimatingThePlayer();
        PlayerDashing();
        SwitchGun();
    }

    public void SwitchGun()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ActualGunSwitch();
        }
    }

    private void ActualGunSwitch()
    {
        if (availableWeapons.Count > 0)
        {
            currentGun++;
            if (currentGun >= availableWeapons.Count)
            {
                currentGun = 0;
            }

            foreach (WeaponsSystem weapon in availableWeapons)
            {
                weapon.gameObject.SetActive(false);
            }

            availableWeapons[currentGun].gameObject.SetActive(true);
            SettingWeaponsUI();
        }
        else
        {
            Debug.LogWarning("No guns available, pick one up");
        }
    }

    private void SettingWeaponsUI()
    {
        UIManager.instance.ChangeWeaponUI(
            availableWeapons[currentGun].GetComponent<WeaponsSystem>().GetWeaponImageUI(),
            availableWeapons[currentGun].GetComponent<WeaponsSystem>().GetWeaponName()
            );
    }

    private void PlayerDashing()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            currentMovementSpeed = dashSpeed;
            canDash = false;

            playerAnimator.SetTrigger("Dash");
            GetComponent<PlayerHealthHandler>().MakePlayerInvincible();
            //Access the player health handler and make sure he is invincible for a while

            StartCoroutine(DashCooldownCounter());
            StartCoroutine(DashLengthCounter());
        }
    }

    IEnumerator DashCooldownCounter()
    {
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }

    IEnumerator DashLengthCounter()
    {
        yield return new WaitForSeconds(dashLength);
        currentMovementSpeed = movementSpeed;
    }

    private void AnimatingThePlayer()
    {
        if (movementInput != Vector2.zero)
        {
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }

    private void PointingGunAtMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(transform.localPosition);

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        weaponsArm.rotation = Quaternion.Euler(0, 0, angle);

        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            weaponsArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            weaponsArm.localScale = Vector3.one;
        }
    }    

    private void PlayerMoving()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");

        movementInput.Normalize();

        playerRigidbody.velocity = movementInput * currentMovementSpeed;
    }

    public bool PlayerIsDashing()
    {
        if (currentMovementSpeed == dashSpeed)
            return true;        
        else        
            return false;
        
    }

    public void AddToAvailableWeapons(WeaponsSystem weaponToAdd)
    {
        availableWeapons.Add(weaponToAdd);
        currentGun = availableWeapons.Count - 2;
        ActualGunSwitch();
    }

    public List<WeaponsSystem> GetAvailableWeaponsOnPlayer(){ return availableWeapons;}

    public Transform GetWeaponsArm() { return weaponsArm; }
    //public bool isPlayerDashing() { return canDash; }
}
