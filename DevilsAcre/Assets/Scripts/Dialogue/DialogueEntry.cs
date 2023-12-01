using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue Entry")]
public class DialogueEntry : ScriptableObject
{
    [TextArea(2,6)]
    public string[] entries;
}
