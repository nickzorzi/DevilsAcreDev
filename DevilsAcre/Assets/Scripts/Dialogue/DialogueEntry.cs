using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Dialogue Entry")]
public class DialogueEntry : ScriptableObject
{
    
    public entryData[] entries;



    [System.Serializable]

    public struct entryData
    {
        public string name;
        public Texture icon;
        [TextArea(2, 6)]
        public string description;
    }
}
