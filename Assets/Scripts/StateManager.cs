using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class StateManager : MonoBehaviour
{
    public static StateManager Instance;
    public List<Plot> plots;

    public DataPlotter dataPlotter;

    // Defining a plot
    public GameObject PlotSpawn;
    public string inputfile;
    public int columnX = 0;
    public int columnY = 1;
    public int columnZ = 2;

    // public List<Dictionary<string, object>> pointList; // will hold data from CSV reader



    // public enum CameraState { Default, Focus, Analyze }
    // public CameraState currentCameraState = CameraState.Default;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        plots = new List<Plot>();
    }

    public void AddPlot(Plot plot)
    {
        plots.Add(plot);
        Debug.Log("Plot added to StateManager. Number of plots: " + plots.Count);
    }

    public void NewPlot(){
        dataPlotter.CreateNewPlot(inputfile, plots.Count, PlotSpawn, columnX, columnY, columnZ);
    }

    public void SelectPoint(int plotID, int dataPointID)
    {
        Debug.Log("Selected point " + dataPointID + " in plot " + plotID);
        plots[plotID].GetPlotObj().transform.Find("DataPointInfoCanvas").gameObject.transform.Find("DataPointStats").gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = plots[plotID].GetPointInfo(dataPointID);
    }

}
