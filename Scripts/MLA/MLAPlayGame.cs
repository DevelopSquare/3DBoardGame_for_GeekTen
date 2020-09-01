/*
  Author      藤澤典隆 remake by KatoMori
  LastUpdate  2020/05/2
  Since       2020/03/11
  Contents    PlayGameシーン全てを統括する

  1.TurnStartInit カメラ位置など初期化
  2.PlayerTurn or CPTurn　行動選択
  3.ExcuteMotion  3-1,3-2後に移動、回転
    3-1.CanMoveCollision 移動後の駒の処理
    3-2.ChangeTurn       ターン交代
  5.WinnerChechk    処理条件確認、画面遷移

*/
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Effect;

//Human == AgentA, CPU == AgentB
namespace GameManager.PlayGameManager
{
    public class MLAPlayGame : StateBase
    {
        /*
        //プレイヤーの駒選択関連
        int selectedFaceId = -1;
        int selectedPieceId = -1;
        int selectedRotateDirection = 0;
        /*
        int effectHumanPiece=-1;
        int humanPlayerKing;
        */
        int effectAgentAPiece = -1;
        int AgentAPlayerKing;
        Piece.Pieces selectPiece;
        Dictionary<int, List<int>> selectAllRelativePieceMoveRange;
        List<int> selectRelativePieceMoveRange;
        List<int> selectAbsolutePieceMoveRange;
        Field.SurfaceInfo selecttedSurfaceInfo;
        Vector3 selectedPieceDirection;

        //各種フラグ
        bool isNeedTurnStartInit = true;
        bool isActionDecide = false;
        bool isDisPlayForm = false;

        //駒の動きの種類
        ActionKind action = ActionKind.Unset;

        //外部より利用するクラス　初期化はawake
        CP.CPBrain CPB;
        CP.CPBrain HUM;
        PlayCameraManager PCM;
        TurnManager TM;
        ListManager.ListCtrl LM;
        GameJudgment GJ;
        public InputID.InputManager IM;
        

        //必要なGameObject
        
        GameObject checkMenue;
        GameObject viewCamera;
        Text movingText;
        Text checkText;

        enum ActionKind
        {
            Unset,
            Move,
            RotateRight,
            RotateLeft
        }


        public void Start()
        {
            //必要なオブジェクトのfind
            checkText = GameObject.Find("CheckText").GetComponent<Text>();
            movingText = GameObject.Find("WhileMoving").GetComponent<Text>();
            checkMenue = GameObject.Find("CheckMenue");
            viewCamera = GameObject.Find("ViewCamera");
            checkMenue.SetActive(false);
            viewCamera.SetActive(false);

            movingText.text = "Opponent Turn...";

            CPB = new CP.CPBrain();
            HUM = new CP.CPBrain();

            PCM = GetComponent<PlayCameraManager>();
            TM = GetComponent<TurnManager>();
            LM = GetComponent<ListManager.ListCtrl>();
            GJ = GetComponent<GameJudgment>();

            //test2 10 pawns　written by KatoMori
            HUM.SetAgentCPPieces(61);
            CPB.SetAgentCPPieces(50);
            /*
            //キングのIDを取得
            var hpkingList = ManagerStore.humanPlayer.GetMyPiecesByKind(PieceKind.King);
            humanPlayerKing = hpkingList[0].GetPieceId();
            
            //ターン開始時の初期化
            selectedPieceId = humanPlayerKing;
            PCM.ChangeCamera(selectedPieceId);
            selectPiece = GameManager.ManagerStore.humanPlayer.GetPieceById(humanPlayerKing);
            */
            TurnStartInit();
        }

        //humanPlayer のターン
        private void HUMTurn()
        {
            //StartCoroutine(DelayMethod());
            HUM.SetInfo(GameManager.ManagerStore.humanPlayer, ManagerStore.fieldManager);
            var CPMove = HUM.BestMove();
            ExcuteMotion(GameManager.ManagerStore.humanPlayer.GetPieceById(CPMove.GetMovePieceId()), CPMove.GetMoveFaceId(), CPMove.GetRotateDirection());
            //Debug.Log("PieceId:" + CPMove.GetMovePieceId() + " FaceId:" + CPMove.GetMoveFaceId() + " Direction:" + CPMove.GetRotateDirection());
        }
        //CPのターン。
        //画面隠す。CPの移動、ターン切り替え、リスト更新、画面を戻す。
        private void CPTurn()
        {
            //StartCoroutine(DelayMethod());
            CPB.SetInfo(GameManager.ManagerStore.cp, ManagerStore.fieldManager);
            var CPMove = CPB.BestMove();
            ExcuteMotion(GameManager.ManagerStore.cp.GetPieceById(CPMove.GetMovePieceId()), CPMove.GetMoveFaceId(), CPMove.GetRotateDirection());
            //Debug.Log("PieceId:" + CPMove.GetMovePieceId() + " FaceId:" + CPMove.GetMoveFaceId() + " Direction:" + CPMove.GetRotateDirection());
        }

