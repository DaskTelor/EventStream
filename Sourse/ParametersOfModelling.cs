namespace EventStream.Sourse
{
    internal class ParametersOfModelling
    {
        public int CountOfBenches { get; }//количество станков
        public double TimeToArrive { get; }//прибытие заготовки
        public double TimeToArriveDeviation { get; }
        public double[] TreatmentTime { get; }//обработка детали на разных станках
        public double[] TreatmentTimeDeviation { get; }
        public double[] TransferTime { get; }//передача деталей
        public double[] TransferTimeDeviation { get; }
        public double TimeOfInputStream { get; }
        public ParametersOfModelling(
            int CountOfBenches,
            double TimeToArrive,
            double TimeToArriveDeviation,
            double[] TreatmentTime,
            double[] TreatmentTimeDeviation,
            double[] TransferTime,
            double[] TransferTimeDeviation,
            double TimeOfInputStream)
        {
            this.CountOfBenches = CountOfBenches; 
            this.TimeToArrive = TimeToArrive; 
            this.TimeToArriveDeviation = TimeToArriveDeviation;
            this.TreatmentTime = TreatmentTime; 
            this.TreatmentTimeDeviation = TreatmentTimeDeviation;
            this.TransferTime = TransferTime; 
            this.TransferTimeDeviation = TransferTimeDeviation;
            this.TimeOfInputStream = TimeOfInputStream;
        }
    }
}
