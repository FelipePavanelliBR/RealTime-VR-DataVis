using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using System;

public class DataPlotter : MonoBehaviour
{
    public string inputfile; // name of pathname, no extension
    private List<Dictionary<string, object>> pointList; // will hold data from CSV reader

    // Indices for columns to be assigned
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;

    // Full column names
    public string xName;
    public string yName;
    public string zName;

    // The prefab for the data points to be instantiated
    public GameObject PointPrefab;
    public GameObject PointHolder;

    public float plotScale = 6f;
    public float plotElevation = 0.01f;


    void Start()
    {
        pointList = CSVReader.Read(inputfile);
        Debug.Log(pointList);
        List<string> columnList = new List<string>(pointList[1].Keys);
        // Print number of keys (using .count)
        Debug.Log("There are " + columnList.Count + " columns in CSV");
        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];

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
        for (var i = 0; i < pointList.Count; i++)
        {
        // Get value in poinList at ith "row", in "column" Name, normalize
        float x = (System.Convert.ToSingle(pointList[i][xName]) - xMin) / (xMax - xMin);
        float y = (System.Convert.ToSingle(pointList[i][yName]) - yMin) / (yMax - yMin);
        float z = (System.Convert.ToSingle(pointList[i][zName]) - zMin) / (zMax - zMin);

        //instantiate the prefab with coordinates defined above
        GameObject dataPoint = Instantiate(PointPrefab, new Vector3(x, y, z) * plotScale, Quaternion.identity);

        //make dataPoint a child of PointHolder
        dataPoint.transform.parent = PointHolder.transform;

        // Assigns original values to dataPointName
        string dataPointName = pointList[i][xName] + " " + pointList[i][yName] + " " + pointList[i][zName];

        // Assigns name to the prefab
        dataPoint.transform.name = dataPointName;


        // Gets material color and sets it to a new RGBA color we define
        dataPoint.GetComponent<Renderer>().material.color =
        new Color(x,y,z, 1.0f);
        }

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
