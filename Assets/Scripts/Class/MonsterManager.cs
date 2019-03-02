using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MonsterManager : Singleton<MonsterManager>
{

    private MonsterScript[] monsters;
    public int monsterNumber;
    public GameObject prefabMonster;


    private List<int> freeIndice = new List<int>();
    
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        monsters = new MonsterScript[monsterNumber];
        for(int i = 0; i < monsterNumber; ++i )
        {
            freeIndice.Add(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (freeIndice.Count !=0)
        {
            CreateMonster();
        }
    }

    void CreateMonster()
    {
        int indiceTemp = UnityEngine.Random.Range(0, freeIndice.Count - 1);
        GameObject MonsterCreate = Instantiate(prefabMonster);
        MonsterCreate.GetComponent<MonsterScript>().position = UnityEngine.Random.Range(GetRangeMin(indiceTemp),GetRangeMax(indiceTemp));
        // TODO : rajoutez un MonsterAspect
        monsters[indiceTemp] = MonsterCreate.GetComponent<MonsterScript>();

    }

    public void LeavingMonster(int position)
    {
        GameObject leavingMonster = monsters[position].gameObject;
        monsters[position] = null;
        Destroy(leavingMonster);
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
        LeavingMonster(position);
        // Renvoyez tout ca à Luc pour obtenir la nouvelle valeur de satisfaction.
        freeIndice.Add(position);
    }

}
