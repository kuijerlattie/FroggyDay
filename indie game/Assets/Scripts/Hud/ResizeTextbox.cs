using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

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
        string[] text = Regex.Split(_text.text, "\n");
        Debug.Log("length: " + text[0].Length + " fontsize: " + _text.fontSize);
        _image.rectTransform.sizeDelta = new Vector2(text[0].Length * _text.fontSize, _text.preferredHeight + 5);
    }
}
