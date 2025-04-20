using UnityEngine;
using UnityEngine.UI;

public class GridAutoSizer : MonoBehaviour
{
    public RectTransform backgroundRect; // ∆Â≈Ã±≥æ∞Õº
    private GridLayoutGroup grid;

    void Start()
    {
        grid = GetComponent<GridLayoutGroup>();

        float cellWidth = backgroundRect.rect.width / 3f;
        float cellHeight = backgroundRect.rect.height / 3f;

        grid.cellSize = new Vector2(cellWidth, cellHeight);
    }
}