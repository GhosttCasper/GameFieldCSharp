using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *В свободное от занятий по программированию время Вася любит играть в самостоятельно придуманную им игру «Жизнь».
 * Игра происходит на поле, состоящем из n × m одинаковых клеток.
 * Для удобства Вася нумерует все строки поля целыми числами от 1 до n в порядке сверху-вниз,
 * а также нумерует все столбцы поля целыми числами от 1 до m в порядке слева-направо.
 * Вася считает соседними те клетки поля, которые имеют общее ребро.
 * Процесс игры состоит из k итераций.
 * На каждой итерации игры каждая клетка поля находится в одном из трех состояний:
 * Неактивна. * На поле Вася помечает такую клетку цифрой 1 .
 * Стабильна. На поле Вася помечает такую клетку цифрой 2 .
 * Нестабильна. На поле Вася помечает такую клетку цифрой 3 .
 * Активными Вася называет те клетки поля, которые находятся в стабильном или нестабильном состояниях.
 * Вася начинает игру с поля, каждая клетка которого находится в некотором заранее выбранном им состоянии.
 * При переходе к следующей итерации игры Вася формирует новое поле,
 * состояние каждой клетки в котором назначается по следующим правилам:
 * Если у клетки на предыдущей итерации была более, чем одна соседняя клетка в стабильном состоянии,
 * то на следующей итерации клетка будет находиться в стабильном состоянии.
 * Если не выполняется первое правило и у клетки на предыдущей итерации был хотя бы один сосед в активном состоянии,
 * то на следующей итерации клетка будет находиться в нестабильном состоянии.
 * Если не выполняются предыдущие правила, то на следующей итерации клетка будет находиться в неактивном состоянии.
 * Вася хочет заранее спланировать для каждой клетки, сколько изменений состояния ему придется сделать за весь ход игры.
 * Так как Вася еще не успел разобраться во всех тонкостях программирования, он просит Вас помочь ему в этой задаче.
 *
 * В первой строке входных данных записано три целых числа — n , m , k ( 1 ≤ n , m , k ≤ 1 0 0 ).
 * В последующих n строках записано состояние каждой клетки исходно выбранного Васей поля.
 * В j -й ( 1 ≤ j ≤ n ) из этих строк записано m чисел — a j 1 , … , a j m ,
 * где a j l ∈ { 1 , 2 , 3 } — состояние клетки в строке с номером j и столбце с номером l исходно выбранного Васей поля.
 * Все числа в каждой строке разделены ровно одним пробелом.
 *
 * В i -й ( 1 ≤ i ≤ n ) строке выходных данных Вам необходимо вывести m целых чисел — b i 1 , … , b i m ,
 * где b i j — количество изменений состояния клетки в строке с номером i и столбце с номером j .
 */
namespace GameFieldCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = Console.ReadLine();
            var array = str.Split();
            int n = int.Parse(array[0]);
            int m = int.Parse(array[1]);
            int k = int.Parse(array[2]);

            byte[,] field = new byte[n, m];

            for (int j = 0; j < n; j++)
            {
                str = Console.ReadLine();
                array = str.Split();

                for (int l = 0; l < m; l++)
                {
                    byte state = byte.Parse(array[l]);
                    field[j, l] = state;
                }
            }

            int[,] stateChangesCount = new int[n, m];

            for (int i = 0; i < k; i++)
            {
                byte[,] nextFieldState = (byte[,])field.Clone();
                for (int j = 0; j < n; j++)
                {
                    for (int l = 0; l < m; l++)
                    {
                        //int incactiveStatesCount = 0; // 1
                        int stableStatesCount = 0; // 2
                        int unstableStatesCount = 0; // 3

                        if (j < n - 1)
                            SwitchState(field[j + 1, l], ref stableStatesCount, ref unstableStatesCount);

                        if (l < m - 1)
                            SwitchState(field[j, l + 1], ref stableStatesCount, ref unstableStatesCount);

                        if (j >= 1)
                            SwitchState(field[j - 1, l], ref stableStatesCount, ref unstableStatesCount);

                        if (l >= 1)
                            SwitchState(field[j, l - 1], ref stableStatesCount, ref unstableStatesCount);

                        if (stableStatesCount > 1)
                            nextFieldState[j, l] = 2;
                        else
                        {
                            if (stableStatesCount + unstableStatesCount >= 1)
                                nextFieldState[j, l] = 3;
                            else
                                nextFieldState[j, l] = 1;
                        }

                        if (nextFieldState[j, l] != field[j, l])
                            stateChangesCount[j, l] += 1;
                    }
                }
                field = (byte[,])nextFieldState.Clone();
            }

            StringBuilder output = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    output.Append(stateChangesCount[i, j] + " ");
                }
                output.Append("\n");
            }
            Console.WriteLine(output);
        }

        private static void SwitchState(byte state, ref int stableStatesCount, ref int unstableStatesCount)
        {
            switch (state)
            {
                case 2:
                    stableStatesCount += 1;
                    break;
                case 3:
                    unstableStatesCount += 1;
                    break;
            }
        }
    }
}
