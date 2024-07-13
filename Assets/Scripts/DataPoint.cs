using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPoint
{
   private float postionX;
   private float postionY;
   private float postionZ;
   private string transformName;
   private int dataPointID; // unique identifier for each data point; serves as index in pointList of each Plot
   private Color color;

   /* Constructor for a single DataPoint
   Should be instantiated and placed into its corresponding Plot structure in DataPlotter.cs*/
   public DataPoint (float postionX, float postionY, float postionZ, string transformName, int dataPointID, Color color)
   {
       this.postionX = postionX;
       this.postionY = postionY;
       this.postionZ = postionZ;
       this.transformName = transformName;
       this.dataPointID = dataPointID;
       this.color = color;
   }
}
