using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon;

    void Awake()
    {
        weapon = Instantiate(weaponHolder);
        weapon.gameObject.SetActive(false);
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check kalau player masuk trigger zone.
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Objek Player Memasuki trigger");
            Weapon currentWeapon = other.GetComponentInChildren<Weapon>(); // cek kalau player punya weapon atau tidak
            
            if (currentWeapon != null) 
            { 
                currentWeapon.transform.SetParent(null); // Remove weapon yang digunakan
                currentWeapon.gameObject.SetActive(false);// mematikan visual senjata
            }

            weapon.transform.SetParent(other.transform);
            weapon.parentTransform = other.transform; 
            weapon.transform.localPosition = Vector3.zero;

            // Aktifkan visual dari weapon
            TurnVisual(true);


        }
        else 
        {
            Debug.Log("Bukan Objek Player yang memasuki Trigger");
        }

    }

    // turn on/off dari weapon.
    void TurnVisual(bool on)
    {
        weapon.gameObject.SetActive(on);
    }

    // turn on/off dari weapon juga?
    void TurnVisual(bool on, Weapon weapon)
    {
        weapon.gameObject.SetActive(on);
    }
}
