using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class minimapPosition : MonoBehaviour {

    public float xoffset = 0;
    public float yoffset = 0;

    private float previousWidth;
    private float previousHeight;
	// Use this for initialization
	void Start () {
        AdjustScale();
	}
	
	// Update is called once per frame
	void Update () {
       if(previousWidth != Screen.width || previousHeight != Screen.height)
        {
            AdjustScale();
        }
    }

    private void AdjustScale()
    {
        previousWidth = Screen.width;
        previousHeight = Screen.height;
        Vector2 pos = new Vector2(previousWidth - 250 + xoffset, previousHeight - 250 + yoffset); //minimap position 
        GetComponent<Image>().rectTransform.position = pos;
        GetComponent<Image>().rectTransform.pivot = Vector2.zero;
    }
}
