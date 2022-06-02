using System;
using System.Collections.Generic;
using System.Text;

namespace EventStream.Sourse
{
    internal class FinalStatisticOfModelling//финальная статистика
    {
        private StatisticOfModelling stats;

        //общее количество станков
        public int CountOfBenches { get; private set; }

        //коэффициент использования станков
        public double[] CoefficientsOfBench { get; private set;  }

        //среднее время детали на станке или в очереди
        public double[] AverageTimeOnBench { get; private set; }

        //максимальное число одновременно ожидающих деталей на станке
        public int[] MaxAwaitingDetails { get; private set; }

        //среднее время ожидание деталей на станке
        public double[] AverageAwaitingDetails { get; private set; }

        //среднее время ожидание деталей на станке
        public double TimeOfInputStream { get => stats.TimeOfInputStream;}

        //общее количество деталей
        public int CountOfDetails { get; private set; }
        public SortedList<double, bool>[] AwaitingDetails { get => stats.AwaitingDetails; }

        //общее время моделирования
        public double SumTimeOfModelling { get; private set; }
        public FinalStatisticOfModelling(StatisticOfModelling stats)
        {
            CoefficientsOfBench = new double[stats.CountOfBenches];
            AverageTimeOnBench = new double[stats.CountOfBenches];
            MaxAwaitingDetails = new int[stats.CountOfBenches];
            AverageAwaitingDetails = new double[stats.CountOfBenches];
            this.stats = stats;
        }

        //расчитываем значения из данных
        public void Init()
        {
            CountOfDetails = stats.CountOfDetails;
            CountOfBenches = stats.CountOfBenches;
            SumTimeOfModelling = stats.TotalWorkingTime;

            for (int i = 0; i < CountOfBenches; i++)
                CoefficientsOfBench[i] = stats.TotalTimeOnBench[i] / stats.TotalWorkingTime;

            for (int i = 0; i < CountOfBenches; i++)
                AverageTimeOnBench[i] = stats.TotalTimeOnBench[i] / CountOfDetails;

            for (int i = 0; i < CountOfBenches; i++)
                MaxAwaitingDetails[i] = stats.MaxAwaitingDetails[i];

            for (int i = 0; i < CountOfBenches; i++)
                AverageAwaitingDetails[i] = stats.TotalTimeOfAwaiting[i] / CountOfDetails;


            for (int i = 0; i < CountOfBenches; i++)
            {
                int awaitingDetails;
                int maxAwaitingDetails;

                awaitingDetails = 0;
                maxAwaitingDetails = 0;

                foreach (bool item in AwaitingDetails[i].Values)
                    if (item)
                    {
                        awaitingDetails++;
                        if (maxAwaitingDetails < awaitingDetails)
                            maxAwaitingDetails++;                       
                    }
                    else awaitingDetails--;

                MaxAwaitingDetails[i] = maxAwaitingDetails;
            }

        }
        public string GetStringStats()
        {
            StringBuilder outputReport = new StringBuilder();
            outputReport.AppendLine("СТАТИСТИКА");
            outputReport.AppendLine("Количество станков: " + CountOfBenches);
            outputReport.AppendLine("Время, в течении которого идет поток деталей: " + TimeOfInputStream);

            outputReport.AppendLine("Коэффициенты использования станков: ");
            for (int i = 0; i < CoefficientsOfBench.Length; i++)
                outputReport.AppendLine($"\t[{i + 1}] {CoefficientsOfBench[i]}");

            outputReport.AppendLine("Максимальное число одновременно ожидающих деталей на станке: ");
            for (int i = 0; i < MaxAwaitingDetails.Length; i++)
                outputReport.AppendLine($"\t[{i + 1}] {MaxAwaitingDetails[i]}");

            outputReport.AppendLine("Среднее время ожидание деталей на станке: ");
            for (int i = 0; i < AverageAwaitingDetails.Length; i++)
                outputReport.AppendLine($"\t[{i + 1}] { AverageAwaitingDetails[i]}");

            outputReport.AppendLine("Общее количество деталей: " + CountOfDetails);
            outputReport.AppendLine($"Общее время моделирования: {SumTimeOfModelling}" + Environment.NewLine);
            return outputReport.ToString();
        }
    }

    internal class StatisticOfModelling//то что нужно заффиксировать
    {
        //детали, которые ожидают у станка до какого то определенного времени
        public SortedList<double, bool>[] AwaitingDetails { get; set; }

        //количество станков
        public int CountOfBenches { get; private set; }//++

        //общее время моделирования
        public double TotalWorkingTime { get; set; }//++

        //количество выпущенных деталей
        public int CountOfDetails { get; set; }//++

        //время работы каждого из станков
        public double[] TotalTimeOnBench { get; set; }//++

        //максимальное количество деталей ожидающих у станка
        public int[] MaxAwaitingDetails { get; private set; }//++

        //общее время ожидания деталей на станках
        public double[] TotalTimeOfAwaiting { get; set; }

        //время, в течении которого идет поток
        public double TimeOfInputStream { get; private set; }

        public StatisticOfModelling(ParametersOfModelling parameters)
        {
            TimeOfInputStream = parameters.TimeOfInputStream;
            CountOfBenches = parameters.CountOfBenches;
            TotalTimeOnBench = new double[parameters.CountOfBenches];
            MaxAwaitingDetails = new int[parameters.CountOfBenches];
            TotalTimeOfAwaiting = new double[parameters.CountOfBenches];

            AwaitingDetails = new SortedList<double, bool>[parameters.CountOfBenches];
            for (int i = 0; i < AwaitingDetails.Length; i++)
                AwaitingDetails[i] = new SortedList<double, bool>();
        }
    }
}
