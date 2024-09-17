using UnityEngine;
using RedLobsterStudios.Util;
using TMPro;
public class InteractionManager : MonoSingleton<InteractionManager> 
{
    private TMP_Text interactionText;

    private TMP_Text InteractionText
    {
        get
        {
            if(interactionText == null)
                interactionText = FindFirstObjectByType<InteractionText>().GetComponent<TMP_Text>();
            return interactionText;
        }
    }

    public void SetInteractionText(string text)
    {
        InteractionText.text = text;
    }
}
