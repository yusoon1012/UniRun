using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public static partial class Global_Function
{
    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Log(object message)
    {
#if DEBUG_MODE
        Debug.Log(message);

#endif
    }

    [System.Diagnostics.Conditional("DEBUG_MODE")]
    public static void Assert(bool condition)
    {
#if DEBUG_MODE

        Debug.Assert(condition);
#endif

    }

    //! GameObject �޾Ƽ� Text������Ʈ ã�Ƽ� text�ʵ� �� �����ϴ� �Լ�
    public static void SetText(this GameObject target, string text)
    {
        Text textComponent = target.GetComponent<Text>();
        if (textComponent == null || textComponent == default) { return; }
        textComponent.text = text;
    }
    
    //!LoadScene �Լ� �����Ѵ�
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //!�� ���͸� ���Ѵ�.
    public static Vector2 AddVector(this Vector3 origin,Vector2 addVector)
    {
        Vector2 result = new Vector2(origin.x, origin.y);
        result += addVector;
        return result;
    }

}
