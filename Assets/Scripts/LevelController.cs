using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public TileController[] dots;
    public int columns = 6;
    public int rows = 8;

    private List<TileController> ItensSelecionados = new List<TileController>();

    //Serialized fields helped me to keep track visual on inspector from what I needed

    [SerializeField]
    private List<Vector2> deletedDots = new List<Vector2>();

    [SerializeField]
    private List<TileController> dotsInBoard = new List<TileController>(); 

    void Start()
    {
        TouchController.OnTouchEnd += ClickRelease;
        TouchController.OnTouchableFound += ClickedDown;
    }

    /// <summary>
    /// Instantiate a dot.
    /// </summary>
    /// <param name="index">random variable between the 5 possible dots</param>
    /// <returns></returns>
    private TileController CatchDot(int index)
    {
        TileController dot = Instantiate<TileController>(dots[index]);
        dot.id = index;
        return dot;
    }

    /// <summary>
    /// Loads the dots on the screen
    /// /// </summary>
    public void LoadGrid()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                int randIndex = Random.Range(0, dots.Length);
                TileController item = CatchDot(randIndex);

				//position  each dot in place on the board
                item.transform.position = new Vector2(x, y);

				//set the position of each dot on their class
				item.X = x;
				item.Y = y;

                item.gameObject.SetActive(true);

                dotsInBoard.Add(item);
            }
        }
    }

    private void ClickedDown(TileController selectedDot)
    {    
        selectedDot.SelectDot();
        StartCoroutine(CheckIfClickedDot(selectedDot));

        //This was part of the first attempt with the mechanics of 3 match on the jelly style and not candy crush
        /*GameController.instance.SetColor(selectedDot.id);

        if (anterior > itens.Length) { //Setting the first dot
			anterior = selectedDot.id;
			GameController.instance.selectedDots.Add (selectedDot.gameObject);
			GameController.instance.countDots = 1;
            GameController.instance.SetColor(selectedDot.id);
        } else if (selectedDot.id == anterior) { //Checks if next dot is equal the last one
            //itemSelecionado.SelectDot();
            StartCoroutine(CheckIfClickedDot(selectedDot));                                                   
		} else { //Checks if next dot is different from the last one
			anterior = selectedDot.id;
            //GameController.instance.SetColor(itemSelecionado.id);
            //GameController.instance.selectedDots.Add (itemSelecionado.gameObject);
            //GameController.instance.countDots = 1;
            StartCoroutine(CheckIfClickedDot(selectedDot));
       }*/
    }

    /// <summary>
    /// Deal with the 3 matchs if its possible or no. 
    /// Checking all the sequence that the polayer selected.
    /// </summary>
    /// <param name="dot">Dot that was clicked by the user</param>
    /// <returns></returns>
    IEnumerator CheckIfClickedDot(TileController dot)
    {
        bool canAdd = false;

        if(GameController.instance.selectedDots.Count == 0) GameController.instance.selectedDots.Add(dot.gameObject);

        for (int i = 0; i < GameController.instance.selectedDots.Count; i++)
        {
            TileController itemList = GameController.instance.selectedDots[i].GetComponent<TileController>();
            if (dot.X == itemList.X && dot.Y == itemList.Y || dot.id != itemList.id)
            {
                canAdd = false;
                break;
            }
            else
            {
                canAdd = true;
            }
        }

        if (canAdd)
        {
            GameController.instance.countDots++;
            GameController.instance.SetColor(dot.id);
            GameController.instance.selectedDots.Add(dot.gameObject);
        }
        else
        {
            dot.DeSelectDot();
            GameController.instance.SetColor(dot.id);
        }

        yield return null;
    }

    /// <summary>
    /// When the mouse ou touch is released.
    /// Checks which dots we should remove from the board and the ones that asre not a 3 matrch and should turn it back to their normal behaviour
    /// </summary>
    private void ClickRelease()
    {
	    if (GameController.instance.countDots >= 3) {
            foreach (GameObject go in GameController.instance.selectedDots) {
                deletedDots.Add(new Vector2(go.GetComponent<TileController>().X, go.GetComponent<TileController>().Y));
                Destroy (go);
			}
            StartCoroutine(UpdateGridAfterMatch());
        } else {
            foreach (GameObject go in GameController.instance.selectedDots) {
                go.GetComponent<TileController>().DeSelectDot();
            }
		}

        GameController.instance.ResetGameController();
    }

    /// <summary>
    /// Instantiate new dots after a match happens
    /// </summary>
    /// <returns></returns>
    IEnumerator  UpdateGridAfterMatch()
    {       
        //check how many new dots are necessary for each column
        for (int i = 0; i < columns; i++)
        {
            foreach (Vector2 v in deletedDots)
            {
                if (v.x == i)
                {
                    int randIndex = Random.Range(0, dots.Length);
                    TileController item = CatchDot(randIndex);
                    item.transform.position = new Vector3(Mathf.RoundToInt(v.x), 10f);
                }
            }
        }

        deletedDots.Clear();

        yield return null;
    }

    //I was trying to do the refil fro the board by code ´butr was consuming a lot of time so I went with a different approach
    //for sure not the best one when we are talking about optimization for mobile, but at least work. I used rigidbody and instantiate on the 
    //new missing possitions new dots
    /*private void FillBlankSpaces()
    {
        for (int i = 0; i <= rows; i++)
        {
            for (int j = 0; j <= columns; j++)
            {
                for (int k = 0; k < deletedDots.Count; k++)
                {
                    if (deletedDots[k] == new Vector2(j, i))
                    {
                        Debug.Log("WE FOUND A BLANK SPACE");
                        //GETS ALL THE POINTS ABOVE THIS ONE AND DESCEND ONE
                        foreach (TileController t in dotsInBoard)
                        {
                           
                        }
                    }
                }
            }
        }
    }*/
}
