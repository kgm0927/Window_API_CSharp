#include <winsock2.h>
#include <stdio.h>

int main(int argc, char* argv[])
{
    WSADATA wsa;
    if(WSAStartup(MAKEWORD(2, 2), &wsa) != 0)
        return -1;

    u_short x = 0x1234;
    u_long y = 0x12345678;

    u_short x2;
    u_long y2;

    // 호스트 바이트 -> 네트워크 바이트
    printf("호스트 바이트 -> 네트워크 바이트\n");
    printf("0x%x -> 0x%x\n", x, x2 = htons(x));
    printf("0x%x -> 0x%x\n", y, y2 = htonl(y));

    // 네트워크 바이트 -> 호스트 바이트
    printf("네트워크 바이트 -> 호스트 바이트\n");
    printf("0x%x -> 0x%x\n", x2, ntohs(x2));
    printf("0x%x -> 0x%x\n", y2, ntohl(y2));

    // 잘못된 사용 예
  printf("잘못된 사용 예\n");
    printf("0x%x -> 0x%x\n", x, htonl(x));

    WSACleanup();
    return 0;
}