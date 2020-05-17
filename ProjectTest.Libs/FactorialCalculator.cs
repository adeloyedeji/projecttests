namespace ProjectTest.Libs
{
    public class FactorialCalculator
    {
        public FactorialCalculator()
        {

        }

        public int GetFactorial(int num)
        {
            if (num == 0)
            {
                return 1;
            }
            return GetFactorial(num) * GetFactorial(num - 1);
        }
    }
}
