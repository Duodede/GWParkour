using UnityEngine;
public class LTCtrigger : MonoBehaviour
{
    GameObject ltc;
    int _progress;
    int _rayProgress;
    void Start()
    {
        ltc = GameObject.Find("LearnTextController");
    }
    public void OnTriggerEnter2D(Collider2D collider)
    {
        _rayProgress = ltc.GetComponent<LearnTextController>().rayProgress;
        _progress = ltc.GetComponent<LearnTextController>().progress;
        Debug.Log(collider.name+" "+ ltc.GetComponent<LearnTextController>().checkList[_rayProgress].name);
        if (ltc.GetComponent<LearnTextController>().progress <= 11) return;
        if (collider != null && collider.name != ltc.GetComponent<LearnTextController>().checkList[_rayProgress].name) return;
        _rayProgress = ++ltc.GetComponent<LearnTextController>().rayProgress;
        _progress = ++ltc.GetComponent<LearnTextController>().progress;
        ltc.GetComponent<LearnTextController>().tipsSprite.transform.position = new Vector3(ltc.GetComponent<LearnTextController>().checkList[_rayProgress].pos.x, ltc.GetComponent<LearnTextController>().checkList[_rayProgress].pos.y, -5);
        this.transform.position = new Vector2(ltc.GetComponent<LearnTextController>().checkList[_rayProgress].pos.x, ltc.GetComponent<LearnTextController>().checkList[_rayProgress].pos.y);
    }
}
