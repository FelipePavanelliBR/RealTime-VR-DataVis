using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNavigationControllers : MonoBehaviour
{
   
    public GameObject block;
    public float rotateSpeed = 10f;
    public float smoothTime = 0.1f;
    
    private float targetRotationY;
    private float currentRotationY;
    private float rotationVelocity;


    public Canvas PlotTypeInfoCanvas;
    public Canvas DataPointInfoCanvas;

    // Start is called before the first frame update
    void Start()
    {
        targetRotationY = block.transform.eulerAngles.y;
        currentRotationY = targetRotationY;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal_Primary");
        if (horizontalInput > 0.9 || horizontalInput < -0.9)
        {
            targetRotationY += horizontalInput * rotateSpeed;
        }

        currentRotationY = Mathf.SmoothDampAngle(currentRotationY, targetRotationY, ref rotationVelocity, smoothTime);
        block.transform.eulerAngles = new Vector3(block.transform.eulerAngles.x, currentRotationY, block.transform.eulerAngles.z);
        

        // LookAt method for inverted Canvas; Making the cameras "look away", so it's always facing the user
        PlotTypeInfoCanvas.transform.rotation = Quaternion.LookRotation(PlotTypeInfoCanvas.transform.position - Camera.main.transform.position);
        DataPointInfoCanvas.transform.rotation = Quaternion.LookRotation(DataPointInfoCanvas.transform.position - Camera.main.transform.position);

    }
}
