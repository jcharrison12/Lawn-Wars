using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    private MyGrid grid;
    public Camera camera;
    public Plane ground;
    public GameObject character;
    public GameObject hl;
    private Vector3 previousMousePos = new Vector3();

    public void Awake()
    {
        grid = FindObjectOfType<MyGrid>();
    }
    public void Start()
    {
        Camera camera = Camera.main;
        ground = new Plane(Vector3.up, 0f);
    }

    public void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        //Initialise the enter variable
        float enter = 0.0f;

        if (ground.Raycast(ray, out enter))
        {
            Vector3 hoverPoint = grid.GetNearestPointOnGrid(ray.GetPoint(enter));
            Vector3 characterPoint = grid.GetNearestPointOnGrid(character.transform.position);
            List<Vector3> adjacency = grid.GetAdjacentPointsOnGrid(characterPoint);
            if(grid.GetNearestPointOnGrid(hoverPoint) != previousMousePos)
            {
                Destroy(hl);
                if (adjacency.Contains(hoverPoint))
                {
                    hl = grid.DrawHighlight();
                    hl.transform.position = new Vector3(hoverPoint.x, hoverPoint.y + .05f, hoverPoint.z);
                    previousMousePos = hoverPoint;
                }
            }
        }
            //Detect when there is a mouse click
        if (Input.GetMouseButton(0))
        {
            //Create a ray from the Mouse click position
            Ray ray2 = camera.ScreenPointToRay(Input.mousePosition);

            //Initialise the enter variable
            float enter2 = 0.0f;

            if (ground.Raycast(ray2, out enter2))
            {
                //Get the point that is clicked
                Vector3 hitPoint = ray2.GetPoint(enter2);

                PlaceNear(hitPoint);
            }
        }
    }

    public void PlaceNear(Vector3 clickPoint)
    {
        Vector3 finalPosition = grid.GetNearestPointOnGrid(clickPoint);
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = finalPosition;

    }
}
