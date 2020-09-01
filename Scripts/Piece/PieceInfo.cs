/*
  Author      藤澤典隆
  LastUpdate  2020/04/13
  Since       2020/03/11
  prefabの名前を駒.cs内で定義しているので注意が必要
*/
using System.Collections.Generic;
using UnityEngine;


namespace Piece
{
    public abstract class PieceInfo
    {
        public PieceKind Name;
        public float Cost;
        public Dictionary<PlayerKind,GameObject> Prefab=new Dictionary<PlayerKind, GameObject>();
        public Dictionary<int, List<int>> MoveRange=new Dictionary<int, List<int>>();
        public Sprite Img;
    }
}
