using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MysqlControl.mysqlOpen();
        MysqlControl.mysqlRead();
        MysqlControl.mysqlClose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
