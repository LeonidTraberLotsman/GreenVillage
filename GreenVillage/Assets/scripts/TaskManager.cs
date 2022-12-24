using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public List<storage> storages;
   


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
        public storage my_storage;

        public DeliverTask(Item item,Vector3 place)
        {
            itemToDeliver = item;
            placeToDeliver = place;
        }

        public DeliverTask(Item item, storage the_storage)
        {
            itemToDeliver = item;
            placeToDeliver = the_storage.load_point.position ;
            my_storage = the_storage;
        }

        public override IEnumerator DoTask(Person person)
        {
            yield return person.ReachPoint(itemToDeliver.transform.position);
            itemToDeliver.transform.gameObject.SetActive(true);
            itemToDeliver.transform.position = person.transform.position + Vector3.up * 4;
            itemToDeliver.transform.SetParent(person.transform);
            yield return person.ReachPoint(placeToDeliver);

            if (my_storage != null)
            {
                my_storage.Load(itemToDeliver);
                itemToDeliver.transform.gameObject.SetActive(false);
            }

            itemToDeliver.transform.SetParent(null);
            yield return null;

            

        }

    }

    public class WoodCutTask : NPCtask
    {
        public tree Tree;
        GameObject woodItemPrefab;
        TaskManager taskManager;

        public WoodCutTask(tree cut_tree,GameObject prefab, TaskManager manager)
        {
            woodItemPrefab = prefab;
            Tree = cut_tree;
            taskManager = manager;


        }

        public override IEnumerator DoTask(Person person)
        {
            yield return person.ReachPoint(Tree.transform.position);
            yield return new WaitForSeconds(2);
            Destroy(Tree.gameObject);
            GameObject wood = Instantiate(woodItemPrefab);
            wood.transform.position = Tree.transform.position+Vector3.up*5;

            taskManager.all_tasks.Add(new DeliverTask(wood.GetComponent<Item>(), taskManager.Get_nearest(wood.transform.position," ")));


        }

    }

    public class BuilderTask : NPCtask
    {
       
        potential_building building;
        TaskManager my_task_manager;
        public BuilderTask(potential_building b, TaskManager  taskManager)
        {
            building = b;
            my_task_manager = taskManager;
        }

        public override IEnumerator DoTask(Person person)
        {
            

            while (building.woods.Count<building.HowMuchWeNeed)
            {
                yield return null;
                storage that_storage = null;

                that_storage = my_task_manager.Get_nearest(building.transform.position, "wood");
                if(that_storage == null)
                {
                    Debug.Log("no storage");
                    continue;
                }
                woodItem wood = null;


                foreach (Item item in that_storage.StoragedItem)
                {
                    if (item is woodItem)
                    {
                        wood = item as woodItem;
                        break;
                    }
                }

                if (wood == null)
                {
                    Debug.Log("no wood");
                    continue;
                }

                that_storage.StoragedItem.Remove(wood);
                DeliverTask task = new DeliverTask(wood, building.transform.position);
                yield return task.DoTask(person);
                building.woods.Add(wood);
            }

            yield return person.ReachPoint(building.transform.position);

            foreach (woodItem item in building.woods.ToArray())
            {
                Destroy(item.gameObject);
            }

            yield return new WaitForSeconds(2);

           

            building.Became_normal();

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

            if (prof == professions.builder && all_tasks[i] is BuilderTask)
            {
                Debug.Log("builder task created");
                NPCtask t = all_tasks[i];
                all_tasks.Remove(t);
                return t;
            }
        }
        //Debug.Log("no work");
        return null;
    }

    public void AddPotentialBuilding(potential_building building)
    {
        Debug.Log("add building");
        all_tasks.Add(new BuilderTask(building,this));
    }
    // Start is called before the first frame update
    void Start()
    {
        //all_tasks.Add(new DeliverTask(example, new Vector3(0, 0, 0)));
        //all_tasks.Add(new WoodCutTask(treeManager.forest[0]));
        for (int i = 0; i < 0; i++)
        {
            example = Instantiate(examplePrefab);
            example.transform.position = new Vector3(Random.Range(-20, 20), 5, Random.Range(-20, 20));
            all_tasks.Add(new DeliverTask(example, new Vector3(0, 0, 0)));
        }
        
    }
    
    public storage Get_nearest(Vector3 point,string res)
    {
        if (storages.Count==0)
        {
            return null;
        }

        storage potential = null;
        float min = 100000;
        foreach (storage that_storage in storages)
        {
            float dist = Vector3.Distance(point, that_storage.transform.position);

            if (dist < min)
            {
                if (res == "wood")
                {
                    bool IsWood = false;
                    foreach (Item item in that_storage.StoragedItem)
                    {
                        if(item is woodItem)
                        {
                            IsWood = true;
                        }
                        else
                        {
                            Debug.Log("no wood at storage");
                        }
                    }
                    if (!IsWood) continue;
                }
                potential = that_storage;
                min = dist;

               
            }
            
        }

        return potential;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
