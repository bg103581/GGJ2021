using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat
{
    #region PROPERTIES
    private CatColor catColor;
    private CatMark catMark;
    private CatNeckline catNeckline;
    private CatBehavior catBehavior;
    #endregion

    public Cat(CatColor color, CatMark mark, CatNeckline neckline, CatBehavior behavior) {
        catColor = color;
        catMark = mark;
        catNeckline = neckline;
        catBehavior = behavior;
    }

    #region GETTERS AND SETTERS
    public string Id { get; set; }
    public CatColor getCatColor() { return catColor; }
    public CatMark getcCatMark() { return catMark; }
    public CatNeckline getCatNeckline() { return catNeckline; }
    public CatBehavior getCatBehavior() { return catBehavior; }
    #endregion
}
