using ClosedXML.Excel;

namespace BlazorJQueryDataTable.Components.Helpers
{
    public class ExcelService
    {
        public byte[] ExportToExcel<T>(List<T>? data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            // İlk satırı başlık olarak ekleyelim (varsayılan olarak tüm propertilerin adını alıyoruz)
            var properties = typeof(T).GetProperties();
            for (int i = 0; i < properties?.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
            }

            // Verileri tabloya ekleyelim
            for (int i = 0; i < data?.Count; i++)
            {
                var item = data[i];
                for (int j = 0; j < properties?.Length; j++)
                {
                    // Explicitly cast the value to object
                    object? value = properties?[j]?.GetValue(item);
                    worksheet.Cell(i + 2, j + 1).Value = (XLCellValue)value;
                }
            }

            // Excel dosyasını belleğe yazalım
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] ExportToExcel<T>(T[]? data)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            // İlk satırı başlık olarak ekleyelim
            var properties = typeof(T).GetProperties();
            for (int i = 0; i < properties?.Length; i++)
            {
                worksheet.Cell(1, i + 1).Value = properties[i].Name;
            }

            // Verileri tabloya ekleyelim
            for (int i = 0; i < data?.Length; i++)
            {
                var item = data[i];
                for (int j = 0; j < properties?.Length; j++)
                {
                    var propertyValue = properties[j].GetValue(item);
                    if (propertyValue != null)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = propertyValue.ToString();
                    }
                }
            }

            // Excel dosyasını belleğe yazalım
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
