using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   

    [SerializeField] float _speed = 100f;
    [SerializeField] PlayerController playerCtl;
    [SerializeField] Transform _transform;
     Rigidbody2D rigidbody2D;
    Camera _camera;
    Vector3 _direction;
    [SerializeField]Transform playerTransform;
    EnemyPool _pool;
    float _ttlCounter = 0f;
    bool a;
    [SerializeField] float _ttl = 5f;
    private ParticleSystem system;
    

    // Start is called before the first frame update
    void Start()
    {
        this._transform = transform;
        this._camera = Camera.main;
        GameObject player= GameObject.Find("Player");
        this.playerCtl = player.GetComponent<PlayerController>();
        this.playerTransform = player.transform; this._direction = playerTransform.position - _transform.position;
        _direction.z = 0;
        _direction = _direction.normalized;  
        this.rigidbody2D = gameObject.GetComponent<Rigidbody2D>();

        
    }
    
    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate(){
        this.Move();
        this.Rotate();
        this._ttlCounter += Time.deltaTime; //Time between last frame and current frame
        if(_ttlCounter >= this._ttl) this.BackToPool();

    }
     
    void Move(){
        _direction = playerTransform.position - _transform.position;
        _direction.z = 0;
        _direction = _direction.normalized;    
        //_transform.position += _direction*Time.deltaTime * _speed;
        rigidbody2D.MovePosition (_transform.position + _direction* this._speed * Time.fixedDeltaTime);
    }

    void Rotate(){
        Vector3 pos = _transform.position + _direction;
        float AngleRad = Mathf.Atan2(pos.y - this._transform.position.y, pos.x - this._transform.position.x);
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        //this._transform.rotation = Quaternion.Euler(0, 0, AngleDeg - 90);
        rigidbody2D.MoveRotation(Quaternion.Euler(0, 0, AngleDeg - 90));
        }
    public void Init(EnemyPool pool){
        this._pool = pool; 

    }
        void BackToPool(){
        this._ttlCounter = 0f;
        this._pool.AddEnemy(this);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        system = GetComponent<ParticleSystem>();
        if(system!=null)
        if(collision.gameObject.tag=="Player")
        {   
            system.Play(true);
            //system.Stop();
        }
    }
    
}
