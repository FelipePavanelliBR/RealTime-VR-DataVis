using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPointInteractions : MonoBehaviour
{
    public void OnMouseDown()
    {
        SelectPointInPlot();
    }

    public void SelectPointInPlot(){
        string plotFullString = this.transform.parent.gameObject.transform.parent.name;
        string dataPointFullString = this.transform.name;

        int plotIDSubstringPosition = plotFullString.IndexOf(" ");
        int dataPointIDSubstringPosition = dataPointFullString.IndexOf(" ");

        string plotID = plotFullString.Substring(0, plotIDSubstringPosition);
        string dataPointID = dataPointFullString.Substring(0, dataPointIDSubstringPosition);

        StateManager.Instance.SelectPoint(int.Parse(plotID), int.Parse(dataPointID));
    }
}
