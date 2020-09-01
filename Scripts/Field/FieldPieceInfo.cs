using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
public class FieldPieceInfo : MonoBehaviour
{
        public Dictionary<int, int> FieldPieceInfoDict = new Dictionary<int,int>(); // int pieceId, int faceId 

        void Start()
        {
            FieldPieceInfoDict.Add(-1,-1);
        }

    }


}

