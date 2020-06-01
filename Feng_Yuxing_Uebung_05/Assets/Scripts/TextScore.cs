using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextScore : MonoBehaviour
{

    TMP_Text ts;
    // Start is called before the first frame update
    void Start()
    {
        this.ts = GetComponent<TMP_Text>();
    }
    void Update(){
        ts.text = Score.score.ToString();
    }
}
