using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterPage : MonoBehaviour
{
    public InputField Username, Userpwd, reUserpwd;
    public Text loginHint;

    public void registerClick()
    {
        if (Username.text != null && Userpwd.text != null && reUserpwd.text != null)
        {
            if(!MysqlControl.userCheck(Username.text))
            {
                if(Userpwd.text == reUserpwd.text)
                {
                    MysqlControl.userWrite(Username.text, Userpwd.text);
                    loginHint.text = "註冊成功";
                }
                else
                    loginHint.text = "密碼不一致";
            }
            else
                loginHint.text = "使用者名稱重複";
        }
        else
            loginHint.text = "欄位不可空白";
    }

    public void loginClick()
    {
        SceneManager.LoadScene(0);
    }
}
