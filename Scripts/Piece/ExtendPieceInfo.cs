using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Piece;

namespace Piece.Extend
{
    static public class ExtendPieceInfo 
    {
        public static List<int> GetVisibleFaces(this Pieces pieces)
        {
            if(pieces.GetKind() == PieceKind.King)
            {
                if (pieces.GetShape() == 4)
                {
                    return new List<int> { 0, 1,2,3 };
                }else if(pieces.GetShape() == 6)
                {
                    return new List<int> { 0, 1, 2, 3,4,5 };

                }
                else
                {
                    return new List<int> { 0, 1, 2, 3, 4, 5 ,6,7,8,9};

                }

            }
            else if(pieces.GetKind() == PieceKind.Pawn)
            {
                if (pieces.GetShape() == 4)
                {
                    return new List<int> { 0, 1,3};

                }
                else if(pieces.GetShape() == 6)
                {
                    return new List<int> { 0, 1, 5};

                }
                else
                {
                    return new List<int> { 0, 1, 9};

                }

            }
            else if(pieces.GetKind() == PieceKind.Queen)
            {
                if (pieces.GetShape() == 4)
                {
                    return new List<int> { 0, 1, 3,4};

                }
                else if(pieces.GetShape() == 6)
                {
                    return new List<int> { 0, 1, 5,6};

                }
                else
                {
                    return new List<int> { 0, 1, 9,10};

                }

            }

            return new List<int>();

        }
    }

}


