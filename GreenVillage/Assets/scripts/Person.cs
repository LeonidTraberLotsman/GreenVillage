using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Person : MonoBehaviour
{
    public int sleepWanting = 1000;
    public int foodWanting = 1000;
    public NavMeshAgent agent;
    public Bed myBed;
    public Transform Table;

    public TaskManager taskManager;
    public TaskManager.NPCtask my_task;
    public TaskManager.professions my_profession;
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
                    yield return Work();
                    
                }
            }
        }
    }

    public IEnumerator Work()
    {
        if (my_task == null)
        {
            my_task = taskManager.GetTask(my_profession);
            if (my_task == null)
            {
                yield return ReachPoint(new Vector3(0, 0, 7));
                yield break;
            }
        }
        
            yield return my_task.DoTask(this);
        my_task = null;
        
        Debug.Log("work");
        yield return null;
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
