namespace ElinsDataParser.Elins;

internal static class Tags
{
    /// <summary>Block Number — номер блока данных.</summary>
    public const string BlockNumber = "bn";

    /// <summary>Date — дата проведения эксперимента (дд.мм.гггг).</summary>
    public const string Date = "da";

    /// <summary>Time — время начала измерения (чч-мм-сс).</summary>
    public const string Time = "ti";

    /// <summary>User Sample — описание образца, введённое пользователем.</summary>
    public const string UserSample = "us";

    /// <summary>Instrument / Sample Info — информация об измерении или приборе (например, P-45X).</summary>
    public const string InstrumentInfo = "is";

    /// <summary>Cycle Number — номер цикла в эксперименте.</summary>
    public const string CycleNumber = "cn";

    /// <summary>Block Info — дополнительная информация о блоке (например, состав образца).</summary>
    public const string BlockInfo = "bi";

    /// <summary>Weight / Wait Time — масса образца или время ожидания (в зависимости от метода).</summary>
    public const string Weight = "wt";

    /// <summary>Cycle — номер цикла (дополнительный или вложенный).</summary>
    public const string Cycle = "cy";

    /// <summary>Cycle Repetition — количество повторений цикла.</summary>
    public const string CycleRepetition = "cr";

    /// <summary>Step — номер шага в эксперименте.</summary>
    public const string Step = "st";

    /// <summary>Drive Value / Deviation — управляющее значение или смещение.</summary>
    public const string DriveValue = "dv";

    /// <summary>Scan Count / Step Count — количество точек в шаге (или частот в импедансном измерении).</summary>
    public const string ScanCount = "sc";

    /// <summary>Step Status — состояние шага (например, "ожидание", "выполнение").</summary>
    public const string StepStatus = "ss";

    /// <summary>Step Message — текстовое сообщение или комментарий к шагу.</summary>
    public const string StepMessage = "sm";

    /// <summary>Waveform / Work Type — тип режима шага (импеданс, поляризация, импульс и т.д.).</summary>
    public const string WorkType = "ww";

    /// <summary>Start Potential (Eₛ) — начальный потенциал, В.</summary>
    public const string StartPotential = "se";

    /// <summary>Final Potential (E_f) — конечный потенциал, В.</summary>
    public const string FinalPotential = "fe";

    /// <summary>Peak Potential (E_p) — пиковое значение потенциала, В.</summary>
    public const string PeakPotential = "pe";

    /// <summary>Peak Current (I_p) — пиковый ток, А.</summary>
    public const string PeakCurrent = "pi";

    /// <summary>Peak Charge (Q_p) — заряд при пиковом токе, Кл.</summary>
    public const string PeakCharge = "pq";

    /// <summary>Current Step — текущий шаг (обычно 0, если неактивен).</summary>
    public const string CurrentStep = "cs";

    /// <summary>Jump Type / Point Count — количество точек в серии или тип перехода.</summary>
    public const string JumpType = "jt";

    /// <summary>Wave Step — шаг волны (например, частотный шаг для импеданса).</summary>
    public const string WaveStep = "ws";

    /// <summary>Point Count — количество точек данных (dp) в текущем блоке.</summary>
    public const string VoltammetryCount = "pc";

    /// <summary>Data Point — строка с измеренными данными (обычно: Время, Потенциал, Ток).</summary>
    public const string VoltammetryPoint = "dp";

    /// <summary>Point Count — количество точек данных (Pz) в текущем блоке.</summary>
    public const string ImpedanceCount = "pz";

    /// <summary>Data Point — строка с измеренными данными (обычно: частота, Re(Z), Im(Z)).</summary>
    public const string ImpedancePoint = "dz";
}
