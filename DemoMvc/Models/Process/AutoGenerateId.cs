namespace DemoMvc.Models.Process
{
    public class AutoGenerateId
    {
        public string GenerateId(string inputID, int prefixLength)
        {
            string strOutput = "";
            //lay phan text cua inputID
            string prefix = inputID.Substring(0, prefixLength);
            //lay phan so cua inputID
            string numberPart = inputID.Substring(prefixLength);
            //chuyen so thanh so nguyen
            int number = int.Parse(numberPart);
            //tang so len 1 don vi
            number++;
            //chuyen so ve chuoi
            strOutput = prefix + number.ToString();
            return strOutput;
        }
        public string GenerateId(string inputID)
        {
            //STD008
            var match = System.Text.RegularExpressions.Regex.Match(inputID, @"^(?<prefix>[A-Za-z]+)(?<number>\d+)$");
            if (!match.Success)
            {
                // Nếu inputID không đúng định dạng, sinh mã mới với prefix và số 001
                string prefix = new string(inputID.Where(char.IsLetter).ToArray());
                if (string.IsNullOrEmpty(prefix)) prefix = "PS";
                return prefix + "001";
            }
            string prefixMatch = match.Groups["prefix"].Value;
            //STD
            string numberPart = match.Groups["number"].Value;
            //008
            int number = int.Parse(numberPart) + 1;
            //9
            string newNumberPart = number.ToString().PadLeft(numberPart.Length, '0');
            //STD009
            return prefixMatch + newNumberPart;
        }
    }
}