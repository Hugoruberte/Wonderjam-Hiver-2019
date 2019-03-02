using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MonsterManager : Singleton<MonsterManager>
{
    public float timeBetweenMonster = 1.0f;

    public int monsterNumber;
    public MonsterScript prefabMonster;

    //VARIABLE TO MOVE BUT I DONT KNOW WHERE TO PUT IT NOW...
    public float orderSuccessValue = 10;

    private float timeToCreateMonster;

    private MonsterScript[] monsters;
    private List<int> freeIndice = new List<int>();



    // Start is called before the first frame update
    protected override void Start()
    {
        monsters = new MonsterScript[monsterNumber];
        for(int i = 0; i < monsterNumber; ++i )
        {
            freeIndice.Add(i);
        }

        setTimeToCreateMonster(timeBetweenMonster);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (freeIndice.Count !=0 && Time.time >= timeToCreateMonster)
        {
            CreateMonster();
            setTimeToCreateMonster(timeBetweenMonster);
        }
    }

    void setTimeToCreateMonster(float waitingTimeToCreateMonster)
    {
        timeToCreateMonster = Time.time + waitingTimeToCreateMonster;
    }

    void CreateMonster()
    {
        //index of free index
        int indiceTemp = UnityEngine.Random.Range(0, freeIndice.Count - 1);

        //free index
        int freePlace = freeIndice[indiceTemp];
        freeIndice.RemoveAt(indiceTemp);

        MonsterScript MonsterCreate = Instantiate(prefabMonster);
        //set MonsterCreate
        MonsterCreate.index = freePlace;
        MonsterCreate.position = UnityEngine.Random.Range(GetRangeMin(indiceTemp),GetRangeMax(indiceTemp));
        // TODO : rajoutez un MonsterAspect

        //fill the array of monsters
        monsters[freePlace] = MonsterCreate;

    }

    public void LeavingMonster(int position)
    {
        GameObject leavingMonster = monsters[position].gameObject;
        monsters[position] = null;
        Destroy(leavingMonster);
        freeIndice.Add(position);
    }

    int GetRangeMin(int indice)
    {
        // TODO : Rajouter une fonction pour obtenir la valeur min de z
        return 0;
    }

    int GetRangeMax(int indice)
    {
        // TODO : Rajouter une fonction pour obtenir la valeur max de z
        return 0;
    }

    void SetCocktail()
    {
        
    }

    public void TimerEnd(int position)
    {
        ToleranceManager.instance.UpdateGaugeValue(-orderSuccessValue);
        LeavingMonster(position);
        // Renvoyez tout ca à Luc pour obtenir la nouvelle valeur de satisfaction.
    }

}
