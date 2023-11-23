using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public static int coinValue = 0;
    Text coin;

    // Start is called before the first frame update
    void Start()
    {
       coin = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        coin.text = "" + coinValue;
    }
}
