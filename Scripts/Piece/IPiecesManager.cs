/*
  Author      藤澤典隆
  LastUpdate  2020/07/11
  Since       2020/03/16
*/

using UnityEngine;
using System.Collections.Generic;

namespace Piece
{
    interface IPiecesManager
    {
        GameObject GetPiecePrefab(PieceKind pieceKind, PlayerKind playerKind);
        float GetSummonCost(PieceKind pieceKind);
        Dictionary<int, List<int>> GetMoveRange(PieceKind pieceKind);
        Sprite GetImg(PieceKind pieceKind);
    }

}
