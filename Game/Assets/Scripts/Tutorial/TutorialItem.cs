using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialItemActionType
{
    None,
    ShowTemperatureMeter
}

[Serializable]
public class TutorialItem
{
    public string Text;
    public TutorialItemActionType Action;
}
