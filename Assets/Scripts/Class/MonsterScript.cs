using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public int position;
    public float waiting = 2.0f;
    public int monsterAspect;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnWaitingEnd()
    {
        MonsterManager.instance.TimerEnd(position);
    }

    void SetCocktail(Cocktail)
    {
        // Appelez la fonction de Luc de satisfaction et renvoyez au MonsterManager la valeur a ajouter à la satisfaction globale
    }
}
