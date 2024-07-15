using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System;

public class DataPlotter : MonoBehaviour
{
    private List<Dictionary<string, object>> pointList; // will hold data from CSV reader

    // Full column names
    private string xName;
    private string yName;
    private string zName;

    // The prefab for the data points to be instantiated
    public GameObject PointPrefab;
    public GameObject PlotPrefab;

    public float plotScale = 6f;
    public float plotElevation = 0.01f;


    public void CreateNewPlot(string inputfile, int plotID, GameObject plotSpawn, int columnX, int columnY, int columnZ)
    {
        pointList = CSVReader.Read(inputfile);
        List<string> columnList = new List<string>(pointList[1].Keys);
        GameObject plotInScene = Instantiate(PlotPrefab, plotSpawn.transform.position, Quaternion.identity);
        plotInScene.transform.name = plotID + " Plot";


        Plot currPlot = new Plot(pointList, columnList, 0, plotScale, plotInScene);

        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];


        currPlot.SetColumnNames(xName, yName, zName);

        foreach (string key in columnList)
        Debug.Log("Column name is " + key);

        // Get maxes of each axis
        float xMax = FindMaxValue(xName);
        float yMax = FindMaxValue(yName);
        float zMax = FindMaxValue(zName);

        // Get minimums of each axis
        float xMin = FindMinValue(xName);
        float yMin = FindMinValue(yName);
        float zMin = FindMinValue(zName);
        Debug.Log("===Maximuns===");
        Debug.Log("xMax: " + xMax);
        Debug.Log("yMax: " + yMax);
        Debug.Log("zMax: " + zMax);
        
    
        Debug.Log("===Minimuns===");
        Debug.Log("xMin: " + xMin);
        Debug.Log("yMin: " + yMin);
        Debug.Log("zMin: " + zMin);


        //Loop through Pointlist
        Debug.Log(pointList.Count);
        for (var i = 0; i < pointList.Count; i++)
        {
        // Get value in poinList at ith "row", in "column" Name, normalize
        float x = (System.Convert.ToSingle(pointList[i][xName]) - xMin) / (xMax - xMin);
        float y = (System.Convert.ToSingle(pointList[i][yName]) - yMin) / (yMax - yMin);
        float z = (System.Convert.ToSingle(pointList[i][zName]) - zMin) / (zMax - zMin);

        //instantiate the prefab with coordinates defined above
        
        GameObject dataPoint = Instantiate(PointPrefab, new Vector3(x, y, z) * plotScale, Quaternion.identity); 
        
        //make dataPoint a child of PointHolder
        GameObject PointHolder = plotInScene.transform.Find("PointHolder").gameObject;
        dataPoint.transform.parent = PointHolder.transform;  
        
        // Assigns original values to dataPointName
        string dataPointName =i + " " + pointList[i][xName] + " " + pointList[i][yName] + " " + pointList[i][zName];

        // Assigns name to the prefab
        dataPoint.transform.name = dataPointName;


        // Gets material color and sets it to a new RGBA color we define
        Color c = new Color(x, y, z, 1.0f);
        dataPoint.GetComponent<Renderer>().material.color = c;
        
        DataPoint dp = new DataPoint(x, y, z, dataPointName, i, c);
        currPlot.AddPointToScene(dp);
        }

        StateManager.Instance.AddPlot(currPlot);

        // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // cube.transform.position = PointHolder.transform.position;
        // cube.transform.localScale = new Vector3(plotScale,plotScale, plotScale);
    }

    private float FindMaxValue(string columnName)
{
    //set initial value to first value
    float maxValue = Convert.ToSingle(pointList[0][columnName]);

    //Loop through Dictionary, overwrite existing maxValue if new value is larger
    for (var i = 0; i < pointList.Count; i++)
    {
        if (maxValue < Convert.ToSingle(pointList[i][columnName]))
            maxValue = Convert.ToSingle(pointList[i][columnName]);
    }

    //Spit out the max value
    return maxValue;
}


private float FindMinValue(string columnName)
   {

       float minValue = Convert.ToSingle(pointList[0][columnName]);

       //Loop through Dictionary, overwrite existing minValue if new value is smaller
       for (var i = 0; i < pointList.Count; i++)
       {
           if (Convert.ToSingle(pointList[i][columnName]) < minValue)
               minValue = Convert.ToSingle(pointList[i][columnName]);
       }

       return minValue;
   }

}
