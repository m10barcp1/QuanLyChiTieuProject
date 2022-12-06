using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_Controller : MonoBehaviour
{
    public List<GameObject> canvas;


    public void btn_AddSpeding_OnCliked()
    {
        foreach (var i in canvas) i.SetActive(false);
        canvas[0].SetActive(true);
    }
    public void btn_Statistical_OnCliked()
    {
        foreach (var i in canvas) i.SetActive(false);
        canvas[1].SetActive(true);
    }

    public void btn_Ranking_OnCliked()
    {
        foreach (var i in canvas) i.SetActive(false);
        canvas[2].SetActive(true);
    }
    
    public void btn_ViewDebt_OnCliked()
    {
        foreach (var i in canvas) i.SetActive(false);
        canvas[3].SetActive(true);
    }
    public void btn_ViewLoan_OnCliked()
    {
        foreach (var i in canvas) i.SetActive(false);
        canvas[4].SetActive(true);
    }

    public void btn_Close()
    {
        foreach (var i in canvas) i.SetActive(false);
    }

}
