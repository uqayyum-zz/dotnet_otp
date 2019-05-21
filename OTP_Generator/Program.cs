using OtpNet;
using System;

namespace OTP_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Kick Off!");
        }

        //Helper Methods
        static private int generateOTP(string key)
        {
            int code = -1;
            try
            {
                var totp = new Totp(Base32Encoding.ToBytes(key));
                while (totp.RemainingSeconds() < 29) { }
                code = Int32.Parse(totp.ComputeTotp(DateTime.UtcNow));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return code;
        }
    }
}
