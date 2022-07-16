using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;

public class DrawLine : MonoBehaviour
{
    //Vector3 camera_coordinates, clean_coordinates;

    List<Vector3> linePoints;
    float timer;
    public float timerDelay;
    public float lineWidth;

    GameObject newLine;
    LineRenderer drawLine;

    // Start is called before the first frame update
    void Start()
    {
        linePoints = new List<Vector3>();
        timer = timerDelay;

        //camera_coordinates = NRFrame.HeadPose.position;

        newLine = new GameObject();
        drawLine = newLine.AddComponent<LineRenderer>();
        drawLine.material = new Material(Shader.Find("Sprites/Default"));
        //drawLine.material.SetColor("LineMaterial", new Color(1f, 1f, 1f, 1f));
        drawLine.startColor = Color.red;
        drawLine.endColor = Color.blue;
        drawLine.startWidth = lineWidth;
        drawLine.endWidth = lineWidth;

        //Debug.Log(linePoints);
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            linePoints.Add(new Vector3(NRFrame.HeadPose.position[0], NRFrame.HeadPose.position[1], 2.82f));
            //Debug.Log(GetMousePosition());
            drawLine.positionCount = linePoints.Count;
            drawLine.SetPositions(linePoints.ToArray());

            timer = timerDelay;

            Debug.Log(NRFrame.HeadPose.position);
        }



        // clean the linePoints.Clean()
    }

    Color randomColor()
    {
        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}




// OLDONE PART 2

//// reference: https://www.youtube.com/watch?v=_ILOVprdq4o

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using NRKernal;

//public class DrawLine : MonoBehaviour
//{
//    public float lineWidth;
//    public GameObject brush;
//    LineRenderer currentlineRenderer;

//    Vector3 lastPos;

//    void Start()
//    {
//        CreateBrush();
//    }

//    void Update()
//    {
//        Draw();

//    }

//    void Draw()
//    {
//        Vector3 headPos = new Vector3(NRFrame.HeadPose.position[0], NRFrame.HeadPose.position[1], 3);
//        if (headPos != lastPos)
//        {
//            AddPoint(headPos);
//            lastPos = headPos;
//        }
//        Debug.Log("head pos: " + headPos);
//        Debug.Log("brush pos: " + currentlineRenderer.transform.position);
//    }

//    void CreateBrush()
//    {
//        GameObject brushInstance = Instantiate(brush);
//        currentlineRenderer = brushInstance.GetComponent<LineRenderer>();
//        currentlineRenderer.startColor = Color.green; //randomColor()
//        currentlineRenderer.endColor = Color.red;
//        currentlineRenderer.startWidth = lineWidth;
//        currentlineRenderer.endWidth = lineWidth;

//        Vector3 headPos = new Vector3(NRFrame.HeadPose.position[0], NRFrame.HeadPose.position[1], 3);
//        Debug.Log(headPos);

//        currentlineRenderer.SetPosition(0, headPos);
//        currentlineRenderer.SetPosition(1, headPos);
//    }

//    void AddPoint(Vector3 pointPos)
//    {
//        currentlineRenderer.positionCount++;
//        int positionIndex = currentlineRenderer.positionCount - 1;
//        currentlineRenderer.SetPosition(positionIndex, pointPos);
//    }

//    Color randomColor()
//    {
//        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
//    }
//}


// OLD ONE

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using NRKernal;

//public class DrawLine : MonoBehaviour
//{
//    Vector3 camera_coordinates, clean_coordinates;

//    List<Vector3> linePoints;
//    float timer;
//    public float timerDelay;
//    GameObject newLine;
//    LineRenderer drawLine;
//    public float lineWidth;
//    // Start is called before the first frame update
//    void Start()
//    {
//        camera_coordinates = NRFrame.HeadPose.position;

//        linePoints = new List<Vector3>();
//        timer = timerDelay;
//    }

//    // Update is called once per frame
//    void Update()
//    {

//        timer -= Time.deltaTime; //in order to capture points not every frame
//        if (timer <= 0)
//        {
//            camera_coordinates = NRFrame.HeadPose.position;
//            clean_coordinates = new Vector3(camera_coordinates[0], camera_coordinates[1], 0); //only y coordinates

//            linePoints.Add(clean_coordinates * 3);
//            Debug.Log(clean_coordinates * 3);
//            //Debug.Log(camera_coordinates);
//            newLine = new GameObject();
//            drawLine = newLine.AddComponent<LineRenderer>();
//            //drawLine.material = new Material(Shader.Find("Sprite/Default"));
//            drawLine.startColor = Color.green; //randomColor()
//            drawLine.endColor = Color.red;
//            drawLine.startWidth = lineWidth;
//            drawLine.endWidth = lineWidth;

//            drawLine.positionCount = linePoints.Count;
//            drawLine.SetPositions(linePoints.ToArray());
//            //drawLine.positionCount = linePoints.Count;
//            //drawLine.SetPositions(linePoints.ToArray());

//            //Debug.Log(linePoints);

//            timer = timerDelay;
//        }



//        // clean the linePoints.Clean()
//    }

//    Color randomColor()
//    {
//        return Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
//    }
//}
