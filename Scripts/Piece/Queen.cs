/*
  Author      藤澤典隆
  LastUpdate  2020/07/11
  Since       2020/03/16
  Contents    Queenの情報
*/

using System.Collections.Generic;
using UnityEngine;

namespace Piece
{
    class Queen : PieceInfo
    {
        public Queen()
        {
            Name = PieceKind.Queen;
            Img = (Sprite)Resources.Load<Sprite>("PieceImg/QueenImg");
            Cost = 50;
            Prefab[PlayerKind.CP] = (GameObject)Resources.Load("BlackQueen");
            Prefab[PlayerKind.HumanPlayer] = (GameObject)Resources.Load("WhiteQueen");
            MoveRange = new Dictionary<int, List<int>>()
            {
                { 4 , new List<int>() { 0 , 4} },
                { 6 , new List<int>() { 0 , 6} },
                { 10 , new List<int>(){ 0 , 10 } },
            };
        }

    }
}
