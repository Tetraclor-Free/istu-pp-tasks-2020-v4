using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3
{
    public partial class Form1 : Form
    {
        BusinessLogic logic;

        public Form1()
        {
            InitializeComponent();

            logic = new BusinessLogic(new FileDataSource(@"..\..\..\bd.bin"));

            //logic.Save(new SampleEmployeeRecord("Дмитрий Г. Д.;Разработчик;Разработка;50000"));
            //logic.Save(new TempWorkerRecord("Алексей А. Ж.;Разработчик;Разработка;100000;20.12.2022"));
            //logic.Save(new TraineeRecord("Александр Ф. Ю.;Стажер;Бухгалтерия;5555;УДГУ"));
            AddButton.Click += AddButton_Click;
            ChangeButton.Click += ChangeButton_Click;
            DeleteButton.Click += DeleteButton_Click;
            CreateReportButton.Click += CreateReportButton_Click;

            ViewRecords();
        }

        private void CreateReportButton_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            var result = dialog.ShowDialog();
            if (result != DialogResult.OK) return;
            var min = (int)FromNumeric.Value;
            var max = (int)ToNumeric.Value;
            var report = logic.GetReport(min, max);
            File.WriteAllText(dialog.FileName, report);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var record = (EmployeeRecord)ViewRecordsListBox.SelectedItem;
            if (record == null) return;
            logic.Delete(record.id);
            ViewRecords();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            var record = (EmployeeRecord)ViewRecordsListBox.SelectedItem;
            OpenAddUpdateRecordForm(record);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            OpenAddUpdateRecordForm(null);
        }

        public void ViewRecords()
        {
            ViewRecordsListBox.DataSource = logic.GetAll();
        }

        public void OpenAddUpdateRecordForm(EmployeeRecord record)
        {
            var form = new AddUpdateRecordForm(record);
            DialogResult result = DialogResult.Cancel;
            try
            {
                result = form.ShowDialog();
                if (result != DialogResult.OK) return;

                var newRecord = form.EmployeeRecord;
                logic.Save(newRecord);
            }
            catch(ArgumentException e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            ViewRecords();
        }
    }
}
