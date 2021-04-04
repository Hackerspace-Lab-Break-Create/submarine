using UnityEngine;

public class FlashText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI Text;
    public float timer = 0.0F;
    public float totalTimer;
    public float MaxTime = 0.0F;

    private void Start()
    {
        Text = gameObject.transform
            .Find("Canvas/Text").GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        // Flash the text
        timer += Time.deltaTime;
        totalTimer += Time.deltaTime;
        if (timer <= 0.5F)
        {
            Text.color = Color.white;
        }
        else if (timer > 0.5F)
        {
            Text.color = Color.red;
        }
        else if (timer >= 1.0F)
        {
            timer = 0.0F;
        }

        if (totalTimer >= MaxTime)
        {
            Destroy(gameObject);
        }
    }
}
