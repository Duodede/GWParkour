using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatSystemManager : MonoBehaviour
{
    private bool m_isTestPadOn;
    public GameObject testPad;
    public void Start()
    {
        CheatSystem.manager = this;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchTestPad(!testPad.activeSelf);
        }
    }
    /// <summary>
    /// 开关控制台
    /// </summary>
    /// <param name="active"></param>
    private void SwitchTestPad(bool active)
    {
        testPad.SetActive(active);
        m_isTestPadOn = active;
        CheatSystem.isTestPadOn = active;
    }
}
