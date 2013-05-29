/************************************************************************/
/*    ����ģʽ      ���ڼ̳���MonoBehaviour�ĵ���                                                    */
/************************************************************************/

using UnityEngine;
using System.Collections;

public class SingletonMono<T> where T : new()
{
    private static T m_instance;
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                Debug.LogError("NULL");
            }
            return m_instance;
        }
        set
        {
            if(m_instance != null)
            {
                Debug.LogError("! NULL");
            }
            m_instance = value;
        }
    }
}
