from app.MainMenu import MainMenu


def main():
    
    mainMenu = MainMenu()
    while True:
        selection = mainMenu.display()

        if selection == "container":
            pass
        elif selection == "image":
            pass
        elif selection == "compose":
            pass
        elif selection == "exit":
            break
    

main()