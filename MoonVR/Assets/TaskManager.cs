using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TaskManager : MonoBehaviour
{
    public GameObject[] taskSet;
    public Interactable[] taskButtons;
    public UIATaskProcedure procedureManager;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var task in taskSet)
        {
            task.SetActive(false);
        }

        for (int i = 0; i < taskButtons.Length; i++)
        {
            int closureIndex = i; 
            taskButtons[closureIndex].OnClick.AddListener(() => TaskOnClick(closureIndex));
        }

    }


    public void TaskOnClick(int buttonIndex)
    {
        /*count++;

        for (int i = 0; i < taskSet.Length; i++)
        {
            if (i == buttonIndex)
            {
                taskSet[i].SetActive(true);
            }

            else
            {
                taskSet[i].SetActive(false);
            }
            
        }*/

		if (procedureManager.activeProcedure == buttonIndex && taskSet[0].activeSelf) {
			taskSet[0].SetActive(false);
			procedureManager.nextTaskIcon.SetActive(false);
			procedureManager.previousTaskIcon.SetActive(false);
			procedureManager.doneIcon.SetActive(false);

		} else {
			procedureManager.SetProcedure(buttonIndex);
			taskSet[0].SetActive(true);

		}


	}
    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideTask() {
		taskSet[0].SetActive(false);
	}
}
