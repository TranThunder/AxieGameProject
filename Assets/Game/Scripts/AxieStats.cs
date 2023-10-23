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
    public bool isatk=true;
    Vector2 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isatk)
        {
            transform.position = Vector2.Lerp(pos, pos + new Vector2(3.1f, 0), 0.1f) * Time.deltaTime;
        }
        

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
