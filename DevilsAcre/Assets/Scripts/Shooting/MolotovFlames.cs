using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolotovFlames : MonoBehaviour
{
    [SerializeField] private AudioClip fire;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlaySound(fire);
        
        Destroy(this.gameObject,6);
    }
}
