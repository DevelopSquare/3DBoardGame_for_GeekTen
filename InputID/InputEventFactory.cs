/*
  Author      森友雅
  LastUpdate  2020/06/16
  Since       2020/03/14
  Contents    オブジェクトにアタッチして使用(このファイルからではなくInputManagerから利用してください)　
                関数
                ・Vector2 GetFlick
                ・GameObject GetTouch
  　　　　　　　
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputID
{  
    public class InputEventFactory : MonoBehaviour
    {

        public Canvas canvas;

        private RaycastHit hit;

        static bool IsAllowedFlick = false;//クリックされたらtrue
        static Vector2 startPos;
        static Vector2 previousPos;
        public bool rock = false;


        /// <summary>
        /// Flick処理をしたいときに利用
        /// </summary>
        /// <returns></returns>
        public Vector2 GetFlick()
        {
            Vector2 movePos;//移動後の座標
            Vector2 relativePos;//移動前の座標

            if (rock == false)
            {
                if (Input.GetMouseButtonDown(0) == true)
                {//miniCamera内でクリックをしたら
                    if (Input.mousePosition.y <= UnityEngine.Camera.main.GetComponent<UnityEngine.Camera>().rect.height * Screen.height)
                    {
                        previousPos = Input.mousePosition;
                        relativePos = previousPos - previousPos;
                        IsAllowedFlick = true;
                    }
                    else
                    {
                        relativePos.x = 0;
                        relativePos.y = 0;
                    }
                }
                else if (Input.GetMouseButton(0) == true && IsAllowedFlick == true)
                {//クリックされた状態でフリックされたとき
                    movePos = Input.mousePosition;
                    relativePos = movePos - previousPos;
                    previousPos = movePos;
                }
                else
                {//なにもされていないとき
                    IsAllowedFlick = false;
                    relativePos.x = 0;
                    relativePos.y = 0;
                }
            }
            else
            {
                relativePos.x = 0;
                relativePos.y = 0;
            }

            return relativePos;
        }


        /// <summary>
        /// mainカメラ内でGameObjectを取得
        /// 常にrayを飛ばし続けるので注意
        /// (rayを飛ばす条件を追加して利用してね)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
         public GameObject GetTouch()
        {
            GameObject objectId;
            Ray ray;
            if (DoCursorExistWithinRange() == true)
            {

                ray = UnityEngine.Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

                if (Physics.Raycast(ray, out hit) && rock == false)  
                {
                    objectId = hit.collider.gameObject;
                    return objectId;
                }
            
            }

            return null;

         }


        /// <summary>
        /// マウスカーソルの位置が画面内であればtrueそうでなければfalseを返す
        /// </summary>
        /// <returns></returns>
        private bool DoCursorExistWithinRange()
        {
            if(0 <= Input.mousePosition.y && Input.mousePosition.y <= Screen.height)
                if(0 <= Input.mousePosition.x && Input.mousePosition.x <= Screen.width)
                {
                    return true;
                }
            return false;
        }


    }
}
