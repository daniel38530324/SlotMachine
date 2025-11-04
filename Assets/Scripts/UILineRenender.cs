using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(CanvasRenderer))]
public class UILineRenderer : Graphic
{
    private List<Vector2> points = new List<Vector2>(); // 用於存儲線條節點
    [SerializeField] private float lineWidth = 5f;
    [SerializeField] private Color lineColor = Color.white;

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (points == null || points.Count < 2)
            return;

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector2 start = points[i];
            Vector2 end = points[i + 1];

            Vector2 direction = (end - start).normalized;
            Vector2 perpendicular = new Vector2(-direction.y, direction.x) * lineWidth / 2f;

            // 建立矩形的四個頂點
            UIVertex vertex = UIVertex.simpleVert;
            vertex.color = lineColor;

            vertex.position = start - perpendicular;
            vh.AddVert(vertex);

            vertex.position = start + perpendicular;
            vh.AddVert(vertex);

            vertex.position = end + perpendicular;
            vh.AddVert(vertex);

            vertex.position = end - perpendicular;
            vh.AddVert(vertex);

            int index = i * 4;
            vh.AddTriangle(index, index + 1, index + 2);
            vh.AddTriangle(index, index + 2, index + 3);
        }
    }

    /// <summary>
    /// 新增一個 UI 節點 (會依序連成折線)
    /// </summary>
    public void AppendUIElement(RectTransform uiElement)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            RectTransformUtility.WorldToScreenPoint(null, uiElement.position),
            null,
            out localPoint
        );

        points.Add(localPoint); // ✅ 不再刪除舊點
        SetVerticesDirty();
    }

    /// <summary>
    /// 更新最後一個點為滑鼠位置（可視覺化拖線效果）
    /// </summary>
    public void SetMouse()
    {
        if (points.Count == 0) return;

        Vector2 point;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out point);

        if (points.Count > 1)
            points[points.Count - 1] = point;
        else
            points.Add(point);

        SetVerticesDirty();
    }

    public void SetLineColor(Color newColor)
    {
        lineColor = newColor;
        SetVerticesDirty();
    }

    public void SetWidth(float width)
    {
        lineWidth = width;
        SetVerticesDirty();
    }

    public void ResetSelf()
    {
        points.Clear();
        lineColor = Color.white;
        lineWidth = 5f;
        SetVerticesDirty();
    }

    /// <summary>
    /// 手動設定整條折線
    /// </summary>
    public void SetPoints(List<RectTransform> uiElements)
    {
        points.Clear();
        foreach (var element in uiElements)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform,
                RectTransformUtility.WorldToScreenPoint(null, element.position),
                null,
                out localPoint
            );
            points.Add(localPoint);
        }
        SetVerticesDirty();
    }

    public void Clear()
    {
        points.Clear();
        SetVerticesDirty();
    }
}
