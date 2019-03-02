using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private int position;
    private int attente = 2;
    private int monsterAspect;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int Position
    {
        get { return position; }
        set { position = value; }
    }

    public int Attente
    {
        get { return attente; }
        set { attente = value; }
    }

    public int MonsterAspect
    {
        get { return monsterAspect; }
        set { monsterAspect = value; }
    }

    int youGotCocktail()
    {
        
    }
}
