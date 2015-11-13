using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPGauge : HudListener
{
    //  [SerializeField]
    // protected string listeningForB;
    // Update is called once per frame
    private Slider hp;
    protected override void Awake()
    {
        hp = GetComponentInChildren<Slider>();
        
       // Messenger.AddListener<int, int>(listeningFor, DoSomething);
        //     Messenger.AddListener<int>(listeningForB, DoSomething);
        Messenger.AddListener(listeningFor, HPChange);
        Messenger.AddListener<int>("PlayerEnabled", SetHP);
    }

    void SetHP(int health)
    {
        hp.maxValue = health;
        hp.value = hp.maxValue;
    }
    void HPChange()
    {
        hp.value -= 1;
    }
    /*
    protected override void DoSomething(int msga, int msgb)
    {
        GetComponentInChildren<Slider>().maxValue = msgb;
        GetComponentInChildren<Slider>().value = msga;
    }*/
    //protected override void DoSomething(int message)
    //{
    //    GetComponent<Slider>().value = message;
    //}
}
