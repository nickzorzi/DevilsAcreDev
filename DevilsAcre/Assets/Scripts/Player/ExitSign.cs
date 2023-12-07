using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSign : MonoBehaviour
{


    
    
    private float timer = 0;


    public void getWaitTime(float timePaused)
    {
        timer = timePaused;
    }


    private void Update()
    {
        // add stuff to display remaining time;
    }



}
