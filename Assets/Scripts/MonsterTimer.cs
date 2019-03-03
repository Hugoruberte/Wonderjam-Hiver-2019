using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MonsterTimer : MonoBehaviour
{
    float timeLeft = 300.0f;
    public Image timer;
    [HideInInspector] public UnityEvent onFinish = new UnityEvent();
      
   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer.fillAmount -= 1.0f * Time.deltaTime;
    }
}
