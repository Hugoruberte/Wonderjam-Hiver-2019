using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MonsterManager : Singleton<MonsterManager>
{
    private MonsterScript[] monstres = new MonsterScript[10];
    public GameObject prefabMonster;
 
    
    private int[] freeIndice = new int[10] {0,1,2,3,4,5,6,7,8,9}; 
    
    
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (freeIndice.Length !=0)
        {
            CreateMonster();
        }
    }

    void CreateMonster()
    {
        int indiceTemp = UnityEngine.Random.Range(0, freeIndice.Length - 1);
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
    

}
