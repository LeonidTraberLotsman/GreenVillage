using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour
{
    public int sleepWanting = 0;
    public int foodWanting = 0;
     public NavMeshAgent agent;
    public Bed myBed;
    public Transform Table;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(LifeCircle());
    }
    public IEnumerator FindFood()
    {
        yield return ReachPoint(Table.position);
        foodWanting = 100;
    }
    public IEnumerator LifeCircle()
    {
        while (true)
        {
            //yield return new WaitForSeconds(0.1f);
            sleepWanting--;
            foodWanting--;
            if (sleepWanting < 20)//Если устал идём спать
            {
                yield return Sleep();
            }
            else
            {
                if (foodWanting < 20)//Если устал идём спать
                {
                    yield return FindFood();
                }
                else
                {
                    yield return ReachPoint(new Vector3(0, 0, 7));
                }
            }
        }
    }

    public IEnumerator Sleep()
    {
        yield return ReachPoint(myBed.AccessPoint.position);
        yield return myBed.ProcessSleeping(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ReachPoint(Vector3 point)
    {
        agent.destination = point;
        while (Vector3.Distance(transform.position, point) > 5)
        {
            yield return null;
        }
    }

}
