using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Login : MonoBehaviour
{
    public InputField userNameInputField;
    public InputField passwordInputField;
    public Text messageText;
    public GameManager manager;

    string password
    { get { return passwordInputField.text; } }

    string userName
    { get { return userNameInputField.text; } }

    public void CheckUserNameAndPassword()
    {
        if (userName == "" || password == "")
        {
            SetMessage("输入不能为空");
            return;
        }

        if (CheckLogin(userName, password))
        {
            SetMessage("");
            manager.LoadScene(1);
        }
        else
        {
            SetMessage("账号或密码错误");
        }
    }

    bool CheckLogin(string u, string p)
    {
        if (u == "admin" && p == "admin")
            return true;

        return false;
    }

    bool SetMessage(string Message)
    {
        messageText.text = Message;
        return true;
    }

}