        private void SkipCpTurn()
        {
            TM.ChangeTuen();
        }

        
        //プレイヤーのターン開始時の初期化
        //キングを選択状態に。各種フラグをfalseに初期化。
        private void TurnStartInit()
        {

            LM.DisplayList();
           // ChangeMovePoint();
            isNeedTurnStartInit = false;
            isActionDecide = false;
            isDisPlayForm = false;
            action = ActionKind.Unset;
           // HiddenCheckText();
        }
        
        //メイン
        public void Update()
        {
            //Judge the Game
            if (1 == 2)
            {
                Debug.Log("aaaa");
            }
            else
            {
                //ターン開始
                //初期化
                if (isNeedTurnStartInit == true) TurnStartInit();
                //行動の選択
                if (TM.IsPlayerTurn() == true) HUMTurn();
                else if (TM.IsPlayerTurn() == false) CPTurn();
                //else SkipCpTurn();//test用のCPスキップ関数　上の行と入れ替えるとcpをスキップする

                /*
                //回転をaとsで実行出来る
                if (Input.GetKey(KeyCode.A))
                {
                    ButtonLeftRotate();
                }
                if (Input.GetKey(KeyCode.S))
                {
                    ButtonRightRotate();
                }
                */
            }


        }
        
        //駒を実際に動かす　ターン交代 勝利条件確認
        private void ExcuteMotion(Piece.Pieces piece, int moveFaceId, int rotateDirection)
        {
            /*
                if (moveFaceId > -1)
                {
                //canMoveCollision == 移動先に駒が存在し、自分の駒であれば動けない
                if (CanMoveCollision(TM.IsPlayerTurn(), moveFaceId) == true)
                    {
                        piece.MovePiece(moveFaceId);
                    }
                    else
                    {
                        Debug.LogWarning("error:PlyaGame l290 移動先に味方の駒が存在?");
                    }                    
                }
                piece.RotatePiece(rotateDirection);
             */
                GJ.WinnerCheck();
                TM.ChangeTuen();        
        }
        

        private void WhileMoveing(bool isMoving)
        {
            movingText.enabled = isMoving;
        }
        
    }

}

/*
//動作確認のYesNoのディスプレイを表示
private bool DisplayCheckText()
{
    bool returnValue = false;
    if (isDisPlayForm == false && action != ActionKind.Unset && isActionDecide == false)
    {
        if (action == ActionKind.Move)
        {
            List<int> tes = new List<int>();
            tes.Add(selectedFaceId);
            ManagerStore.fieldManager.PointMovableFace(tes);
            checkText.text = selectedFaceId + "に移動しますが宜しいですか";
            returnValue = true;
        }
        else if (action == ActionKind.RotateRight)
        {
            checkText.text = "右に回転しますが宜しいですか";
            returnValue = true;
        }
        else if (action == ActionKind.RotateLeft)
        {
            checkText.text = "左に回転しますが宜しいですか";
            returnValue = true;
        }

        isDisPlayForm = true;
        checkMenue.SetActive(true);
    }
    return returnValue;
}

//動作確認のYesNoのディスプレイを非表示に
private void HiddenCheckText()
{
    isDisPlayForm = false;
    checkMenue.SetActive(false);
}
*/



/*
//左右のボタンが押された時
public void ButtonRightRotate()
{
    if (TM.IsPlayerTurn() && isActionDecide == false)
    {
        action = ActionKind.RotateRight;
        selectedRotateDirection = 1;
    }

}
public void ButtonLeftRotate()
{
    if (TM.IsPlayerTurn() && isActionDecide == false)
    {
        action = ActionKind.RotateLeft;
        selectedRotateDirection = -1;
    }
}

//YesNoの確認ボタンが押された時
public void ButtonYes()
{
    isActionDecide = true;
}
public void ButtonNo()
{
    action = ActionKind.Unset;
    ChangeMovePoint();
    HiddenCheckText();
}
*/

