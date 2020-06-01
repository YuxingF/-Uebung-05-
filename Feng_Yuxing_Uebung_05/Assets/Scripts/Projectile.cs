using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Projectile : MonoBehaviour
{
    
    [SerializeField] float _speed = 5.0f;
    [SerializeField] float _ttl = 5f;
    AudioSource audios;
    LebensPunkte sp;
    [SerializeField] int lebensPunkte = 50;
    [SerializeField]  int damage =  20;
    Score score;
    [SerializeField] int bonus = 1;
    ProjectilePool _pool;
    Vector2 _direction;
    Rigidbody2D _rigidbody;
    Camera _camera;
    float _ttlCounter = 0f;
    
    
    public virtual Projectile Init(ProjectilePool pool, Vector2 position, Vector2 direction){
        this._pool = pool; //the caller sets the corresponding objectpool
        this.transform.position = position; //For stuff like teleportation we use the transform. We dont want the physics engine to interrupt ;)
        

        this._direction = direction;
        this._direction = this._direction.normalized; //we normalize AFTER setting z to 0, otherwise we dont have vectors of size 1 anymore
        sp.init(lebensPunkte, damage);
        this.Rotate();
        return this;
    }

    //Awake is called much earlier that start. Alternatively you could set these values in Init
    void Awake(){
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._camera = Camera.main;
        audios = GetComponent<AudioSource>();
        sp = GetComponent<LebensPunkte>();
        score = GetComponent<Score>(); 
        
    }

    void Update()
    {        
        this._ttlCounter += Time.deltaTime; //Time between last frame and current frame
        if(_ttlCounter >= this._ttl) this.BackToPool();
        
    }

    void FixedUpdate() {
        this.Move();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(!enemy) return;
        audios.Play();
        LebensPunkte lp =other.gameObject.GetComponent<LebensPunkte>();
        sp.minusLebnspunkte(lp.getDamage());
        lp.minusLebnspunkte(sp.getDamage());
        if(lp.ifDamage())
            {enemy.Hit();
            score.addScore(bonus);
            Debug.Log(Score.score);
            }
        if(sp.ifDamage())
            this.BackToPool();
    }

    void Move(){
        
        this._rigidbody.MovePosition(this._rigidbody.position + (this._direction * Time.fixedDeltaTime * this._speed));
    }
    
    void Rotate(){
        Vector2 pos = this._rigidbody.position + this._direction;
        float angleRad = Mathf.Atan2(pos.y - this._rigidbody.position.y, pos.x - this._rigidbody.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        this.transform.rotation = Quaternion.Euler(0,0, angleDeg - 90);
    }

    //reset ttl and let everything else handle the pool
    void BackToPool(){
        this._ttlCounter = 0f;
        this._pool.AddProjectile(this);
    }
}
