using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyPool _pool;
    [SerializeField] float _spawnFrequency = .5f;

    Transform _transform;
    float _counter = 0f;
    
    void Awake(){
        this._transform = transform;
    }
    
    void Update(){
        this._counter += Time.deltaTime;

        if(this._counter >= this._spawnFrequency){
            this.SpawenEnemy();
        }
    }


    void SpawenEnemy(){
        this._pool.GetEnemy(transform.position);

        this._counter = 0f;
    }
}
