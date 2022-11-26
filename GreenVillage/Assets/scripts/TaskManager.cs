using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

   

    public Item example;
    public Item examplePrefab;
    public List<NPCtask> all_tasks = new List<NPCtask>();
    treeManager treeManager;
    public GameObject WoodPrefab;
    public enum professions{ 
        WoodCutter,
        Postman,
        Cooker,
        builder,
        farmer
    }

    public class NPCtask
    {
        public  virtual IEnumerator DoTask(Person person)
        {
            yield return null;
        }
    }

    public class DeliverTask: NPCtask
    {
        public Item itemToDeliver;
        public Vector3 placeToDeliver;

        public DeliverTask(Item item,Vector3 place)
        {
            itemToDeliver = item;
            placeToDeliver = place;
        }

        public override IEnumerator DoTask(Person person)
        {
            yield return person.ReachPoint(itemToDeliver.transform.position);
            itemToDeliver.transform.position = person.transform.position + Vector3.up * 4;
            itemToDeliver.transform.SetParent(person.transform);
            yield return person.ReachPoint(placeToDeliver);
            itemToDeliver.transform.SetParent(null);
            yield return null;

        }

    }

    public class WoodCutTask : NPCtask
    {
        public tree Tree;
        GameObject woodItemPrefab;

        public WoodCutTask(tree cut_tree,GameObject prefab)
        {
            woodItemPrefab = prefab;
            Tree = cut_tree;
            
        }

        public override IEnumerator DoTask(Person person)
        {
            yield return person.ReachPoint(Tree.transform.position);
            yield return new WaitForSeconds(2);
            Destroy(Tree.gameObject);
            GameObject wood = Instantiate(woodItemPrefab);
            wood.transform.position = Tree.transform.position+Vector3.up*5;
            
            

        }

    }

    public NPCtask GetTask(professions prof)
    {




        for (int i = 0; i < all_tasks.Count; i++)
        {
            //Debug.Log(i);
            if(prof==professions.Postman && all_tasks[i] is DeliverTask)
            {
                Debug.Log("task created");
                NPCtask t = all_tasks[i];
                all_tasks.Remove(t);
                return t;
            }

            if (prof == professions.WoodCutter && all_tasks[i] is WoodCutTask)
            {
                Debug.Log("wood task created");
                NPCtask t = all_tasks[i];
                all_tasks.Remove(t);
                return t;
            }
        }
        //Debug.Log("no work");
        return null;
    }

    // Start is called before the first frame update
    void Start()
    {
        all_tasks.Add(new DeliverTask(example, new Vector3(0, 0, 0)));
        //all_tasks.Add(new WoodCutTask(treeManager.forest[0]));
        for (int i = 0; i < 8; i++)
        {
            example = Instantiate(examplePrefab);
            example.transform.position = new Vector3(Random.Range(-20, 20), 5, Random.Range(-20, 20));
            all_tasks.Add(new DeliverTask(example, new Vector3(0, 0, 0)));
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
