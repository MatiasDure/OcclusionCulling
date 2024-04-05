using System.Text;
using Unity.Profiling;
using UnityEngine;

public class TriangleCounter : MonoBehaviour
{
    //string statsText
    private float _accum = 0f;
    private int _frames = 0;
    ProfilerRecorder trianglesRecorder;

    void OnEnable()
    {
        trianglesRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");
    }

    void OnDisable()
    {
        trianglesRecorder.Dispose();
    }

    public void Track(float pTimeLeft)
    {
        if (trianglesRecorder.Valid)
        {
            _accum += trianglesRecorder.LastValue;
            _frames++;

            if (pTimeLeft <= 0)
            {
                float trianglesAverage = _accum / _frames;
                _accum = 0;
                _frames = 0;
                FileHandler.WriteToFile("triangles: " + trianglesAverage);

                //Debug.Log("Triangles: " + trianglesAverage);
            }
        }
    }

}
