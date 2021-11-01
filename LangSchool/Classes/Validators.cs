using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Classes
{
    public class Validators
    {
        public static bool IsEmail(string input)
        {
            string pattern = @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";

            return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsPassCorrect(string input)
        {
            string pattern = @"(?=^.{6,}$)((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$)";
            return Regex.IsMatch(input, pattern);
        }
    }
}
