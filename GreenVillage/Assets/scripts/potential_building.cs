using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class potential_building : MonoBehaviour
{
    public List<Renderer> renderers;
    public List<Color> colors;
    public bool isGhosty=false;

    public void Became_ghosty()
    {
        if (isGhosty) return;
        isGhosty = true;
        foreach (var item in renderers)
        {
            colors.Add(item.material.color);
            item.material.color = new Color(0.0f, 0.0f, 1.0f, 0.5f);
        }
        StartCoroutine(BeBuild());
    }

    public IEnumerator BeBuild()
    {
        yield return new WaitForSeconds(2);
        Became_normal();
    }
    public void Became_normal()
    {
        if (!isGhosty) return;
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].material.color = colors[i];
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //Became_ghosty();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
