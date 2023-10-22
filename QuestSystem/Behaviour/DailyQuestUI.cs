using UnityEngine.UI;
using UnityEngine;

using TMPro;

namespace VS.Subnautica.QuestSystem.Behaviour
{
    public class DailyQuestUI : MonoBehaviour
    {
        private Canvas canvas;
        private CanvasRenderer renderer;
        private CanvasScaler scaler;

        public TMP_Text questText;

        public void Awake()
        {
            gameObject.transform.name = "Daily Quests UI";

            // Adding canvas components.
            canvas = gameObject.AddComponent<Canvas>();
            renderer = canvas.gameObject.EnsureComponent<CanvasRenderer>();
            scaler = canvas.gameObject.EnsureComponent<CanvasScaler>();

            canvas.sortingOrder = 9999;
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // Setting up the canvas scaling settings so it's responsive.
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.referenceResolution = new Vector2(1920F, 1080F);

            CreateQuestText();
        }

        private void CreateQuestText()
        {
            GameObject questTextGameObject = new GameObject();
            questTextGameObject.transform.name = "Current Quests";
            questTextGameObject.transform.SetParent(canvas.transform);

            RectTransform rectTransform = questTextGameObject.AddComponent<RectTransform>();
            // Sets the anchor to top right.
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.anchorMin = new Vector2(1, 1);

            // Resets position and size relative to anchors.
            rectTransform.offsetMin = new Vector2(-1920, -1080);
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(1920, 1080);

            // Because otherwise purple and essence would have died from a cardiac arrest.
            questText = rectTransform.gameObject.AddComponent<TextMeshProUGUI>();
            questText.horizontalAlignment = HorizontalAlignmentOptions.Right;
            questText.margin = new Vector4(24, 24, 24, 24);
            // Loads up the correct font for the text, otherwise it would not render.
            questText.font = Resources.Load<TMP_FontAsset>($"Fonts/AddressableResources/Fonts & Materials/PTSans SDF.asset");
            questText.fontSize = 16;
            questText.text = "<b>No current quest.</b>\nWait for the next day to get new daily quests!";
        }
    }
}
