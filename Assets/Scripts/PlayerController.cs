using UnityEngine;
using UnityEngine.UI; // Import for UI functionality
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Camera mainCamera; // Assign the Main Camera in the inspector
    public Transform bulletSpawn; // Where bullets spawn
    public GameObject bulletPrefab;

    public enum WeaponType
    {
        Pistol,
        MachineGun,
        FullCircleMachineGun
    }

    public WeaponType currentWeapon = WeaponType.Pistol;
    public float machineGunFireRate = 0.1f; // Time between shots for machine gun
    private float lastFireTime = 0f;

    private int enemyKills = 0; // Track enemy kills
    public TMP_Text upgradePopupText; // Assign a Text UI element for popup messages
    private bool isPopupActive = false; // To prevent popup spam

    void Update()
    {
        // Player movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        // Rotate player towards mouse position
        RotateToMouse();

        // Weapon shooting or rotating logic
        switch (currentWeapon)
        {
            case WeaponType.Pistol:
                if (Input.GetMouseButtonDown(0)) // Left Mouse Button
                {
                    Shoot();
                }
                break;

            case WeaponType.MachineGun:
                if (Input.GetMouseButton(0)) // Hold to shoot
                {
                    if (Time.time - lastFireTime >= machineGunFireRate)
                    {
                        Shoot();
                        lastFireTime = Time.time;
                    }
                }
                break;

            case WeaponType.FullCircleMachineGun:
                if (Input.GetMouseButton(0)) // Hold to shoot in all directions
                {
                    if (Time.time - lastFireTime >= machineGunFireRate)
                    {
                        Shoot360();
                        lastFireTime = Time.time;
                    }
                }
                break;
        }
    }

    void RotateToMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 lookDirection = hitInfo.point - transform.position;
            lookDirection.y = 0; // Keep the player rotation on the XZ plane
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            // Instantiate bullet and shoot towards mouse click position
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);

            // Calculate direction to target
            Vector3 direction = (hitInfo.point - bulletSpawn.position).normalized;

            // Add force to the bullet in the direction of the target
            bullet.GetComponent<Rigidbody>().AddForce(direction * 1000f); // Adjust force as needed
        }
    }

    void Shoot360()
    {
        // Shoot in all directions around the player
        for (int i = 0; i < 360; i += 30) // Shoot in 12 directions (30 degrees apart)
        {
            Quaternion rotation = Quaternion.Euler(0, i, 0);
            Vector3 direction = rotation * Vector3.forward;

            // Instantiate bullet and shoot in this direction
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(direction * 1000f); // Adjust force as needed
        }
    }

    // Call this method when an enemy is killed
    public void OnEnemyKilled()
    {
        enemyKills++;

        if (enemyKills >= 20 && currentWeapon == WeaponType.Pistol)
        {
            UpgradeWeapon(WeaponType.MachineGun, "Weapon upgraded: Machine Gun!");
        }
        else if (enemyKills >= 30 && currentWeapon == WeaponType.MachineGun)
        {
            UpgradeWeapon(WeaponType.FullCircleMachineGun, "Weapon upgraded: 360° Machine Gun!");
        }
    }

    // Handle upgrading the weapon and showing popup
    void UpgradeWeapon(WeaponType newWeaponType, string popupMessage)
    {
        if (currentWeapon != newWeaponType)
        {
            currentWeapon = newWeaponType;

            // Display upgrade popup message
            ShowPopup(popupMessage);
        }
    }

    // Display a popup message
    void ShowPopup(string message)
    {
        if (!isPopupActive)
        {
            isPopupActive = true;
            upgradePopupText.text = message;
            Invoke("HidePopup", 2f); // Hide after 2 seconds
        }
    }

    // Hide the popup message
    void HidePopup()
    {
        upgradePopupText.text = "";
        isPopupActive = false;
    }

    // Method to change weapon based on UI button click
    public void ChangeWeapon(int weaponIndex)
    {
        switch (weaponIndex)
        {
            case 0:
                currentWeapon = WeaponType.Pistol;
                break;
            case 1:
                currentWeapon = WeaponType.MachineGun;
                break;
            case 2:
                currentWeapon = WeaponType.FullCircleMachineGun;
                break;
        }

        // Optionally, you can add a popup or visual indicator to show the weapon change
        ShowPopup("Weapon changed to: " + currentWeapon.ToString());
    }
}
