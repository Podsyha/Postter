namespace Postter.Common.Helpers;

public class RandomUtils
{
    private static readonly string ALPHABET = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                              "abcdefghijklmnopqrstuvwxyz" + 
                                              "0123456789" +
                                              "!@#$%^&*()_+";

    public char[] generateChars(int length)
    {
        Random random = new Random();
        char[] result = new char[length];

        for (int i = 0; i < length; ++i)
        {
            int index = random.Next(ALPHABET.Length);

            result[i] = ALPHABET[index];
        }

        return result;
    }
}