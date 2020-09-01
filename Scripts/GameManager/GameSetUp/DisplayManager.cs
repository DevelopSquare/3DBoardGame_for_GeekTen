/*
  Author      森友雅
  LastUpdate  2020/04/13
  Since       2020/03/16
  Contents    N GameSetUp
              画面上の更新を行うスクリプト
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GameManager.GameSetUp
{
    public class DisplayManager : MonoBehaviour
    {
        public Image point0;
        public Image point1;
        public Image point2;
        public Image point3;
        public Image point4;
        public Image point5;
        public Image point6;
        public Image point7;
        public Image point8;
        public Image point9;
        public Image removeField;

        public Dictionary<int, Image> dic = new Dictionary<int, Image>();

        public Text pointText;

        private void Start()
        {
            dic.Add(0, point0);
            dic.Add(1, point1);
            dic.Add(2, point2);
            dic.Add(3, point3);
            dic.Add(4, point4);
            dic.Add(5, point5);
            dic.Add(6, point6);
            dic.Add(7, point7);
            dic.Add(8, point8);
            dic.Add(9, point9);
            removeField.enabled = false;
        }


        /// <summary>
        /// ポイントのゲージを更新して画面上に表示
        /// </summary>
        /// <param name="point"></param>
        /// <param name="selectPiecePoint"></param>
        /// <param name="state"></param>
        /// 
        public void PointGauge(int point, int selectPiecePoint, int state)
        {
            int piecePoint = selectPiecePoint;

            for (int i = 0; i < 10; i++)
            {
                dic[i].enabled = true;

                if (i * 10 < point)
                {
                    dic[i].color = new Color(1.0f, 1.0f, 1.0f, 0.6f);

                    if ((selectPiecePoint - point) >= 0 && state == 1)
                    {
                        if (point - piecePoint >= 0)
                        {
                            dic[i].color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
                        }
                    }
                    selectPiecePoint += 10;
                }
                else
                {
                    dic[i].color = new Color(1.0f, 1.0f, 1.0f, 0.2f);
                }
            }
        }


        /// <summary>
        /// 残りptのテキストを画面上に更新して表示
        /// </summary>
        /// <param name="point"></param>
        /// 
        public void PointText(int point)
        {
            if (point <= 0)//ptが0だと文字が赤
            {
                pointText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
            }
            else//ptが残っていると文字が白
            {
                pointText.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            pointText.text = point.ToString() + "pt";
        }

        
        /// <summary>
        /// マスの駒が選択されたときに取り消す範囲を可視化
        /// </summary>
        /// <param name="iSselectedSquarePiece"></param>
        /// 
        public void VisualizeRemoveField(bool iSselectedSquarePiece)
        {
            if(iSselectedSquarePiece == true)
            {
                removeField.enabled = true;
            }
            else if(iSselectedSquarePiece == false)
            {
                removeField.enabled = false;
            }
        }

        
    }
}
