/************************************************************************/
/*    µ¥ÀýÄ£Ê½                                                          */
/************************************************************************/

using UnityEngine;
using System.Collections;

public abstract class Singleton<T> where T : new()
{
    private static T m_instance;
    static object m_lock = new object();
    public static T Instance
    {
        get 
        {
            if(m_instance == null)
            {
                lock (m_lock)
                {
                    if(m_instance == null)
                        m_instance = new T();
                }
            }
            return m_instance;
        }
    }
}
