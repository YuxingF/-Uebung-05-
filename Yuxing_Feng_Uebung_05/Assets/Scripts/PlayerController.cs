using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //private fields with [SerializeField]-Attribute can be set in inspector. Public would be correct, too ;)
    [SerializeField] float _speed = 1f;
    [SerializeField] Weapon _startingWeapon;
    [SerializeField] bool _physicalMovement = true;
    [SerializeField] int lebensPunkte = 300;
    [SerializeField]  int damage =  20;
    Rigidbody2D _rigidbody;
    Camera _camera;
    List<Weapon> _weapons;
    LebensPunkte sp;
    SceneController sc;
    GameObject feedback;

    int _currentWeapon;


    void Awake(){
        this._rigidbody = GetComponent<Rigidbody2D>();
        sp = GetComponent<LebensPunkte>();
        sp.init(lebensPunkte, damage);
        this._camera = Camera.main;
        sc = GetComponent<SceneController>();
    }

    void Start()
    {
        this._weapons = new List<Weapon>();
        this.AddWeapon(this._startingWeapon);
        this._currentWeapon = 0;

    }

    //Standard UpdateLoop (once per Frame)
    void Update()
    {
        if(!this._physicalMovement)
            this.transform.position += (new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0) * Time.deltaTime * this._speed);
        this.Shoot();
        this.SwitchWeapon();
        this.checkLeben();
        this.feedBack();
    }

    void FixedUpdate() {
        if(this._physicalMovement)
            this.Move();

        this.Rotate();
    }

    void Move(){
        this._rigidbody.MovePosition(this._rigidbody.position + (new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.fixedDeltaTime * this._speed));
    }

    void Shoot(){
        this._weapons[this._currentWeapon].Shoot();
    }
    void Rotate(){
        Vector2 mousePos = this._camera.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePos.y - this._rigidbody.position.y, mousePos.x - this._rigidbody.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        this._rigidbody.rotation = angleDeg - 90;//diese -90 sind nötig für Sprites, die nach oben zeigen. Nutzen Sie andere Assets, könnte es sein, dass die das anpassen müssen
        
    }
    void SwitchWeapon(){

        if(!Input.GetMouseButtonDown(1)) return;

        this._currentWeapon++;
        if(this._currentWeapon >= this._weapons.Count) this._currentWeapon = 0;
    }
    public void AddWeapon(Weapon weapon){
        weapon.transform.position = this._rigidbody.position;
        weapon.transform.SetParent(this.transform);
        this._weapons.Add(weapon);
    }
    void checkLeben(){
        if(sp.ifDamage())
            {
                sc.LoadEnd();
                Destroy(this.gameObject);
                
            }       
    }
    void feedBack(){
        if(Score.score>15){
            feedback = GameObject.FindGameObjectWithTag("Feedback");           
            feedback.GetComponent<TMP_Text>().text = "Great";
        }
        if(Score.score>50)
        {
            feedback = GameObject.FindGameObjectWithTag("Feedback");           
            feedback.GetComponent<TMP_Text>().text = "Excellent";
        }           
    }
    
}
