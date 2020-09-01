/*
  Author      小路重虎
  LastUpdate  2020/07/04
  Since       2020/05/09
  Contents    盤面再現用のクラス
*/
/*
  PositionObjectsプレハブから使用すること
  プレハブを置いてGetComponent<PositionObjects>()で使える
  初期状態は何も置いてない状態、GetPlayer(0).AddMyPieces()とかで駒を置ける
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;
using Piece;
using Field;
using Move;

namespace Position
{
    public class PositionObjects : MonoBehaviour, IPosition
    {
        private PlayerBase[] player = new PlayerBase[2];//0:先手,1:後手
        private FieldManager fieldManager;
        private PiecesManager piecesManager;
        private GameObject field;

        void Awake()
        {
            Init();
        }
        
        //PlayerBase取得 nは0か1
        public PlayerBase GetPlayer(int n)
        {
            return this.player[n];
        }

        //FieldManager取得
        public FieldManager GetFieldManager()
        {
            return this.fieldManager;
        }

        //PiecesManager取得
        public PiecesManager GetPiecesManager()
        {
            return this.piecesManager;
        }

        //局面を動かす
        public void Move(IForwardMove move)
        {
            int movePieceId = fieldManager.IsPieceOnFace(move.GetMoveFromFaceId());
            PlayerBase myPlayer, enemyPlayer;

            if (player[0].HasPiece(movePieceId))
            {
                myPlayer = player[0];
                enemyPlayer = player[1];
            }
            else
            {
                myPlayer = player[1];
                enemyPlayer = player[0];
            }

            if (move.IsMove())//動きの場合
            {
                int capturedPieceId = fieldManager.IsPieceOnFace(move.GetMoveToFaceId());
                if (capturedPieceId != -1)//駒を取っていた場合
                {
                    enemyPlayer.DeleteMyPieces(capturedPieceId);
                }
                myPlayer.MovePiece(movePieceId, move.GetMoveToFaceId());
            }
            else//回転の場合
            {
                myPlayer.RotatePiece(movePieceId, move.GetRotateDirection());
            }
        }

        //局面を戻す
        public void MoveBack(IReversibleMove move)
        {
            int movePieceId = fieldManager.IsPieceOnFace(move.GetMoveToFaceId());
            PlayerBase myPlayer, enemyPlayer;

            if (player[0].HasPiece(movePieceId))
            {
                myPlayer = player[0];
                enemyPlayer = player[1];
            }
            else
            {
                myPlayer = player[1];
                enemyPlayer = player[0];
            }

            if (move.IsMove())//動きの場合
            {
                Pieces movePiece = myPlayer.GetPieceById(movePieceId);
                myPlayer.MovePiece(movePieceId, move.GetMoveFromFaceId());//駒をもとの位置へ
                int rotationNum = 0;
                while (movePiece.GetForwardFaceId() != move.GetMoveFromForwardFaceId())
                {
                    myPlayer.RotatePiece(movePieceId, 1);//向きが合うまで回転させて駒の向き修正
                    rotationNum++;
                    if (rotationNum > 20)
                    {//20回転以上で中断
                        Debug.Log("エラー　向きを戻せませんでした"); break;
                    }
                }
                if (move.IsCaptured())//駒を取っていた場合
                {
                    enemyPlayer.AddMyPieces
                        (move.GetCapturedPieceKind(), move.GetMoveToFaceId()
                        , move.GetCapturedPieceForwardFaceId(), false);//取った敵の駒を復活させる
                }
            }
            else//回転の場合
            {
                myPlayer.RotatePiece(movePieceId, -move.GetRotateDirection());
            }
        }

        //局面保存
        public PositionData Save()
        {
            return PositionData.SaveByPlayer(player[0], player[1]);
        }

        //局面読み込み
        public void Load(PositionData pd)
        {
            Clear();
            foreach(PiecePosition piecePos in pd.piecePositions)
            {
                player[piecePos.owner].AddMyPieces
                    (piecePos.pieceKind, piecePos.faceId, piecePos.forwardFaceId, false);
            }
        }

    //privateメソッド

        //オブジェクトの初期セッティング
        private void Init()
        {
            fieldManager = transform.Find("FieldManager").GetComponent<FieldManager>();
            piecesManager = transform.Find("PiecesManager").GetComponent<PiecesManager>();
            SetPlayers();
        }

        //Player２つをResourcesから新規生成
        private void SetPlayers()
        {
            GameObject prefab = (GameObject)Resources.Load("InitialPosition/Player");
            GameObject obj;
            PlayerKind playerKind;
            for (int i = 0; i <= 1; i++)
            {
                obj = Instantiate(prefab, this.transform);
                obj.name = $"Player{i}";
                this.player[i] = obj.GetComponent<PlayerBase>();
                if (i == 0)//
                {
                    playerKind = PlayerKind.HumanPlayer;
                }
                else
                {
                    playerKind = PlayerKind.CP;
                }
                this.player[i].SetPlayerKind(playerKind);
                this.player[i].SetManager(fieldManager, piecesManager);
            }
        }

        //局面データ全削除
        private void Clear()
        {
            foreach (PlayerBase player in this.player)
            {
                if (player != null)
                {
                    player.ClearPieces();
                    Destroy(player.gameObject);
                }
            }
            SetPlayers();
        }

        
    }

}
