from abc import ABC, abstractmethod
from sshkeyboard import listen_keyboard, stop_listening
from base.Colors import Colors
from base.ShellUtils import ShellUtils

class MenuItem:
    def __init__(self, key, displayValue):
        self.key: str = key
        self.displayValue: str = displayValue

class Menu(ABC):

    def __init__(self) -> None:
        super().__init__()
        self.selectedIndex: int = 0
        self.maxIndex: int = self.getItems().__len__() - 1

    @abstractmethod
    def getHeader(self) -> str:
        pass

    @abstractmethod
    def getItems(self) -> list[MenuItem]:
        pass

    def display(self) -> str:
        
        self._draw()
        listen_keyboard(
            on_press=self._press,
            delay_second_char = 0.05,
            until=None,
        )
        ShellUtils.clearScreen()
        
        return self.getItems()[self.selectedIndex].key

    def _press(self, key):
        if key == "up":
            self.selectedIndex = self.selectedIndex - 1 if self.selectedIndex > 0 else self.maxIndex
        elif key == "down":
            self.selectedIndex = self.selectedIndex + 1 if self.selectedIndex < self.maxIndex else 0
        elif key == "enter":
            stop_listening()

        self._draw()
    
    def _draw(self) -> None:
        ShellUtils.clearScreen()

        print(self.getHeader())

        for ix,item in enumerate(self.getItems()):
            print(
                (Colors.BLUE if ix == self.selectedIndex else "")
                + f"> {item.displayValue}"
                + Colors.END
            )
        print()