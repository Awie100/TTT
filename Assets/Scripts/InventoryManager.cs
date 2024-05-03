using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameObject slot, piece;
    public List<Piece> pieces;

    List<GameObject> slots = new List<GameObject>();
    List<RawImage> images;

    public Piece selected;

    public void Populate(List<PieceInfo> infos)
    {
        foreach (var info in infos)
        {
            var slotIns = Instantiate(slot, transform);
            var button = slotIns.GetComponentInChildren<Button>();

            var pie = Instantiate(piece, slotIns.transform.GetChild(0).GetChild(0)).GetComponent<Piece>();
            pie.DrawPiece(info.Deconstruct());

            slots.Add(slotIns);
            images.Add(slotIns.GetComponent<RawImage>());

            var index = slots.Count - 1;
            button.onClick.AddListener(delegate { OnClick(index); });
            pieces.Add(pie);
        }

        OnClick(0);
    }

    public void OnClick(int index)
    {
        foreach (var image in images)
        {
            image.color = Color.white;
        }

        images[index].color = new Color(0.7f, 1f, 1f);
        selected = pieces[index];
    }

    public void Clear()
    {
        images = new List<RawImage>();
        pieces = new List<Piece>();

        foreach (GameObject obj in slots)
        {
            Destroy(obj);
        }

        slots.Clear();
    }

    private void Start()
    {

    }
}