/*
    //移動先に駒が存在し、自分の駒であれば動けない
    private bool CanMoveCollision(bool isPlyerTurn,int faceId)
    {
        bool returnValue=true;
        if (faceId > -1)
        {
            int pieceONFace = ManagerStore.fieldManager.IsPieceOnFace(faceId);
            if (pieceONFace != -1)
            {
                ////移動先が自分の駒なら移動出来ない
                if (isPlyerTurn == true && ManagerStore.humanPlayer.HasPiece(pieceONFace)) return false;
                if (isPlyerTurn == false && ManagerStore.cp.HasPiece(pieceONFace)) return false;

                //移動先が相手の駒なら消滅させる
                if (ManagerStore.cp.HasPiece(pieceONFace))
                {                        
                    (ManagerStore.cp.GetPieceById(pieceONFace)).BeatedEffect();
                }
                //自分の駒ならviewカメラに切り替える
                if (ManagerStore.humanPlayer.HasPiece(pieceONFace))
                {
                    viewCamera.transform.position = ManagerStore.humanPlayer.GetPieceById(pieceONFace).PieceCamera.GetCameraPosition();
                    viewCamera.transform.rotation = Quaternion.Euler(ManagerStore.humanPlayer.GetPieceById(pieceONFace).PieceCamera.GetCameraAngle());
                    viewCamera.SetActive(true);
                    PCM.CameraOff();
                    effectHumanPiece = pieceONFace;
                    (ManagerStore.humanPlayer.GetPieceById(pieceONFace)).BeatedEffect();
                }
            }
        }
        return returnValue;
    }
    */

/*
//NPCの移動中。2秒画面隠す
private IEnumerator DelayMethod()
{            
WhileMoveing(true);
yield return new WaitForSeconds(2);
WhileMoveing(false);
}
*/


/*
//駒の移動範囲取得と強調
private void ChangeMovePoint()
{
    selectAllRelativePieceMoveRange = GameManager.ManagerStore.piecesManager.GetMoveRange(selectPiece.GetKind());
    selectRelativePieceMoveRange = selectAllRelativePieceMoveRange[selectPiece.GetShape()];

    //絶対位置に変換し、移動可能なマス(敵の駒が存在するか、駒がない)をハイライト 
    selectAbsolutePieceMoveRange = new List<int>();
    selectedPieceDirection = selectPiece.GetForwardDirection();
    int absFaceId;
    foreach (int moveFace in selectRelativePieceMoveRange)
    {
        absFaceId = GameManager.ManagerStore.fieldManager.ConvertRelative2AbsId(selectPiece.GetFaceId(), moveFace, selectedPieceDirection);
        int returnPieceId = ManagerStore.fieldManager.IsPieceOnFace(absFaceId);
        if (returnPieceId == -1 || (returnPieceId > -1 && ManagerStore.cp.HasPiece(returnPieceId) ==true))
        {
            selectAbsolutePieceMoveRange.Add(absFaceId);
        }

    }

    //移動可能範囲表示
    //GameManager.ManagerStore.fieldManager.PointMovableFace(selectAbsolutePieceMoveRange);
}
*/
/*
        //PLayerのターンの処理
        private void PlayerTurn()
        {

            //行動が未選択
            if (action == ActionKind.Unset)
            {
                //1,駒が選択された→カメラ切り替え 未選択の場合、元の番号を保持する
                int pieceChangeChechk= LM.GetSelectedPieces();
                if (pieceChangeChechk > -1)
                {
                    selectedPieceId = pieceChangeChechk;
                    selectPiece = GameManager.ManagerStore.humanPlayer.GetPieceById(selectedPieceId);
                    PCM.ChangeCamera(selectedPieceId);
                    ChangeMovePoint();
                } 

                //2.Inputから何らかのオブジェクトを受け付けた→面か確認して、移動可能か確認する
                GameObject returnObj=null;
                if (Input.GetMouseButtonDown(0) == true) returnObj = IM.GetTouchListner(selectedPieceId);
                if (returnObj != null)
                {
                    Field.SurfaceInfo selectedSurFaceInfo = returnObj.GetComponent<Field.SurfaceInfo>();
                    if (selectedSurFaceInfo != null)
                    {

                        selectedFaceId = selectedSurFaceInfo.FaceId;
                        foreach (int moveFace in selectAbsolutePieceMoveRange)
                        {
                            if (selectedFaceId == moveFace)
                            {
                                action = ActionKind.Move;
                                break;
                            }
                        }
                        //駒が移動可能なマスでなければ-1に
                        if (action != ActionKind.Move) selectedFaceId = -1;
                    }
                }
            }
            //行動が選択済みで、Yes、Noが未クリック
            else if (action != ActionKind.Unset && isActionDecide == false)
            {
                DisplayCheckText();
            }
            //確認画面でYesが押された
            else if (action != ActionKind.Unset && isActionDecide == true)
            {
                HiddenCheckText();
                ExcuteMotion(selectPiece, selectedFaceId, selectedRotateDirection);
                isNeedTurnStartInit = true;
            }
        }
        */
