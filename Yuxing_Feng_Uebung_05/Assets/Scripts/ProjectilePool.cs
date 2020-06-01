using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] Projectile _projectilePrefab;
    [SerializeField] bool _beAnActualPool = true;
    
    List<Projectile> _inactiveProjectiles;
    
    void Start(){
        this._inactiveProjectiles = new List<Projectile>();
    }
    
    public Projectile GetProjectile(Vector2 position, Vector2 direction){
        Projectile projectile;

        if(this._inactiveProjectiles.Count > 0 && this._beAnActualPool){
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
        if(this._beAnActualPool){
            projectile.gameObject.SetActive(false);
            this._inactiveProjectiles.Add(projectile);
        }else{
            Destroy(projectile.gameObject);
        }
        
    }
}
