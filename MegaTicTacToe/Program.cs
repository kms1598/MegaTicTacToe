using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaTicTacToe
{
    class smallBoard
    {
        char[,] small = new char[3, 3];
        public char win;
        public int numO = 0, numX = 0;
        int num = 0;

        public char[,] Small
        {
            set
            {
                small = value;
            }
            get
            {
                return small;
            }
        }

        public char Win
        {
            set
            {
                win = value;
            }
            get
            {
                return win;
            }
        }

        public int Num
        {
            set
            {
                num = value;
            }
            get
            {
                return num;
            }
        }

        public smallBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    small[i, j] = '-';
                }
            }

            win = '-';
        }

        public bool JudgeWin() //작은 보드의 승리 판단, bool로 승리 확정 여부 반환
        {
            for (int i = 0; i < 3; i++)
            {
                if (small[i, 1] != '-' && small[i, 0] == small[i, 1] && small[i, 1] == small[i, 2])
                {
                    win = small[i, 1];
                    break;
                }
                if (small[1, i] != '-' && small[0, i] == small[1, i] && small[1, i] == small[2, i])
                {
                    win = small[1, i];
                    break;
                }
            }

            if (small[1, 1] != '-' && small[0, 0] == small[1, 1] && small[1, 1] == small[2, 2])
            {
                win = small[1, 1];
            }

            if (small[1, 1] != '-' && small[0, 2] == small[1, 1] && small[1, 1] == small[2, 0])
            {
                win = small[1, 1];
            }

            if (win != '-')
            {
                isWin(win);
                return true;
            }

            if (Num == 9)
            {
                isWin((numO > numX) ? 'O' : 'X');
                return true;
            }

            return false;
        }

        void isWin(char win) //승리 확정
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    small[i, j] = win;
                }
            }
        }
    }

    class bigBoard
    {
        smallBoard[,] big = new smallBoard[3, 3];
        int targetRow, targetCol;
        int turn;
        int win;
        bool isFree = true;

        public int TargetRow
        {
            set
            {
                targetRow = value;
            }
            get
            {
                return targetRow;
            }
        }

        public int TargetCol
        {
            set
            {
                targetCol = value;
            }
            get
            {
                return targetCol;
            }
        }

        public int Turn
        {
            set
            {
                turn = value;
            }
            get
            {
                return turn;
            }
        }

        public int Win
        {
            get
            {
                return win;
            }
        }

        public bigBoard()
        {
            turn = 1;
            win = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    this.big[i, j] = new smallBoard();
                }
            }
        }

        public void PrintBoard() //보드 출력
        {
            char A = 'A';
            Console.Clear();
            Console.Write(" ");

            for (int i = 1; i < 10; i++)
            {
                Console.Write(" " + i);
                if (i % 3 == 0)
                    Console.Write(" ");
            }

            Console.Write("\r\n");

            for (int i = 0; i < 3; i++)
            {
                for (int n = 0; n < 3; n++)
                {
                    Console.Write((char)(A + 3 * i + n));
                    for (int j = 0; j < 3; j++)
                    {
                        for (int m = 0; m < 3; m++)
                        {
                            Console.Write(" " + big[i, j].Small[n, m]);
                        }
                        Console.Write(" ");
                    }
                    Console.Write("\r\n");
                }
                Console.Write("\r\n");
            }
        }

        public void GetInput() //입력 받기
        {
            int letter;
            int num;
            while (true)
            {
                if (turn % 2 == 1)
                    Console.Write("플레이어1 턴");
                else
                    Console.Write("플레이어2 턴");

                Console.WriteLine(" (" + turn + "번째 턴)");

                if (isFree)
                    Console.WriteLine("수를 둘 칸을 입력하세요");
                else
                    Console.WriteLine((char)(targetRow * 3 + 'A') + "~" + (char)(targetRow * 3 + 'C') + ", " + (char)(targetCol * 3 + '1') + "~" + (char)(targetCol * 3 + '3') + " (" + (targetRow * 3 + targetCol + 1) + " 번째 칸)에 맞춰 입력해주세요");
                string input = Console.ReadLine();

                if (input.Length != 2)
                {
                    Console.WriteLine("길이 오류\r\n");
                    continue;
                }

                letter = input[0] < 'a' ? input[0] - 'A' : input[0] - 'a';
                num = input[1] - '1';

                if (letter < 0 || letter > 8)
                {
                    Console.WriteLine("문자 오류\r\n");
                    continue;
                }
                if (num < 0 || num > 8)
                {
                    Console.WriteLine("숫자 오류\r\n");
                    continue;
                }

                if (!isFree)
                {
                    if (letter / 3 != targetRow || (input[1] - '1') / 3 != targetCol)
                    {
                        Console.WriteLine("범위 오류\r\n");
                        continue;
                    }
                }

                if(big[letter / 3, (input[1] - '1') / 3].Small[letter % 3, num % 3] != '-')
                {
                    Console.WriteLine("이미 선택된 칸입니다.\r\n");
                    continue;
                }

                isFree = false;

                targetRow = letter % 3; //다음 위치 결정
                targetCol = num % 3;

                if(turn % 2 == 1) //현재 턴 플레이어 판별
                {
                    big[letter / 3, num / 3].Small[letter % 3, num % 3] = 'O';
                    big[letter / 3, num / 3].numO++;
                }
                else
                {
                    big[letter / 3, num / 3].Small[letter % 3, num % 3] = 'X';
                    big[letter / 3, num / 3].numX++;
                }

                big[letter / 3, num / 3].Num++;
                big[letter / 3, num / 3].JudgeWin();

                if (big[targetRow, targetCol].Win != '-')
                    isFree = true; //놓은 위치가 승리 판정되면 다음 차례는 아무 칸이나 가능

                break;
            }
        }

        public char JudgeWin() //승리 판별
        {
            for (int i = 0; i < 3; i++)
            {
                if (big[i, 1].Win != '-' && big[i, 0].Win == big[i, 1].Win && big[i, 1].Win == big[i, 2].Win)
                {
                    win++;
                    return big[i, 1].Win;
                }
                if (big[1, i].Win != '-' && big[0, i].Win == big[1, i].Win && big[1, i].Win == big[2, i].Win)
                {
                    win++;
                    return big[1, i].Win;
                }
            }
            if (big[1, 1].Win != '-' && big[0, 0].Win == big[1, 1].Win && big[1, 1].Win == big[2, 2].Win)
            {
                win++;
                return big[1, 1].Win;
            }
            if (big[1, 1].Win != '-' && big[0, 2].Win == big[1, 1].Win && big[1, 1].Win == big[2, 0].Win)
            {
                win++;
                return big[1, 1].Win;
            }

            return '-';
        }
    }

    class main
    {
        static void Main(string[] args)
        {
            bigBoard b = new bigBoard();
            char final = '-';

            while (true)
            {
                b.PrintBoard();
                b.GetInput();

                final = b.JudgeWin();

                if(final != '-')
                {
                    b.PrintBoard();
                    Console.WriteLine(final + "님이 승리했습니다!");
                    break;
                }

                if(b.Win == 9)
                {
                    b.PrintBoard();
                    Console.WriteLine("비겼습니다!");
                    break;
                }

                b.Turn++;
            }

            return;
        }
    }
}