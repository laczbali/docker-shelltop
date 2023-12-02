from app.AppUtils import AppUtils
from base.MenuBase import Menu, MenuItem


class MainMenu(Menu):

    def getHeader(self) -> str:
        return AppUtils.generateHeader(None)

    def getItems(self) -> list[MenuItem]:
        return [
            MenuItem("container", "Check and manage containers"),
            MenuItem("image", "Check and manage images"),
            MenuItem("compose", "Up and down compositions"),
            MenuItem("exit", "Exit"),
        ]