using UnityEngine;
using UnityEngine.UI;

public class CanvasColorChanger : MonoBehaviour
{
    public Text text; // Assign in Inspector
    public Color red = Color.red; // Set desired color in Inspector

    void Start()
    {
        if (text != null)
        {
            ChangeColor(red);
        }
        else
        {
            Debug.LogWarning("Target Image is not assigned!");
        }
    }

    public void ChangeColor(Color color)
    {
        text.color = color;
    }
}
