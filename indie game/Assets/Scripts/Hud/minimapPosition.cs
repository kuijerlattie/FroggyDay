using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class minimapPosition : MonoBehaviour {

    public float xoffset = 0;
    public float yoffset = 0;

    private float previousWidth;
    private float previousHeight;

    Image overlay;

	// Use this for initialization
	void Start () {
        overlay = GetComponent<Image>();
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
        Vector2 pos = new Vector2(previousWidth - 250 + xoffset, previousHeight - 250 + yoffset);
        overlay.rectTransform.position = pos;
        overlay.rectTransform.pivot = Vector2.zero;
    }
}
