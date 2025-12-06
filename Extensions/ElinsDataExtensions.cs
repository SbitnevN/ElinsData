using ElinsDataParser.Data;
using System.Text;

namespace ElinsDataParser.Extensions;

public static class ElinsDataExtensions
{
    public static double FindNearestFrequency(this ElinsData data, double frequency)
    {
        foreach (double currentFrequency in data.Frequencies)
        {
            if (currentFrequency > frequency)
                return currentFrequency;
        }

        return frequency;
    }

    public static void FillPotential(this ElinsData data, double start, double end, double step)
    {
        IEnumerator<Step> enumerator = data.Steps.GetEnumerator();
        for (double current = start; current < end; current += step)
        {
            if (!enumerator.MoveNext())
                return;

            enumerator.Current.FillPotential(current);
        }
    }

    public static string ToCsv(this ElinsData data)
    {
        const int colWidth = 30;
        StringBuilder csvBuilder = new StringBuilder();

        csvBuilder.AppendLine(
            "Потенциал, В".PadRight(colWidth) +
            "Частота, Гц".PadRight(colWidth) +
            "Re, Ом".PadRight(colWidth) +
            "Im, Ом".PadRight(colWidth) +
            "C, Ф/м2".PadRight(colWidth)
        );

        foreach (ImpedancePoint point in data.ImpedancePoints)
        {
            string potential = point.Potential.ToString("F12").Replace('.', ',');
            string frequency = point.Frequency.ToString("F12").Replace('.', ',');
            string re = point.ImpedanceReal.ToString("F12").Replace('.', ',');
            string im = point.ImpedanceImaginary.ToString("F12").Replace('.', ',');
            string c = point.Capacitance.ToString("F12").Replace('.', ',');

            csvBuilder.AppendLine(
                potential.PadRight(colWidth) +
                frequency.PadRight(colWidth) +
                re.PadRight(colWidth) +
                im.PadRight(colWidth) +
                c.PadRight(colWidth)
            );
        }

        return csvBuilder.ToString();
    }
}
