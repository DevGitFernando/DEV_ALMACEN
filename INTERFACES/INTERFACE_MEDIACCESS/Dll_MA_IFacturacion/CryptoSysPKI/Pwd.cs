namespace CryptoSysPKI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public class Pwd
    {
        private Pwd()
        {
        }

        public static string Prompt(int maxChars, string caption)
        {
            StringBuilder sbPassword = new StringBuilder(maxChars);
            if (PWD_Prompt(sbPassword, sbPassword.Capacity, caption) < 0)
            {
                return string.Empty;
            }
            return sbPassword.ToString();
        }

        public static string Prompt(int maxChars, string caption, string prompt)
        {
            StringBuilder sbPassword = new StringBuilder(maxChars);
            if (PWD_PromptEx(sbPassword, sbPassword.Capacity, caption, prompt, 0) < 0)
            {
                return string.Empty;
            }
            return sbPassword.ToString();
        }

        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PWD_Prompt(StringBuilder sbPassword, int nPwdLen, string strCaption);
        [DllImport("diCrPKI.dll", CharSet=CharSet.Ansi)]
        private static extern int PWD_PromptEx(StringBuilder sbPassword, int nPwdLen, string strCaption, string strPrompt, int flags);
    }
}

