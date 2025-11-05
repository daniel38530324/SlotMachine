using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(CanvasRenderer))]
public class UILineRenderer : Graphic
{
    private List<Vector2> points = new List<Vector2>(); // 用於存儲線條節點
    [SerializeField] private float lineWidth = 5f;
    [SerializeField] private Color lineColor = Color.white;

    [SerializeField] private Material customMaterial;

    public override Material material
    {
        get
        {
            if (customMaterial != null)
                return customMaterial;
            return base.material;
        }
        set
        {
            customMaterial = value;
        }
    }
    public override Texture mainTexture
    {
        get
        {
            if (material != null && material.mainTexture != null)
                return material.mainTexture;
            return s_WhiteTexture;
        }
    }
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();

        if (points == null || points.Count < 2)
            return;

        float totalLength = 0f;
        for (int i = 0; i < points.Count - 1; i++)
            totalLength += Vector2.Distance(points[i], points[i + 1]);

        float currentLength = 0f;

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector2 start = points[i];
            Vector2 end = points[i + 1];
            float segmentLength = Vector2.Distance(start, end);

            Vector2 direction = (end - start).normalized;
            Vector2 perpendicular = new Vector2(-direction.y, direction.x) * lineWidth / 2f;

            // 將 UV x 根據線段位置平均分配
            float uvStart = currentLength / totalLength;
            float uvEnd = (currentLength + segmentLength) / totalLength;
            currentLength += segmentLength;

            // === 左下 ===
            UIVertex v0 = UIVertex.simpleVert;
            v0.color = lineColor;
            v0.position = start - perpendicular;
            v0.uv0 = new Vector2(uvStart, 0);

            // === 左上 ===
            UIVertex v1 = UIVertex.simpleVert;
            v1.color = lineColor;
            v1.position = start + perpendicular;
            v1.uv0 = new Vector2(uvStart, 1);

            // === 右上 ===
            UIVertex v2 = UIVertex.simpleVert;
            v2.color = lineColor;
            v2.position = end + perpendicular;
            v2.uv0 = new Vector2(uvEnd, 1);

            // === 右下 ===
            UIVertex v3 = UIVertex.simpleVert;
            v3.color = lineColor;
            v3.position = end - perpendicular;
            v3.uv0 = new Vector2(uvEnd, 0);

            int index = vh.currentVertCount;
            vh.AddVert(v0);
            vh.AddVert(v1);
            vh.AddVert(v2);
            vh.AddVert(v3);

            vh.AddTriangle(index, index + 1, index + 2);
            vh.AddTriangle(index, index + 2, index + 3);
        }
    }

    // protected override void OnPopulateMesh(VertexHelper vh)
    // {
    //     vh.Clear();

    //     if (points == null || points.Count < 2)
    //         return;

    //     for (int i = 0; i < points.Count - 1; i++)
    //     {
    //         Vector2 start = points[i];
    //         Vector2 end = points[i + 1];

    //         Vector2 direction = (end - start).normalized;
    //         Vector2 perpendicular = new Vector2(-direction.y, direction.x) * lineWidth / 2f;

    //         // 建立矩形的四個頂點
    //         UIVertex vertex = UIVertex.simpleVert;
    //         vertex.color = lineColor;

    //         vertex.position = start - perpendicular;
    //         vh.AddVert(vertex);

    //         vertex.position = start + perpendicular;
    //         vh.AddVert(vertex);

    //         vertex.position = end + perpendicular;
    //         vh.AddVert(vertex);

    //         vertex.position = end - perpendicular;
    //         vh.AddVert(vertex);

    //         int index = i * 4;
    //         vh.AddTriangle(index, index + 1, index + 2);
    //         vh.AddTriangle(index, index + 2, index + 3);
    //     }
    // }

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

        points.Add(localPoint); //不再刪除舊點
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
