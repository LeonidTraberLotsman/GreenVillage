using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public Transform AccessPoint;
    public Transform SleepingPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public IEnumerator ProcessSleeping(Person p)
    {
        p.agent.enabled = false;
        p.transform.position = SleepingPoint.position;
        while (p.sleepWanting < 100)
        {
            yield return new WaitForSeconds(0.1f);
            p.sleepWanting++;
        }
        p.transform.position = AccessPoint.position;
        p.agent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
