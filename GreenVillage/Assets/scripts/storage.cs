using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class storage : MonoBehaviour
{
    public List<Item> StoragedItem;
    public Transform load_point;

    public void Load(Item item)
    {
        StoragedItem.Add(item);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
