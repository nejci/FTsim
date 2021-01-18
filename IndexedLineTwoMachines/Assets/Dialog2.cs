using UnityEngine;
using System;

// example:
// Dialog2.MessageBox("error", "Sorry but you're S.O.L", () => { Application.Quit() });

public class Dialog2 : MonoBehaviour {

    Rect m_windowRect;
    Action m_action_ok;
    Action m_action_cancel;
    string m_title;
    string m_msg;
	string m_button_ok;
	string m_button_cancel;

    static public void MessageBox(string title, string msg, string button_ok, string button_cancel, Action action_ok, Action action_cancel)
    {
        GameObject go = new GameObject("Dialog2");
        Dialog2 dlg = go.AddComponent<Dialog2>();
		dlg.Init(title, msg, button_ok, button_cancel, action_ok, action_cancel);
    }

	void Init(string title, string msg, string button_ok, string button_cancel, Action action_ok, Action action_cancel)
    {
        m_title = title;
        m_msg = msg;
		m_button_ok = button_ok;
		m_button_cancel = button_cancel;
        m_action_ok = action_ok;
        m_action_cancel = action_cancel;
    }

    void OnGUI()
    {
        const int maxWidth = 300;
        const int maxHeight = 200;

        int width = Mathf.Min(maxWidth, Screen.width - 20);
        int height = Mathf.Min(maxHeight, Screen.height - 20);
        m_windowRect = new Rect(
            (Screen.width - width) / 2,
            (Screen.height - height) / 2,
            width,
            height);

        m_windowRect = GUI.Window(0, m_windowRect, WindowFunc, m_title);
    }

    void WindowFunc(int windowID)
    {
        const int border = 10;
        const int width = 50;
        const int height = 25;
        const int spacing = 10;

        Rect l = new Rect(
            border,
            border + spacing,
            m_windowRect.width - border * 2,
            m_windowRect.height - border * 2 - height - spacing);
        GUI.Label(l, m_msg);

        Rect buttonOK = new Rect(
            m_windowRect.width - 2*width - border - spacing,
            m_windowRect.height - height - border,
            width,
            height);

        Rect buttonEXIT = new Rect(
            m_windowRect.width - width - border,
            m_windowRect.height - height - border,
            width,
            height);

		if (GUI.Button(buttonOK, m_button_ok))
        {
            Destroy(this.gameObject);
            m_action_ok();
        }
		if (GUI.Button(buttonEXIT, m_button_cancel))
        {
            Destroy(this.gameObject);
            m_action_cancel();
        }

    }
}
