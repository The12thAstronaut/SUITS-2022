using JetBrains.Annotations;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class UIATaskProcedure : MonoBehaviour
{
    public TextMeshPro currentTask;
    public TextMeshPro titleLabel;
    public TextMeshPro taskTitle;
    public TextMeshPro stepIndicator;
    public Interactable previousTask;
    public Interactable nextTask;
    public Interactable doneButton;

    public GameObject previousTaskIcon;
    public GameObject nextTaskIcon;
    public GameObject doneIcon;

	public List<ProcedureTask> taskList = new List<ProcedureTask>();
	public List<string> title = new List<string>();
    public GameObject UIA;
    public GameObject DCU;
    public GameObject[] switches;
    public GameObject dcuBatt;
    private int activeTaskIndex = 0;
    private int activeSubtaskIndex = 0;
    private int activeSubSubtaskIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        DCU.SetActive(false);

        previousTaskIcon.SetActive(false);
        nextTask.OnClick.AddListener(() => NextTask());
        previousTask.OnClick.AddListener(() => ReverseTask());
        doneButton.OnClick.AddListener(() => TaskDone());


        if (titleLabel != null)
        {
            titleLabel.SetText(title[0]);
        }

        taskTitle.SetText(taskList[activeTaskIndex].taskTitle);
        stepIndicator.SetText("Step: " + (activeTaskIndex + 1) + "." + (activeSubtaskIndex + 1) + " / " + taskList.Count);
        currentTask.SetText(taskList[activeTaskIndex].taskSteps[activeSubtaskIndex].taskStepTitle);
    }

    public void UpdateTask()
    {
		if (activeTaskIndex < taskList.Count - 1)
		{
            if (taskList[activeTaskIndex].taskSteps[activeSubtaskIndex].taskSubSteps.Count == 0)
            {
				taskTitle.SetText(taskList[activeTaskIndex].taskTitle);
				stepIndicator.SetText("Step: " + (activeTaskIndex + 1) + "." + (activeSubtaskIndex + 1) + " / " + taskList.Count);
				currentTask.SetText(taskList[activeTaskIndex].taskSteps[activeSubtaskIndex].taskStepTitle);
			}
            else
            {
				taskTitle.SetText(taskList[activeTaskIndex].taskSteps[activeSubtaskIndex].taskStepTitle);
				stepIndicator.SetText("Step: " + (activeTaskIndex + 1) + "." + (activeSubtaskIndex + 1) + "." + (activeSubSubtaskIndex + 1) + " / " + taskList.Count);
                currentTask.SetText(taskList[activeTaskIndex].taskSteps[activeSubtaskIndex].taskSubSteps[activeSubSubtaskIndex]);
			}
		}
		else
		{
			currentTask.SetText(taskList[activeTaskIndex].taskTitle);
			taskTitle.SetText("");
			stepIndicator.SetText("Step: " + (activeTaskIndex + 1) + " / " + taskList.Count);
			nextTaskIcon.SetActive(false);
			doneIcon.SetActive(true);
		}

		if (activeTaskIndex == 0 && activeSubtaskIndex == 0 && activeSubSubtaskIndex == 0)
		{
			previousTaskIcon.SetActive(false);
		}
	}


    public void NextTask()
    {
        nextTaskIcon.SetActive(true);
        previousTaskIcon.SetActive(true);
        //doneIcon.SetActive(true);

        if (activeSubSubtaskIndex < taskList[activeTaskIndex].taskSteps[activeSubtaskIndex].taskSubSteps.Count - 1)
        {
            activeSubSubtaskIndex++;
        }
        else
        {
            activeSubSubtaskIndex = 0;
			if (activeSubtaskIndex < taskList[activeTaskIndex].taskSteps.Count - 1)
			{
				activeSubtaskIndex++;
			}
			else
			{
				activeTaskIndex++;
				activeSubtaskIndex = 0;
			}
		}

        UpdateTask();
	}

    public void ReverseTask()
    {

        nextTaskIcon.SetActive(true);
        previousTaskIcon.SetActive(true);

        if (!((activeTaskIndex == 0 && activeSubtaskIndex == 0) && activeSubSubtaskIndex == 0))
        {
			if (activeSubSubtaskIndex == 0)
			{
                if (activeSubtaskIndex > 0)
                {
                    activeSubtaskIndex--;
                    if (taskList[activeTaskIndex].taskSteps[activeSubtaskIndex].taskSubSteps.Count != 0)
                    {
						activeSubSubtaskIndex = taskList[activeTaskIndex].taskSteps[activeSubtaskIndex].taskSubSteps.Count - 1;
					}
                    else
                    {
						activeSubSubtaskIndex = 0;
					}
				}
                else
                {
                    if (activeTaskIndex > 0)
                    {
                        activeTaskIndex--;
                        activeSubtaskIndex = taskList[activeTaskIndex].taskSteps.Count - 1;
						if (taskList[activeTaskIndex].taskSteps[activeSubtaskIndex].taskSubSteps.Count != 0)
						{
							activeSubSubtaskIndex = taskList[activeTaskIndex].taskSteps[activeSubtaskIndex].taskSubSteps.Count - 1;
						}
						else
						{
							activeSubSubtaskIndex = 0;
						}
					}
                }

			}
			else
			{
                activeSubSubtaskIndex--;
			}

			UpdateTask();
		}
	}

    public void TaskDone()
    {
        activeTaskIndex = 0;
		activeSubtaskIndex = 0;
		activeSubSubtaskIndex = 0;
		UpdateTask();
        nextTaskIcon.SetActive(true);
        doneIcon.SetActive(false);
        previousTaskIcon.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        
        for (int i = 0; i < switches.Length; i++)
        {
            if (activeTaskIndex == 0)
            {
                if (i == 0 || i == 5)
                {
                    switches[i].SetActive(true);
                    
                    switches[i].GetComponent<SwitchHologram>().enabled = true;
                  

                }

                else
                {
                    switches[i].SetActive(false);
                }

            }

            else if (activeTaskIndex == 1)
            {
				if (activeSubtaskIndex == 0)
				{
					if (i == 8)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = true;

					}
					else
					{
						switches[i].SetActive(false);
					}
				}
				else if (activeSubtaskIndex == 2)
				{
					if (i == 8)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = false;

					}

					else
					{
						switches[i].SetActive(false);
					}
				}
			}

            else if (activeTaskIndex == 2)
            {
                if (activeSubtaskIndex == 0)
                {
					if (i == 6 || i == 7)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = true;

					}
					else
					{
						switches[i].SetActive(false);
					}
				}
                else if (activeSubtaskIndex == 2)
                {
					if (i == 6 || i == 7)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = false;

					}
					else
					{
						switches[i].SetActive(false);
					}
				}
                else if (activeSubtaskIndex == 3)
                {
					if (i == 8)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = true;

					}
					else
					{
						switches[i].SetActive(false);
					}
				}
				else if (activeSubtaskIndex == 5)
				{
					if (i == 8)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = false;

					}
					else
					{
						switches[i].SetActive(false);
					}
				}
			}

            else if (activeTaskIndex == 3)
            {
                if (activeSubtaskIndex == 0)
                {
					if (i == 6 || i == 7)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = true;
					}
					else
					{
						switches[i].SetActive(false);
					}
				}
                else if (activeSubtaskIndex == 2)
				{
					if (i == 6 || i == 7)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = false;
					}
					else
					{
						switches[i].SetActive(false);
					}
				}

			}


            else if (activeTaskIndex == 4)
            {
				if (activeSubtaskIndex == 0)
				{
                    if (activeSubSubtaskIndex == 0)
                    {
						if (i == 2)
						{
							switches[i].SetActive(true);
							switches[i].GetComponent<SwitchHologram>().enabled = true;

						}
						else
						{
							switches[i].SetActive(false);
						}
					}
                    else if (activeSubSubtaskIndex == 2)
                    {
						if (i == 2)
						{
							switches[i].SetActive(true);
							switches[i].GetComponent<SwitchHologram>().enabled = false;

						}
						else
						{
							switches[i].SetActive(false);
						}
					}
				}
				else if (activeSubtaskIndex == 1)
				{
					if (activeSubSubtaskIndex == 0)
					{
						if (i == 1)
						{
							switches[i].SetActive(true);
							switches[i].GetComponent<SwitchHologram>().enabled = true;

						}

						else
						{
							switches[i].SetActive(false);
						}
					}
					else if (activeSubSubtaskIndex == 2)
					{
						if (i == 1)
						{
							switches[i].SetActive(true);
							switches[i].GetComponent<SwitchHologram>().enabled = false;

						}

						else
						{
							switches[i].SetActive(false);
						}
					}
				}
			}


            else if (activeTaskIndex == 5)
            {
                if (activeSubtaskIndex == 0)
                {
					if (i == 9)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = true;

					}
					else
					{
						switches[i].SetActive(false);
					}
				}
                else if (activeSubtaskIndex == 1)
                {
                    if (activeSubSubtaskIndex == 0)
                    {
						if (i == 9)
						{
							switches[i].SetActive(true);
							switches[i].GetComponent<SwitchHologram>().enabled = false;

						}
						else
						{
							switches[i].SetActive(false);
						}
					}
                    else if (activeSubSubtaskIndex == 2)
                    {
						if (i == 9)
						{
							switches[i].SetActive(true);
							switches[i].GetComponent<SwitchHologram>().enabled = true;

						}
						else
						{
							switches[i].SetActive(false);
						}
					}
                }
                else
                {
					if (i == 9)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = false;

					}
					else
					{
						switches[i].SetActive(false);
					}
				}
                
            }

            else if (activeTaskIndex == 6)
            {
                if (activeSubtaskIndex == 0)
                {
					if (i == 6 || i == 7)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = true;

					}
					else
					{
						switches[i].SetActive(false);
					}
				}
                else if (activeSubtaskIndex == 2)
                {
					if (i == 6 || i == 7)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = false;

					}
					else
					{
						switches[i].SetActive(false);
					}
				}
                
            }

            else if (activeTaskIndex == 7)
            {
                if (i == 6)
                {
                    switches[i].SetActive(true);
                    switches[i].GetComponent<SwitchHologram>().enabled = false;

                }

                else
                {
                    switches[i].SetActive(false);
                }
            }

            else if (activeTaskIndex == 8)
            {
				if (activeSubtaskIndex == 0)
				{
					if (i == 8)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = true;

					}
					else
					{
						switches[i].SetActive(false);
					}
				}
				else if (activeSubtaskIndex == 2)
				{
					if (i == 8)
					{
						switches[i].SetActive(true);
						switches[i].GetComponent<SwitchHologram>().enabled = false;

					}
					else
					{
						switches[i].SetActive(false);
					}
				}
			}

            else if (activeTaskIndex == 9)
            {
				switches[i].SetActive(false);
            }


            /*else if (activeTaskIndex == 10)
            {
                if (i == 6)
                {
                    switches[i].SetActive(true);
                    switches[i].GetComponent<SwitchHologram>().enabled = true;

                }

                else
                {
                    switches[i].SetActive(false);
                }
            }

            else if (activeTaskIndex == 11)
            {
                if (i == 6)
                {
                    switches[i].SetActive(true);
                    switches[i].GetComponent<SwitchHologram>().enabled = false;

                }

                else
                {
                    switches[i].SetActive(false);
                }
            }

            else if (activeTaskIndex == 12)
            {
                UIA.SetActive(true);
                DCU.SetActive(false);

                if (i == 0)
                {
                    switches[i].SetActive(true);
                    switches[i].GetComponent<SwitchHologram>().enabled = false;

                }

                else
                {
                    switches[i].SetActive(false);
                }
            }

            else if (activeTaskIndex == 13)
            {

                UIA.SetActive(false);
                switches[i].SetActive(false);
                DCU.SetActive(true);
                dcuBatt.SetActive(true);

            }

            else if (activeTaskIndex == 14)
            {

                UIA.SetActive(true);
                DCU.SetActive(false);

                if (i == 0)
                {
                    switches[i].SetActive(true);
                    switches[i].GetComponent<SwitchHologram>().enabled = true;

                }

                else
                {
                    switches[i].SetActive(false);
                }
            }*/


        }
    }
}

[System.Serializable]
public class ProcedureTask
{
    public string taskTitle;
    public List<ProcedureTaskSteps> taskSteps;
}

[System.Serializable]
public class ProcedureTaskSteps
{
    public string taskStepTitle;
	public List<string> taskSubSteps;
}