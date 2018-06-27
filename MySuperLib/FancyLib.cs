using System;

namespace MySuperLib
{
    public class FancyLib
    {
        public string SayHello(string name)
        {
            if(name=="random") name=generateRandomName();
            return $"Hello {name}";
        }

        public string generateRandomName()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxy";
            var stringChars = new char[8];
            var random = new Random();
            stringChars[0] = chars[random.Next(chars.Length/2)];
            for (int i = 1; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[(chars.Length+1)/2+random.Next(chars.Length/2)];
            }

            var finalString = new String(stringChars);
            return finalString;
            // return Guid.NewGuid().ToString("n").Substring(0, 8);
        }
    }
}
