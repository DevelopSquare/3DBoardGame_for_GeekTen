/*
  Author      藤澤典隆
  LastUpdate  2020/08/29 (tomomasa)
  LastUpdate  2020/08/28 (髙橋）
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

using UnityEngine;
using System;
using UnityEngine.UI;
using Effect;
using UnityEngine.SceneManagement;
using GameManager;

namespace GameManager.PlayGameManager
{
    
    public class PlayGame : StateBase
    {
        //プレイヤーの駒選択関連
        int selectedFaceId = -1;
        int selectedPieceId = -1;
        int selectedRotateDirection = 0;
        int effectHumanPiece=-1;
        int humanPlayerKing;
        Piece.Pieces selectPiece;
        Dictionary<int, List<int>> selectAllRelativePieceMoveRange;
        List<int> selectRelativePieceMoveRange;
        List<int> selectAbsolutePieceMoveRange;
        Field.SurfaceInfo selecttedSurfaceInfo;
        Vector3 selectedPieceDirection;

        //record保存専用
        int fromFaceIdforRecord;
        int toFaceIdforRecord;
        PieceKind pieceKindforRecord;

        //各種フラグ
        bool isNeedTurnStartInit = true;
        bool isActionDecide = false;
        bool isDisPlayForm = false;
        bool isEnd = false;

        //駒の動きの種類
        ActionKind hpaction = ActionKind.Unset;

        //外部より利用するクラス
        CP.CPBrain CPB;
        PlayCameraManager PCM;
        TurnManager TM;
        ListManager.ListCtrl LM;
        GameJudgment GJ;
        public InputID.InputManager IM;

        //必要なGameObject
        GameObject checkMenue;
        GameObject viewCamera;
        Image movingPanel;
        Text movingText;
        Text checkText;
        Text restPieceText;

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
            movingPanel = GameObject.Find("WhileMovingPanel").GetComponent<Image>();
            movingText = GameObject.Find("WhileMoving").GetComponent<Text>();
            restPieceText = GameObject.Find("RestPiece").GetComponent<Text>();

            checkMenue = GameObject.Find("CheckMenue");
            viewCamera = GameObject.Find("ViewCamera");
            checkMenue.SetActive(false);
            viewCamera.SetActive(false);

            movingText.text = "Enemy Turn…";

            CPB = new CP.CPBrain();
            PCM = GetComponent<PlayCameraManager>();
            TM = GetComponent<TurnManager>();
            LM = GetComponent<ListManager.ListCtrl>();
            GJ = GetComponent<GameJudgment>();

            //test2 10 pawns　written by KatoMori
            CPB.SetCPPieces();

            //キングのIDを取得
            var hpkingList = ManagerStore.humanPlayer.GetMyPiecesByKind(PieceKind.King);
            humanPlayerKing = hpkingList[0].GetPieceId();

            
            //初期位置の保存
            GameManager.ResultData.positionRecord.Add(Position.PositionData.SaveByPlayer(ManagerStore.humanPlayer, ManagerStore.cp));

            //ターン開始時の初期化
            selectedPieceId = humanPlayerKing;
            PCM.ChangeCamera(selectedPieceId);
            selectPiece = GameManager.ManagerStore.humanPlayer.GetPieceById(humanPlayerKing);
            TurnStartInit();
        }


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

                //Queenの二週目の挙動を無理やり矯正(kisuke)
                if(selectPiece.GetKind()==PieceKind.Queen && moveFace == (selectPiece.GetShape()))
                {
                    var fFaceId_ = selectPiece.GetForwardFaceId();
                    var ffFace_ = ManagerStore.fieldManager.GetFace2Face(fFaceId_,selectPiece.GetFaceId());
                    absFaceId = ffFace_.GetComponent<Field.SurfaceInfo>().FaceId;

                }

                int returnPieceId = ManagerStore.fieldManager.IsPieceOnFace(absFaceId);
                if (returnPieceId == -1 || (returnPieceId > -1 && ManagerStore.cp.HasPiece(returnPieceId) ==true))
                {
                    selectAbsolutePieceMoveRange.Add(absFaceId);
                }
            }
            GameManager.ManagerStore.fieldManager.PointMovableFace(selectAbsolutePieceMoveRange);
        }

        //動作確認のYesNoのディスプレイを表示
        private bool DisplayCheckText()
        {
            bool returnValue = false;
            //キングが回転しないようにする(8/27 きすけ)
            if (selectPiece.GetKind() == PieceKind.King && (hpaction == ActionKind.RotateRight || hpaction == ActionKind.RotateLeft) && isActionDecide == false)
            {
                hpaction = ActionKind.Unset;
                return false;
            }

            if (isDisPlayForm == false && hpaction != ActionKind.Unset && isActionDecide == false)
            {
                IM.RockOn();

                if (hpaction == ActionKind.Move)
                {
                    List<int> tes = new List<int>();
                    tes.Add(selectedFaceId);
                    ManagerStore.fieldManager.PointMovableFace(tes);
                    //checkText.text = selectedFaceId + "に移動しますが宜しいですか";
                    checkText.text = "このマスに移動しますか";
                    returnValue = true;
                }
                else if (hpaction == ActionKind.RotateRight)
                {
                    checkText.text = "右に回転しますか";
                    returnValue = true;
                }
                else if (hpaction == ActionKind.RotateLeft)
                {
                    checkText.text = "左に回転しますか";
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
            StartCoroutine(DelayMethod(5, () =>
            {
                IM.RockOff();
            }));
            isDisPlayForm = false;
            checkMenue.SetActive(false);
        }
        
        //フレーム数をカウントする
        private IEnumerator DelayMethod(int delayFrameCount, Action action)
        {
            for (var i = 0; i < delayFrameCount; i++)
            {
                yield return null;
            }
            action();
        }

        private void DisplayRestPieceNum(int piece) {
            PieceKind kind = ManagerStore.humanPlayer.GetPieceById(piece).GetKind();
            int restNum = ManagerStore.humanPlayer.GetMyPiecesByKind(kind).Count;
            if (restPieceText == null) Debug.Log("null");
            else
            {
                restPieceText.text = "×" + restNum;
                foreach (PieceKind k in Enum.GetValues(typeof(PieceKind)))
                {
                    if (kind == k)
                    {
                        //Debug.Log("変更");
                        var img = ManagerStore.piecesManager.GetImg(kind);
                        GameObject.Find("PieceImg").gameObject.GetComponent<Image>().sprite =img;
                        break;
                    }
                }    
            }
            
        } 


        //PLayerのターンの処理
        private void PlayerTurn()
        {

            //行動が未選択
            if (hpaction == ActionKind.Unset)
            {
                //1,駒が選択された→カメラ切り替え 未選択の場合、元の番号を保持する
                int pieceChangeChechk= LM.GetSelectedPieces();
                if (pieceChangeChechk > -1)
                {
                    selectedPieceId = pieceChangeChechk;
                    DisplayRestPieceNum(selectedPieceId);
                    selectPiece = GameManager.ManagerStore.humanPlayer.GetPieceById(selectedPieceId);
                    PCM.ChangeCamera(selectedPieceId);
                    ChangeMovePoint();
                } 

                //2.Inputから何らかのオブジェクトを受け付けた→面か確認して、移動可能か確認する
                GameObject returnObj=null;
                if (Input.GetMouseButtonUp(0)) returnObj = IM.GetTouchListner();
                
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
                                hpaction = ActionKind.Move;
                                break;
                            }
                        }
                        //駒が移動可能なマスでなければ-1に
                        if (hpaction != ActionKind.Move) selectedFaceId = -1;
                    }
                }
            }
            //行動が選択済みで、Yes、Noが未クリック
            else if (hpaction != ActionKind.Unset && isActionDecide == false)
            {
                DisplayCheckText();
            }
            //確認画面でYesが押された
            else if (hpaction != ActionKind.Unset && isActionDecide == true)
            {
                HiddenCheckText();
                ExcuteMotion(selectPiece, selectedFaceId, selectedRotateDirection);
                isNeedTurnStartInit = true;
            }


        }



        //CPのターン。
        //画面隠す。CPの移動、ターン切り替え、リスト更新、画面を戻す。
        private void CPTurn()
        {
            WhileMoveing(true);
            StartCoroutine(DelayMethod(1.5f, () =>{WhileMoveing(false);}));

            CPB.SetInfo(GameManager.ManagerStore.cp, ManagerStore.fieldManager);
            var CPMove = CPB.BestMove();
            //くるくる回転するだけ。
            Debug.Log("CPPiece : "+ CPMove.GetMovePieceId()+ "CPMove : " + CPMove.GetMoveFaceId()+ "CPMove : " + CPMove.GetRotateDirection());
            ExcuteMotion(GameManager.ManagerStore.cp.GetPieceById(CPMove.GetMovePieceId()),CPMove.GetMoveFaceId(), CPMove.GetRotateDirection());
            //ExcuteMotion(GameManager.ManagerStore.cp.GetPieceById(CPMove.GetMovePieceId()), CPMove.GetMoveFaceId(), CPMove.GetRotateDirection());
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
            DisplayRestPieceNum(selectedPieceId);
            LM.DisplayList();
            ChangeMovePoint();
            isNeedTurnStartInit = false;
            isActionDecide = false;
            isDisPlayForm = false;
            hpaction = ActionKind.Unset;
            HiddenCheckText();
            selectedRotateDirection = 0;
            selectedFaceId = -1;
        }

        
        public void Update()
        {
            //エフェクト作動中、及びNPCのターンはプレイヤーはゲームに干渉できない。
            if (isEnd) Debug.Log("end") ;
            else if (effectHumanPiece > -1 || movingText.enabled == true)
            {

                if (ManagerStore.humanPlayer.HasPiece(effectHumanPiece) || movingText.enabled == true)
                {
                    //Debug.Log($"エフェクト作動中ピース: {effectHumanPiece}  NPCターンかどうか:{movingText.enabled}");
                }
                else
                {

                    //選択中の駒が取られた 
                    if (selectedPieceId == effectHumanPiece)
                    {
                        //取られた駒がキング以外ならキングを選択状態にする
                        if (effectHumanPiece != humanPlayerKing)
                        {
                            selectedPieceId = humanPlayerKing;
                            selectPiece = ManagerStore.humanPlayer.GetPieceById(humanPlayerKing);
                            effectHumanPiece = -1;
                            PCM.ChangeCamera(humanPlayerKing);
                        }
                    }
                    else
                    {
                        //選択中の駒以外が取られた
                        PCM.ChangeCamera(selectedPieceId);
                    }

                    //書き込み
                    Debug.Log("before:"+GameManager.ResultData.positionRecord.Count);
                    GameManager.ResultData.positionRecord.Add(Position.PositionData.SaveByPlayer(ManagerStore.humanPlayer, ManagerStore.cp));
                    //Move.ReversibleMove rm = new Move.ReversibleMove();
                    //rm.InitCapturedMove(fromFaceIdforRecord,toFaceIdforRecord, pieceKindforRecord,GameManager.ManagerStore.fieldManager.GetFace2Face(fromFaceIdforRecord,toFaceIdforRecord).GetComponent<Field.SurfaceInfo>().FaceId);
                    //GameManager.ResultData.moveRecord.Add(rm);
                    Debug.Log("after:"+GameManager.ResultData.positionRecord.Count);

                    GJ.WinnerCheck();
                    if (ResultData.winner != PlayerKind.None)
                    {
                        isEnd = true;
                        StartCoroutine(DelayMethod(2.0f, () =>
                        {
                            Destroy(GameObject.Find("Field"));
                            Destroy(GameObject.Find("ManagerStore"));
                            SceneManager.LoadScene("Result");
                        }));
                    }
                    else
                    {
                        effectHumanPiece = -1;
                        viewCamera.SetActive(false);
                    }

                }

            }
            else
            {

                //初期化
                if (isNeedTurnStartInit == true) TurnStartInit();
                //行動の選択
                if (TM.IsPlayerTurn() == true) PlayerTurn();
                else if (TM.IsPlayerTurn() == false) CPTurn();
                //else SkipCpTurn();//test用のCPスキップ関数　上の行と入れ替えるとcpをスキップする
            }

        }



        
        //駒を実際に動かす　ターン交代 勝利条件確認
        private void ExcuteMotion(Piece.Pieces piece, int moveFaceId, int rotateDirection)
        {
            fromFaceIdforRecord = piece.GetFaceId();
            toFaceIdforRecord = moveFaceId;
            if (moveFaceId > -1)
            {
                if (CanMoveCollision(TM.IsPlayerTurn(), moveFaceId,piece) == true)
                {
                    //Debug.Log("pi:"+piece.IsAnimate());
                    piece.MoveBeginEffect();
                    //Debug.Log("pi:"+piece.IsAnimate());
                    piece.MovePiece(moveFaceId);
                    piece.MoveFinishEffect();
                    //Debug.Log("pi:" + piece.IsAnimate());

                    if (effectHumanPiece == -1) {
                        GameManager.ResultData.positionRecord.Add(Position.PositionData.SaveByPlayer(ManagerStore.humanPlayer, ManagerStore.cp));
                        //Move.ReversibleMove rm = new Move.ReversibleMove();
                        //rm.InitNotCapturedMove(fromFaceIdforRecord, toFaceIdforRecord);
                        //GameManager.ResultData.moveRecord.Add(rm);
                    }
                    
                }
            }
            else {
                //駒を移動しない場合Record記録　移動する場合はエフェクト終了後に記録
                GameManager.ResultData.positionRecord.Add(Position.PositionData.SaveByPlayer(ManagerStore.humanPlayer, ManagerStore.cp));
                //Move.ReversibleMove rm = new Move.ReversibleMove();
                //rm.InitRotation(fromFaceIdforRecord,rotateDirection);
                //GameManager.ResultData.moveRecord.Add(rm);
            }
            if(rotateDirection!=0)piece.RotatePiece(rotateDirection);
            GJ.WinnerCheck();
            TM.ChangeTuen();        
        }



        //移動できるか確認　移動先に存在する駒の種類を確認する
        private bool CanMoveCollision(bool isPlyerTurn,int faceId,Piece.Pieces moveingPiece)
        {
            bool returnValue=true;
            if (faceId > -1)
            {
                int pieceONFace = ManagerStore.fieldManager.IsPieceOnFace(faceId);
                if (pieceONFace != -1)
                {

                    //移動先が自分の駒なら移動出来ない
                    if (isPlyerTurn == true && ManagerStore.humanPlayer.HasPiece(pieceONFace)) return false;
                    if (isPlyerTurn == false && ManagerStore.cp.HasPiece(pieceONFace)) return false;

                    Piece.Pieces beatedPiece=null;
                    //移動先が相手の駒なら消滅させる
                    if (ManagerStore.cp.HasPiece(pieceONFace))
                    {
                        effectHumanPiece = pieceONFace;
                        beatedPiece = ManagerStore.cp.GetPieceById(pieceONFace);
                        beatedPiece.BeatedEffect();

                        StartCoroutine(DelayMethod(40, () =>
                        {
                            ManagerStore.cp.DeleteMyPieces(pieceONFace);
                        }));
                    }
                    //自分の駒ならviewカメラに切り替える
                    if (ManagerStore.humanPlayer.HasPiece(pieceONFace))
                    {
                        viewCamera.transform.position = ManagerStore.humanPlayer.GetPieceById(pieceONFace).PieceCamera.GetCameraPosition();
                        viewCamera.transform.rotation = Quaternion.Euler(ManagerStore.humanPlayer.GetPieceById(pieceONFace).PieceCamera.GetCameraAngle());
                        viewCamera.SetActive(true);
                        PCM.CameraOff();
                        effectHumanPiece = pieceONFace;
                        beatedPiece = ManagerStore.humanPlayer.GetPieceById(pieceONFace);
                        beatedPiece.BeatedEffect();
                    }

                    //Move.ReversibleMove rm = new Move.ReversibleMove();
                    //if (pieceONFace == -1) {
                    //    rm.InitNotCapturedMove(moveingPiece.GetFaceId(),faceId);
                        
                    //}
                    //else {
                    //    //caputure Piece forward face Id移動後か？？
                    //    rm.InitCapturedMove(moveingPiece.GetFaceId(),faceId,beatedPiece.GetKind(),ManagerStore.fieldManager.GetFace2Face(moveingPiece.GetFaceId(),faceId).GetComponent<Field.SurfaceInfo>().FaceId);
                    //}

                    pieceKindforRecord = beatedPiece.GetKind();


                }
            }
            return returnValue;
        }


        private IEnumerator DelayMethod(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action();
        }

        private void WhileMoveing(bool isMoving)
        {
            movingPanel.enabled = isMoving;
            movingText.enabled = isMoving;
        }


        //各種ボタン
        public void ButtonRightRotate()
        {
            if (TM.IsPlayerTurn() && isActionDecide == false)
            {
                hpaction = ActionKind.RotateRight;
                selectedRotateDirection = 1;
            }
        }
        public void ButtonLeftRotate()
        {
            if (TM.IsPlayerTurn() && isActionDecide == false)
            {
                hpaction = ActionKind.RotateLeft;
                selectedRotateDirection = -1;
            }
        }
        public void ButtonYes()
        {
            isActionDecide = true;
        }
        public void ButtonNo()
        {
            hpaction = ActionKind.Unset;
            selectedRotateDirection = 0;
            ChangeMovePoint();
            HiddenCheckText();
        }


    }

}
