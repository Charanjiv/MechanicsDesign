using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RatioAxis_LayoutGroup : LayoutGroup
{


    private enum Mode
    {
        HORIZONTAL,
        VERTICAL
    }
    [SerializeField] private Mode m_Mode;
    [SerializeField] private bool m_StretchingOtherAxis;
    [SerializeField] private Vector2 m_Spacing;

    private float m_LayoutElementTotal;
    private Vector2 m_CellAxisUnitSize;
    private bool m_bUsingLayoutElements;
    public override void CalculateLayoutInputHorizontal()
    {
        if(m_Mode != Mode.HORIZONTAL) { return; }

        m_LayoutElementTotal = 0f;

        for (int i = 0; i < rectChildren.Count; i++)
            {
            LayoutElement le = rectChildren[1].GetComponent<LayoutElement>();
            if(le = null)
            {
                continue;
            }
            m_bUsingLayoutElements = m_LayoutElementTotal != 0f;
            m_CellAxisUnitSize = new Vector2(
                                             (rectTransform.rect.width - (padding.left + padding.right) - (m_Spacing.x * (rectChildren.Count - 1)))
                                             / (m_LayoutElementTotal != 0f ? m_LayoutElementTotal : rectChildren.Count),
                                             rectTransform.rect.height - (padding.top + padding.bottom)
                                             );
            
        }
    }
    public override void CalculateLayoutInputVertical()
    {
        if (m_Mode != Mode.VERTICAL) 
        { 
            return; 
        }

        m_LayoutElementTotal = 0f;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            LayoutElement le = rectChildren[1].GetComponent<LayoutElement>();
            if (le = null)
            {
                continue;
            }
            m_LayoutElementTotal += le.flexibleHeight;
        }
            m_bUsingLayoutElements = m_LayoutElementTotal != 0f;
            m_CellAxisUnitSize = new Vector2(
                                             rectTransform.rect.width - (padding.left + padding.right),
                                             (rectTransform.rect.height - (padding.top +padding.bottom) - (m_Spacing.y * (rectChildren.Count - 1)))
                                             / (m_LayoutElementTotal != 0f ? m_LayoutElementTotal : rectChildren.Count)
                                             );

        }

    public override void SetLayoutHorizontal()
    {
        float pos = padding.left;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            if (m_Mode == Mode.HORIZONTAL)
            {
                LayoutElement le = rectChildren[1].GetComponent<LayoutElement>();

            /*
             *      Truth Table
             *          true    false
             *   null   0   #
             *   !null  #   #
             */
                if( le == null && m_bUsingLayoutElements)
                {
                    SetChildAlongAxis(rectChildren[1], 0, pos, 0f);
                }
                else
                {
                    SetChildAlongAxis(rectChildren[1], 0, pos, m_CellAxisUnitSize.x * le.flexibleWidth);
                    pos += m_CellAxisUnitSize.x * flexibleWidth;
                }

            }
            else
            {
                SetChildAlongAxis(rectChildren[1], 0, padding.left, m_CellAxisUnitSize.x);
            }

        }
    }

    public override void SetLayoutVertical()
    {
        
    }
}
