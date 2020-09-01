/*
  Author      藤澤典隆
  LastUpdate  2020/07/11
  Since       2020/03/16
  フィールドに設置された駒がもつクラス
*/
using UnityEngine;
using UnityEngine.UI;
namespace Piece
{
    interface IPieces
    {
        void Init(int pieceId, PieceKind kindName,int faceId,int forwardFaceId);

        int GetPieceId();
        PieceKind GetKind();
        int GetFaceId();
        int GetShape();
        Vector3 GetForwardDirection();
        int GetForwardFaceId();

        //SetFaceIdではShapeも設定する。
        void SetFaceId(int faceId);
        void SetForwardFaceId(int forwardFaceId);
    }
}
