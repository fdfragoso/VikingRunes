using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance = null;
    public LevelController levelController; 

    [SerializeField]
    public List<GameObject> selectedDots;

    [SerializeField]
    public int countDots = 1; // count how many jellys the player is touching at once.

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        levelController = GetComponent<LevelController>();
        InitGame();
    }

    void InitGame() {
        selectedDots = new List<GameObject>();
        countDots = 1;
        levelController.LoadGrid();
    }

    public void ResetGameController()
    {
        countDots = 1;
        selectedDots.Clear();
    }
    
    //On the screen didnt went so well, but the idea behind was the line between the dots to be on the same color
    //from the dot to connect with. Sometimes the pickewd color inst the right one, unfortunately I didnt spend a lot of timne on those visual details
    public void SetColor(int itemId)
    {
        switch (itemId)
        {
            //blue
            case 0:
                DrawLine.instance.startColor = new Color(0.54f, 0.72f, 0.89f, 1f);
                DrawLine.instance.endColor = new Color(0.54f, 0.72f, 0.89f, 1f);
                break;
            //red
            case 1:
                DrawLine.instance.startColor = new Color(0.90f, 0.36f, 0.29f, 1f);
                DrawLine.instance.endColor = new Color(0.90f, 0.36f, 0.29f, 1f);
                break;
            //green
            case 2:
                DrawLine.instance.startColor = new Color(0.57f, 0.8f, 0.54f, 1f);
                DrawLine.instance.endColor = new Color(0.57f, 0.8f, 0.54f, 1f);
                break;
            //purple
            case 3:
                DrawLine.instance.startColor = new Color(0.6f, 0.37f, 0.65f, 1f);
                DrawLine.instance.endColor = new Color(0.6f, 0.37f, 0.65f, 1f);
                break;
            //yellow
            case 4:
                DrawLine.instance.startColor = new Color(0.90f, 0.85f, 0.13f, 1f);
                DrawLine.instance.endColor = new Color(0.90f, 0.85f, 0.13f, 1f);
                break;

        }
    }
}
