using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIMain : MonoBehaviour
{
    private VisualElement uiRoot;
    private CombatManager combatManager;
    private HealthComponent healthComponent;

    private Label waveLabel;
    private Label pointsLabel;
    private Label enemiesLeftLabel;
    private Label healthLabel;

    void Start()
    {
        InitializeUI();
        UpdateUI();
    }

    private void InitializeUI()
    {
        uiRoot = GetComponent<UIDocument>()?.rootVisualElement;
        combatManager = FindObjectOfType<CombatManager>();
        healthComponent = FindObjectOfType<HealthComponent>();

        if (uiRoot != null)
        {
            waveLabel = uiRoot.Q<Label>("Wave");
            pointsLabel = uiRoot.Q<Label>("Point");
            enemiesLeftLabel = uiRoot.Q<Label>("EnemiesLeft");
            healthLabel = uiRoot.Q<Label>("Health");
        }
    }

    public void UpdateUI()
    {
        if (combatManager == null || healthComponent == null) return;

        int playerHealth = healthComponent.GetHealth();

        if (waveLabel != null)
            waveLabel.text = "Wave: " + combatManager.waveNumber;

        if (pointsLabel != null)
            pointsLabel.text = "Points: " + combatManager.points;

        if (enemiesLeftLabel != null)
            enemiesLeftLabel.text = "Enemies Left: " + combatManager.totalEnemies;

        if (healthLabel != null)
        {
            if (playerHealth > 0)
            {
                healthLabel.text = "Health: " + playerHealth;
            }
            else
            {
                DisplayGameOver();
            }
        }
    }

    private void DisplayGameOver()
    {
        if (healthLabel != null) healthLabel.text = null;
        if (waveLabel != null) waveLabel.text = null;
        if (pointsLabel != null) pointsLabel.text = null;
        if (enemiesLeftLabel != null) enemiesLeftLabel.text = null;

        if (pointsLabel != null)
            pointsLabel.text = "Game Over!\nYour Points: " + combatManager.points;
    }
}
