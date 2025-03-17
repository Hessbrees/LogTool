using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "LogDataSO", menuName = "SoE/Tools/LogData", order = 0)]
    public class LogDataSO : ScriptableObject
    {
        [ListDrawerSettings(HideAddButton = true, ShowFoldout = false, DefaultExpandedState = true, DraggableItems = false, ShowPaging = false)]
        [SerializeField] public List<LogData> logDataList;

        private void OnEnable()
        {
            UpdateLogSetting();
        }

        private void OnValidate()
        {
            UpdateLogSetting();
        }

        private void UpdateLogSetting()
        {
            EnumUpdateButton.UpdateData<LogDataType, LogData>(ref logDataList, "logDataType");
        }
    }

    [Serializable]
    public class LogData
    {
        [HideLabel, HorizontalGroup, SerializeField, ReadOnly] public LogDataType logDataType;

        [HideLabel, HorizontalGroup, ShowInInspector] public bool IsActive
        {
            get
            {
#if UNITY_EDITOR
                return UnityEditor.EditorPrefs.GetBool($"LogData/{logDataType}", true);
#else
                return false;
#endif
            }
#if UNITY_EDITOR
            set => UnityEditor.EditorPrefs.SetBool($"LogData/{logDataType}", value);
#else
            set{}
#endif
        }

        [HideLabel, HorizontalGroup, SerializeField] public Color logColor = Color.white;
    }

    public enum LogDataType
    {
        Command = 0,
        ValidateValue = 1,
        Default = 2,
        Behaviour = 3,
        Combat = 4,
        Input = 5,
        Formula = 6,
        Inventory = 7,
        Quests = 8,
        Achievement = 9,

    }

