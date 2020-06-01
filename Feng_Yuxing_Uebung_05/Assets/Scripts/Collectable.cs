using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] Weapon _weapon;
    private void OnTriggerEnter2D(Collider2D other) {
        PlayerController pc = other.GetComponent<PlayerController>();
        if(!pc) return;

        pc.AddWeapon(this._weapon);
        Destroy(gameObject);
    }
}
