using OtpNet;
using System;
using System.IO;
using System.Threading;

namespace OTP_Generator
{
    class Program
    {
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

        static int Main(string[] args)
        {
            int code = -1;
            if (!(File.Exists(AppDomain.CurrentDomain.BaseDirectory + Resources.logFileName)))
                System.IO.File.Create(AppDomain.CurrentDomain.BaseDirectory + Resources.logFileName).Close();

            if (args.Length < 1)
            {
                using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + Resources.logFileName, true))
                {
                    _readWriteLock.EnterWriteLock();
                    writer.WriteLine("Invalid Arguments");
                    foreach (string arg in args)
                    {
                        writer.WriteLine(arg);
                    }
                    _readWriteLock.ExitWriteLock();
                }
            }

            string key = args[0];

            try
            {
                code = generateOTP(key);
                using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + Resources.logFileName, true))
                {
                    _readWriteLock.EnterWriteLock();
                    writer.WriteLine(code.ToString().PadLeft(6, '0'));
                    _readWriteLock.ExitWriteLock();
                }
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "OTP_GeneratorLog.txt", true))
                {
                    _readWriteLock.EnterWriteLock();
                    writer.WriteLine("Major Log : ERROR while executiopn :  --   " + DateTime.Now.ToLongDateString() + "  --  " + ex.Message);
                    _readWriteLock.ExitWriteLock();
                }
            }
            Console.WriteLine(code);
            return code;
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
