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

    private static PaletteManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private List<CrosswordleRenderer> _renderers;
    private List<CrosswordleRenderer> Renderers
    {
        get
        {
            if (_renderers == null)
                _renderers = new List<CrosswordleRenderer>();
            return _renderers;
        }
    }
    private ColorPalette Palette => new ColorPalette(Text, None, NothingFound, Wrong, BadPosition, Correct, Border, Backdrop, Empty);

    public static void RegisterRenderer(CrosswordleRenderer renderer)
    {
        Instance.Renderers.Add(renderer);
        renderer.UpdatePalette(Instance.Palette);
    }

    public static void DeregisterRenderer(CrosswordleRenderer renderer)
    {
        Instance.Renderers.Remove(renderer);
    }

    private void OnValidate()
    {
        foreach (var renderer in Renderers)
        {
            renderer.UpdatePalette(Palette);
        }
    }
}
