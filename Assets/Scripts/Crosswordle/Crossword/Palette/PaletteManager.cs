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
        _renderers = new List<CrosswordleRenderer>();
    }

    private List<CrosswordleRenderer> _renderers;
    private ColorPalette _palette;


    public static void RegisterRenderer(CrosswordleRenderer renderer)
    {
        Instance._renderers.Add(renderer);
    }

    public static void DeregisterRenderer(CrosswordleRenderer renderer)
    {
        Instance._renderers.Remove(renderer);
    }

    private void InstatiatePalette()
    {
        _palette = new ColorPalette(Text, None, NothingFound, Wrong, BadPosition, Correct, Border, Backdrop, Empty);
    }

    private void OnValidate()
    {
        foreach (var renderer in _renderers)
        {
            renderer.UpdatePalette(_palette);
        }
    }
}
