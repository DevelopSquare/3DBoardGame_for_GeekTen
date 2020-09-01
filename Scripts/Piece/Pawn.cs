/*
  Author      藤澤典隆
  LastUpdate  2020/07/11
  Since       2020/03/16
  Contents    Pawnの情報
*/

using System.Collections.Generic;
using UnityEngine;

namespace Piece
{
    class Pawn : PieceInfo
    {
        public Pawn()
        {
            Name = PieceKind.Pawn;
            Cost = 10;
            Img = (Sprite)Resources.Load<Sprite>("PieceImg/PawnImg");
            Prefab[PlayerKind.CP] = (GameObject)Resources.Load("BlackPawn");
            Prefab[PlayerKind.HumanPlayer] = (GameObject)Resources.Load("WhitePawn");
            MoveRange = new Dictionary<int, List<int>>()
            {
                { 4 , new List<int>() { 0 } },
                { 6 , new List<int>() { 0  } },
                { 10 , new List<int>(){ 0  } },
            };
        }

    }
}
