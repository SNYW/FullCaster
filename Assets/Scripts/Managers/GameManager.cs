using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Mage playerMage;
    public Material spriteMat;
    public ColourPalette selectedColourPalette;
    public static GameManager Instance;
    public bool playing = true;
    public Spell testSpell;
    public int level;
    public float expForLevel;
    public float expGrowthPerLevel;
    public int currentExp;
    public PlayerState playerState;

    public List<Enemy> Enemies = new List<Enemy>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ResetGame();
        PaletteManager.SetSpritePalette(spriteMat, selectedColourPalette);
        PopupManager.Instance.OpenChoicePopup();
    }

    public enum PlayerState
    {
        Moving,
        HasTarget
    }

    private void Update()
    {
        playerState = GetClosestEnemy() != null ? PlayerState.HasTarget : PlayerState.Moving;
    }

    public Enemy GetClosestEnemy()
    {
        float minDist = playerMage.range;
        Enemy returnEnemy = null;
        foreach (var enemy in Enemies)
        {
            var dist = Utils.GetDistance(playerMage.transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                returnEnemy = enemy;
            }
        }
        return returnEnemy;
    }

    public void AddExp(int amount)
    {
        if (currentExp + amount <= expForLevel)
        {
            currentExp += amount;
        }
        else
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        expForLevel = (int)(expForLevel + expForLevel * expGrowthPerLevel);
        currentExp = 0;
        SpawnManager.Instance.UpdateSpawnDelay(level);
        PopupManager.Instance.OpenChoicePopup();
    }

    public void ResetGame()
    {
        level = 0;
        currentExp = 0;
        expForLevel = 100;
        playing = true;
    }

    public void PauseGame()
    {
        playing = false;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        playing = true;
        Time.timeScale = 1;
    }

    public void AddPlayerGems(int amount)
    {
        if (PlayerPrefs.HasKey("gems"))
        {
            PlayerPrefs.SetInt("gems", PlayerPrefs.GetInt("gems") + amount);
        }
        else
        {
            PlayerPrefs.SetInt("gems", amount);
        }
    }
}
