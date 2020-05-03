using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(GridBuilder))]
public class GridBuilderEditor : Editor
{
    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneUpdate;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneUpdate;
    }


    private void OnSceneUpdate(SceneView sv)
    {
        Event e = Event.current;
        GridBuilder gridBuilder = (GridBuilder)target;

        if(e.control)
        {
            var mousePos = e.mousePosition;
            var ppp = EditorGUIUtility.pixelsPerPoint;
            mousePos.y = sv.camera.pixelHeight - mousePos.y * ppp;
            mousePos.x *= ppp;

            Ray ray = sv.camera.ScreenPointToRay(mousePos);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                if(e.type == EventType.MouseDown)
                {
                    if(e.button ==0)
                    {
                        if(hit.collider.TryGetComponent(out EditorHexTile editorHexTile))
                        {
                            gridBuilder.InstantiateHexTile(editorHexTile.Coords, editorHexTile.transform.position);
                        }
                    }
                }
            }
        }
    }
}
