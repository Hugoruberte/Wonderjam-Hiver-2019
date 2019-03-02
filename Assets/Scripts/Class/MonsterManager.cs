using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MonsterManager : Singleton<MonsterManager>
{
    private MonsterScript[] monstres = new MonsterScript[10];
    public GameObject prefabMonster;


    private List<int> freeIndice = new List<int>();
    
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        freeIndice.Add(1);
        freeIndice.Add(2);
        freeIndice.Add(3);
        freeIndice.Add(4);
        freeIndice.Add(5);
        freeIndice.Add(6);
        freeIndice.Add(7);
        freeIndice.Add(8);
        freeIndice.Add(9);
        
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
        MonsterCreate.GetComponent<MonsterScript>().Position = UnityEngine.Random.Range(GetRangeMin(indiceTemp),GetRangeMax(indiceTemp));
        // TODO : rajoutez un MonsterAspect
        monstres[indiceTemp] = MonsterCreate.GetComponent<MonsterScript>();

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
        monstres[position] = null;
        // Renvoyez tout ca à Luc pour obtenir la nouvelle valeur de satisfaction.
        freeIndice.Add(position);
    }

}
