using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField] float _speed = 5.0f;
    [SerializeField] float _ttl = 5f;
    Rigidbody2D rigidbody2D;

    ProjectilePool _pool;
    Vector3 _direction;
    Transform _transform;
    Camera _camera;
    float _ttlCounter = 0f;
    
    public virtual Projectile Init(ProjectilePool pool, Vector3 position, Vector3 direction){
        this._pool = pool; //the caller sets the corresponding objectpool
        this._transform.position = position;
        this._direction = direction;
        this._direction.z = 0;
        this._direction = this._direction.normalized; //we normalize AFTER setting z to 0, otherwise we dont have vectors of size 1 anymore
        this.Rotate();
        return this;
    }

    //Awake is called much earlier that start. Alternatively you could set these values in Init
    void Awake(){

        this._transform = this.transform;
        this._camera = Camera.main;
        this.rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        
    }

    void Start()
    {
        //We cache important components, later this will be done in Awake() (Not important to know currently)
        
    }

    void FixedUpdate()
    {
        this.Move();
        
        this._ttlCounter += Time.deltaTime; //Time between last frame and current frame
        if(_ttlCounter >= this._ttl) this.BackToPool();
    }

    void Move(){
        
        //this._transform.position = this._transform.position + (this._direction * Time.deltaTime * this._speed);
        rigidbody2D.MovePosition (_transform.position + _direction* this._speed * Time.fixedDeltaTime);
    }
    
    void Rotate(){
        Vector3 pos = this._transform.position + this._direction;
        float AngleRad = Mathf.Atan2(pos.y - this._transform.position.y, pos.x - this._transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        this._transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
    }

    //reset ttl and let everything else handle the pool
    void BackToPool(){
        this._ttlCounter = 0f;
        this._pool.AddProjectile(this);
    }
}
