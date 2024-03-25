using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcheryPhaseControl : MonoBehaviour
{
    [SerializeField]
    private GameObject GameMenu, Phase1Obj, Phase2Obj, Phase3Obj;

    [SerializeField]
    private ArrowController arrow;

    private bool active = false;
    private int phaseNum = 0;

    private void Start()
    {
        arrow = FindObjectOfType<ArrowController>();
    }

    private void Update()
    {
        if (active)
        {
            if (arrow.arrowCount == 0)
            {
                switch (phaseNum)
                {
                    case 1:
                        Phase1Obj.SetActive(false);
                        arrow.ResetArrow(10);
                        Phase2();
                        break;
                    case 2:
                        Phase2Obj.SetActive(false);
                        arrow.ResetArrow(10);
                        Phase3();
                        break;
                    case 3:
                        Phase3Obj.SetActive(false);
                        active = false;
                        break;
                }
            }
        }
    }

    public void Phase1()
    {
        arrow.ResetArrow(5);
        Phase1Obj.SetActive(true);
        active = true;
        phaseNum++;
    }
    void Phase2()
    {
        Phase2Obj.SetActive(true);
        phaseNum++;
    }

    void Phase3()
    {
        Phase3Obj.SetActive(true);
        phaseNum++;
    }
}
