/*
  Author      小路重虎
  LastUpdate  2020/05/09
  Since       2020/05/09
  Contents    PositionData用での保存用クラス
              インスタンス１個で駒１個分を保存
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Position
{
    public class PiecePosition
    {
        public int owner;//先手:0,後手:1
        public PieceKind pieceKind;
        public int faceId;
        public int forwardFaceId;
    }
}
