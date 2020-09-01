/*
  Author      田中木介
  LastUpdate  2020/05/30（2020/08/28 髙橋）
  Since       2020/03/14
  Contents    
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameManager;
using Piece;
using Piece.Extend;


namespace MiniMap
{
    public class MiniMapManager : MonoBehaviour
    {
        private enum PlayerKind 
        {
            Player01,
            Player02
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

            ChangePiecesLayer();
        }

        private void ChangePiecesLayer()
        {


            var pieces = ManagerStore.humanPlayer.GetMyPieces();

            //pieces[0].gameObject.layer = 10;//LayerMask.NameToLayer("Player02");
            //Debug.Log(pieces[0].gameObject.layer);
            var foundPieces = CheckExitPieces(pieces);

            ResetLayer(foundPieces);

            for (int i =0; i<foundPieces.Count;i++)
            {
                int pieceId = foundPieces[i];
                if (ManagerStore.cp.HasPiece(pieceId))
                {

                    var piece = ManagerStore.cp.GetPieceById(pieceId);
                    //piece.gameObject.layer = 9;//LayerMask.NameToLayer("Player01");
                    ChangeLayer(piece, PlayerKind.Player01.ToString());

                    //foundPieces.Remove(foundPieces[i]);

                    //Debug.Log("Layer excution " + piece.gameObject.layer);
                    //Debug.Log("Layer name " + piece.GetPieceId());
                }



            }


        }

        private List<int> CheckExitPieces(List<Pieces> pieces)
        {
            var foundPieces = new List<int>();

            foreach (Pieces piece in pieces)
            {
                var forwardFace = piece.GetForwardFaceId();
                var visibleFaces = piece.GetVisibleFaces();

                for (int i = 0; i < visibleFaces.Count; i++)
                {
                    var fFaceID = ManagerStore.fieldManager.ConvertRelative2AbsId(piece.GetFaceId(), visibleFaces[i],piece.GetForwardDirection());

                    var fFace = ManagerStore.fieldManager.IsPieceOnFace(fFaceID);

                    //Debug.Log("PieceFace "+fFace);
                    if (fFace > -1) foundPieces.Add(fFace);
                }
            }

            return foundPieces;
        }

        private void ResetLayer(List<int> foundPieces)
        {
            var cpPieces = ManagerStore.cp.GetMyPieces();
            foreach(Pieces piece in cpPieces)
            {
                if (foundPieces.IndexOf(piece.GetPieceId()) < 0)
                {
                    //piece.gameObject.layer = LayerMask.NameToLayer(PlayerKind.Player02.ToString()); 
                    ChangeLayer(piece, PlayerKind.Player02.ToString());
                }
                
            }
        }

        private void ChangeLayer(Pieces pieces , string layerName)
        {
            //Circleだったのを、Bodyと子のEnemyに変更
            var pieceCircleObj = pieces.transform.Find("Body").gameObject.transform.Find("Enemy").gameObject;
            if (pieceCircleObj == null) return;
            pieceCircleObj.layer= LayerMask.NameToLayer(layerName);

        }
    }


}

