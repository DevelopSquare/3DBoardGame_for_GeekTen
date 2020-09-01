/*
  Author      田中木介
  LastUpdate  2020/03/14
  Since       2020/03/11
  Contents    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{

    interface IField 
    {
        Vector3 GetPositionFromId(int id);

        void WriteFieldInfo(int pieceId,int faceId);

        void DeleteFieldInfo(int pieceId);

        int ReadFieldInfo(int pieceId);

        List<int> GetAdjacentSurface(int crrId , Vector3 FowardDirection);

        void PointMovableFace(List<int> movableFace);

        int ConvertRelative2AbsId(int crrId,int relativeId,Vector3 pieceDirection);

        void SetPieceAsChild(int faceId,GameObject pieceObj);

        int IsPieceOnFace(int faceId);

        List<int> GetPieceCPSettableFace();

        List<int> GetPiecePlayerSettableFace();


    }

}

