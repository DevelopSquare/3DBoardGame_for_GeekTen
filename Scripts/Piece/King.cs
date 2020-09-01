/*
  Author      藤澤典隆
  LastUpdate  2020/07/11
  Since       2020/03/16
  Contents    Kingの情報
*/

using System.Collections.Generic;
using UnityEngine;

namespace Piece
{
    class King : PieceInfo
    {
        public King()
        {
            Name = PieceKind.King;
            Cost = 0;
            Img = Resources.Load<Sprite>("PieceImg/KingImg");
            if (Img == null) Debug.Log("Img null");
            else Debug.Log("Img not null");
            var obj = (GameObject)Resources.Load("BlackKing");
            this.Prefab.Add(PlayerKind.CP, obj);
            this.Prefab[PlayerKind.HumanPlayer] = (GameObject)Resources.Load("WhiteKing");
            MoveRange = new Dictionary<int, List<int>>()
            {
                { 4 , new List<int>() { 0 , 1,  2 , 3 } },
                { 6 , new List<int>() { 0 , 1 , 2 , 3 , 4 , 5 , } },
                { 10 , new List<int>(){ 0 , 1 , 2 , 3 , 4 , 5 , 6 , 7 , 8 , 9 } },
            };
        }

    }
}
