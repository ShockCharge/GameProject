using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform weaponHolder; // The transform where the weapon will be attached
    public GameObject[] weaponPrefabs; // Array of weapon prefabs to switch between
    public float weaponSize = 1.0f; // Default size for the weapon
    public float weaponRotation = 0.0f; // Default rotation for the weapon in degrees

    private GameObject equippedWeapon; // The currently equipped weapon
    private int currentWeaponIndex = 0; // Index to track the current weapon
    [SerializeField] public float attackRadius = 7.0f; // Radius for attack detection
    [SerializeField] public float destroyDelay = 1.0f; // Delay before the object disappears
    private string hitTag = "hitTag"; // Tag for destructible objects

    void Start()
    {
        EquipWeapon();
    }

    void Update()
    {
        // Check for TAB key press to switch weapons
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWeapon();
        }

        // Update weapon size
        if (equippedWeapon != null)
        {
            equippedWeapon.transform.localScale = Vector3.one * weaponSize; // Set the weapon size
        }

        // Attack on mouse click (example: left mouse button)
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    public void EquipWeapon()
    {
        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon); // Destroy the current weapon if it exists
        }

        // Instantiate the weapon prefab and set its parent to the weapon holder
        equippedWeapon = Instantiate(weaponPrefabs[currentWeaponIndex], weaponHolder.position, Quaternion.Euler(0, weaponRotation, 0), weaponHolder);

        // Adjust the weapon's position and rotation
        equippedWeapon.transform.localPosition = Vector3.zero; // Reset position
        equippedWeapon.transform.localRotation = Quaternion.Euler(0, weaponRotation, 0); // Set custom rotation
    }

    private void SwitchWeapon()
    {
        // Increment the index and wrap around if necessary
        currentWeaponIndex = (currentWeaponIndex + 1) % weaponPrefabs.Length;

        EquipWeapon(); // Equip the next weapon
    }

    private void Attack()
    {
        Vector3 attackPosition = weaponHolder.position;

        // Detect hits within the attack radius
        Collider[] hitColliders = Physics.OverlapSphere(attackPosition, attackRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the hit collider has the specified tag
            if (hitCollider.CompareTag(hitTag))
            {
                StartCoroutine(DestroyAfterDelay(hitCollider.gameObject)); // Destroy after delay
            }
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(destroyDelay); // Wait for the specified delay
        Destroy(obj); // Destroy the object
    }
}
