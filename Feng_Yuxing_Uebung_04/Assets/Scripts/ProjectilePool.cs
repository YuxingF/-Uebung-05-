using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] Projectile _projectilePrefab;
    
    List<Projectile> _inactiveProjectiles;
    
    void Start(){
        this._inactiveProjectiles = new List<Projectile>();
    }
    
    public Projectile GetProjectile(Vector3 position, Vector3 direction){
        Projectile projectile;

        if(this._inactiveProjectiles.Count > 0){
            projectile = this._inactiveProjectiles[0];
            this._inactiveProjectiles.Remove(projectile);
        }else{
            projectile = Instantiate(this._projectilePrefab, position, Quaternion.identity);
            projectile.transform.SetParent(this.transform);
        }
        projectile.Init(this, position, direction);
        projectile.gameObject.SetActive(true);
        return projectile;
    }

    public void AddProjectile(Projectile projectile){
        projectile.gameObject.SetActive(false);
        this._inactiveProjectiles.Add(projectile);
    }
}
