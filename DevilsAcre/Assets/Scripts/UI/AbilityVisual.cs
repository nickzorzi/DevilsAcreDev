using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultipleAbilityIconCD : MonoBehaviour
{
    [System.Serializable]
    public class Ability
    {
        public Image image;
        public Text text;
        public KeyCode key;
        public float cooldown;
        public bool isCooldown;
        public float currentCooldown;
    }

    public List<Ability> abilities = new List<Ability>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (Ability ability in abilities)
        {
            ability.image.fillAmount = 0;
            ability.text.text = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Ability ability in abilities)
        {
            if (Input.GetKeyDown(ability.key) && !ability.isCooldown)
            {
                ability.isCooldown = true;
                ability.currentCooldown = ability.cooldown;
            }

            AbilityCooldown(ability);
        }
    }

    private void AbilityCooldown(Ability ability)
    {
        if (ability.isCooldown)
        {
            ability.currentCooldown -= Time.deltaTime;

            if (ability.currentCooldown <= 0f)
            {
                ability.isCooldown = false;
                ability.currentCooldown = 0f;

                if (ability.image != null)
                {
                    ability.image.fillAmount = 0f;
                }
                if (ability.text != null)
                {
                    ability.text.text = "";
                }
            }
            else
            {
                if (ability.image != null)
                {
                    ability.image.fillAmount = ability.currentCooldown / ability.cooldown;
                }
                if (ability.text != null)
                {
                    ability.text.text = Mathf.Ceil(ability.currentCooldown).ToString();
                }
            }
        }
    }
}
