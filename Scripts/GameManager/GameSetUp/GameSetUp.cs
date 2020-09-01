/*
  Author      森友雅
  LastUpdate  2020/08/28
  Since       2020/03/16
  Contents    N GameSetUp
            　ゲームセッアップではこのスクリプトを軸に実行される
*/

using UnityEngine;
using Field;
using InputID;
using System.Collections.Generic;

namespace GameManager.GameSetUp
{
    public class GameSetUp : MonoBehaviour
    { 
        // 使用スクリプト
        private SetObjectManager setobject;
        private RelativeIdManager relativeid;
        private DisplayManager display;
        private CardManager card;
        private InputManager input;
        private InputEventFactory inputf;
        GetInfoManager getinfo = new GetInfoManager();

        public GameObject removeObj;

        private int totalPoint = 100;
        private int state = 0;//=0..未選択  =1..新規設置駒選択中
        private int objId;
        private int  selectedObjId = -1;
        private int selecteObjId = -1;
        bool iSselectedSquarePiece = false;

        void Start()
        {
            setobject = GetComponent<SetObjectManager>();
            display = GetComponent<DisplayManager>();
            relativeid = GetComponent<RelativeIdManager>();
            card = GetComponent<CardManager>();
            input = GameObject.Find("InputManager").GetComponent<InputManager>();
            inputf = GameObject.Find("InputManager").GetComponent<InputEventFactory>();
        }


        void Update()
        {
            if(ManagerStore.fieldManager.IsPieceOnFace(61) == -1)
            {//kingの駒を設置
                setobject.SetPiece(61,-10);
            }


            if (Input.GetMouseButtonDown(0) == true && input.GetTouchListner() != null)
            {//左クリックされた瞬間
                GameObject getObject = input.GetTouchListner();

                if (getObject.name != "InvisibleWall")
                {
                    //inputf.rock = true;
                    objId = relativeid.ChangeToRelativeId(getObject);

                    selectedObjId = objId;

                    if (objId == -1)
                    {//マスが選択されたら
                        //inputf.rock = false;
                        if (setobject.CanBeUsed(getObject.GetComponent<SurfaceInfo>().FaceId) == true)
                        {
                            selectedObjId = getObject.GetComponent<SurfaceInfo>().FaceId;
                        }
                        removeObj.SetActive(true);
                    }
                }
            }


            if(Input.GetMouseButton(0) == true && selectedObjId > -1)
            {//マスを選択中

                if (ManagerStore.fieldManager.IsPieceOnFace(selectedObjId) != -1)
                {//マスにある駒を選択中
                    iSselectedSquarePiece = true;
                }
            }

            card.InputRockFromFlag();

            if (Input.GetMouseButton(0) == false)
            {//左クリックを解除したら
                if (ManagerStore.fieldManager.IsPieceOnFace(selectedObjId) != -1 && input.GetTouchListner() == removeObj)
                {//設置済みの駒の取り消し
                    //Debug.Log("設置済みの駒の取り消し");

                    totalPoint += getinfo.PiecePoint(selectedObjId);
                     setobject.RemovePiece(selectedObjId);    
                }         
                else if (selectedObjId != -1 && input.GetTouchListner() != null) 
                {//マスか駒カードとマウスカーソルが重なったら  
                    int pieceId = objId;
                    GameObject getObject = input.GetTouchListner();
 
                    if (getObject.name != "InvisibleWall")
                    {//ダイアログが表示されてなかったら
                        objId = objId = relativeid.ChangeToRelativeId(getObject);

                        if (objId == -1)
                        {//マスor駒カード

                            if (setobject.CanBeUsed(getObject.GetComponent<SurfaceInfo>().FaceId) == true)
                            {//マス
                                selecteObjId = getObject.GetComponent<SurfaceInfo>().FaceId;

                                if(selectedObjId == selecteObjId)
                                {//選択してた場所と同じ場所を選択
                                }            
                                else if (ManagerStore.fieldManager.IsPieceOnFace(selectedObjId) != -1)
                                {//選択マスに駒が存在していたら

                                    if (ManagerStore.fieldManager.IsPieceOnFace(selecteObjId) != -1)
                                    {//設置済みの駒と駒の位置を交換
                                        //Debug.Log("設置済みの駒と駒の位置を交換");

                                        int piece1 = -1;
                                        int piece2 = -1;

                                        piece1 = getinfo.PieceId(selectedObjId);
                                        piece2 = getinfo.PieceId(selecteObjId);
                                        setobject.RemovePiece(selectedObjId);
                                        setobject.RemovePiece(selecteObjId);
                                        setobject.SetPiece(selectedObjId, piece2);
                                        setobject.SetPiece(selecteObjId, piece1);
                                    }
                                    else
                                    {//設置済みの駒を未設置のマスへ移動
                                        //Debug.Log("設置済みの駒を未設置のマスへ移動");

                                        int piece2 = -1;

                                        piece2 = getinfo.PieceId(selectedObjId);
                                        setobject.RemovePiece(selectedObjId);
                                        setobject.SetPiece(selecteObjId, piece2);
                                    }
                                }
                                else if (totalPoint - (getinfo.PiecePoint(selectedObjId) - getinfo.PiecePoint(selecteObjId)) >= 0)
                                {//駒の新規設置
                                    if (selectedObjId < -1)
                                    {
                                        //Debug.Log("駒を新規設置");

                                        int calculatePoint = 0;

                                        calculatePoint += getinfo.PiecePoint(selectedObjId) - getinfo.PiecePoint(selecteObjId);
                                        totalPoint -= calculatePoint;

                                        if (getinfo.PiecePoint(selecteObjId) > 0)
                                        {
                                            setobject.RemovePiece(selecteObjId);
                                        }

                                        setobject.SetPiece(selecteObjId, pieceId);
                                        state = 0;
                                    }
                                }
                                else
                                {//設置しようとしたがポイント不足
                                    //Debug.Log("ポイントが足らんぞよ");
                                }

                            }
                        }
                    }
                }
                iSselectedSquarePiece = false;
                removeObj.SetActive(false);
            }
            //カードの移動処理
            selectedObjId = card.Move(selectedObjId);
            //
            //情報を更新してDisplayに表示
            display.PointGauge(totalPoint, getinfo.PiecePoint(selectedObjId), state);
            display.PointText(totalPoint);
            display.VisualizeRemoveField(iSselectedSquarePiece);
        }


        /// <summary>
        /// 残りのポイント値を取得する
        /// </summary>
        /// <returns></returns>
        /// 
        public int RemainPoint()
        {
            return totalPoint;
        }


        public void WritePoint(int point)
        {
            totalPoint = point;
            //情報を更新してDisplayに表示
            display.PointGauge(totalPoint, getinfo.PiecePoint(selectedObjId), state);
            display.PointText(totalPoint);
        }

    }
}
