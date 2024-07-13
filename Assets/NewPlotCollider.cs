using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlotCollider : MonoBehaviour
{
    public void OnMouseDown()
    {
        StateManager.Instance.NewPlot();
    
    }

public void NewPlot(){
            StateManager.Instance.NewPlot();

}

}
