using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginPage : MonoBehaviour
{
    public InputField Username, Userpwd;
    public Text loginHint;
    // Start is called before the first frame update
    void Start()
    {
        //MysqlControl.mysqlRead();
    }

    public void loginClick()
    {
        if(MysqlControl.userRead(Username.text, Userpwd.text))
        {
            loginHint.text = "登入成功";
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
