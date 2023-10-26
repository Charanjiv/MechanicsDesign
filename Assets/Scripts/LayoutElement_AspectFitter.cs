using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LayoutEllement_AspectFitter : LayoutElement
{ 
    [SerializeField] private float m_Aspect;
    [SerializeField] private AspectPriority m_AspectType;

    protected IEnumerator Start()
    {
        yield return null;
        OnValidate();
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        if(m_AspectType == AspectPriority.WIDTH_FROM_HEIGHT)
        {
            preferredWidth = (transform as RectTransform).rect.height * m_Aspect;
        }
        else
        {
            preferredHeight = (transform as RectTransform).rect.width / m_Aspect;

        }
        


    }
    public override void CalculateLayoutInputVertical()
    {
        OnValidate();
    }





}
public enum AspectPriority
{
    WIDTH_FROM_HEIGHT,
    HEIGHT_FROM_WIDTH
}