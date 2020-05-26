using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private fields with [SerializeField]-Attribute can be set in inspector. Public would be correct, too ;)
    [SerializeField] float _speed = 1f;
    [SerializeField] Weapon _startingWeapon;
    public Transform _transform;
    Camera _camera;
    List<Weapon> _weapons;
    int _currentWeapon;
    [SerializeField] Rigidbody2D rigidbody2D;
    Vector2 position2;
    public float MaxX ;
    public float MinX ;
    public float MaxY ;
    public float MinY ;

    void Start()
    {
        this._transform = transform;
        this._camera = Camera.main;
        
        this._weapons = new List<Weapon>();
        this.AddWeapon(this._startingWeapon);
        this._currentWeapon = 0;
        this.MaxX = 8.5f;
        this.MinX = -8.5f;
        this.MaxY = 5.5f;
        this.MinY = -3.5f;

    }

    //Standard UpdateLoop (once per Frame)
    void Update()
    {
        this.Shoot();
        this.SwitchWeapon();
    }
    void FixedUpdate(){
        this.Move();
        this.Rotate();
        this.BoundaryControl();
 
    }

    void Move(){
        //this._transform.position += (new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * Time.deltaTime * this._speed);
        rigidbody2D.MovePosition (rigidbody2D.position + new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"))* this._speed * Time.fixedDeltaTime);
        
    }

    void Shoot(){
        this._weapons[this._currentWeapon].Shoot();
    }
    void Rotate(){
        Vector2 mousePos = this._camera.ScreenToWorldPoint(Input.mousePosition);
        float angleRad = Mathf.Atan2(mousePos.y - this._transform.position.y, mousePos.x - this._transform.position.x);
        float angleDeg = (180 / Mathf.PI) * angleRad;
        //this._transform.rotation = Quaternion.Euler(0, 0, angleDeg - 90);//diese -90 sind nötig für Sprites, die nach oben zeigen. Nutzen Sie andere Assets, könnte es sein, dass die das anpassen müssen
        rigidbody2D.MoveRotation(Quaternion.Euler(0, 0, angleDeg - 90));
    }
    void SwitchWeapon(){

        if(!Input.GetMouseButtonDown(1)) return;

        this._currentWeapon++;
        if(this._currentWeapon >= this._weapons.Count) this._currentWeapon = 0;
    }
    public void AddWeapon(Weapon weapon){
        weapon.transform.position = this._transform.position;
        weapon.transform.SetParent(this._transform);
        this._weapons.Add(weapon);
    }
    public void BoundaryControl(){
            float fx = Mathf.Clamp(rigidbody2D.position.x,MinX,MaxX);
            float fy = Mathf.Clamp(rigidbody2D.position.y,MinY,MaxY);
            this._transform.position = new Vector3(fx,fy,0);
    }
    
       /* private void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.collider.tag=="Enemy")
        {
            Destroy(collision.gameObject);
        }
        }
        */
}
