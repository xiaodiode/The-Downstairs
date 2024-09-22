
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerLightEclipse))]
class EditorPlayerLightEclipse : Editor
{
    void OnSceneGUI()
    {
        int nSamples = 100;
        PlayerLightEclipse eclipse = target as PlayerLightEclipse;
        Vector3[] points = new Vector3[nSamples * 2];
        for (int i = 0; i < nSamples; i++)
        {
            float angle1 = Mathf.PI * 2 / nSamples * i;
            Vector3 point1 = new Vector3(Mathf.Cos(angle1) * eclipse.lenLongAxis,
                Mathf.Sin(angle1) * eclipse.lenShortAxis, 0);
            point1 += eclipse.transform.position;
            float angle2 = Mathf.PI * 2 / nSamples * (i+1);
            Vector3 point2 = new Vector3(Mathf.Cos(angle2) * eclipse.lenLongAxis,
                Mathf.Sin(angle2) * eclipse.lenShortAxis, 0);
            point2 += eclipse.transform.position;
            points[i*2] = point1;
            points[i*2+1] = point2;
        }
        Handles.DrawLines(points);
    }
    
    // void OnSceneGUI()
    // {
    //     PlayerLightEclipse connectedObjects = target as PlayerLightEclipse;
    //     if (connectedObjects.objs == null)
    //         return;
    //
    //     Vector3 center = connectedObjects.transform.position;
    //     for (int i = 0; i < connectedObjects.objs.Length; i++)
    //     {
    //         GameObject connectedObject = connectedObjects.objs[i];
    //         if (connectedObject)
    //         {
    //             Handles.DrawLine(center, connectedObject.transform.position);
    //         }
    //         else
    //         {
    //             Handles.DrawLine(center, Vector3.zero);
    //         }
    //     }
    // }
}

