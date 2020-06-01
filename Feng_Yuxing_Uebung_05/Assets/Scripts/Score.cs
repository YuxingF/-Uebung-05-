using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField]public static int score;

    public void addScore(int bonus){
        score = score + bonus;
    }
    public int getScore(){
        return score;
    }



}
