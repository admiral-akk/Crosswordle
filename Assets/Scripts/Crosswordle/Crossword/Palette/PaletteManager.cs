using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteManager : MonoBehaviour
{
    [Header("Text Color")]
    [SerializeField] private Color Text;
    [Header("Textbox Colors")]
    [SerializeField] private Color None;
    [SerializeField] private Color NothingFound;
    [SerializeField] private Color Wrong;
    [SerializeField] private Color BadPosition;
    [SerializeField] private Color Correct;
    [Header("Game Colors")]
    [SerializeField] private Color Border;
    [SerializeField] private Color Backdrop;
    [SerializeField] private Color Empty;

    public ColorPalette Palette
    {
        get
        {
            return new ColorPalette(Text, None, NothingFound, Wrong, BadPosition, Correct, Border, Backdrop, Empty);
        }
    }
}
