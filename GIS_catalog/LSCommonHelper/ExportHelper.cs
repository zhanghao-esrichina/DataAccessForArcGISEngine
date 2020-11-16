//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.IO;
//using System.Windows.Forms;
//using Microsoft.Office.Interop.Excel;
//namespace LSCommonHelper
//{
//    public class ExportHelper
//    {
//        // Methods
//        public static void ExportDataGridViewToExcel(string caption, string date, DataGridView dgv)
//        {
//            if (dgv.RowCount != 0)
//            {
//                int columnCount = dgv.ColumnCount;
//                try
//                {
//                    try
//                    {
//                        int num3;
//                        int num4;
//                        int num2 = 1;
//                        ApplicationClass class2 = new ApplicationClass();
//                        class2.Application.Workbooks.Add(true);
//                        class2.Caption = caption;
//                        class2.Cells[1, 1] = caption;
//                        class2.Cells[2, 1] = date;
//                        for (num3 = 0; num3 < dgv.ColumnCount; num3++)
//                        {
//                            class2.Cells[3, num2] = dgv.Columns[num3].HeaderText;
//                            class2.get_Range(class2.Cells[3, num2], class2.Cells[3, num2]).Cells.Borders.LineStyle = 1;
//                            class2.get_Range(class2.Cells[3, num2], class2.Cells[3, num2]).ColumnWidth = dgv.Columns[num3].Width / 8;
//                            num2++;
//                        }
//                        class2.get_Range(class2.Cells[1, 1], class2.Cells[1, columnCount]).MergeCells = true;
//                        class2.get_Range(class2.Cells[1, 1], class2.Cells[1, 1]).RowHeight = 30;
//                        class2.get_Range(class2.Cells[1, 1], class2.Cells[1, columnCount]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
//                        class2.get_Range(class2.Cells[2, 1], class2.Cells[2, 2]).MergeCells = true;
//                        class2.get_Range(class2.Cells[2, 1], class2.Cells[2, 2]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
//                        object[,] objArray = new object[dgv.RowCount, columnCount];
//                        for (num3 = 0; num3 < dgv.RowCount; num3++)
//                        {
//                            num2 = 1;
//                            num4 = 0;
//                            while (num4 < dgv.ColumnCount)
//                            {
//                                if (dgv.Rows[num3].Cells[num4].Value != null)
//                                {
//                                    objArray[num3, num2 - 1] = dgv.Rows[num3].Cells[num4].Value.ToString();
//                                }
//                                num2++;
//                                num4++;
//                            }
//                        }
//                        for (num3 = 0; num3 < dgv.RowCount; num3++)
//                        {
//                            int num5 = num3 + 4;
//                            for (num4 = 0; num4 < dgv.ColumnCount; num4++)
//                            {
//                                class2.get_Range(class2.Cells[num5, num4 + 1], class2.Cells[num5, num4 + 1]).NumberFormat = "@";
//                            }
//                        }
//                        class2.get_Range(class2.Cells[4, 1], class2.Cells[dgv.RowCount + 3, columnCount]).Value2 = objArray;
//                        class2.get_Range(class2.Cells[4, 1], class2.Cells[dgv.RowCount + 3, columnCount]).Cells.Borders.LineStyle = 1;
//                        class2.Visible = true;
//                    }
//                    catch
//                    {
//                        MessageBox.Show("信息导出失败，请确认你的机子上装有Microsoft Office Excel 2003！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
//                    }
//                }
//                finally
//                {
//                }
//            }
//        }

//        public static void ExportListViewToExcel(string caption, string date, ListView listview)
//        {
//            if (listview.Items.Count != 0)
//            {
//                int count = listview.Columns.Count;
//                try
//                {
//                    try
//                    {
//                        int num3;
//                        int num4;
//                        int num2 = 1;
//                        ApplicationClass class2 = new ApplicationClass();
//                        class2.Application.Workbooks.Add(true);
//                        class2.Caption = caption;
//                        class2.Cells[1, 1] = caption;
//                        class2.Cells[2, 1] = date;
//                        for (num3 = 0; num3 < listview.Columns.Count; num3++)
//                        {
//                            class2.Cells[3, num2] = listview.Columns[num3].Text;
//                            class2.get_Range(class2.Cells[3, num2], class2.Cells[3, num2]).Cells.Borders.LineStyle = 1;
//                            class2.get_Range(class2.Cells[3, num2], class2.Cells[3, num2]).ColumnWidth = listview.Columns[num3].Width / 8;
//                            num2++;
//                        }
//                        class2.get_Range(class2.Cells[1, 1], class2.Cells[1, count]).MergeCells = true;
//                        class2.get_Range(class2.Cells[1, 1], class2.Cells[1, 1]).RowHeight = 30;
//                        class2.get_Range(class2.Cells[1, 1], class2.Cells[1, count]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
//                        class2.get_Range(class2.Cells[2, 1], class2.Cells[2, 2]).MergeCells = true;
//                        class2.get_Range(class2.Cells[2, 1], class2.Cells[2, 2]).HorizontalAlignment = XlHAlign.xlHAlignLeft;
//                        object[,] objArray = new object[listview.Items.Count, count];
//                        for (num3 = 0; num3 < listview.Items.Count; num3++)
//                        {
//                            num2 = 1;
//                            num4 = 0;
//                            while (num4 < listview.Columns.Count)
//                            {
//                                if (listview.Items[num3].SubItems[num4].Text != null)
//                                {
//                                    objArray[num3, num2 - 1] = listview.Items[num3].SubItems[num4].Text;
//                                }
//                                num2++;
//                                num4++;
//                            }
//                        }
//                        for (num3 = 0; num3 < listview.Items.Count; num3++)
//                        {
//                            int num5 = num3 + 4;
//                            for (num4 = 0; num4 < listview.Columns.Count; num4++)
//                            {
//                                class2.get_Range(class2.Cells[num5, num4 + 1], class2.Cells[num5, num4 + 1]).NumberFormat = "@";
//                            }
//                        }
//                        class2.get_Range(class2.Cells[4, 1], class2.Cells[listview.Items.Count + 3, count]).Value2 = objArray;
//                        class2.get_Range(class2.Cells[4, 1], class2.Cells[listview.Items.Count + 3, count]).Cells.Borders.LineStyle = 1;
//                        class2.Visible = true;
//                    }
//                    catch
//                    {
//                        MessageBox.Show("信息导出失败，请确认你的机子上装有Microsoft Office Excel 2003！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
//                    }
//                }
//                finally
//                {
//                }
//            }
//        }

//        public static void ExportStringToTxt(string sStr, string sFilePath)
//        {
//            StreamWriter writer = new StreamWriter(sFilePath);
//            writer.Write(sStr);
//            writer.Flush();
//            writer.Close();
//        }
//    }


//}
