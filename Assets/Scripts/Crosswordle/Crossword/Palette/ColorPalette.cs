using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public readonly struct BoxColor
{
    public readonly Color Background;
    public readonly Color Text;

    public BoxColor(Color background, Color text)
    {
        Background = background;
        Text = text;
    }
}
public readonly struct ColorPalette 
{
    public readonly BoxColor None;
    public readonly BoxColor NothingFound;
    public readonly BoxColor Wrong;
    public readonly BoxColor BadPosition;
    public readonly BoxColor Correct;
    public readonly Color Border;
    public readonly Color Backdrop;
    public readonly Color Empty;

    public ColorPalette(Color text, Color none, Color nothingFound, Color wrong, Color badPosition, Color correct, Color border, Color backdrop, Color empty)
    {
        None = new BoxColor(none, text);
        NothingFound = new BoxColor(nothingFound, text);
        Wrong = new BoxColor(wrong, text);
        BadPosition = new BoxColor(badPosition, text);
        Correct = new BoxColor(correct, text);
        Border = border;
        Backdrop = backdrop;
        Empty = empty;
    }
}
