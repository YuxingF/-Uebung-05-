using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]EnemyPool _pool;
    [SerializeField]Vector3 _position;
    Transform _transform;
    float frequency = 0.3f;
    float timeCounter = 0f;

    void Start()
    {   this._transform = transform;
        transform.position = new Vector3 (9.5f,6.5f,0);
        this._position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.timeCounter += Time.deltaTime;
        if(this.timeCounter >= this.frequency)
            {
                getEnemy();
                transform.RotateAround(new Vector3 (-0.28f,1.27f, 0f), new Vector3 (0f, 0f, 1f), 10f);
                this._position = transform.position;
            } 
    }
    void getEnemy(){
        this.timeCounter = 0f;
        _pool.GetEnemy(_position);
        _pool.GetEnemy(_position);
    }
}
