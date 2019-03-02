using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = System.Random;
using Vector3 = UnityEngine.Vector3;

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
    private List<float> xMin = new List<float>();
    private List<float> xMax = new List<float>();


    // Start is called before the first frame update
    protected override void Start()
    {
        monsters = new MonsterScript[monsterNumber];
        for(int i = 0; i < monsterNumber; ++i )
        {
            freeIndice.Add(i);
        }

        setTimeToCreateMonster(timeBetweenMonster);

        xMin.Add(-6.56f);
        xMin.Add(-4.5f);
        xMin.Add(-2.5f);
        xMin.Add(-0.5f);
        xMin.Add(1.5f);
        xMin.Add(3.5f);
        xMin.Add(5.5f);
        xMin.Add(7.5f);
        
        xMax.Add(-6f);
        xMax.Add(-4f);
        xMax.Add(-2f);
        xMax.Add(0f);
        xMax.Add(2f);
        xMax.Add(4f);
        xMax.Add(6f);
        xMax.Add(8f);

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
        float Yposition = MonsterCreate.GetComponentInChildren<SpriteRenderer>().transform.position.y;
        float Zposition = MonsterCreate.GetComponentInChildren<SpriteRenderer>().transform.position.z;
        MonsterCreate.index = freePlace;
        Debug.Log(MonsterCreate.index);
        MonsterCreate.position = UnityEngine.Random.Range(xMin[freePlace],xMax[freePlace]);
        MonsterCreate.GetComponentInChildren<SpriteRenderer>().transform.position =
            new Vector3(MonsterCreate.position, Yposition, Zposition);
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

    public void TimerEnd(int position)
    {
        ToleranceManager.instance.UpdateGaugeValue(-orderSuccessValue);
        LeavingMonster(position);
    }

}
