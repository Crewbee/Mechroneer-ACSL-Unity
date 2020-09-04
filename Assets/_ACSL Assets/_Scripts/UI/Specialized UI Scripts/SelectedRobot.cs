using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedRobot : MonoBehaviour
{
    Button button;
    Color ogNormalColor;
    Color ogSelectedColor;
    bool selected = false;

    static SelectedRobot m_selectedRobot;

    private void Awake()
    {
        button = GetComponent<Button>();
        ogNormalColor = button.colors.normalColor;
        ogSelectedColor = button.colors.selectedColor;
    }

    public void RobotSelected()
    {

        selected = !selected;
        if (selected)
        {
            ColorBlock block = button.colors;
            block.normalColor = Color.gray;
            block.selectedColor = Color.gray;
            button.colors = block;

            if (m_selectedRobot)
            {
                m_selectedRobot.RobotSelected();
            }
            m_selectedRobot = this;
        }
        else
        {
            ColorBlock block = button.colors;
            block.normalColor = ogNormalColor;
            block.selectedColor = ogSelectedColor;
            button.colors = block;

            m_selectedRobot = null;
        }
    }
}
