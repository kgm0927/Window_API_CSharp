#include <stdio.h>
int main()
{
	char colors[6][10] = { "red", "purple", "blue", "green", "yellow", "orange" };
	for (int i = 0; i < 6; i++)
	{
		printf(" colors[%d] : %s \n", i, colors[i]);
	}

	for (int i = 0; i < 6; i++)
	{
		printf(" colors[%d] : %s \n", i, *(colors+i));
	}
	for (int i = 0; i < 6; i++)
	{
		printf(" colors[%d] : %c \n", i, **(colors + i));
	}

	for (int i = 0; i < 6; i++)
	{
		printf(" colors[%d] : %c \n", i, *(*(colors + i)+0) );
	}

//-----------실습결과 ---------------------------------------------------------
	for (int i = 0; i < 6; i++)
	{
		for (int j = 0; j < 10; j++)
			printf("%c", *(*(colors + i) + j));  //printf("%c", colors[i][j]);
		printf("%\n");
	}

	for (int i = 0; i < 6; i++)
	{
		printf(" colors[%d] : ", i);
		for (int j = 0; j < 10; j++)
		{
			printf("%c", *(*(colors + i) + j));
			if (*(*(colors + i) + j) == '\0')
			{
				printf("%\n");
				break;
			}
		}
	}
}

// 주석처리 ctrl+k+c 
// 주석해제 ctrl+k+u

//int main()
//{
//	int a[6] = {1,2,3,4,5,6};
//
//	for (int i = 0; i < 6; i++)
//	{
//		printf("a[%d] : %d \n", i, a[i]);
//	}
//
//	for (int i = 0; i < 6; i++)
//	{
//		printf("a[%d] : %d \n", i, *(a+i));
//	}
//}