using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barman : Singleton<Barman>
{
    public enum Aspect
    {
        CHILL,
        NORMAL,
        STRESSED,
    };

    private Aspect aspect;
    private GameObject currentCockail;
    private GameObject currentMonster;
    private int currentIndex = 0; //index of list monsters in front of the player

    // Start is called before the first frame update
    protected override void Start ()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updateAspect();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //Move(KeyCode.LeftArrow); NEED monsters list
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Move(KeyCode.RightArrow); NEED monsters list
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            print("Le Barman sert un monstre");
            // TODO set cocktail du monstre
            //currentMonster.setCocktail();
        }
    }

    void updateAspect()
    {
        if (StressGauge.instance.currentStress <= StressGauge.instance.maxNoStress)
        {
            aspect = Aspect.CHILL;
        }
        else if (StressGauge.instance.currentStress <= StressGauge.instance.maxNormalStress)
        {
            aspect = Aspect.NORMAL;
        }
        else
        {
            aspect = Aspect.STRESSED;
        }
        //TODO : change sprite renderer 
    }

    void Move(KeyCode key, List<MonoBehaviour> listMonsters, int sizeList) //TODO : MonoBehaviour -> Monster
    {
        //Go Left
        if (key == KeyCode.LeftArrow)
        {
            int nextIndex = currentIndex - 1;
            if (nextIndex > 0)
            {
                currentIndex = nextIndex;
            }
        }
        //Go Right
        else if (key == KeyCode.RightArrow)
        {
            int nextIndex = currentIndex + 1;
            if (nextIndex < sizeList)
            {
                currentIndex = nextIndex;
            }
        }
        //else : already at the most left or right position 
        MonoBehaviour currentMonster = listMonsters[currentIndex];
        transform.position = currentMonster.transform.position;
    }


} 
