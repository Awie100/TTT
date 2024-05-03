using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{

    public virtual List<PieceInfo> GetPieces()
    {
        List<PieceInfo> pieces = new List<PieceInfo>();
        pieces.Add(new PieceInfo(1, 5, 0.9f, 0.4f, true));
        pieces.Add(new PieceInfo(1, 8, 0.4f, 0.4f, false));
        pieces.Add(new PieceInfo(1, 3, 0.7f, 0.7f, true));
        pieces.Add(new PieceInfo(1, 24, 0.7f, 0.3f, true));
        pieces.Add(new PieceInfo(1, 24, 0.9f, 0.3f, false));
        pieces.Add(new PieceInfo(1, 6, 0.2f, 0.5f, true));

        return pieces;
    }

    public virtual List<TileInfo> GetTiles()
    {
        List<TileInfo> tiles = new List<TileInfo>();
        tiles.Add(new TileInfo(4, 1, 1, 0));
        tiles.Add(new TileInfo(6, 1, -1, 1));
        tiles.Add(new TileInfo(7, -1, -1, 2));
        tiles.Add(new TileInfo(20, -1, 1, 3));

        return tiles;
    }

    public virtual void BoardUpdate(List<TileManager> board)
    {

    }

    public virtual bool WinCheck(List<TileManager> board)
    {
        return false;
    }

    public virtual bool TieCheck(List<TileManager> board)
    {
        return false;
    }

    public virtual bool PlaceValid(TileManager tile, Piece piece)
    {
        return true;
    }
}
