using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    [SerializeField] ParticleSystem _particles;

    [SerializeField] int lebensPunkte = 10;
    [SerializeField] int damage =  30;

    LebensPunkte sp;
    EnemyPool _pool;
    Rigidbody2D _rigidbody;
    Rigidbody2D _player;
    SpriteRenderer _renderer;
    AudioSource audios;
    Score score;
    [SerializeField] int bonus = 1;
    public Enemy Init(EnemyPool pool, Vector2 position){
        this._rigidbody.simulated = true;
        this._renderer.enabled = true;
        this._pool = pool;
        this.transform.position = position;
        sp.init(lebensPunkte, damage);
        return this;
    }
    void Awake(){
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._player = FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>();
        this._renderer = GetComponent<SpriteRenderer>();
        audios = GetComponent<AudioSource>();
        sp = GetComponent<LebensPunkte>();
        score = GetComponent<Score>(); 
        
    }


    private void OnCollisionEnter2D(Collision2D other) {
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if(!pc) return;
        LebensPunkte lp =other.gameObject.GetComponent<LebensPunkte>();
        lp.minusLebnspunkte(sp.getDamage());
        sp.minusLebnspunkte(lp.getDamage());
        this._particles.Play();
        audios.Play();
        if(sp.ifDamage())
            {
            score.addScore(bonus);
            Debug.Log(Score.score);
            this.Hit();
 
            }
    }

    void FixedUpdate() {
        this._rigidbody.MovePosition(this._rigidbody.position + (this._player.position - this._rigidbody.position).normalized * Time.fixedDeltaTime * this._speed);
        this.Rotate();
    }

    void Rotate(){
        Vector2 playerPos = this._player.position;
        float angleRad = Mathf.Atan2(playerPos.y - this._rigidbody.position.y, playerPos.x - this._rigidbody.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        this._rigidbody.rotation = angleDeg - 90;//diese -90 sind nötig für Sprites, die nach oben zeigen. Nutzen Sie andere Assets, könnte es sein, dass die das anpassen müssen
        
    }

    public void Hit(){
        StartCoroutine(this.WaitForParticlesToEnd(() => this._pool.AddEnemy(this)));
    }

    IEnumerator WaitForParticlesToEnd(System.Action callback){
        this._rigidbody.simulated = false;
        this._renderer.enabled = false;

        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        while(this._particles.isPlaying){
            
            yield return wait;
        }
        callback();
    }
}
