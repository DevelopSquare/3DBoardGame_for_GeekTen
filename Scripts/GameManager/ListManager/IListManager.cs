using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Piece;

namespace GameManager
{
    namespace ListManager
    {
        public interface IListManager 
        {
            List<Pieces> EnumPieces();

            int GetSelectedPieces();

            void DisplayList();

          //  void SetPlayer(PlayerBase player);

        }

    }


}

