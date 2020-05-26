using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
   
    [SerializeField] Enemy _enemyPrefab;
    List<Enemy> _inactiveEnemy;
    void Start()
    {
        this._inactiveEnemy = new List<Enemy>();
    }
    public Enemy GetEnemy(Vector3 position){
        Enemy enemy;
        if(this._inactiveEnemy.Count > 0){
            enemy = this._inactiveEnemy[0];
            this._inactiveEnemy.Remove(enemy);
        }else{
            enemy = Instantiate(this._enemyPrefab, position, Quaternion.identity);
            enemy.transform.SetParent(this.transform);
        }
        enemy.Init(this);
        enemy.gameObject.SetActive(true);
        return enemy;
    }
        public void AddEnemy(Enemy enemy){
        enemy.gameObject.SetActive(false);
        this._inactiveEnemy.Add(enemy);
    }
}