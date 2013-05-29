/************************************************************************/
/*    单例模式      用于继承与MonoBehaviour的单例                                                    */
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
