/*
  Author      小路重虎
  LastUpdate  2020/06/13
  Since       2020/03/11
  Contents    プレイヤー情報に関するスクリプト
              IPlayerインターフェース（PlayerBaseクラスのインタフェース）
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Field;
using Piece;

namespace Player
{
    interface IPlayer
    {
        //PlayerKindを設定する
        void SetPlayerKind(PlayerKind playerKind);

        //FieldManager,PiecesManagerを置き換える
        void SetManager(FieldManager fieldManager, PiecesManager piecesManager);

        //プレイヤーKindを取得
        PlayerKind GetPlayerKind();

        //駒をプレイヤーのものとして登録
        void AddMyPieces(PieceKind piecekind, int absoluteFaceId, int forwardFaceId, bool withAnimation = true);

        //駒を削除する
        void DeleteMyPieces(int pieceId);

        //面の絶対Idを指定するとそこに駒が移動する 移動先に駒があると消される
        void MovePiece(int pieceId, int faceId);

        //駒をrotate辺回転する　ex)rotateが-1で10角形なら-36度
        void RotatePiece(int pieceId, int rotateDirection);

        //プレイヤーが持っている駒を取得
        List<Pieces> GetMyPieces();

        //駒の種類を限定してプレイヤーが持っている駒を取得
        List<Pieces> GetMyPiecesByKind(PieceKind piecekind);

        //IDから駒を取得
        Pieces GetPieceById(int pieceId);

        //そのIDの駒を持っているかどうか
        bool HasPiece(int pieceId);

        //駒オブジェクトを全削除 シーン切り替え時以外はDestroyの前に必ず実行
        void ClearPieces();
    }
}
