using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class MonsterScript : MonoBehaviour
{
    //monster variables
    public int position;
    public int monsterAspect;

    //time variables
    public float waiting = 2.0f;
    private WaitForSeconds waitBeforeLeaving;
    private float startWaitingTime;
    private float timeCoeff = 3;
    private float timeLimit = 0.2f;

    //





    // Start is called before the first frame update
    void Start()
    {
        startWaitingTime = Time.time;
        waitBeforeLeaving = new WaitForSeconds(waiting);
        StartCoroutine(Leaving());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Leaving()
    {
        yield return waitBeforeLeaving;
        MonsterManager.instance.LeavingMonster(position);
    }
    void OnWaitingEnd()
    {
        MonsterManager.instance.TimerEnd(position);
    }

    void SetCocktail(ChemicalElementEntity Cocktail)
    {
        //time Points
        float currentWaitingTime = Time.time;
        //actual waiting Time
        float waitingTime = currentWaitingTime - startWaitingTime;

        //tolerance points lost from time
        float toleranceTimePoints = ((waitingTime/waiting) - timeLimit) * timeCoeff;

        //color Points
        Cocktail.att

        // Appelez la fonction de Luc de satisfaction et renvoyez au MonsterManager la valeur a ajouter à la satisfaction globale
    }


}
