#include <iostream>
#include <windows.h>

using namespace std;


int main() {
    SetConsoleOutputCP(CP_UTF8);

    const DWORD pid = GetCurrentProcessId();
    cout << "Идентификатор текщуего процесса: " << pid << endl;

    HANDLE pseudoHandle = GetCurrentProcess();
    cout << "Псевдодескриптор: " << pseudoHandle << endl;

    HANDLE duplicatedHandle = NULL;

    BOOL duplicated = DuplicateHandle(
        GetCurrentProcess(),
        pseudoHandle,
        GetCurrentProcess(),
        &duplicatedHandle,
        0,
        FALSE,
        DUPLICATE_SAME_ACCESS
    );

    cout << "Дескриптор, полученный функцией DuplicateHandle: " << duplicatedHandle << std::endl;

    HANDLE openedHandle = OpenProcess(
        PROCESS_ALL_ACCESS,
        FALSE,
        pid
    );

    cout << "Дескриптор, полученный функцией OpenProcess: " << openedHandle << std::endl;

    if (openedHandle) {
        CloseHandle(openedHandle);
        cout << "Закрываем дескриптор, полученный функцией OpenProcess: " << openedHandle << std::endl;
    }

    if (duplicatedHandle) {
        CloseHandle(duplicatedHandle);
        cout << "Закрываем дескриптор, полученный функцией DuplicateHandle: " << duplicatedHandle << std::endl;
    }

    return 0;
}
