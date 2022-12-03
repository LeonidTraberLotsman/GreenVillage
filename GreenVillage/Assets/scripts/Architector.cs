using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Architector : MonoBehaviour
{
    public enum Building
    {
        house1,
        table2,
        bed3,
        light4,
        workbench5,
        plate6,
        plants_house,
        stock,
        track,
    }

    public List<GameObject> buildings_prefabes;
    public Cell[][] cells= new Cell[50][];
    public int Modificator = 3;
    public Transform[] points;

    public void Start(){
        for(int i=0;i<50;i++){
            cells[i]=new Cell[50];
        }

        //foreach(Transform point in points){
        //    Debug.Log(PlaceBuilding(point.position));
        //}

        for (int j = 0; j < 50; j++)for (int i = 0; i < 50; i++)
        {
            //PlaceBuilding(FromCoord(new Vector2(j, i)), 0);
            //    continue;




                if ((i+j)%6==0)PlaceBuilding(FromCoord(new Vector2(j,i)),3);
            if((i+j)%2==0)PlaceBuilding(FromCoord(new Vector2(j,i)),1);
            else          PlaceBuilding(FromCoord(new Vector2(j, i)), 0);
            }
    }

    public GameObject HousePrefab;

    public bool PlaceBuilding(Vector3 point, int num){
        Cell that_cell = GetCell(point);
        if(that_cell.MyObj!=null) return false;
        Debug.Log(that_cell.MyObj);
        GameObject building=Instantiate(buildings_prefabes[num]);
        that_cell.MyObj=building;
        Debug.Log(that_cell.MyObj);
        Debug.Log(that_cell.coord);
        building.transform.position=FromCoord(that_cell.coord);
        return true;
    }

    

    public Cell GetCell(Vector3 coord){
        return GetCell(ToCoord(coord));
    }

    public Cell GetCell(Vector2 coord){
        if (coord.x > 50) coord = new Vector2(50, coord.y);
        if (coord.x <0) coord = new Vector2(0, coord.y);
        if (coord.y > 50) coord = new Vector2(coord.x,50);
        if (coord.y  <0) coord = new Vector2(coord.x,0);
        Cell result=cells[Mathf.RoundToInt(coord.x)][Mathf.RoundToInt(coord.y)];
        if(result!=null){
            return result;
        }
        result= cells[Mathf.RoundToInt(coord.x)][Mathf.RoundToInt(coord.y)]=new Cell(coord);
        return result;
    }

    public Vector3 FromCoord(Vector2 ish){
        return new Vector3(ish.x*Modificator,0,ish.y*Modificator)+transform.position;
    }

    public Vector2 ToCoord(Vector3 ish){
        ish-=transform.position;
        return new Vector2(Mathf.RoundToInt(ish.x/Modificator),Mathf.RoundToInt(ish.z/Modificator));
    }



   public class Cell{
    public Vector2 coord;
    public GameObject MyObj;
    public Cell(Vector2 coord){
        this.coord=coord;
    }
   }
}
