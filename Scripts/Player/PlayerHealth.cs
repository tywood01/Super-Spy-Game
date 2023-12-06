using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SocialPlatforms;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private PlayerLook look;
    private bool dead;
    private InputManager inputManager;
    private PlayerUI playerUI;

    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth = 150;
    public float lerpSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    private float durationTimer;
    private float pulse;
    [Header("Health Bar")]
    public Image damageOverlay;
    public float duration;
    public float fadeSpeed;
    public float maxDamageOverlayOpacity;
    public float pulseScale;
    public float pulseSpeed;
    public float lowHealth;

    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        
        health = maxHealth;
        pulse = 0;
        damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, 0);

        look = GetComponent<PlayerLook>();
        inputManager = GetComponent<InputManager>();
        playerUI = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateUI();

        if (health == 0 && !dead)
        {
            dead = true;
            Die();
        }

        if (health < lowHealth)
        {
            pulse = Mathf.Sin(pulseSpeed * Time.realtimeSinceStartup);
            float r = damageOverlay.color.r;
            float g = damageOverlay.color.g;
            float b = damageOverlay.color.b;
            damageOverlay.color = new Color(r, g, b, maxDamageOverlayOpacity + pulseScale * pulse);
            return;
        }

        if (damageOverlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = damageOverlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateUI()
    {
        float frontFill = frontHealthBar.fillAmount;
        float backFill = backHealthBar.fillAmount;
        float healthFraction = health / maxHealth;
        float percentComplete;

        if (backFill > healthFraction)
        {
            frontHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            percentComplete = lerpTimer / lerpSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(backFill, healthFraction, percentComplete);
        }
        
        if (frontFill < healthFraction)
        {
            backHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            percentComplete = lerpTimer / lerpSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(frontFill, backHealthBar.fillAmount, percentComplete);
        }
    }

    public void Damage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0f;
        damageOverlay.color = new Color(damageOverlay.color.r, damageOverlay.color.g, damageOverlay.color.b, maxDamageOverlayOpacity);
    }

    public void Heal(float healing)
    {
        health += healing;
        lerpTimer = 0f;
    }

    public void Die()
    {
        Rigidbody rb = this.AddComponent<Rigidbody>();
        GetComponent<CharacterController>().enabled = false;
        inputManager.onFoot.Disable();
        Cursor.lockState = CursorLockMode.None;
        playerUI.PauseGame();
        rb.AddForce(1,0,0);
        playerUI.Die();
    }
}
