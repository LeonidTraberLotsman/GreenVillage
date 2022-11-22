using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeManager : MonoBehaviour
{
    public GameObject tree_prefab;
    public List<tree> forest;
    int howManyTreesShouldBe = 30;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTrees());
    }

    IEnumerator SpawnTrees()
    {
        while (true)
        {
            for (int i = 0; i < howManyTreesShouldBe - forest.Count; i++)
            {
                AddTree();
            }
            yield return new WaitForSeconds(30);
        }
    }

    void AddTree()
    {
        GameObject new_tree = Instantiate(tree_prefab);
        new_tree.transform.position = transform.position + new Vector3(Random.Range(-15f, 15f), 0, Random.Range(-15f, 15f));
        forest.Add(new_tree.GetComponent<tree>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
