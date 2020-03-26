using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace coreUtils
{
    /// <summary>
    /// Excel帮助类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strFileName">保存位置</param>
        /// <param name="dateFormat">时间格式（默认：yyyy-MM-dd）</param>
        public static void DataTableToExcel(DataTable dtSource, string strFileName, string dateFormat = "yyyy-MM-dd")
        {
            using (MemoryStream ms = DataTableToExcel(dtSource, dateFormat))
            {
                string strPath = strFileName.Substring(0, strFileName.LastIndexOf("\\"));
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }



        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="dateFormat"></param>
        /// <returns></returns>
        private static MemoryStream DataTableToExcel(DataTable dtSource, string dateFormat)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat(dateFormat);

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式

                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();
                    }

                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        headStyle.Alignment = HorizontalAlignment.Center;//水平居中
                        headStyle.VerticalAlignment = VerticalAlignment.Center;//垂直居中
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                    }
                    #endregion

                    rowIndex = 1;
                }
                #endregion


                #region 填充内容
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);
                    newCell.CellStyle.Alignment = HorizontalAlignment.Center;
                    newCell.CellStyle.VerticalAlignment = VerticalAlignment.Center;//垂直居中
                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            System.DateTime dateV;
                            System.DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                //sheet.Dispose();
                return ms;
            }
        }

        /// <summary>
        /// 读取excel 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable ImportFor2003(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = (HSSFSheet)hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            HSSFRow headerRow = (HSSFRow)sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                HSSFCell cell = (HSSFCell)headerRow.GetCell(j);
                if (cell != null)//过滤表头空白单元格20180817
                {
                    dt.Columns.Add(cell.ToString());
                }
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                HSSFRow row = (HSSFRow)sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();
                if (row != null)
                {
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }

                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }

        #region 导出多表头
        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strFileName">保存位置</param>
        /// <param name="html">表头html结构</param>
        /// <param name="dateFormat">时间格式（默认：yyyy-MM-dd）</param>
        public static void DataTableToComplexExcel(DataTable dtSource, string strFileName, string html, string dateFormat = "yyyy-MM-dd")
        {
            using (MemoryStream ms = DataTableToComplexExcel(dtSource, html, dateFormat))
            {
                string strPath = strFileName.Substring(0, strFileName.LastIndexOf("\\"));
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="html">表头html结构</param>
        /// <param name="dateFormat">时间格式（默认：yyyy-MM-dd）</param>
        private static MemoryStream DataTableToComplexExcel(DataTable dtSource, string html, string dateFormat)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat(dateFormat);

            //设置单元格样式及字体
            HSSFCellStyle cellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            HSSFFont font = (HSSFFont)workbook.CreateFont();
            font.FontHeightInPoints = 10;//字体大小
            font.Boldweight = 700;//字体粗细
            cellStyle.SetFont(font);
            cellStyle.Alignment = HorizontalAlignment.Center;//水平居中
            cellStyle.VerticalAlignment = VerticalAlignment.Center;//垂直居中

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式

                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();
                    }

                    #region 列头及样式
                    {
                        //获取页面html并对tr进行筛选
                        MatchCollection rowCollection = Regex.Matches(html, @"<tr[^>]*>[\s\S]*?<\/tr>", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                        //写在tr循环中 
                        for (int i = 0; i < rowCollection.Count; i++)
                        {
                            HSSFRow rowTitle = (HSSFRow)sheet.CreateRow(i);
                            string rowContent = rowCollection[i].Value;

                            //对td进行筛选
                            MatchCollection columnCollection = Regex.Matches(rowContent, @"<td[^>]*>[\s\S]*?<\/td>", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                            //遍历td
                            for (int j = 0; j < columnCollection.Count; j++)
                            {
                                var match = Regex.Match(columnCollection[j].Value, "<td.*?rowspan=\"(?<row>.*?)\".*?colspan=\"(?<col>.*?)\".*?row=\"(?<row1>.*?)\".*?col=\"(?<col1>.*?)\".*?class=\"(?<class>.*?)\">(?<value>.*?)<\\/td>", RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                                if (match.Success)
                                {
                                    int rowspan = Convert.ToInt32(match.Groups["row"].Value);//表格跨行
                                    int colspan = Convert.ToInt32(match.Groups["col"].Value);//表格跨列
                                    int rowNo = Convert.ToInt32(match.Groups["row1"].Value);//所在行
                                    int colNo = Convert.ToInt32(match.Groups["col1"].Value);//所在列
                                    string value = match.Groups["value"].Value;

                                    if (colspan == 1)//判断是否跨列
                                    {
                                        var cell = rowTitle.CreateCell(colNo);//创建列
                                        cell.SetCellValue(value);//设置列的值
                                        cell.CellStyle = cellStyle;
                                        if (value.Length > 0)
                                        {
                                            int width = value.Length * 25 / 6;
                                            if (width > 255)
                                                width = 250;
                                            sheet.SetColumnWidth(colNo, width * 256);
                                        }
                                    }
                                    //判断是否跨行、跨列
                                    if (rowspan > 1 || colspan > 1)
                                    {
                                        int firstRow = 0, lastRow = 0, firstCol = 0, lastCol = 0;
                                        if (rowspan > 1)//跨行
                                        {
                                            firstRow = rowNo;
                                            lastRow = firstRow + rowspan - 1;
                                        }
                                        else
                                        {
                                            firstRow = lastRow = i;
                                        }
                                        if (colspan > 1)//跨列
                                        {
                                            firstCol = colNo;
                                            int cols = colNo + colspan;
                                            for (; colNo < cols; colNo++)
                                            {
                                                var cell = rowTitle.CreateCell(colNo);
                                                cell.SetCellValue(value);
                                                cell.CellStyle = cellStyle;
                                            }
                                            lastCol = colNo - 1;
                                        }
                                        else
                                        {
                                            firstCol = lastCol = colNo;
                                        }

                                        //设置起始行数，结束行数，起始列数，结束列数
                                        sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(firstRow, lastRow, firstCol, lastCol));
                                    }
                                }
                            }
                        }
                        rowIndex = rowCollection.Count;
                    }
                    #endregion
                }
                #endregion

                #region 填充内容
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    HSSFCell newCell = (HSSFCell)dataRow.CreateCell(column.Ordinal);
                    newCell.CellStyle.Alignment = HorizontalAlignment.Center;//水平居中;
                    newCell.CellStyle.VerticalAlignment = VerticalAlignment.Center;//垂直居中

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型
                            System.DateTime dateV;
                            System.DateTime.TryParse(drValue, out dateV);
                            newCell.SetCellValue(dateV);

                            newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                //sheet.Dispose();
                return ms;
            }
        }
        #endregion

        /// <summary>
        /// 从Excel中将数据导出到DataTable
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static DataTable ImportExceltoDt(string filePath)
        {
            DataTable dt = new DataTable();
            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {

                hssfworkbook = new HSSFWorkbook(file);
            }
            HSSFSheet sheet = hssfworkbook.GetSheetAt(0) as HSSFSheet;
            dt = ImportDt(sheet, 0, true);
            return dt;
        }

        /// <summary>
        /// 从Excel中将数据导出到DataTable
        /// </summary>
        /// <param name="sheet">列</param>
        /// <param name="HeaderRowIndex">起始行</param>
        /// <param name="needHeader">是否需要标头</param>
        /// <returns></returns>
        static DataTable ImportDt(HSSFSheet sheet, int HeaderRowIndex, bool needHeader)
        {
            DataTable table = new DataTable();
            HSSFRow headerRow;
            int cellCount;
            string exceptionMessage = string.Empty;
            try
            {
                if (HeaderRowIndex < 0 || !needHeader)
                {
                    headerRow = sheet.GetRow(0) as HSSFRow;
                    cellCount = headerRow.LastCellNum;
                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        DataColumn column = new DataColumn(Convert.ToString(i));
                        table.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = sheet.GetRow(HeaderRowIndex) as HSSFRow;
                    cellCount = headerRow.LastCellNum;
                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        if (headerRow.GetCell(i) == null)
                        {
                            if (table.Columns.IndexOf(Convert.ToString(i)) > 0)
                            {
                                DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                                table.Columns.Add(column);
                            }
                            else
                            {
                                DataColumn column = new DataColumn(Convert.ToString(i)); table.Columns.Add(column);
                            }
                        }
                        else if (table.Columns.IndexOf(headerRow.GetCell(i).ToString()) > 0)
                        {
                            DataColumn column = new DataColumn(Convert.ToString("重复列名" + i));
                            table.Columns.Add(column);
                        }
                        else
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                            table.Columns.Add(column);
                        }
                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = (HeaderRowIndex + 1); i <= sheet.LastRowNum; i++)
                {
                    try
                    {
                        HSSFRow row;
                        if (sheet.GetRow(i) == null)
                        {
                            row = sheet.CreateRow(i) as HSSFRow;
                        }
                        else
                        {
                            row = sheet.GetRow(i) as HSSFRow;
                        }
                        DataRow dataRow = table.NewRow();
                        for (int j = row.FirstCellNum; j <= cellCount; j++)
                        {
                            try
                            {
                                if (row.GetCell(j) != null)
                                {
                                    switch (row.GetCell(j).CellType)
                                    {
                                        case CellType.String:
                                            string str = row.GetCell(j).StringCellValue;
                                            if (str != null && str.Length > 0)
                                            {
                                                dataRow[j] = str.ToString();
                                            }
                                            else
                                            {
                                                dataRow[j] = null;
                                            }
                                            break;
                                        case CellType.Numeric:
                                            if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                            {
                                                dataRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                            }
                                            else
                                            {
                                                dataRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                            }
                                            break;
                                        case CellType.Boolean:
                                            dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                            break;
                                        case CellType.Error:
                                            dataRow[j] = "ERROR";
                                            break;
                                        case CellType.Formula:
                                            switch (row.GetCell(j).CachedFormulaResultType)
                                            {
                                                case CellType.String:
                                                    string strFORMULA = row.GetCell(j).StringCellValue;
                                                    if (strFORMULA != null && strFORMULA.Length > 0)
                                                    {
                                                        dataRow[j] = strFORMULA.ToString();
                                                    }
                                                    else
                                                    {
                                                        dataRow[j] = null;
                                                    }
                                                    break;
                                                case CellType.Numeric:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);
                                                    break;
                                                case CellType.Boolean:
                                                    dataRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                                    break;
                                                case CellType.Error:
                                                    dataRow[j] = "ERROR";
                                                    break;
                                                default:
                                                    dataRow[j] = ""; break;
                                            }
                                            break;
                                        default: dataRow[j] = ""; break;
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                exceptionMessage = exception.Message + exception.StackTrace;
                                //wl.WriteLogs(exception.ToString());                    
                            }
                        }
                        table.Rows.Add(dataRow);
                    }
                    catch (Exception exception)
                    {
                        exceptionMessage = exception.Message + exception.StackTrace;
                        //wl.WriteLogs(exception.ToString());            
                    }
                }
            }
            catch (Exception exception)
            {
                exceptionMessage = exception.Message + exception.StackTrace;
                //wl.WriteLogs(exception.ToString());      
            }
            return table;
        }

        /// <summary>
        /// 将Excel文件中的数据读出到DataTable中
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable ImportFor2007(string filePath)
        {
            DataTable dt = new DataTable();
            IWorkbook workbook = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //office2003 HSSFWorkbook
                workbook = new XSSFWorkbook(fs);
            }
            ISheet sheet = workbook.GetSheetAt(0);
            dt = ExportToDataTable(sheet, 0, true);
            return dt;

        }

        /// <summary>
        /// 将指定sheet中的数据导入到datatable中
        /// </summary>
        /// <param name="sheet">指定需要导出的sheet</param>
        /// <param name="HeaderRowIndex">列头所在的行号，-1没有列头</param>
        /// <param name="needHeader"></param>
        /// <returns></returns>
        private static DataTable ExportToDataTable(ISheet sheet, int HeaderRowIndex, bool needHeader)
        {
            DataTable dt = new DataTable();
            XSSFRow headerRow = null;
            int cellCount;
            try
            {
                if (HeaderRowIndex < 0 || !needHeader)
                {
                    headerRow = sheet.GetRow(0) as XSSFRow;
                    cellCount = headerRow.LastCellNum;
                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        DataColumn column = new DataColumn(Convert.ToString(i));
                        dt.Columns.Add(column);
                    }
                }
                else
                {
                    headerRow = sheet.GetRow(HeaderRowIndex) as XSSFRow;
                    cellCount = headerRow.LastCellNum;
                    for (int i = headerRow.FirstCellNum; i <= cellCount; i++)
                    {
                        ICell cell = headerRow.GetCell(i);
                        if (cell == null)
                        {
                            break;//到最后 跳出循环
                        }
                        else
                        {
                            DataColumn column = new DataColumn(headerRow.GetCell(i).ToString());
                            dt.Columns.Add(column);
                        }

                    }
                }
                int rowCount = sheet.LastRowNum;
                for (int i = HeaderRowIndex + 1; i <= sheet.LastRowNum; i++)
                {
                    XSSFRow row = null;
                    if (sheet.GetRow(i) == null)
                    {
                        row = sheet.CreateRow(i) as XSSFRow;
                    }
                    else
                    {
                        row = sheet.GetRow(i) as XSSFRow;
                    }
                    DataRow dtRow = dt.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            switch (row.GetCell(j).CellType)
                            {
                                case CellType.Boolean:
                                    dtRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);
                                    break;
                                case CellType.Error:
                                    dtRow[j] = "ERROR";
                                    break;
                                case CellType.Formula:
                                    switch (row.GetCell(j).CachedFormulaResultType)
                                    {

                                        case CellType.Boolean:
                                            dtRow[j] = Convert.ToString(row.GetCell(j).BooleanCellValue);

                                            break;
                                        case CellType.Error:
                                            dtRow[j] = "ERROR";
                                            break;
                                        case CellType.Numeric:
                                            dtRow[j] = Convert.ToString(row.GetCell(j).NumericCellValue);

                                            break;
                                        case CellType.String:
                                            string strFORMULA = row.GetCell(j).StringCellValue;
                                            if (strFORMULA != null && strFORMULA.Length > 0)
                                            {
                                                dtRow[j] = strFORMULA.ToString();
                                            }
                                            else
                                            {
                                                dtRow[j] = null;
                                            }
                                            break;
                                        default:
                                            dtRow[j] = "";
                                            break;
                                    }
                                    break;
                                case CellType.Numeric:
                                    if (DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                    {
                                        dtRow[j] = DateTime.FromOADate(row.GetCell(j).NumericCellValue);
                                    }
                                    else
                                    {
                                        dtRow[j] = Convert.ToDouble(row.GetCell(j).NumericCellValue);
                                    }
                                    break;
                                case CellType.String:
                                    string str = row.GetCell(j).StringCellValue;
                                    if (!string.IsNullOrEmpty(str))
                                    {

                                        dtRow[j] = Convert.ToString(str);


                                    }
                                    else
                                    {
                                        dtRow[j] = null;
                                    }
                                    break;
                                default:
                                    dtRow[j] = "";
                                    break;
                            }

                        }
                    }
                    dt.Rows.Add(dtRow);
                }

            }
            catch (Exception)
            {

                return null;
            }
            return dt;
        }

        /// <summary>
        /// 将DataTable中的数据导入Excel文件中
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file"></param>
        /// <param name="sheetName"></param>
        public static void DataTable2Excel(DataTable dt, string file, string sheetName)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(sheetName);
            IRow header = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = header.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }
            //数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            byte[] buffer = stream.ToArray();
            using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
            {
                fs.Write(buffer, 0, buffer.Length);
                fs.Flush();
            }
        }

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private static object GetValueType(XSSFCell cell)
        {
            if (cell == null)
            {
                return null;
            }
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return null;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;

                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Formula:
                default:
                    return "=" + cell.StringCellValue;
            }
        }

    }
}

