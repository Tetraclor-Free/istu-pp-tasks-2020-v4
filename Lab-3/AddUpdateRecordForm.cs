using Lab_1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_3
{
    public partial class AddUpdateRecordForm : Form
    {
        public AbstractTaskRecord TaskRecord;
        AbstractTaskRecord Record;
        Dictionary<string, AbstractTaskRecord> humanTypeToSample = new Dictionary<string, AbstractTaskRecord>();

        public AddUpdateRecordForm(AbstractTaskRecord record)
        {
            InitializeComponent();
            if (record != null)
                RecordTypesComboBox.Enabled = false;
            record = record ?? new TaskRecord() { dateEnd = DateTime.Now};
            Record = record;
            var samples = new List<AbstractTaskRecord>() 
                {  new TaskRecord(), new TaskWithCustomerRecord(), new TaskWithMoneyRecord()};
            var startItem = "";
            foreach (var sample in samples)
            {
                var str = sample.ToString();
                var type = str.Substring(0, str.IndexOf("ИД:"));
                humanTypeToSample[type] = sample;
                if(sample.GetType() == record.GetType())
                {
                    startItem = type;
                    humanTypeToSample[type] = record.Clone();
                }
            }
            RecordTypesComboBox.DataSource = humanTypeToSample.Keys.ToList();
            RecordTypesComboBox.SelectedIndexChanged += RecordTypesComboBox_SelectedIndexChanged;
            RecordTypesComboBox.SelectedItem = startItem;

            SaveButton.Click += SaveButton_Click;
            CancelButton.Click += CancelButton_Click;

            tableLayoutPanel1.Controls.Clear();
            GenerateBaseFileds(record);
            GenerateExtFields(record);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            var humanType = (string)RecordTypesComboBox.SelectedItem;
            dynamic newRecord = humanTypeToSample[humanType].Clone();// dynamic, тип определится во время работы программы

            if(RecordTypesComboBox.Enabled == false)
                newRecord.id = Record.id;
            
            newRecord.description = tableLayoutPanel1.GetControlFromPosition(1, 1).Text;
            newRecord.status = tableLayoutPanel1.GetControlFromPosition(1, 2).Text;
            newRecord.executor = tableLayoutPanel1.GetControlFromPosition(1, 3).Text;
            newRecord.dateEnd = DateTime.Parse(tableLayoutPanel1.GetControlFromPosition(1, 4).Text);

            if (newRecord is TaskWithCustomerRecord)
            {
                newRecord.customer = tableLayoutPanel1.GetControlFromPosition(1, 5).Text;
            }
            else if (newRecord is TaskWithMoneyRecord)
            {
                newRecord.money = uint.Parse(tableLayoutPanel1.GetControlFromPosition(1, 5).Text);
            }
            TaskRecord = newRecord;
        }

        private void RecordTypesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var humanType = (string)RecordTypesComboBox.SelectedItem;
            tableLayoutPanel1.Controls.Clear();
            GenerateBaseFileds(Record);
            GenerateExtFields(humanTypeToSample[humanType]);
        }

        void GenerateBaseFileds(AbstractTaskRecord record)
        {
            AddLabelInputPair("Id:", new Label() { Text = record.id.ToString()});
            AddLabelInputPair("Описание:", new TextBox() { Text = record.description, Width = 300 });

            var allow = BusinessLogic.AllowStatuses.ToList();
            if (allow.Remove(record.status))
                allow.Insert(0, record.status);

            var comboBox = new ComboBox() { DataSource = allow, SelectedItem = record.status };

            AddLabelInputPair("Статус:", comboBox);
            AddLabelInputPair("Исполнитель:", new TextBox() { Text = record.executor, Width = 300 });
            AddLabelInputPair("Дата Завершения:", new DateTimePicker() { Value = record.dateEnd });
        }

        void GenerateExtFields(AbstractTaskRecord employeeRecord)
        {
            if (employeeRecord is TaskWithCustomerRecord)
            {
                var customer = (employeeRecord as TaskWithCustomerRecord).customer;
                AddLabelInputPair("Заказчик: ",
                    new TextBox() { Text = customer });
            } 
            else if (employeeRecord is TaskWithMoneyRecord) 
            {
                AddLabelInputPair("Оплата: ",
                    new NumericUpDown() { Minimum = 0, Maximum = uint.MaxValue, Value = (employeeRecord as TaskWithMoneyRecord).money });
            }
        }

        void AddLabelInputPair(string labeltext, Control inputControl)
        {
            tableLayoutPanel1.Controls.Add(new Label() { Text = labeltext });
            tableLayoutPanel1.Controls.Add(inputControl);
        }
    }
}
