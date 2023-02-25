using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    [SerializeField]
    private float size = 1f;
    private int grid_length = 10;
    public Hashtable[,] tileinfo;

    void Start()
    {
        tileinfo = new Hashtable[grid_length, grid_length];
        var dataset = Resources.Load<TextAsset>("Lawn_wars_demolevel");
        string[] lines = dataset.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string[] data = lines[i].Split(',');
            for (int j = 0; j < data.Length; j++)
            {
                string[] scores = data[j].Split('|');
                Hashtable temp = new Hashtable();
                temp.Add("gridlocation", GetNearestPointOnGrid(new Vector3(transform.position.x + i*size, 0f, transform.position.z + j*size)));
                temp.Add("sun", int.Parse(scores[0]));
                temp.Add("water", int.Parse(scores[1]));
                temp.Add("nutrients", int.Parse(scores[2]));
                tileinfo[i, j] = temp;
            }

        }
        //foreach(var i in tileinfo)
        //{
        //    Debug.Log(i["gridlocation"]);
        //    Debug.Log(i["sun"]);
        //    Debug.Log(i["water"]);
        //    Debug.Log(i["nutrients"]);
        //}

    }
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;

        return result;
    }
    public List<Vector3> GetAdjacentPointsOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        List<Vector3> result = new List<Vector3>();
        result.Add(
            new Vector3(
            (float)xCount * size + size,
            (float)yCount * size,
            (float)zCount * size));
        result.Add(
            new Vector3(
            (float)xCount * size - size,
            (float)yCount * size,
            (float)zCount * size));
        result.Add(
            new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size + size));
        result.Add(
            new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size - size));
        result.Add(
             new Vector3(
             (float)xCount * size + size,
             (float)yCount * size,
             (float)zCount * size + size));
        result.Add(
             new Vector3(
             (float)xCount * size + size,
             (float)yCount * size,
             (float)zCount * size - size));
        result.Add(
             new Vector3(
             (float)xCount * size - size,
             (float)yCount * size,
             (float)zCount * size + size));
        result.Add(
             new Vector3(
             (float)xCount * size - size,
             (float)yCount * size,
             (float)zCount * size - size));

        for(int i = 0; i < result.Count; i++)
        {
            result[i] += transform.position;
        }
        return result;
    }
    public GameObject DrawHighlight()
    {
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quad.transform.localScale = new Vector3(size, 1, size);
        quad.transform.Rotate(Vector3.right, 90f);
        Renderer quadRenderer = quad.GetComponent<Renderer>();
        quadRenderer.material.color = new Color(1, 0.92f, 0.016f, .5f);
        return quad;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < grid_length*size; x += size)
        {
            for (float z = 0; z < grid_length*size; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(transform.position.x+x, 0f, transform.position.z+z));
                Gizmos.DrawSphere(point, 0.1f);
            }

        }
    }
}
