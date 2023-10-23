using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxieStats : MonoBehaviour
{
    public int level;
    public float hp;
    public float atk;
    public float def;
    public float crit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Stats(int lv)
    {
        level = lv;
        hp = 300 + lv * 10;
        atk = 30 + lv * 3;
        crit = 40;
        def = 50 + lv * 5;
    }
}
