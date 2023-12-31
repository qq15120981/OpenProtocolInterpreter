﻿namespace OpenProtocolInterpreter
{
    /// <summary>
    /// Digital Input Numbers. Used in <see cref="IOInterface.Mid0220"/>, <see cref="IOInterface.Mid0221"/>, 
    /// <see cref="IOInterface.Mid0223"/>, <see cref="IOInterface.Mid0224"/> and <see cref="IOInterface.Mid0225"/>.
    /// </summary>
    public enum DigitalInputNumber
    {
        Off = 0,
        ResetBatch = 1,
        UnlockTool = 2,
        ToolDisableNo = 3,
        ToolDisableNc = 4,
        ToolTighteningDisable = 5,
        ToolLooseningDisable = 6,
        RemoteStartPulse = 7,
        RemoteStartCont = 8,
        ToolStartLoosening = 9,
        BatchIncrement = 10,
        BypassPSet = 11,
        AbortJob = 12,
        JobOff = 13,
        ParameterSetToggle = 14,
        ResetRelays = 15,
        ParameterSetSelectBit0 = 16,
        ParameterSetSelectBit1 = 17,
        ParameterSetSelectBit2 = 18,
        ParameterSetSelectBit3 = 19,
        JobSelectBit0 = 20,
        JobSelectBit1 = 21,
        JobSelectBit2 = 22,
        JobSelectBit3 = 23,

        LineControlStart = 28,
        LineControlAlert1 = 29,
        LineControlAlert2 = 30,
        AckErrorMessage = 31,
        FieldbusDigIn1 = 32,
        FieldbusDigIn2 = 33,
        FieldbusDigIn3 = 34,
        FieldbusDigIn4 = 35,
        FlashToolGreenLight = 36,

        ManualMode = 43,
        ParameterSetSelectBit4 = 45,
        ParameterSetSelectBit5 = 46,
        ParameterSetSelectBit6 = 47,
        ParameterSetSelectBit7 = 48,
        JobSelectBit4 = 49,
        JobSelectBit5 = 50,
        JobSelectBit6 = 51,
        JobSelectBit7 = 52,
        BatchDecrement = 53,
        JobRestart = 54,
        EndOfCycle = 55,

        ClickWrench1 = 62,
        ClickWrench2 = 63,
        ClickWrench3 = 64,
        ClickWrench4 = 65,
        IdCard = 66,
        AutomaticMode = 67,
        ExternalMonitored1 = 68,
        ExternalMonitored2 = 69,
        ExternalMonitored3 = 70,
        ExternalMonitored4 = 71,
        ExternalMonitored5 = 72,
        ExternalMonitored6 = 73,
        ExternalMonitored7 = 74,
        ExternalMonitored8 = 75,
        SelectNextParameterSet = 76,
        SelectPreviousParameterSet = 77,

        TimerEnableTool = 79,
        MasterUnlockTool = 80,
        STScanRequest = 81,
        DisconnectTool = 82,
        JobSelectBit8 = 83,
        ParameterSetSelectBit8 = 84,
        RequestSTScan = 85,
        ResetNokCounter = 86,
        BypassIdentifier = 87,
        ResetLatestIdentifier = 88,
        ResetAllIdentifier = 89,
        SetHomePosition = 90,
        DigoutMonitored1 = 91,
        DigoutMonitored2 = 92,
        DigoutMonitored3 = 93,
        DigoutMonitored4 = 94,
        DisableSTScanner = 95,
        DisableFieldbusCarriedSignals = 96,
        ToggleCwCcw = 97,
        ToggleCwCcwForNextRun = 98,
        SetCcw = 99,

        OpenProtocolCommandsDisable = 104,
        LogicDigIn1 = 105,
        LogicDigIn2 = 106,
        LogicDigIn3 = 107,
        LogicDigIn4 = 108,
        LogicDigIn5 = 109,
        LogicDigIn6 = 110,
        LogicDigIn7 = 111,
        LogicDigIn8 = 112,
        LogicDigIn9 = 113,
        LogicDigIn10 = 114,

        ForcedCcwOnce = 120,
        ForcedCcwToggle = 121,
        ForcedCwOnce = 122,
        ForcedCwToggle = 123,

        ParameterSetSelectBit9 = 129,
        StoreCurrentTighteningProgramInTheTool = 130,
        ActiveXmlResultSend = 131,
        ToolInWorkSpace = 132,
        ToolInProductSpace = 133,
        FlashToolYellowLight = 134,
        XmlEmergencyMode = 135,
        MFUTest = 136,
        ToolInParkPosition = 137,
        EnableOperation = 138,
        StopTightening = 139,
        StartLooseningPulse = 140,

        PulsorToolEnable = 150,
        PerformAirHoseTest = 151,
        LastDigIn = 152,

        ToolBlueLightIOControlled = 201,
        ToolBlueLight = 202,
        ToolGreenLightIOControlled = 203,
        ToolGreenLight = 204,
        ToolRedLightIOControlled = 205,
        ToolRedLight = 206,
        ToolYellowLightIOControlled = 207,
        ToolYellowLight = 208,
        ToolWhiteLightIOControlled = 209,
        ToolWhiteLight = 210
    }
}
