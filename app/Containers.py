from base.MenuBase import Menu, MenuItem


class Containers(Menu):

    def getHeader(self) -> str:
        return "Containers"

    def getItems(self) -> list[MenuItem]:
        return [
            MenuItem(
                "item-1",
                "Item 1",
                None),
        ]