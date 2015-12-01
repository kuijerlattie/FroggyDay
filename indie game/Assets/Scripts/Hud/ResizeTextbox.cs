using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

[RequireComponent(typeof(Image))]
public class ResizeTextbox : MonoBehaviour {

    public Text _text;
    Image _image;
    private const int EDGESIZE = 10;

	void Start () {
        _image = GetComponent<Image>();
	}

    public void UpdateSize(string text)
    {
        string[] splitText = Regex.Split(text, "\r\n");
        _image.rectTransform.sizeDelta = new Vector2(splitText[0].Length * (_text.fontSize * _text.transform.lossyScale.x), _text.preferredHeight * splitText.Length + EDGESIZE);
    }
}
