using Lab_2;
using Lab_1;
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
        BusinessLogicWithReport logic;

        public Form1()
        {
            InitializeComponent();

            //var dataSource = new FileDataSource(@"../../../data.bin",
            //    new Dictionary<byte, Func<AbstractTaskRecord>>()
            //    {
            //        [1] = (() => new TaskRecord()),
            //        [2] = (() => new TaskWithCustomerRecord()),
            //        [3] = (() => new TaskWithMoneyRecord())
            //    });
            var dataSource = new MemoryDataSource();
            logic = new Lab_2.BusinessLogicWithReport(dataSource);

            dataSource.Save(new TaskWithCustomerRecord("Это описание точно длиннее 20 символов", "Закрыта", "Me", DateTime.Now, "Customer"));
            dataSource.Save(new TaskRecord("Починить крышу на бане в дачнм поселке", "Новая", "Влад", DateTime.Now.AddDays(10)));
            dataSource.Save(new TaskWithMoneyRecord("Сделать выданный срочный заказ", "Новая", "Влад", DateTime.Now.AddDays(5), 2000000));

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
            var from_ = fromDateTimePicker.Value;
            var to = toDateTimePicker.Value;
            var report = logic.MakeReport(from_, to);
            File.WriteAllText(dialog.FileName, report);
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var record = (AbstractTaskRecord)ViewRecordsListBox.SelectedItem;
            if (record == null) return;
            logic.Delete(record.id);
            ViewRecords();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            var record = (AbstractTaskRecord)ViewRecordsListBox.SelectedItem;
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

        public void OpenAddUpdateRecordForm(AbstractTaskRecord record)
        {
            var form = new AddUpdateRecordForm(record);
            DialogResult result = DialogResult.Cancel;
            try
            {
                result = form.ShowDialog();
                if (result != DialogResult.OK) return;

                var newRecord = form.TaskRecord;
                logic.Save(newRecord);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }

            ViewRecords();
        }
    }
}
