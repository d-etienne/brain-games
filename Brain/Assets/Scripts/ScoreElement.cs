using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{

    public TMP_Text usernameText;
    public TMP_Text MemoryScoreText;
    public TMP_Text MemoryBadgesText;
    public TMP_Text MemoryGamesPlayedText;

    public void NewScoreElement(string _username, int _score, int _badges, int _gp)
    {
        usernameText.text = _username;
        MemoryScoreText.text = _score.ToString();
        MemoryBadgesText.text = _badges.ToString();
        MemoryGamesPlayedText.text = _gp.ToString();
    }

}