using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeManager : MonoBehaviour
{

    //public TaskManager taskManager; 
    public GameObject tree_prefab;
    public GameObject wood_item;
    public List<tree> forest= new List<tree>();
    int howManyTreesShouldBe = 6;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTrees());
        

    }

    IEnumerator SpawnTrees()
    {
        while (true)
        {
            //forest.RemoveAll(null);
            Debug.Log("Spawning " + (howManyTreesShouldBe - forest.Count).ToString());
            for (int i = 0; i < howManyTreesShouldBe - forest.Count; i++)
            //for (int i = 0; i < 15; i++)
            {
                AddTree();
            }
            yield return new WaitForSeconds(10);
            Debug.Log("waited ");
        }
    }

    void AddTree()
    {
        GameObject new_tree = Instantiate(tree_prefab);
        new_tree.transform.position = transform.position + new Vector3(Random.Range(-15f, 15f), 0, Random.Range(-15f, 15f));
        forest.Add(new_tree.GetComponent<tree>());
        new_tree.GetComponent<tree>().manager = this;
        GetComponent<TaskManager>().all_tasks.Add(new TaskManager.WoodCutTask(new_tree.GetComponent<tree>(), wood_item));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
