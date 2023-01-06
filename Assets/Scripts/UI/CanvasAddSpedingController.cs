using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAddSpedingController : MonoBehaviour
{
    public TMP_InputField value;
    public TMP_InputField time;
    public Dropdown name_Bank;
    public Dropdown source;
    public Dropdown target;
    public TMP_InputField description;


    public void btnAddClicked()
    {
        UserData.current.AddExpenditures(
            int.Parse(value.text.ToString()),
            time.text.ToString(),
            (UserData.Name_Bank) name_Bank.value,
            (UserData.Source) source.value,
            (UserData.Target) target.value,
            description.text.ToString()
            );
    }

    
}
