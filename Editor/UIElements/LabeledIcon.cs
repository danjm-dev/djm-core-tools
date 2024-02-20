using UnityEngine;
using UnityEngine.UIElements;

namespace DJM.CoreTools.Editor.UIElements
{
    public sealed class LabeledIcon : VisualElement
    {
        public const string USSClassName = "djm-labeled-icon";
        
        private readonly Image _icon;
        private readonly Label _label;
        
        public float Size
        {
            set
            {
                style.width = value;
                style.height = value;
            }
        }
        public StyleBackground IconBackground
        {
            set => _icon.style.backgroundImage = value;
        }
        public Color IconBorderColor
        {
            set
            {
                _icon.style.borderBottomColor = value;
                _icon.style.borderLeftColor = value;
                _icon.style.borderRightColor = value;
                _icon.style.borderTopColor = value;
            }
        }
        public string Text
        {
            set => _label.text = value;
        }
        public Color LabelColor
        {
            set => _label.style.color = value;
        }
        public Color LabelBackgroundColor
        {
            set => _label.style.backgroundColor = value;
        }

        public LabeledIcon()
        {
            AddToClassList(USSClassName);
            
            style.flexDirection = FlexDirection.Column;
            style.justifyContent = Justify.Center;
            style.alignItems = Align.Center;
            
            _icon = new Image
            {
                style =
                {
                    position = Position.Absolute,
                    width = new StyleLength(Length.Percent(90)),
                    height = new StyleLength(Length.Percent(90)),
                    borderBottomWidth = new StyleFloat(2f),
                    borderLeftWidth = new StyleFloat(2f),
                    borderRightWidth = new StyleFloat(2f),
                    borderTopWidth = new StyleFloat(2f),
                    borderBottomLeftRadius = new StyleLength(5f),
                    borderBottomRightRadius = new StyleLength(5f),
                    borderTopLeftRadius = new StyleLength(5f),
                    borderTopRightRadius = new StyleLength(5f)
                }
            };

            _label = new Label
            {
                style =
                {
                    position = Position.Absolute,
                    unityFontStyleAndWeight = new StyleEnum<FontStyle>(FontStyle.Bold),
                    borderBottomWidth = new StyleFloat(1f),
                    borderLeftWidth = new StyleFloat(2f),
                    borderRightWidth = new StyleFloat(2f),
                    borderTopWidth = new StyleFloat(1f),
                    borderBottomLeftRadius = new StyleLength(5f),
                    borderBottomRightRadius = new StyleLength(5f),
                    borderTopLeftRadius = new StyleLength(5f),
                    borderTopRightRadius = new StyleLength(5f)
                }
            };

            Add(_icon);
            Add(_label);
        }
        
        public new class UxmlFactory : UxmlFactory<LabeledIcon, UxmlTraits> { }
    }
}