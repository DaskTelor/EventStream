using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventStream
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Sourse.Process processWithoutDeviations = new Sourse.Process(
                            new Sourse.ParametersOfModelling(3, 23, 0,
                            new[] { 24d, 29d, 12d }, new[] { 0d, 0d, 0d },
                            new[] { 0d, 5d, 6d }, new[] { 0d, 0d, 0d }, 200));




            Sourse.Process process = new Sourse.Process(
                            new Sourse.ParametersOfModelling(
                                3/*количество станков*/,
                                23/*время между поступлениями деталей*/,
                                3/*отклонение от времени поступления деталей*/,
                            new[] { 24d, 29d, 12d }/*время обработки детали для каждого станка*/,
                            new[] { 1d, 3d, 3d }/*отклонения времени обработки детали для каждого станка*/,
                            new[] { 0d, 5d, 6d }/*время транспортировки к каждому станку*/,
                            new[] { 0d, 2d, 1d }/*отклонения от времени транспортировки к каждому станку*/,
                            200/*время поступления деталей*/));
            

            Sourse.FinalStatisticOfModelling finalStats = new Sourse.FinalStatisticOfModelling(process.Start());
            finalStats.Init();
            textBoxOutput.Text += finalStats.GetStringStats();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxOutput.Clear();
        }
    }
}
