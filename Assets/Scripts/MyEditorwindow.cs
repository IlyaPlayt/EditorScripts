using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class MyEditorwindow : EditorWindow
{
   private GameObject obj;
   private string userData;
   private Color color0;
   private Color color1;
   private Color standartColor;
   private Color[,] boxesColors = new Color[8,8];
   private Rect[,] boxesRects = new Rect[8, 8];
   private Event even;
   private Texture2D tex;
   private void OnEnable()
   {
      Debug.Log("OnEnable");
      for (int i = 0; i < 8; i++)
      {
         for (int j = 0; j < 8; j++)
         {
            boxesColors[i, j]=Color.green;
            boxesRects[i,j]=new Rect(75 * i + 280, 75 * j, 70, 70);
         }
      }
   }
   
   [MenuItem("Window/My Editor Wwindow")]
   private static void OpenMyEditorWindow()
   {
      GetWindow<MyEditorwindow>();
   }

   private void OnGUI()
   {
      even = Event.current;
      GUILayout.BeginHorizontal();
      GUILayout.BeginVertical();
      GUI.Label(new Rect(0,0,70,20),"Tool Bar");
      GUI.Label(new Rect(0,40,70,20),"Color 0");
      GUI.Label(new Rect(0,80,70,20),"Color 1");
      color0=EditorGUI.ColorField(new Rect(50,40,150,20),color0);
      color1=EditorGUI.ColorField(new Rect(50,80,150,20),color1);
      if (GUI.Button(new Rect(0, 120, 200, 20), "Fill All")) 
      {
         for (int i = 0; i < 8; i++)
         {
            for (int j = 0; j < 8; j++)
            {
               boxesColors[i,j] = color0;
            }
         }

      }
      GUI.Label(new Rect(0,550,100,20),"Output Renderer");
      obj = (GameObject)EditorGUI.ObjectField(new Rect(100, 550, 100, 20),obj,typeof(GameObject));
      if (GUI.Button(new Rect(0, 580, 200, 20), "Save to objact"))
      {
         MeshRenderer mr = obj.GetComponent<MeshRenderer>();
         tex=new Texture2D(8,8);
         for (int i = 0; i < 8; i++)
         {
         
            for (int j = 0; j < 8; j++)
            {
               tex.SetPixel(i,j,boxesColors[i,j]);
            }
         
         }
         tex.filterMode = FilterMode.Point;
         tex.Apply();
         mr.sharedMaterial.mainTexture = tex;

      }
    
      standartColor = GUI.color;
     
      for (int i = 0; i < 8; i++)
      {
         
         for (int j = 0; j < 8; j++)
         {
            GUI.color = boxesColors[i, j];
            GUI.Box( boxesRects[i,j], GUIContent.none);
         }
         
      }
      

      GUI.color = standartColor;
      if (even.type == EventType.MouseDown)
      {
         switch (even.button)
         {
            case (0):
            {
               ChangeColor(color0);
               break;
            }
            case (1):
            {
               ChangeColor(color1);
               break;
            }
         }

      }
   }

   private void ChangeColor(Color color)
   {
      for (int i = 0; i < 8; i++)
      {
         for (int j = 0; j < 8; j++)
         {
            if ( boxesRects[i,j].Contains(even.mousePosition) )
            {
               boxesColors[i,j] = color;
            }
          
         }
      }
      
   }
}
