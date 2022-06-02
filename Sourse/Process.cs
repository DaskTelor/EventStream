using System;

namespace EventStream.Sourse
{
    internal class Process
    {
        private StatisticOfModelling rawStats;
        private ParametersOfModelling parameters;

        private EvenlyRandom random;
        public Process(ParametersOfModelling parameters)
        {
            random = new EvenlyRandom();
            this.parameters = parameters;
        }
        public StatisticOfModelling Start()
        {
            rawStats = new StatisticOfModelling(parameters);

            //время освобождения каждого из станков
            double[] timeOfRelease = new double[parameters.CountOfBenches];

            //цикл для каждой детали
            for (double currentTime = 0; currentTime <= parameters.TimeOfInputStream;)
            {
                //получаем время прибытия новой заготовки
                currentTime += random.GetRandomDouble(
                    parameters.TimeToArrive + parameters.TimeToArriveDeviation,
                    parameters.TimeToArrive - parameters.TimeToArriveDeviation);

                //итерация цикла для каждого станка
                for (int indexBench = 0; indexBench < parameters.CountOfBenches; indexBench++)
                {
                    double timeOfLastPartRelease;
                    double timeToTransfer;
                    double timeToWork;

                    //считаем время освобождения детали с предыдущего станка
                    timeOfLastPartRelease = indexBench == 0 ? currentTime : timeOfRelease[indexBench - 1];

                    rawStats.AwaitingDetails[indexBench].Add(timeOfLastPartRelease, true);

                    timeToTransfer = random.GetRandomDouble(
                        parameters.TransferTime[indexBench] + parameters.TransferTimeDeviation[indexBench],
                        parameters.TransferTime[indexBench] - parameters.TransferTimeDeviation[indexBench]);

                    //подсчитываем когда начнется обработка очередной детали текущим станком
                    //учитывая время на передачу от станка к станку или то что деталь была уже готова к обработке
                    timeOfRelease[indexBench] = Math.Max(timeOfLastPartRelease + timeToTransfer, timeOfRelease[indexBench]);

                    //считаем сколько времени деталь ожидала начала работы
                    rawStats.TotalTimeOfAwaiting[indexBench] = 
                        timeOfRelease[indexBench] > timeOfLastPartRelease + timeToTransfer 
                        ? timeOfRelease[indexBench] - timeOfLastPartRelease + timeToTransfer : 0;

                    //считаем за сколько обработается деталь текущим станком
                    timeToWork = random.GetRandomDouble(
                        parameters.TreatmentTime[indexBench] + parameters.TreatmentTimeDeviation[indexBench],
                        parameters.TreatmentTime[indexBench] - parameters.TreatmentTimeDeviation[indexBench]);

                    timeOfRelease[indexBench] += timeToWork;//устанавлиаем время установления станка

                    rawStats.AwaitingDetails[indexBench].Add(timeOfRelease[indexBench], false);

                    rawStats.TotalTimeOnBench[indexBench] += timeToWork;//добавляем в статистику время обработки детали
                }
                rawStats.CountOfDetails++;//подсчитываем количество выпущенных деталей 
            }
            rawStats.TotalWorkingTime = timeOfRelease[parameters.CountOfBenches - 1];
            return rawStats;
        }

    }
}
