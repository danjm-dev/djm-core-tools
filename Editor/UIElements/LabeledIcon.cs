using UnityEngine;
using UnityEngine.UIElements;

namespace DJM.CoreTools.Editor.UIElements
{
    public sealed class LabeledIcon : VisualElement
    {
        public const string USSClassName = "djm-labeled-icon";
        
        private TextElement _label;
        private Image _icon;

        public LabeledIcon() : this(string.Empty)
        {
        }
        
        public LabeledIcon(string text)
        {
            AddToClassList(USSClassName);
            _label.text = text;
            
            _icon = new Image();
            var texture = new Texture2D(5, 5);
            var pixels = new Color[25];
            for (var i = 0; i < pixels.Length; i++)
            {
                pixels[i] = Color.red;
            }
            texture.SetPixels(pixels);
            texture.Apply();
            
            _icon.style.backgroundImage = new StyleBackground(texture);
            
            Add(_icon);
            Add(_label);
        }
        
        public new class UxmlFactory : UxmlFactory<LabeledIcon, UxmlTraits> { }
    }
}