using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//basically a copy&paste of ProjectilePool. This couls be refactored in a generic aproach
public class EnemyPool : MonoBehaviour
{
    [SerializeField] Enemy _enemyPrefab;
    [SerializeField] bool _beAnActualPool = true;
    
    List<Enemy> _inactiveEnemies;
    
    void Start(){
        this._inactiveEnemies = new List<Enemy>();
    }
    
    public Enemy GetEnemy(Vector2 position){
        Enemy enemy;

        if(this._inactiveEnemies.Count > 0 && this._beAnActualPool){
            enemy = this._inactiveEnemies[0];
            this._inactiveEnemies.Remove(enemy);
        }else{
            enemy = Instantiate(this._enemyPrefab, position, Quaternion.identity);
            enemy.transform.SetParent(this.transform);
            
        }
        enemy.Init(this, position);
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    public void AddEnemy(Enemy enemy){
        if(this._beAnActualPool){
            enemy.gameObject.SetActive(false);
            this._inactiveEnemies.Add(enemy);
        }else{
            Destroy(enemy.gameObject);
        }
        
    }
}
