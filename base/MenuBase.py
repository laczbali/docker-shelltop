from abc import ABC, abstractmethod
from sshkeyboard import listen_keyboard, stop_listening
from base.Colors import Colors
from base.ShellUtils import ShellUtils

class MenuItem:
    def __init__(self, key, displayValue, callback):
        self.key: str = key
        self.displayValue: str = displayValue
        self.callback = callback

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

    def display(self, exitItemKey: str = None) -> str:
        
        useLoop = False
        if exitItemKey != None:
            exitItemExists = next((item for item in self.getItems() if item.key == exitItemKey), None) != None
            if not exitItemExists:
                raise Exception(f"Item with key '{exitItemKey}' does not exists, cant use it as exit item")
            useLoop = True

        while True:
            self._draw()
            listen_keyboard(
                on_press=self._press,
                delay_second_char = 0.05,
                until="enter",
            )
            ShellUtils.clearScreen()
            
            selectedItem = self.getItems()[self.selectedIndex]
            if selectedItem.callback != None:
                selectedItem.callback()

            if not useLoop or (selectedItem.key == exitItemKey):
                break

        return self.getItems()[self.selectedIndex].key

    def _press(self, key):
        if key == "up":
            self.selectedIndex = self.selectedIndex - 1 if self.selectedIndex > 0 else self.maxIndex
        elif key == "down":
            self.selectedIndex = self.selectedIndex + 1 if self.selectedIndex < self.maxIndex else 0

        self._draw()
    
    def _draw(self) -> None:
        ShellUtils.clearScreen()

        header = self.getHeader()
        if header != None and header != "":
            ShellUtils.write(header)

        for ix,item in enumerate(self.getItems()):
            ShellUtils.write(
                (Colors.BLUE if ix == self.selectedIndex else "")
                + f"> {item.displayValue}"
                + Colors.END
            )
        ShellUtils.write()