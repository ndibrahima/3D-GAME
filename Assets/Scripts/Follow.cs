using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Camera))]
public class Follow : MonoBehaviour
{
    public List<GameObject> targets;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        var minX = targets.Min(t => t.transform.position.x);
        var maxX = targets.Max(t => t.transform.position.x);
        var minY = targets.Min(t => t.transform.position.y);
        var maxY = targets.Max(t => t.transform.position.y);
        var desiredWidth = maxX - minX;
        var desiredHeight = maxY - minY;
        var currentWidth = Screen.width;
        var currentHeight = Screen.height;
        var targetSize
            = desiredWidth > desiredHeight
            ? ((desiredWidth / currentWidth) * currentHeight) / 2.0f
            : ((desiredHeight / currentHeight) * currentWidth) / 2.0f
            ;
        targetSize += 4.0f;
        this.cam.orthographicSize = Mathf.Lerp(this.cam.orthographicSize, targetSize, Time.deltaTime);

        var position = this.cam.transform.position;
        position.x = maxX * 0.5f + minX * 0.5f;
        position.y = maxY * 0.5f + minY * 0.5f;
        this.cam.transform.position = position;
    }

}