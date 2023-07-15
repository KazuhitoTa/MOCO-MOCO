using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CreateMap : ScriptableObject
{
    public List<MapDate> mapDateList=new List<MapDate>();
    //public MapDate mapDate;
   
   [System.Serializable]
   public class MapDate
   {
       [SerializeField]public int xMax;
       [SerializeField]public int xMin;
       [SerializeField]public int yMax;
       [SerializeField]public int yMin;
       [SerializeField]public List<Vector3> mapObjPos=new List<Vector3>();
       [SerializeField]public List<int> mapGenre=new List<int>();
       


       public List<Vector3> MapObjPos{get=>mapObjPos;}
       public List<int> MapGenre{get=>mapGenre;}

       public int XMax{get=>xMax;}
       public int XMin{get=>xMin;}
       public int YMax{get=>yMax;}
       public int YMin{get=>yMin;}
   }

}
