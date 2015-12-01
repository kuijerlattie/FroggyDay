using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ResizeTextbox : MonoBehaviour {

    public Text _text;
    Image _image;
    public float edgesize = 5;

	void Start () {
        _image = GetComponent<Image>();
	}

    public void UpdateSize(string text)
    {
        string[] splitText = Regex.Split(text, "\r\n");
        _image.rectTransform.sizeDelta = new Vector2(splitText[0].Length * (_text.fontSize * _text.transform.parent.localScale.x), _text.preferredHeight * splitText.Length + edgesize);
    }
}
