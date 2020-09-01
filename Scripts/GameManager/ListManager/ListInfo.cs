using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    namespace ListManager
    {
        
        public class ListInfo : MonoBehaviour
        {
            private PieceBotton[] pieceBottons = default;
            // Start is called before the first frame update
            void Start()
            {
                
            }

            // Update is called once per frame
            void Update()
            {
        
            }

            private void init()
            {
                var objs= GameObject.FindGameObjectsWithTag("PieceBotton");
                for (int i = 0; i<objs.Length; i++)
                {
                    pieceBottons[i] = objs[i].GetComponent<PieceBotton>();
                }
            }

           
        }

    }

}


