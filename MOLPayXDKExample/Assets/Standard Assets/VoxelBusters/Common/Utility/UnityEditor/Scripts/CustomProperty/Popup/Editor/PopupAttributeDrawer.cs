﻿#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace VoxelBusters.Utility
{
	[CustomPropertyDrawer(typeof(PopupAttribute))]
	public class PopupAttributeDrawer : PropertyDrawer
	{
		#region Properties

		public PopupAttribute Attribute 
		{ 
			get 
			{ 
				return ((PopupAttribute)attribute); 
			} 
		}

		#endregion

		#region Methods

		public override void OnGUI (Rect _position, SerializedProperty _property, GUIContent _label)
		{
			string[]	_popupOptions	= Attribute.options;
			int 		_selectedIndex 	= -1;

			EditorGUI.BeginProperty(_position, _label, _property);
			EditorGUI.BeginChangeCheck();

			for (int _iter = 0; _iter < _popupOptions.Length; _iter++)
			{
				if (_property.stringValue == _popupOptions[_iter])
				{
					_selectedIndex = _iter;
				}
			}

			_selectedIndex = EditorGUI.Popup(_position, _label.text, _selectedIndex, _popupOptions);

			if (EditorGUI.EndChangeCheck())
			{
				_property.stringValue = _popupOptions[_selectedIndex];
			}

			EditorGUI.EndProperty();
		}

		#endregion
	}
}
#endif