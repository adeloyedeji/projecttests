using ProjectTest.Libs.Contracts;

namespace ProjectTest.Libs.Services
{
    public class Calculator : ICalculator
    {
        public Calculator()
        {

        }

        public int Factorial(int num)
        {
            var factorial = 1;
            for(int i = 1; i <= num; i++)
            {
                factorial = factorial * i;
            }
            return factorial;
            //if (num == 0 || num == 1)
            //{
            //    return 1;
            //}
            //return num * Factorial(num - 1);
        }
    }
}
