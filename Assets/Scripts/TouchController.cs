using System;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private enum Mode { MOBILE, PC }
    // events
    public static Action OnTouchEnd;
    public static Action<TileController> OnTouchableFound;

    //This was suppose to work with layers, butr in the end I didnt use it
    /*[SerializeField]
    [Header("Layers touchable")]
    private string[] layers;*/

    // properties
    private GameObject lastFound;
    private bool touchStarted = false;

	private bool isRelease = false;

    void Update()
    {
#if UNITY_EDITOR
        TouchingPC();
#endif

#if UNITY_ANDROID
        TouchingMobile();
#endif
    }

    /// <summary>
    /// Release the touch
    /// </summary>
    private void Release()
    {
        lastFound = null;
        touchStarted = false;

		if (OnTouchEnd != null && !isRelease) {
			OnTouchEnd ();
			isRelease = true;
		}
    }

    /// <summary>
    /// Check position touched
    /// </summary>
    /// <param name="position"></param>
    private void Touching(Vector3 screenPosition)
    {
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
		RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

		//verify the hit
		if (hit.collider != null)
		{
			GameObject found = hit.transform.gameObject;
			if (found != lastFound && OnTouchableFound != null)
			{
				lastFound = found;

                Debug.Log(lastFound.name);
                lastFound.GetComponent<TileController>().SelectDot();
				OnTouchableFound(found.GetComponent<TileController>());
			}

			}
    }

    /// <summary>
    /// Touching in Mobile
    /// </summary>
    private void TouchingMobile()
    {
        if (Input.touchCount > 0)
        {
            Vector3 position = Input.GetTouch(0).position;
            Touching(position);
        }
        else if (Input.touchCount == 0)
        {
            Release();
        }

    }

    /// <summary>
    /// Pointing in PC
    /// </summary>
    private void TouchingPC()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 position = Input.mousePosition;
            Touching(position);
        }
		else if (Input.GetMouseButtonUp(0))
        {
			isRelease = false;
            Release();
        }
    }

}