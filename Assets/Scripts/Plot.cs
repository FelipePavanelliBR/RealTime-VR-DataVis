using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using TMPro;
using System;

public class Plot
{
    // Internal Plot variables
    private List<DataPoint> dataPointsInScene; // list of DataPoint objects that will be instantiated in the scene
    private List<Dictionary<string, object>> pointList; // copy of data from CSV reader; List of lines Dictionaries with column header string keys and object values (ints, floats, or strings)
    private List<string> columnList; // list of all column names from CSV file
    private int plotID; // index of this plot in the list of plots in StateManager
    private GameObject plotGameObject; // GameObject of plot in scene

    // Variables for Plot visual representation
    private float scale;
    private string xColumnName;
    private string yColumnName;
    private string zColumnName;
    

    /* Constructor for a single Plot
    Should be instantiated in DataPlotter.cs alongside its internal DataPoint objects*/
    public Plot(List<Dictionary<string, object>> pointList, List<string> columnList, int plotID, float plotScale, GameObject plotGameObject)
    {
        this.pointList = pointList;
        this.columnList = columnList;
        this.plotID = plotID;
        this.plotGameObject = plotGameObject;
        this.scale = plotScale;
        this.dataPointsInScene = new List<DataPoint>();

        SetAllColumnsUI();

    }

    public void AddPointToScene(DataPoint dataPoint)
    {
        dataPointsInScene.Add(dataPoint);
    }

    public void SetColumnNames(string xColumnName, string yColumnName, string zColumnName)
    {
        this.xColumnName = xColumnName;
        this.yColumnName = yColumnName;
        this.zColumnName = zColumnName;

        GameObject plotTypeInfoCanvasInScene = plotGameObject.transform.Find("PlotGrabbable").gameObject.transform.Find("PlotTypeInfoCanvas").gameObject;

        // Setting column name X
        plotTypeInfoCanvasInScene.transform.Find("ColumnXName").gameObject.GetComponent<TextMeshProUGUI>().text = xColumnName;

        // Setting column name Y
        plotTypeInfoCanvasInScene.transform.Find("ColumnYName").gameObject.GetComponent<TextMeshProUGUI>().text = yColumnName;

        // Setting column name Z
        plotTypeInfoCanvasInScene.transform.Find("ColumnZName").gameObject.GetComponent<TextMeshProUGUI>().text = zColumnName;

    }

    public void SetAllColumnsUI()
    {
        string columnsString = "";
        foreach (string key in columnList)
        {
            columnsString += key + " | ";
        }
        plotGameObject.transform.Find("PlotGrabbable").gameObject.transform.Find("DataPointInfoCanvas").gameObject.transform.Find("AllColumnNames").gameObject.GetComponent<TextMeshProUGUI>().text = columnsString;
        Debug.Log("Setting all columns UI for plot " + plotID);
    }

    public string GetPointInfo(int dataPointID){
        // Debug.Log("Getting point info for point " + dataPointID);

        string pointInfo = "";
        foreach (string key in columnList)
        {
            pointInfo += key + ": " + pointList[dataPointID][key] + " ";
        }
        return pointInfo;
    }

    public GameObject GetPlotObj(){
        return plotGameObject;
    }

    


}
