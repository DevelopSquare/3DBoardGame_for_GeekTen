/*
  Author      藤澤典隆
  LastUpdate  2020/05/16
  Since       2020/03/11
  Contents    PlayGameでゲーム勝者を決定する。勝者が決定した場合はリザルトシーンへ移行する

    勝利条件
        -敵のキングを取る
        -敵の極点に自分のピースがいる

*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using UnityEngine.SceneManagement;
using System;

public class GameJudgment : MonoBehaviour
{
    public void WinnerCheck()
    {
        //キングの有無の確認
        List<Piece.Pieces> humanKing = ManagerStore.humanPlayer.GetMyPiecesByKind(PieceKind.King);
        List<Piece.Pieces> cpKing = ManagerStore.cp.GetMyPiecesByKind(PieceKind.King);
        if (humanKing.Count == 0)
        {
            WinnerSet(PlayerKind.CP);
            WinTypeSet(WinType.GetKing);
        }
        else if (cpKing.Count == 0) {
            WinnerSet(PlayerKind.HumanPlayer);
            WinTypeSet(WinType.GetKing);
        }
        

        //極点への到達の有無
        List<int> CPPole = GameManager.ManagerStore.fieldManager.GetPieceCPSettableFace();
        if (ManagerStore.humanPlayer.HasPiece(ManagerStore.fieldManager.IsPieceOnFace(CPPole[0])) == true) {
            WinnerSet(PlayerKind.HumanPlayer);
            WinTypeSet(WinType.ReachPole);
        }
        List<int> HumanPole = GameManager.ManagerStore.fieldManager.GetPiecePlayerSettableFace();
        if (ManagerStore.cp.HasPiece(ManagerStore.fieldManager.IsPieceOnFace(HumanPole[0])) == true) {
            WinnerSet(PlayerKind.CP);
            WinTypeSet(WinType.ReachPole);
        }

    }

    private void WinnerSet(PlayerKind winner)
    {
        ResultData.winner = winner;
    }

    private void WinTypeSet(WinType winType)
    {
        ResultData.wintype = winType;
    }

}
