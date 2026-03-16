using MottSchottkyAnalizer.Math;
using System.Text;

namespace ElinsData.Data;

public static class ElinsRecordExtensions
{
    public static double FindNearestFrequency(this ElinsRecord data, double frequency)
    {
        foreach (double currentFrequency in data.ImpedancePoints.Select(p => p.Frequency).Distinct())
        {
            if (currentFrequency > frequency)
                return currentFrequency;
        }

        return frequency;
    }

    public static bool TryFillPotential(this ElinsRecord data, double start, double end, double step)
    {
        return false;
    }

    public static ElinsRecord FillCapacitance(this ElinsRecord data)
    {
        foreach (ImpedancePoint point in data.ImpedancePoints)
            point.Capacitance = SchottkyMath.Capacitance(point.Frequency, point.ImpedanceImaginary);

        return data;
    }

    public static string ToCsv(this ElinsRecord data, int columnWidth = 30)
    {
        StringBuilder csvBuilder = new StringBuilder();

        csvBuilder.Append("Потенциал, В".PadRight(columnWidth))
            .Append("Частота, Гц".PadRight(columnWidth))
            .Append("Re, Ом".PadRight(columnWidth))
            .Append("Im, Ом".PadRight(columnWidth))
            .Append("C, Ф/м2".PadRight(columnWidth))
            .AppendLine();

        foreach (ImpedancePoint point in data.ImpedancePoints)
        {
            csvBuilder.Append(Normalize(point.PotentialStep.Potential).PadRight(columnWidth))
                .Append(Normalize(point.Frequency).PadRight(columnWidth))
                .Append(Normalize(point.ImpedanceReal).PadRight(columnWidth))
                .Append(Normalize(point.ImpedanceImaginary).PadRight(columnWidth))
                .AppendLine(Normalize(point.Capacitance).PadRight(columnWidth));
        }

        return csvBuilder.ToString();
    }

    internal static void AppendStep(this ElinsRecord data)
    {
        data.Steps++;
        data.StepPotentials.Add(new StepPotential
        {
            Step = data.Steps
        });
    }

    private static string Normalize(double value)
    {
        return value.ToString("F12").Replace('.', ',');
    }
}
