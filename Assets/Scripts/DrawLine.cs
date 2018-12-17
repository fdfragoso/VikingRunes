using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour {

    public static DrawLine instance = null;

    private LineRenderer line;
	private bool isMousePressed;
	public List<Vector3> pointsList;
	private Vector3 mousePos;

    public Color startColor;
    public Color endColor;

	struct myLine
	{
		public Vector3 StartPoint;
		public Vector3 EndPoint;
	};

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    void InitGame()
    {
        //Create Line
        line = gameObject.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Particles/Additive"));
        line.positionCount = 0;
        line.startWidth = 0.1f;
        line.startColor = Color.black;
        line.endColor = Color.black;
        line.useWorldSpace = true;
        isMousePressed = false;
        pointsList = new List<Vector3>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			isMousePressed = true;
			line.positionCount = 0;
			pointsList.RemoveRange (0, pointsList.Count);

            if (line.startColor != null)
            {
                line.startColor = startColor;
                line.endColor = endColor;
            }
            else
            {
                line.startColor = Color.black;
                line.endColor = Color.black;
            }
		}

		if (Input.GetMouseButtonUp (0)) {
			isMousePressed = false;
            line.positionCount = 0;
		}

        // Drawing line between the points
        if (isMousePressed)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            if (!pointsList.Contains(mousePos))
            {
                pointsList.Add(mousePos);
                line.positionCount = pointsList.Count;
                line.SetPosition(pointsList.Count - 1, (Vector3)pointsList[pointsList.Count - 1]);
                //line.SetPosition(0, mousePos);
                //line.SetPosition(1, new Vector3(1f, 1f, 0f));
            }
        }
    }
    
}


