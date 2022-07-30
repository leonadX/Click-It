using NaughtyAttributes;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextmeshproEventInvoker : MonoBehaviour
{

    TextMeshProUGUI m_text;
    public string m_prefix;
    public string m_suffix;

    [Header("PlayerPrefs")]
    [ShowIf("isPlayerPrefs")] public string m_prefKey;
    [ShowIf("isPlayerPrefs")] public PlayerPrefsDataType playerPrefsDataType;
    private void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {
        OnEnable();
    }
    bool isPlayerPrefs() { return true; }
    private void OnEnable()
    {

        switch (playerPrefsDataType)
        {
            case PlayerPrefsDataType.FLOAT:
                Invoke(PlayerPrefs.GetFloat(m_prefKey));
                break;

            case PlayerPrefsDataType.STRING:
                InvokeStr(PlayerPrefs.GetString(m_prefKey));
                break;

            case PlayerPrefsDataType.INT:
                Invoke(PlayerPrefs.GetInt(m_prefKey));
                break;

        }


    }
    public void Invoke(int _i)
    {
        CheckTxtObj();
        m_text.text = m_prefix + _i.ToString() + m_suffix;
    }
    public void InvokeStr(string _str)
    {
        CheckTxtObj();
        m_text.text = m_prefix + _str.ToString() + m_suffix;
    }
    public void Invoke(float _f)
    {
        CheckTxtObj();
        m_text.text = m_prefix + _f.ToString() + m_suffix;
    }
    public void CheckTxtObj()
    {
        if (!m_text)
            m_text = GetComponent<TextMeshProUGUI>();
    }
    public enum PlayerPrefsDataType
    {
        FLOAT, STRING, INT
    }
    public enum Parameters
    {
        // Dont mind the number tags, I was trying out some weird experiments
        NONE = 0,
        MONEY = 1 << 0,
        LEVEL = 2 << 1,
        DAY = 3 << 2,
        SEASON = 4 << 3,
        PLAYERPREFS = 5 << 4,
        NEXT_LEVEL_MONEY = 6 << 5,
        NEXT_LEVEL = 7 << 6,
        LEVEL_PROGRESSION = 8 << 7,
        TIME = 9 << 8,
        TOTAL_EARNING = 10 << 9
        //OTHER = 5 << 4
    }
}
