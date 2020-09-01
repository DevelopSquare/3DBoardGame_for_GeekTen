/*
  Author      藤澤典隆
  LastUpdate  2020/07/11
  Since       2020/03/11
  Contents    Pieceに関するScript。適当な空のオブジェクトにアタッチする。
*/

/*駒の追加する場合:
 抽象クラスPieceInfoを継承したクラスを作成し、PieceManagerのawakeに追加する*/

using System.Collections.Generic;
using UnityEngine;
using GameManager;

namespace Piece
{
    public class PiecesManager : MonoBehaviour, IPiecesManager
    {
        Dictionary<PieceKind, PieceInfo> AllPieceInfo = new Dictionary<PieceKind, PieceInfo>();
        Dictionary<int, Pieces> AllPieces = new Dictionary<int, Pieces>();

        void Awake()
        {
            //Debug.Log((GameObject)Resources.Load("BlackKing"));
            AllPieceInfo = new Dictionary<PieceKind, PieceInfo>()
            {
                { PieceKind.King, new King()  },
                { PieceKind.Queen , new Queen() },
                { PieceKind.Pawn , new Pawn() },
            };
        }


        //PieceのPrefabを返す
        public GameObject GetPiecePrefab(PieceKind pieceKind,PlayerKind playerKind)
        {
            foreach (KeyValuePair<PieceKind,PieceInfo> value in AllPieceInfo)
            {
                if (value.Key == pieceKind) return value.Value.Prefab[playerKind];
            }
            return null;
        }

        //召喚に必要なコスト取得
        public float GetSummonCost(PieceKind pieceKind)
        {
            return AllPieceInfo[pieceKind].Cost;
        }

        //可動範囲の取得。相対FaceIdのDicを返す
        public Dictionary<int, List<int>> GetMoveRange(PieceKind pieceKind)
        {
            return AllPieceInfo[pieceKind].MoveRange;
        }

        public Sprite GetImg(PieceKind pieceKind)
        {
            foreach (KeyValuePair<PieceKind, PieceInfo> value in AllPieceInfo)
            {
                if (value.Key == pieceKind) return value.Value.Img;
            }
            return null;
        }

     }

}
