using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace LSCommonHelper
{
    public class ControlStyleHelper
    {
        // Fields
        public static int currentCol = -1;
        public static bool sort;

        // Methods
        public static void InitDataGridView(DataGridView dataGridView1)
        {
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = SystemColors.GradientActiveCaption;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.GradientActiveCaption;
            dataGridView1.BackgroundColor = Color.White;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.SelectionBackColor = Color.FromArgb(0x80, 0x80, 0xff);
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle = style;
            dataGridView1.ClearSelection();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
        }

        public static void InitDataGridView(DataGridView dataGridView1, bool isReadOnly)
        {
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = SystemColors.GradientActiveCaption;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.GradientActiveCaption;
            dataGridView1.BackgroundColor = Color.White;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.SelectionBackColor = Color.FromArgb(0x80, 0x80, 0xff);
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle = style;
            dataGridView1.ClearSelection();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = isReadOnly;
        }

        public static void InitDataGridView(DataGridView dataGridView1, bool isReadOnly, bool isForbidSortColumn)
        {
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = SystemColors.GradientActiveCaption;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.GradientActiveCaption;
            dataGridView1.BackgroundColor = Color.White;
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.SelectionBackColor = Color.FromArgb(0x80, 0x80, 0xff);
            style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.DefaultCellStyle = style;
            dataGridView1.ClearSelection();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = isReadOnly;
            if (isForbidSortColumn)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        public static void InitFormShowDialogStyle(Form frm)
        {
            frm.ShowInTaskbar = false;
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.ShowIcon = false;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.FormBorderStyle = FormBorderStyle.FixedDialog;
            frm.ShowDialog();
        }

        public static void InitFormShowStyle(Form frm)
        {
            frm.ShowInTaskbar = true;
            frm.MaximizeBox = false;
            frm.MinimizeBox = false;
            frm.ShowIcon = false;
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.FormBorderStyle = FormBorderStyle.FixedDialog;
            frm.Show();
        }

        public static void InitListViewStyle(ListView listView1)
        {
            listView1.View = View.Details;
            listView1.ShowItemToolTips = true;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.CheckBoxes = true;
        }

        public static void InitListViewStyle(ListView listView1, bool isCheckBox)
        {
            listView1.View = View.Details;
            listView1.ShowItemToolTips = true;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.CheckBoxes = isCheckBox;
        }

        public static void SortListViewColumn(object sender, ColumnClickEventArgs e)
        {
            string str3;
            char ch = '▼';
            string str = ch.ToString().PadLeft(4, ' ');
            ch = '▲';
            string str2 = ch.ToString().PadLeft(4, ' ');
            ListView view = sender as ListView;
            if (!sort)
            {
                sort = true;
                str3 = view.Columns[e.Column].Text.TrimEnd(new char[] { '▼', '▲', ' ' });
                view.Columns[e.Column].Text = str3 + str2;
            }
            else if (sort)
            {
                sort = false;
                str3 = view.Columns[e.Column].Text.TrimEnd(new char[] { '▼', '▲', ' ' });
                view.Columns[e.Column].Text = str3 + str;
            }
            if (e.Column == (view.ListViewItemSorter as ListViewColumnSorter).SortColumn)
            {
                if ((view.ListViewItemSorter as ListViewColumnSorter).Order == SortOrder.Ascending)
                {
                    (view.ListViewItemSorter as ListViewColumnSorter).Order = SortOrder.Descending;
                }
                else
                {
                    (view.ListViewItemSorter as ListViewColumnSorter).Order = SortOrder.Ascending;
                }
            }
            else
            {
                (view.ListViewItemSorter as ListViewColumnSorter).SortColumn = e.Column;
                (view.ListViewItemSorter as ListViewColumnSorter).Order = SortOrder.Ascending;
            }
            ((ListView)sender).Sort();
        }

        // Nested Types
        public class ListViewColumnSorter : IComparer
        {
            // Fields
            private int ColumnToSort = 0;
            private CaseInsensitiveComparer ObjectCompare = new CaseInsensitiveComparer();
            private SortOrder OrderOfSort = SortOrder.None;

            // Methods
            public int Compare(object x, object y)
            {
                ListViewItem item = (ListViewItem)x;
                ListViewItem item2 = (ListViewItem)y;
                int num = this.ObjectCompare.Compare(ConvertHelper.ObjectToDouble(item.SubItems[this.ColumnToSort].Text), ConvertHelper.ObjectToDouble(item2.SubItems[this.ColumnToSort].Text));
                if (this.OrderOfSort == SortOrder.Ascending)
                {
                    return num;
                }
                if (this.OrderOfSort == SortOrder.Descending)
                {
                    return -num;
                }
                return 0;
            }

            // Properties
            public SortOrder Order
            {
                get
                {
                    return this.OrderOfSort;
                }
                set
                {
                    this.OrderOfSort = value;
                }
            }

            public int SortColumn
            {
                get
                {
                    return this.ColumnToSort;
                }
                set
                {
                    this.ColumnToSort = value;
                }
            }
        }
    }


}
