/*
  Author      藤澤典隆
  LastUpdate  2020/05/2
  Since       2020/03/11
  Contents    駒のカメラを切り替える

  CameraBaseがpublicになればOK

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager.PlayGameManager
{
    public class PlayCameraManager : MonoBehaviour
    {
        
        private int crrCameraId = 0;

        public void Start()
        {
            //デフォルトでキングとなるように設定する
            crrCameraId = ManagerStore.humanPlayer.GetMyPiecesByKind(PieceKind.King)[0].GetPieceId();
        }

        public void ChangeCamera(int pieceId)
        {
            if (ManagerStore.humanPlayer.HasPiece(crrCameraId))
            {
                var crrPiece = ManagerStore.humanPlayer.GetPieceById(crrCameraId);
                crrPiece.PieceCamera.ActivateCamera(false);
            }
            crrCameraId = pieceId;
            var newPiece = ManagerStore.humanPlayer.GetPieceById(pieceId);
            newPiece.PieceCamera.ActivateCamera(true);
        }

        public void CameraOff()
        {
            if (ManagerStore.humanPlayer.HasPiece(crrCameraId))
            {
                var crrPiece = ManagerStore.humanPlayer.GetPieceById(crrCameraId);
                crrPiece.PieceCamera.ActivateCamera(false);
            }
        }


    }

}

