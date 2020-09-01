using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Piece;


namespace GameManager
{
    namespace ListManager
    {
        public class PieceBotton : MonoBehaviour
        {
            private bool clickFlg = false;
            private int pieceId;
            #pragma warning disable 649
            private Pieces pieces= default;

            public int PieceId
            {
                get
                {
                    return pieceId;
                }

                set
                {
                    pieceId = value;
                }
            }

            public Pieces Piece
            {
                get
                {
                    return pieces;
                }

                set
                {
                    pieces = value;
                }
            }
            // Start is called before the first frame update
            void Start()
            {
        
            }

            // Update is called once per frame
            void Update()
            {
        
            }

            public int PushedObject()
            {
                if (clickFlg)
                {
                    return pieceId;
                }
                return -1;
                
            }

            public Pieces GetPiece()
            {
                return pieces;
            }

            public void OnPush()
            {
                //Debug.Log("Pushed"+gameObject.name);
                clickFlg = true;
            }

            public void OnUp()
            {
                //Debug.Log("Uped" + gameObject.name);
                clickFlg = false;
            }

            public void Delete()
            {
                Debug.Log("Deleted Button ID = "+pieceId);
                Destroy(this.gameObject);
            }
        }

    }

}

