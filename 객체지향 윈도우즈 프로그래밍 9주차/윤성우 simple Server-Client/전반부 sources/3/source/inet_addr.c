/*
 * inet_addr.c
 * Written by SW. YOON
 */

#include <stdio.h>
#include <arpa/inet.h>

int main(int argc, char **argv)
{
	char * addr1 = "1.2.3.4";
	char * addr2 = "1.2.3.256";
	unsigned long conv_addr;
	
	conv_addr = inet_addr(addr1);
	if(conv_addr == INADDR_NONE)
		printf("Error Occur : %d \n", conv_addr);
	else
		printf("Unsigned long addr(network ordered) : %x \n", conv_addr);
	
	conv_addr = inet_addr(addr2);
	if(conv_addr == INADDR_NONE)
		printf("Error Occured : %d \n\n", conv_addr);
	else
		printf("Unsigned long addr(network ordered) : %x \n\n", conv_addr);
	
	return 0;
}
