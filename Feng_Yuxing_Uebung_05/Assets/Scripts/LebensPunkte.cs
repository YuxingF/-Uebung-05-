using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LebensPunkte : MonoBehaviour
{

    int _lebensPunkte;
    int _damage;
    
    void addLebnspunkt(){
        _lebensPunkte++;
    }
    public void minusLebnspunkte(int damage){
        _lebensPunkte = _lebensPunkte -damage ;
    }
    // Start is called before the first frame update
    public int getDamage()
    {
        return this._damage;
    }
    public bool ifDamage(){
    if(_lebensPunkte <= 0)
         return true;
    
    else
        return false;
    }
    
    public void init(int lebensPunkte,int damage){
        this._lebensPunkte = lebensPunkte;
        this._damage = damage;
    }
}
