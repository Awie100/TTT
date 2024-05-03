using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicRules : Rules
{
    List<int[]> wins = new List<int[]>() {
        new int[] { 0, 1, 2 },
        new int[] { 3, 4, 5 },
        new int[] { 6, 7, 8 },
        new int[] { 0, 3, 6 },
        new int[] { 1, 4, 7 },
        new int[] { 2, 5, 8 },
        new int[] { 0, 4, 8 },
        new int[] { 2, 4, 6 }
    };

    public override List<TileInfo> GetTiles()
    {
        List<TileInfo> tiles = new List<TileInfo>();
        for (int i = 0; i < 9; i++)
        {
            tiles.Add(new TileInfo(4, 2 * (i % 3 - 1), 2 * (i / 3 - 1)));
        }
        return tiles;
    }

    public override List<PieceInfo> GetPieces()
    {
        List<PieceInfo> pieces = new List<PieceInfo>();
        pieces.Add(new PieceInfo(1, 4, 0.9f, 0.4f, true, new Color(1,0,0)));
        pieces.Add(new PieceInfo(2, 3, 0.9f, 0.4f, false, new Color(0,0,1)));
        return pieces;
    }

    public override void BoardUpdate(List<TileManager> board)
    {

    }

    public override bool WinCheck(List<TileManager> board)
    {
        foreach (var item in wins)
        {
            var bp0 = board[item[0]].pieces;
            var bp1 = board[item[1]].pieces;
            var bp2 = board[item[2]].pieces;

            if (bp0.Count > 0 && bp1.Count > 0 && bp2.Count > 0)
            {
                if ((bp0[0].value == bp1[0].value) && (bp1[0].value == bp2[0].value)) return true;
            }
        }
        return false;
    }

    public override bool TieCheck(List<TileManager> board)
    {
        foreach (var item in board)
        {
            if (item.pieces.Count == 0) return false;
        }

        return true;
    }

    public override bool PlaceValid(TileManager tile, Piece piece)
    {
        if (tile.pieces.Count > 0) return false;

        tile.place = false;
        return true;
    }
}
