using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class PayoutCalculation : MonoBehaviour
{
    [SerializeField]
    private Transform LineContainer;
    [SerializeField]
    private GameObject[] Lines_Object;

    GameObject TempObj = null;

    internal void GeneratePayoutLinesBackend(int lineIndex, bool isStatic = false)
    {
        if (Lines_Object[lineIndex]) Lines_Object[lineIndex].SetActive(true);

        if(isStatic)
        {
            TempObj = Lines_Object[lineIndex];
        }
    }

    internal void ResetStaticLine()
    {
        if(TempObj!=null)
        {
            TempObj.SetActive(false);
            TempObj = null;
        }
    }

    internal void ResetLines()
    {
        foreach (GameObject child in Lines_Object)
        {
            child.SetActive(false);
        }
    }

}
