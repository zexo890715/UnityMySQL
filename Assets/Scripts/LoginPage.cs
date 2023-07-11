using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginPage : MonoBehaviour
{
    public InputField Username, Userpwd;
    public Text loginHint;

    public void loginClick()
    {
        if(MysqlControl.pwdCheck(Username.text, Userpwd.text))
        {
            loginHint.text = "登入成功";
            SceneManager.LoadScene(2);
        }
        else
        {
            loginHint.text = "帳號或密碼錯誤";
        }
    }

    public void registerClick()
    {
        SceneManager.LoadScene(1);
    }
}
