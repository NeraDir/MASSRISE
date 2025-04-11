using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameController : MonoBehaviour
{
    [SerializeField] private GameObject _escapePanel;
    [SerializeField] private Animator _skillsPanel;

    private bool _isPause;
    private bool _isSkills;

    private void Update()
    {
        Pause();
        TransitionPause();

        if (_isPause)
            return;
        SkillsShow();
    }

    private void SkillsShow()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _isSkills = !_isSkills;
        }
        _skillsPanel.SetInteger("ui_panel_state", _isSkills ? 1 : 0);
    }

    private void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPause = !_isPause;
        }
    }

    private void TransitionPause()
    {
        if (_isPause)
        {
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 0, 0.01f);
            if (Time.timeScale <= 0.1f)
            {
                _escapePanel.SetActive(true);
            }
        }
        else
        {
            _escapePanel.SetActive(false);
            Time.timeScale = Mathf.MoveTowards(Time.timeScale, 1, 0.01f);
        }
    }

    public void Resume()
    {
        _isPause = false;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {

    }
}
