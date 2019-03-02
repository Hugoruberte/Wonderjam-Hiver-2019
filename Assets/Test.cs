using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactive.Engine;

public class Test : MonoBehaviour
{
    void Start()
    {
        ChemicalElementEntity[] entities = new ChemicalElementEntity[] {new Voidd(), new DragonBlood(), new BasilicAshes(), new DraculaSunrise()};
        ChemicalElementEntity result;

        for(int i = 0; i < entities.Length; i++) {
        	for(int j = i; j < entities.Length; j++) {
        		result = entities[i] * entities[j];
        		Debug.Log($"{entities[i]} * {entities[j]} = {result}");
        	}
        }
    }
}
