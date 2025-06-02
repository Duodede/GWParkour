using UnityEngine;

public class CheckPointFlag : MonoBehaviour
{
    public bool isChecked;
    public GameObject checkPointFlag;
    public GameObject checkPointFlag_None;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SwitchFlagState(bool state)
    {
        isChecked = state;
        checkPointFlag.SetActive(state);
        checkPointFlag_None.SetActive(!state);
        if(state)
        {
            Manager.manager.checkedCheckPoints.Add(this.gameObject);
            Manager.manager.lastCheckPoint = this.transform;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
                SwitchFlagState(true);
            }
        }
    }
}
