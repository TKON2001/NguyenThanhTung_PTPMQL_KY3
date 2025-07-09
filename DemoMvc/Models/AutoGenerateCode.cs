namespace DemoMvc.Models
{
    public class AutoGenerateCode
    {
        // Phương thức này sẽ sinh mã EmployeeID mới
        public static string GenerateNewEmployeeId(string lastEmployeeId)
        {
            if (string.IsNullOrEmpty(lastEmployeeId))
            {
                // Nếu không có nhân viên nào, bắt đầu từ ES001
                return "ES001";
            }

            // Tách phần số ra khỏi chuỗi EmployeeID (ví dụ: "ES009" -> "009")
            string numericPartStr = lastEmployeeId.Substring(2); // Lấy chuỗi "009"

            // Chuyển phần số sang kiểu int, tăng lên 1
            if (int.TryParse(numericPartStr, out int numericPart))
            {
                numericPart++; // Tăng lên 1 (ví dụ: 9 -> 10)

                // Định dạng lại thành chuỗi 3 chữ số và ghép với tiền tố "ES"
                string newNumericPart = numericPart.ToString("D3");
                return "ES" + newNumericPart; // Trả về "ES010"
            }
            else
            {
                // Xử lý trường hợp lỗi nếu ID trong CSDL không đúng định dạng
                return "ES001"; // Trả về giá trị mặc định để tránh lỗi
            }
        }
    }
}