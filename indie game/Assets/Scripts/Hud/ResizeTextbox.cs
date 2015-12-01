using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResizeTextbox : MonoBehaviour {

    [SerializeField] Text _text;
    Image _image;

	// Use this for initialization
	void Start () {
        _image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSize();   //make it so it only does this when the text changes
	}

    void UpdateSize()
    {
        _image.rectTransform.sizeDelta = new Vector2(75, _text.preferredHeight + 5);
    }
}
