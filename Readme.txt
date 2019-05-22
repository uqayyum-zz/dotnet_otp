

PROJECT :  "OTP_Generator"


Language: C# (.NET Core 2.1)
Argument(s) : String key //secret key for which you wish to get the code for.

OUTPUT: return 6 digit code generated against the secret key provided in arguments.
The returned value is integer so you will have to convert into string and pad 0 to
the left if the integer starts with a 0.
Also logs the code at the end of the log file (in case of successful generation)
the logged code is in string so it has padded 0 to the left.

--
UQ