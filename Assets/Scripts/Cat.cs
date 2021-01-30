using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat
{
    #region PROPERTIES
    private Fur fur;
    private Pattern pattern;
    private Collar collar;
    private Personality personality;
    #endregion

    public Cat(Fur color, Pattern mark, Collar neckline, Personality behavior) {
        fur = color;
        pattern = mark;
        collar = neckline;
        personality = behavior;
    }

    #region GETTERS AND SETTERS
    public string Id { get; set; }
    public Fur getFur() { return fur; }
    public Pattern getPattern() { return pattern; }
    public Collar getCollar() { return collar; }
    public Personality getPersonality() { return personality; }
    #endregion
}
