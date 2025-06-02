using UnityEngine;
using UnityEngine.UI;
public class DeleteIFBtn : MonoBehaviour
{
    public InputField IFText;
    void Update()
    {
        if (IFText.text != "")
        {
            this.gameObject.SetActive(true);
        }
        else this.gameObject.SetActive(false);
        Debug.Log(IFText.text+"a"+IFText.text.Length);
    }
    public void Onclick()
    {
        IFText.text = "";
    }
}
