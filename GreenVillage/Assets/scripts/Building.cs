using UnityEngine;

public class Building : MonoBehaviour
{
    public Vector2Int Size = Vector2Int.one;

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < Size.x; x++) 
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x+y)%2 == 0)Gizmos.color = Color.yellow;
                else Gizmos.color = Color.white;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));

            }
        }
    }
}
