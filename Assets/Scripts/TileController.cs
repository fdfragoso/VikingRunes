using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    //Position of each dot on the board
    public int X { get; set; } 
    public int Y { get; set; }

    public int id; //id to identify the color
    
    // position on the grid

    public void PositionIt(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    /// <summary>
    /// Selected Dot
    /// </summary>
    public void SelectDot()
    {
        transform.localScale = new Vector2(0.17f, 0.17f);
        transform.GetComponent<BoxCollider2D>().size = new Vector2(6f, 6f);
    }

    /// <summary>
    /// Deselect the dot
    /// </summary>
    public void DeSelectDot()
    {
        transform.localScale = new Vector2(0.13f, 0.13f);
        transform.GetComponent<BoxCollider2D>().size = new Vector2(7.5f, 7.5f);
    }
}
