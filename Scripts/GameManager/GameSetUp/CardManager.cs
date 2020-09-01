/*
  Author      森友雅
  LastUpdate  2020/07/11
  Since       2020/04/08
  Contents    N GameSetUp
              カードの処理を行うスクリプト
*/

using UnityEngine;
using UnityEngine.UI;
using InputID;

namespace GameManager.GameSetUp
{
    public class CardManager : MonoBehaviour
    {
        // 使用スクリプト
        GetInfoManager getinfo = new GetInfoManager();
        private InputEventFactory inputf;

        //手札のcanvas
        public Canvas canvas;

        //手札
        public Image pawnImage;
        public Image queenImage;

        //resourcesから生成するオブジェクト関係
        public Image pawnCard;
        public GameObject pawnObj;
        public Image queenCard;
        public GameObject queenObj;
        private Image selectCard;
        private Image selectImg;
        private GameObject selectObj;
        private GameObject prehab;

        //カードの初期位置・サイズを記憶
        private Vector3 startPosition;
        private Vector3 startPosition2;
        private Rect startHoge;


        void Start()
        {
            startHoge = pawnImage.rectTransform.rect;
            startPosition = pawnImage.transform.position;
            startPosition2 = queenImage.transform.position;
            inputf = GameObject.Find("InputManager").GetComponent<InputEventFactory>();
        }


        private bool rockFlag = false;

        // Update is called once per frame
        public int Move(int squareId)
        {

            //マスから別のマスヘ駒を移動 または　駒同士の位置交換
            if (Input.GetMouseButton(0) == true && squareId > -1)
            {
                if (ManagerStore.fieldManager.IsPieceOnFace(squareId) != -1)
                {
                    inputf.rock = true;
                    if (getinfo.PiecePoint(squareId) == 10)
                    {
                        selectCard = pawnCard;
                        selectObj = pawnObj;
                    }
                    if (getinfo.PiecePoint(squareId) == 50)
                    {
                        selectCard = queenCard;
                        selectObj = queenObj;
                    }

                    if (Input.GetMouseButtonDown(0) == true)
                    {
                        prehab = (GameObject)Instantiate(selectObj);
                    }
                    prehab.transform.SetParent(canvas.transform);
                    prehab.transform.localPosition = new Vector3(0,0,0);
                    prehab.transform.localRotation = Quaternion.identity;
                    prehab.transform.localScale = new Vector3(1, 1, 1);
                    pawnCard.rectTransform.sizeDelta = new Vector2(60, 60);

                    Vector3 objectPointInScreen
                        = UnityEngine.Camera.main.WorldToScreenPoint(prehab.transform.position);

                    Vector3 mousePointInScreen
                        = new Vector3(Input.mousePosition.x,
                                      Input.mousePosition.y,
                                      objectPointInScreen.z);

                    Vector3 mousePointInWorld = UnityEngine.Camera.main.ScreenToWorldPoint(mousePointInScreen);
                    prehab.transform.position = mousePointInWorld;
                }
                rockFlag = true;
            }

            //新規駒を設置
            if (Input.GetMouseButton(0)==true && squareId < -1)
            {
                inputf.rock = true;

                if (getinfo.PiecePoint(squareId) == 10)
                {
                    selectCard = pawnCard;
                    selectObj = pawnObj;
                    selectImg = pawnImage;
                    pawnImage.enabled = false;
                }
                if (getinfo.PiecePoint(squareId) == 50)
                {
                    selectCard = queenCard;
                    selectObj = queenObj;
                    selectImg = queenImage;
                    queenImage.enabled = false;
                }

                if (Input.GetMouseButtonDown(0) == true)
                {
                    prehab = (GameObject)Instantiate(selectObj);
                }
                prehab.transform.SetParent(canvas.transform);
                prehab.transform.localPosition = new Vector3(0, 0, 0);
                prehab.transform.localRotation = Quaternion.identity;
                prehab.transform.localScale = new Vector3(1, 1, 1);
                pawnCard.rectTransform.sizeDelta = new Vector2(60, 60);

                Vector3 objectPointInScreen
                    = UnityEngine.Camera.main.WorldToScreenPoint(prehab.transform.position);

                Vector3 mousePointInScreen
                    = new Vector3(Input.mousePosition.x,
                                  Input.mousePosition.y,
                                  objectPointInScreen.z);

                Vector3 mousePointInWorld = UnityEngine.Camera.main.ScreenToWorldPoint(mousePointInScreen);
                prehab.transform.position = mousePointInWorld;
                rockFlag = true;
            }
            

            if (Input.GetMouseButton(0) == false)
            {
                if (prehab == true)
                {
                    prehab.transform.SetParent(canvas.transform, false);
                    GameObject.Destroy(prehab);
                    selectImg.enabled = true;
                }

                return -1;
            }

            return squareId;
        }


        public void InputRockFromFlag()
        {
            if (rockFlag == true)
            {
                inputf.rock = false;
                rockFlag = false;
            }
        }


    }

}