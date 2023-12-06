from app.Containers import Containers
from base.MenuBase import Menu, MenuItem
from base.ShellUtils import ShellUtils


class MainMenu(Menu):

    def _container(self):
        Containers().display()

    def _exit(self):
        ShellUtils.clearScreen(True)

    def getHeader(self) -> str:
        return ""

    def getItems(self) -> list[MenuItem]:
        return [
            MenuItem(
                "container",
                "Check and manage containers",
                self._container),
            # MenuItem(
            #     "image",
            #     "Check and manage images",
            #     None),
            # MenuItem(
            #     "compose",
            #     "Up and down compositions",
            #     None),
            MenuItem(
                "exit",
                "Exit",
                self._exit),
        ]