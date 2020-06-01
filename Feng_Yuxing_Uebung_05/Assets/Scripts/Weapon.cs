using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] float _shootingCooldown;
    [SerializeField] ProjectilePool _projectilePool;
    protected Transform _transform;
    Camera _camera;
    float _cooldownCounter = 0f;
    AudioSource audios;


    void Start(){
        
        this._transform = transform;
        this._camera = Camera.main;
        audios = GetComponent<AudioSource>();
    }
    
    void Update(){
        this._cooldownCounter += Time.deltaTime;
    }

    public void Shoot(){
        if(this._cooldownCounter < this._shootingCooldown) return;
        if(Input.GetAxis("Fire1") <= 0 || this._cooldownCounter < this._shootingCooldown) return; //escape if not shooting or not able to shoot
        Vector2 mousePos = this._camera.ScreenToWorldPoint(Input.mousePosition);
        audios.Play();
        this.Shoot(this._projectilePool, this._transform.position, this._camera.ScreenToWorldPoint(Input.mousePosition) - this._transform.position);
        
        this._cooldownCounter = 0f;
    }

    protected virtual void Shoot(ProjectilePool pool, Vector2 position,  Vector2 direction){
        pool.GetProjectile(position, direction);
    }
}
