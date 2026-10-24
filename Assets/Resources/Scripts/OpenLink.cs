using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMPLinkOpener : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI pTextComponent;

    void Awake()
    {
        pTextComponent = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 1. Identify if the canvas is overlay or camera-based
        Canvas canvas = pTextComponent.canvas;
        Camera cameraToUse = (canvas.renderMode == RenderMode.ScreenSpaceOverlay) ? null : eventData.pressEventCamera;

        // 2. Pass the correct camera context to find the character
        int characterIndex = TMP_TextUtilities.FindIntersectingCharacter(pTextComponent, eventData.position, cameraToUse, true);
        
        if (characterIndex != -1)
        {
            // 3. Pass the correct camera context to find the link
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(pTextComponent, eventData.position, cameraToUse);
            
            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = pTextComponent.textInfo.linkInfo[linkIndex];
                string url = linkInfo.GetLinkID();
                
                if (!string.IsNullOrEmpty(url))
                {
                    Application.OpenURL(url);
                }
            }
        }
    }
}
