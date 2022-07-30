using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class PlayerManager : MonoBehaviour
{
    private int highscore = 0;

    [SearchObject(typeof(Weapon))]
    public Weapon[] m_weapons;
    [SearchObject(typeof(Weapon))]
    public Weapon m_equippedWeapon;

    [Header("Events")]
    [SearchObject(typeof(IntEvent))]
    public IntEvent OnAttack;
    public StringEvent OnWeaponChanged_ui;
    

    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    private void Start()
    {
        OnWeaponChanged_ui.Raise(m_equippedWeapon.m_name);
    }
    public void Attack()
    {
        OnAttack.Raise(m_equippedWeapon.m_damage);
        
        if (EnemyDisplay.score > EnemyDisplay.highscore)
            SaveScore();
    }

    public void ChangeWeapon(Weapon _W)
    {
        m_equippedWeapon = _W;
        OnWeaponChanged_ui.Raise(_W.m_name);
    }

    

    public void SaveScore()
    {
        EnemyDisplay.highscore = EnemyDisplay.score;
        if (MenuManager.isConnectedToGooglePlayServices)
        {
            Debug.Log("Reporting score...");
            Social.ReportScore(EnemyDisplay.score, GPGSIds.leaderboard_leaderboard, (success) =>
            {
                if (!success) Debug.LogError("Unable to post highscore");
            });
        }
        else
        {
            Debug.Log("Not signed in.. unable to report");
        }
    }
}
